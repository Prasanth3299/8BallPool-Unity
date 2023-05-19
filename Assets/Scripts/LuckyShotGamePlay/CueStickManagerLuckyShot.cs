using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RevolutionGames.Hud;
using RevolutionGames.Data;
using CueStick = RevolutionGames.APIData.CueStick;
using CueStickProperties = RevolutionGames.APIData.CueStickProperties;

namespace RevolutionGames.Game
{
    public class CueStickManagerLuckyShot : MonoBehaviour
    {
        public GameManagerLuckyShot gameManager;
        public Transform cueStickParent;
        public Transform cueStick;
        public GameObject cubeDummy;

        private BoardManagerLuckyShot boardManager;
        public CueStickDataManager cueStickDataManager;
        private CueBallManagerLuckyShot cueBallManager;
        private CueBallCollisionHandlerLuckyShot cueBallCollisionHandler;
        private ForceSliderLuckyShot forceUpdater;
        private GameObject guideLines;
        private LineRenderer guideLine, circleLine, ballReflectLine, cueBallReflectLine;
        private GameObject circleRestrictedImage, circleImage, lineImage, ballLineImage, cueBallLineImage;
        private Rigidbody cueBall;
        private float prevSliderValue = 0;
        private Vector3 startCueStickPosition;
        private bool isStrikeCompleted = false;
        private float forceFromCueStick = 9000; //4000 15000
        private float constantForceFromCueStick = 9000; //4000 15000
        private float initalForce = 9000;
        private float torqueXValue = 0;
        private float torqueZValue = 0;
        private Quaternion rotate90 = Quaternion.identity;
        private bool showGuidelineFlag = true;
        private float maxLength = 0;
        private float initialMaxLength = 1.1f;
        private Vector3 cueBallHitPosition = Vector3.zero, cueBallFinalPos = Vector3.zero;
        private Vector3 ballCenter = Vector3.zero, ballFinalPos = Vector3.zero;
        public RaycastHit hit, leftHit, rightHit, centerHit;
        private float ballAngle = 0, sign = 0, ballRadius = 0;
        private float ballReflectLength = 0, cueBallReflectLength = 0;
        private Vector3 direction = Vector3.zero;
        private bool firstHitDoneBall = true;
        private bool firstHitDoneCueBall = true;
        private GameObject[] balls;
        private float toleranceValue;
        private float speed;
        private float cueSpinAngle = 0;
        private bool disableGuidelineInLocalFlag = false;
        private Quaternion currentRot = Quaternion.identity;
        private Quaternion targetRot = Quaternion.identity;

        public bool ShowGuidelineFlag { get => showGuidelineFlag; set => showGuidelineFlag = value; }
        public float TorqueXValue { get => torqueXValue; set => torqueXValue = value; }
        public float TorqueZValue { get => torqueZValue; set => torqueZValue = value; }
        public float ForceFromCueStick { get => forceFromCueStick; set => forceFromCueStick = value; }
        public Vector3 CueBallFinalPos { get => cueBallFinalPos; set => cueBallFinalPos = value; }
        public Vector3 BallFinalPos { get => ballFinalPos; set => ballFinalPos = value; }
        public Vector3 CueBallHitPosition { get => cueBallHitPosition; set => cueBallHitPosition = value; }
        public bool FirstHitDoneBall { get => firstHitDoneBall; set => firstHitDoneBall = value; }
        public bool FirstHitDoneCueBall { get => firstHitDoneCueBall; set => firstHitDoneCueBall = value; }
        public float CueSpinAngle { get => cueSpinAngle; set => cueSpinAngle = value; }


        private void Awake()
        {
            cueStickDataManager = DataManager.Instance().GetComponent<CueStickDataManager>();

        }
        // Start is called before the first frame update
        void Start()
        {
            cueBall = gameManager.cueBall.GetComponent<Rigidbody>();
            guideLines = gameManager.guideLines;
            guideLine = gameManager.guideLine.GetComponent<LineRenderer>();
            circleLine = gameManager.circleLine.GetComponent<LineRenderer>();
            ballReflectLine = gameManager.ballReflectLine.GetComponent<LineRenderer>();
            cueBallReflectLine = gameManager.cueBallReflectLine.GetComponent<LineRenderer>();
            forceUpdater = gameManager.hudManager.forceUpdater.GetComponent<ForceSliderLuckyShot>();
            boardManager = gameManager.boardManager;
            cueBallManager = gameManager.cueBallManager;
            cueBallCollisionHandler = gameManager.cueBallCollisionHandler;
            balls = gameManager.balls;
            circleRestrictedImage = gameManager.circleRestrictedImage;
            circleImage = gameManager.circleImage;
            lineImage = gameManager.lineImage;
            ballLineImage = gameManager.ballLineImage;
            cueBallLineImage = gameManager.cueBallLineImage;

            prevSliderValue = 0;
            startCueStickPosition = cueStick.localPosition;
            ShowGuidelineFlag = true; //flag to show/hide guideline
            rotate90 = Quaternion.AngleAxis(-90, Vector3.up); //for rotation wrt z axis
            maxLength = 1.1f; // maximum length of the guidelines after collision
            ballRadius = cueBall.GetComponent<SphereCollider>().bounds.extents.x;
            FirstHitDoneBall = true;
            FirstHitDoneCueBall = true;
            toleranceValue = 0.0535f;
            speed = 4f; //cue stick rotation speed

            //GetAPIData
            GetCueStickDataAPICallBack();
        }

        // Update is called once per frame
        void Update()
        {
            //if (isStrikeCompleted)
            //{
            //    ResetCueStick();
            //}

            if (ShowGuidelineFlag)
            {

                hit = GetHitPoint();

                if (hit.collider != null)
                {
                    DisplayGuidelines(hit, cueBallHitPosition);
                }
            }
        }

        //Set cue sticks from API Data
        public void GetCueStickDataAPICallBack()
        {
            /*List<CueStick> cueStickList = new List<CueStick>();
            CueStickProperties cueStickProperties = new CueStickProperties(4, 4, 4, 4, 50);
            CueStick cueStick = new CueStick(cueSprites[0], "Standard Cue", "Standard", "2k", 2000, 10, 4, 4, 1, 1, "Basic", cueStickProperties);*/

            //UpdateCueStickSprite(cueStickDataManager.GetImage());
            UpdateForceCueStickProperty(cueStickDataManager.GetForce());
            UpdateSpinCueStickProperty(cueStickDataManager.GetSpin());
            UpdateAimCueStickProperty(cueStickDataManager.GetAim());
            UpdateTimeCueStickProperty(cueStickDataManager.GetTime());
            CheckCharge();
        }

        public void UpdateForceValue(float forceValue)
        {
            forceFromCueStick = constantForceFromCueStick * forceValue;

            if (prevSliderValue <= forceValue)
            {
                cueStick.localPosition += Vector3.right * -(forceValue - prevSliderValue);
                prevSliderValue = forceValue;
            }
            else
            {
                cueStick.localPosition += Vector3.right * (prevSliderValue - forceValue);
                prevSliderValue = forceValue;
            }
        }

        public void StrikeTheBall()
        {
            gameManager.hudManager.StopTimer = true;
            ShowGuidelineFlag = false;

            //Disable aiming wheel
            gameManager.hudManager.DisableAimingWheel();
            //Remove guidelines
            guideLines.SetActive(false);

            Vector3 newDirection = cueStick.right;
            Vector3 newPosition = cueBall.GetComponent<Transform>().position;
            newPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z + 0.5f);
            //newDirection = new Vector3(newDirection.x+0.5f, newDirection.y, newDirection.z);

            //print(ForceFromCueStick);
            /*if(torqueZValue == 0 && torqueXValue == 1)
            {
                print("Top");
            }
            else if (torqueZValue == 0 && torqueXValue == -1)
            {
                print("Back");
            }
            else if (torqueZValue == 1 && torqueXValue == 0)
            {
                print("right");
            }
            else if (torqueZValue == -1 && torqueXValue == 0)
            {
                print("left");
            }
            else if (torqueZValue == -1 && torqueXValue == -1)
            {
                print("Backleft");
            }
            else if (torqueZValue == 1 && torqueXValue == -1)
            {
                print("Backright");
            }
            else if (torqueZValue == -1 && torqueXValue == 1)
            {
                print("topleft");
            }
            else if (torqueZValue == 1 && torqueXValue == 1)
            {
                print("topright");
            }*/
            //torqueXValue = torqueXValue * forceFromCueStick;
            //torqueZValue = torqueZValue * forceFromCueStick;

            cueBall.AddForce(newDirection * forceFromCueStick, ForceMode.Force);
            //cueBall.velocity = newDirection * 10;
            //print("torque" + torqueXValue + ":" + torqueZValue);
            //cueBall.AddTorque(new Vector3(torqueXValue, 0, torqueZValue), ForceMode.Force);
            //cueBall.AddTorque(new Vector3(0, 0, -2000), ForceMode.Force);

            //cueBall.AddForceAtPosition(newDirection * forceFromCueStick, newPosition);
            cueStick.localPosition = startCueStickPosition;
            StartCoroutine(ResetGameFlag());

        }

        public RaycastHit GetHitPoint()
        {
            //Raycast left
            Physics.Raycast(cueStickParent.position + (cueStickParent.forward * (ballRadius + toleranceValue)), (cubeDummy.transform.position + (cubeDummy.transform.forward * (ballRadius + toleranceValue))) - (cueStickParent.position + (cueStickParent.forward * (ballRadius + toleranceValue))), out leftHit, Screen.width);
            Debug.DrawRay(cueStickParent.position + (cueStickParent.forward * (ballRadius + toleranceValue)), (cubeDummy.transform.position + (cubeDummy.transform.forward * (ballRadius + toleranceValue)) + (cubeDummy.transform.right * 15f)) - (cueStickParent.position + (cueStickParent.forward * (ballRadius + toleranceValue))), Color.green);
            //Raycast right
            Physics.Raycast(cueStickParent.position + (-cueStickParent.forward * (ballRadius + toleranceValue)), (cubeDummy.transform.position + (-cubeDummy.transform.forward * (ballRadius + toleranceValue))) - (cueStickParent.position + (-cueStickParent.forward * (ballRadius + toleranceValue))), out rightHit, Screen.width);
            Debug.DrawRay(cueStickParent.position + (-cueStickParent.forward * (ballRadius + toleranceValue)), (cubeDummy.transform.position + (-cubeDummy.transform.forward * (ballRadius + toleranceValue)) + (cubeDummy.transform.right * 15f)) - (cueStickParent.position + (-cueStickParent.forward * (ballRadius + toleranceValue))), Color.black);
            //Raycast center
            Physics.Raycast(cueStickParent.position, cubeDummy.transform.position - cueStickParent.position, out centerHit, Screen.width);
            Debug.DrawRay(cueStickParent.position, cubeDummy.transform.position + (cubeDummy.transform.right * 15f) - cueStickParent.position);
            hit = centerHit;
            //print(leftHit.collider.name + centerHit.collider.name + rightHit.collider.name);
            cueBallHitPosition = hit.point - ((cubeDummy.transform.position - cueStickParent.position).normalized * ballRadius);

            if (leftHit.collider != null && rightHit.collider != null && leftHit.collider.tag == "Ball" && leftHit.collider.name == rightHit.collider.name)
            {
                hit = centerHit;
                //print("center");
                cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * ballRadius);
                //cueBallHitPosition = hit.point + ((hit.point - hit.collider.GetComponent<Renderer>().bounds.center).normalized * hit.collider.GetComponent<SphereCollider>().bounds.extents.x);
                //cueBallHitPosition = cueBall.GetComponent<Renderer>().bounds.center + ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
            }
            else
            {
                if (leftHit.collider != null && rightHit.collider != null && Math.Round((double)leftHit.distance, 5) <= Math.Round((double)rightHit.distance, 5))
                {
                    hit = leftHit;
                    //print("left");

                    cueBallHitPosition = hit.point + ((hit.point - hit.collider.GetComponent<Transform>().position).normalized * ballRadius);
                    cueBallHitPosition = cueBall.GetComponent<Transform>().position +
                        ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
                    float diff = (ballRadius * 2) - Vector3.Distance(hit.collider.transform.position, cueBallHitPosition);
                    //print("leftdiff " + hit.distance);
                    float mul = 0;
                    if (hit.distance < 0.46f)
                    {
                        mul = 2.5f;
                    }
                    else
                    {
                        mul = 0;
                    }
                    cueBallHitPosition = cueBallHitPosition - ((cueBallHitPosition - cueBall.GetComponent<Transform>().position).normalized * ((diff + (0.03f * mul))));
                    //cueBallHitPosition = cueBall.GetComponent<Transform>().position + ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
                }
                else if (leftHit.collider != null && rightHit.collider != null && Math.Round((double)leftHit.distance, 5) > Math.Round((double)rightHit.distance, 5))
                {
                    hit = rightHit;
                    //print("R");

                    cueBallHitPosition = hit.point + ((hit.point - hit.collider.GetComponent<Transform>().position).normalized * ballRadius);
                    cueBallHitPosition = cueBall.GetComponent<Transform>().position + ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
                    float diff = (ballRadius * 2) - Vector3.Distance(hit.collider.transform.position, cueBallHitPosition);
                    //print("rightdiff " + diff);
                    float mul = 0;
                    if (hit.distance < 0.46f)
                    {
                        mul = 2.5f;
                    }
                    else
                    {
                        mul = 0;
                    }
                    cueBallHitPosition = cueBallHitPosition - ((cueBallHitPosition - cueBall.GetComponent<Transform>().position).normalized * ((diff + (0.03f * mul))));
                    //cueBallHitPosition = cueBall.GetComponent<Transform>().position + ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
                }

                if (centerHit.collider != null && Math.Round((double)centerHit.distance, 5) <= Math.Round((double)hit.distance, 5))
                {
                    hit = centerHit;
                    //print("Cside");
                    cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * ballRadius);
                    float diff = (ballRadius * 2) - Vector3.Distance(hit.collider.transform.position, cueBallHitPosition);
                    cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * (ballRadius + diff));

                    //cueBallHitPosition = cueBall.GetComponent<Renderer>().bounds.center + ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
                    //cueBallHitPosition = hit.point + ((hit.point - hit.collider.GetComponent<Renderer>().bounds.center).normalized * hit.collider.GetComponent<SphereCollider>().bounds.extents.x);
                    //cueBallHitPosition = cueBall.GetComponent<Renderer>().bounds.center + ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
                }
                if (centerHit.collider != null && centerHit.collider.tag == "Ball" && centerHit.collider.name == hit.collider.name)
                {
                    hit = centerHit;
                    //print("centerS");
                    cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * ballRadius); // hit.collider.GetComponent<SphereCollider>().bounds.extents.x
                    //cueBallHitPosition = cueBall.GetComponent<Transform>().position + ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
                    float diff = (ballRadius * 2) - Vector3.Distance(hit.collider.transform.position, cueBallHitPosition);
                    cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * (ballRadius + diff));
                    //cueBallHitPosition = cueBall.GetComponent<Transform>().position + ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);

                }
                if (hit.collider != null && hit.collider.tag != "Ball")
                {
                    //print("Other");
                    cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * ballRadius);
                    cueBallHitPosition = cueBall.GetComponent<Transform>().position + ((cubeDummy.transform.position - cueStickParent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
                }

            }
            return hit;
        }

        public void DisplayGuidelines(RaycastHit hit, Vector3 cueBallHitPosition)
        {


            if (hit.collider.tag == "Ball") // only show reflect lines if it is an object ball
            {

                //cueBall.GetComponent<SphereCollider>().isTrigger = true;
                bool isPlayerGroup = gameManager.CheckGroupType(hit.collider.name);
                ballCenter = hit.collider.GetComponent<Transform>().position;
                cueBallHitPosition = new Vector3(cueBallHitPosition.x, ballCenter.y, cueBallHitPosition.z);

                ballFinalPos = cueBallHitPosition + ((ballCenter - cueBallHitPosition).normalized * 15f);


                //To make the y same as that of the point 1 of line
                ballFinalPos = new Vector3(ballFinalPos.x, ballCenter.y, ballFinalPos.z);
                //Determine the angle to find length and direction of the reflect lines
                ballAngle = Vector3.Angle((cueStickParent.position - cubeDummy.transform.position).normalized, (ballFinalPos - cubeDummy.transform.position).normalized);
                sign = Mathf.Sign(Vector3.Dot((cubeDummy.transform.position - cueStickParent.position), Vector3.Cross(Vector3.up, ballFinalPos)));
                ballAngle = ballAngle * sign;
                //print(ballAngle);
                if (ballAngle >= 0)
                {
                    cueBallReflectLength = ((180 - ballAngle) / 90) * maxLength;
                    if (cueBallReflectLength <= 0.06f)
                        cueBallReflectLength = 0.05f;
                    if (cueBallReflectLength > maxLength)
                        cueBallReflectLength = maxLength - 0.035f;
                    ballReflectLength = maxLength - cueBallReflectLength;

                    cueBallFinalPos = cueBallHitPosition + (rotate90 * (-1 * ballFinalPos) * ballRadius);

                    //Ballreflect line length capping
                    direction = ballFinalPos - ballCenter;
                    direction = direction.normalized;
                    ballFinalPos = ballCenter + (direction * ballReflectLength);

                    //CueBallreflect line length capping
                    direction = cueBallFinalPos - cueBallHitPosition;
                    direction = direction.normalized;
                    cueBallFinalPos = cueBallHitPosition + (direction * cueBallReflectLength);

                }
                else
                {
                    cueBallReflectLength = ((180 + ballAngle) / 90) * maxLength;
                    if (cueBallReflectLength <= 0.06f)
                        cueBallReflectLength = 0.05f;
                    if (cueBallReflectLength > maxLength)
                        cueBallReflectLength = maxLength - 0.035f;
                    ballReflectLength = maxLength - cueBallReflectLength;

                    cueBallFinalPos = cueBallHitPosition + (rotate90 * (1 * ballFinalPos) * ballRadius);

                    //Ballreflect line length capping
                    Vector3 direction = ballFinalPos - ballCenter;
                    direction = direction.normalized;
                    ballFinalPos = ballCenter + (direction * ballReflectLength);


                    //CueBallreflect line length capping
                    direction = cueBallFinalPos - cueBallHitPosition;
                    direction = direction.normalized;
                    cueBallFinalPos = cueBallHitPosition + (direction * cueBallReflectLength);
                }
                //Before drawing guidelines check if distance between the cue ball anf target ball is very less, 
                //if then circle should completely overlap cue ball
                if (Vector3.Distance(hit.collider.transform.position, cueBall.transform.position) <= ((ballRadius * 2) + toleranceValue))
                {
                    cueBallHitPosition = cueBall.transform.position;
                }
                cueBallFinalPos = new Vector3(cueBallFinalPos.x, ballCenter.y, cueBallFinalPos.z);

                //Cue Stick Guideline
                guideLine.SetPosition(0, cueBall.GetComponent<Transform>().position);
                guideLine.SetPosition(1, cueBallHitPosition);
                lineImage.SetActive(true);
                lineImage.transform.position = new Vector3(cueBall.GetComponent<Transform>().position.x, 6.95f, cueBall.GetComponent<Transform>().position.z);
                lineImage.transform.eulerAngles = new Vector3(90, (cueStickParent.eulerAngles.y), 0);
                lineImage.transform.localScale = new Vector3(Vector3.Distance(cueBallHitPosition, cueBall.GetComponent<Transform>().position) * 2,
                    lineImage.transform.lossyScale.y, lineImage.transform.lossyScale.z);
                //if (isPlayerGroup)
                ////{
                    
                    DrawCircle(ballRadius, cueBallHitPosition, 0.035f, 0.035f, Color.white);
                    circleImage.transform.position = new Vector3(cueBallHitPosition.x, 6.95f, cueBallHitPosition.z);
                    circleRestrictedImage.SetActive(false);
                    circleImage.SetActive(true);
                    if (!disableGuidelineInLocalFlag) //put condition to check if this is local mode
                    {
                        // Ball direction
                        ballReflectLine.SetPosition(0, ballCenter);
                        ballReflectLine.SetPosition(1, ballFinalPos);
                        
                        ballLineImage.SetActive(true);
                        ballLineImage.transform.position = new Vector3(ballCenter.x, 6.95f, ballCenter.z);
                        ballLineImage.transform.rotation = Quaternion.Euler(90, 0, Vector3.SignedAngle((ballFinalPos - ballCenter).normalized, Vector3.right, Vector3.up));
                        ballLineImage.transform.localScale = new Vector3(Vector3.Distance(ballCenter, ballFinalPos) * 2,
                            ballLineImage.transform.lossyScale.y, ballLineImage.transform.lossyScale.z);

                        // Cue Ball Direction
                        cueBallReflectLine.SetPosition(0, cueBallHitPosition);
                        cueBallReflectLine.SetPosition(1, cueBallFinalPos);

                        cueBallLineImage.SetActive(true);
                        cueBallLineImage.transform.position = new Vector3(cueBallHitPosition.x, 6.95f, cueBallHitPosition.z);
                        cueBallLineImage.transform.rotation = Quaternion.Euler(90, 0, Vector3.SignedAngle((cueBallFinalPos - cueBallHitPosition).normalized, Vector3.right, Vector3.up));
                        cueBallLineImage.transform.localScale = new Vector3(Vector3.Distance(cueBallHitPosition, cueBallFinalPos) * 2,
                            cueBallLineImage.transform.lossyScale.y, cueBallLineImage.transform.lossyScale.z);
                    }
                    else
                    {
                        // Ball direction
                        ballReflectLine.SetPosition(0, Vector3.zero);
                        ballReflectLine.SetPosition(1, Vector3.zero);
                        ballLineImage.SetActive(false);

                        // Cue Ball Direction
                        cueBallReflectLine.SetPosition(0, Vector3.zero);
                        cueBallReflectLine.SetPosition(1, Vector3.zero);
                        cueBallLineImage.SetActive(false);

                    }
                //}
               /* else
                {
                    print("Ball 2 hit position");
                    DrawCircle(ballRadius, cueBallHitPosition, 0.035f, 0.035f, Color.red);
                    circleRestrictedImage.transform.position = new Vector3(cueBallHitPosition.x, 6.95f, cueBallHitPosition.z);
                    circleRestrictedImage.SetActive(true);
                    circleImage.SetActive(false);
                    //Draw line too
                    // Ball direction
                    ballReflectLine.SetPosition(0, Vector3.zero);
                    ballReflectLine.SetPosition(1, Vector3.zero);
                    ballLineImage.SetActive(false);

                    // Cue Ball Direction
                    cueBallReflectLine.SetPosition(0, Vector3.zero);
                    cueBallReflectLine.SetPosition(1, Vector3.zero);
                    cueBallLineImage.SetActive(false);

                }*/


            }
            else // if not object ball
            {
               
                cueBallHitPosition = new Vector3(cueBallHitPosition.x, 5.95f, cueBallHitPosition.z);

                //print(hit.distance);
                //cueBall.GetComponent<SphereCollider>().isTrigger = false;
                // Cue Stick Guideline
                guideLine.SetPosition(0, cueBall.GetComponent<Transform>().position);
                guideLine.SetPosition(1, cueBallHitPosition);
                DrawCircle(ballRadius, cueBallHitPosition, 0.035f, 0.035f, Color.white);
                circleImage.transform.position = new Vector3(cueBallHitPosition.x, 5.95f, cueBallHitPosition.z);
                circleRestrictedImage.SetActive(false);
                circleImage.SetActive(true);

                lineImage.SetActive(true);
                lineImage.transform.position = new Vector3(cueBall.GetComponent<Transform>().position.x, 6.95f, cueBall.GetComponent<Transform>().position.z);
                lineImage.transform.eulerAngles = new Vector3(90, (cueStickParent.eulerAngles.y), 0);
                lineImage.transform.localScale = new Vector3(Vector3.Distance(cueBallHitPosition, cueBall.GetComponent<Transform>().position) * 2,
                    lineImage.transform.lossyScale.y, lineImage.transform.lossyScale.z);

                //Resetting guidelines
                ballReflectLine.SetPosition(0, Vector3.zero);
                ballReflectLine.SetPosition(1, Vector3.zero);
                ballLineImage.SetActive(false);

                cueBallReflectLine.SetPosition(0, Vector3.zero);
                cueBallReflectLine.SetPosition(1, Vector3.zero);
                cueBallLineImage.SetActive(false);
            }
        }

        public void DrawCircle(float radius, Vector3 centerPos, float startWidth, float endWidth, Color lineColor) //To draw circle related to guideline
        {
            radius = radius - startWidth;
            var segments = 360;
            circleLine.startWidth = startWidth;
            circleLine.endWidth = endWidth;
            circleLine.positionCount = segments + 1;

            var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
            var points = new Vector3[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 360f / segments);
                points[i] = new Vector3(Mathf.Sin(rad) * radius, ballRadius - startWidth, Mathf.Cos(rad) * radius) + centerPos;
            }

            circleLine.startColor = lineColor;
            circleLine.endColor = lineColor;
            circleLine.SetPositions(points);
        }

        public void TapAndRotateCueStick(float angle)
        {

           // print("Start move tao and rotate");
            StopAllCoroutines();

            //cueStickParent.transform.rotation = Quaternion.AngleAxis(angle, cueStickParent.up);
            currentRot = cueStickParent.rotation;
            targetRot = Quaternion.AngleAxis(-angle, Vector3.up);
            float angle1 = Quaternion.Angle(currentRot, targetRot);
            StartCoroutine(Rotate(targetRot, angle1));
            //cueStickParent.eulerAngles = new Vector3(0, -angle, 0);
        }

        public void RotateCueStick(Vector2 dragVector, float dragDistance, Vector3 currentPos)
        {
           // print("Start move");
            float positiveX = Mathf.Abs(dragVector.x);
            float positiveY = Mathf.Abs(dragVector.y);
            dragDistance = dragDistance / 5f;
            //print(dragDistance);
            if (positiveX > positiveY)
            {
                if (dragVector.x > 0)
                {
                    //print("right" + currentPos.z + " : " + cueBall.position.z);
                    if (currentPos.z > cueBall.position.z)
                    {
                        StopAllCoroutines();

                        //Vector3 angle = new Vector3(0, (cueStickParent.eulerAngles.y + (dragDistance * 25) * Time.deltaTime), 0); //right
                        currentRot = cueStickParent.rotation;
                        targetRot = Quaternion.AngleAxis(cueStickParent.eulerAngles.y + (dragDistance), Vector3.up);
                        StartCoroutine(Rotate(targetRot, dragDistance));

                        //cueStickParent.rotation = Quaternion.Slerp(currentRot, targetRot, speed * Time.deltaTime); //10f * (1f - Mathf.Exp(-Time.deltaTime)
                    }
                    else
                    {
                        StopAllCoroutines();

                        //Vector3 angle = new Vector3(0, (cueStickParent.eulerAngles.y - (dragDistance * 25) * Time.deltaTime), 0); //right
                        currentRot = cueStickParent.rotation;
                        targetRot = Quaternion.AngleAxis(cueStickParent.eulerAngles.y - (dragDistance), Vector3.up);
                        StartCoroutine(Rotate(targetRot, dragDistance));

                        //cueStickParent.rotation = Quaternion.Slerp(currentRot, targetRot, speed * Time.deltaTime);
                    }
                }
                else
                {
                    //print("left" + currentPos.z + " : " + cueBall.position.z);
                    if (currentPos.z > cueBall.position.z)
                    {
                        StopAllCoroutines();

                        //cueStickParent.eulerAngles = new Vector3(0, (cueStickParent.eulerAngles.y - (dragDistance)), 0); //left
                        currentRot = cueStickParent.rotation;
                        targetRot = Quaternion.AngleAxis(cueStickParent.eulerAngles.y - (dragDistance), Vector3.up);
                        StartCoroutine(Rotate(targetRot, dragDistance));

                        //cueStickParent.rotation = Quaternion.Slerp(currentRot, targetRot, speed * Time.deltaTime);
                    }
                    else
                    {
                        StopAllCoroutines();

                        //cueStickParent.eulerAngles = new Vector3(0, (cueStickParent.eulerAngles.y + (dragDistance)), 0); //left
                        currentRot = cueStickParent.rotation;
                        targetRot = Quaternion.AngleAxis(cueStickParent.eulerAngles.y + (dragDistance), Vector3.up);
                        StartCoroutine(Rotate(targetRot, dragDistance));

                        //cueStickParent.rotation = Quaternion.Slerp(currentRot, targetRot, speed * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (dragVector.y > 0)
                {
                    //print("up");
                    if (currentPos.x > cueBall.position.x)
                    {
                        StopAllCoroutines();

                        //cueStickParent.eulerAngles = new Vector3(0, cueStickParent.eulerAngles.y - (dragDistance), 0); //up
                        currentRot = cueStickParent.rotation;
                        targetRot = Quaternion.AngleAxis(cueStickParent.eulerAngles.y - (dragDistance), Vector3.up);
                        StartCoroutine(Rotate(targetRot, dragDistance));

                        //cueStickParent.rotation = Quaternion.Slerp(currentRot, targetRot, speed * Time.deltaTime);
                    }
                    else
                    {
                        StopAllCoroutines();

                        //cueStickParent.eulerAngles = new Vector3(0, cueStickParent.eulerAngles.y + (dragDistance), 0); //up
                        currentRot = cueStickParent.rotation;
                        targetRot = Quaternion.AngleAxis(cueStickParent.eulerAngles.y + (dragDistance), Vector3.up);
                        StartCoroutine(Rotate(targetRot, dragDistance));

                        //cueStickParent.rotation = Quaternion.Slerp(currentRot, targetRot, speed * Time.deltaTime);
                    }
                }
                else
                {
                    //print("down" + currentPos.x + " : " + cueBall.position.x);

                    if (currentPos.x > cueBall.position.x)
                    {
                        StopAllCoroutines();
                        //cueStickParent.eulerAngles = new Vector3(0, cueStickParent.eulerAngles.y + (dragDistance), 0); //downright
                        currentRot = cueStickParent.rotation;
                        targetRot = Quaternion.AngleAxis(cueStickParent.eulerAngles.y + (dragDistance), Vector3.up);
                        StartCoroutine(Rotate(targetRot, dragDistance));
                        //cueStickParent.rotation = Quaternion.Slerp(currentRot, targetRot, speed * Time.deltaTime);
                    }
                    else
                    {
                        StopAllCoroutines();

                        //cueStickParent.eulerAngles = new Vector3(0, cueStickParent.eulerAngles.y - (dragDistance), 0); //downleft
                        currentRot = cueStickParent.rotation;
                        targetRot = Quaternion.AngleAxis(cueStickParent.eulerAngles.y - (dragDistance), Vector3.up);
                        StartCoroutine(Rotate(targetRot, dragDistance));
                        //cueStickParent.rotation = Quaternion.Slerp(currentRot, targetRot, speed * Time.deltaTime);

                    }
                }
            }

        }

        IEnumerator Rotate(Quaternion targetRot, float dragDistance)
        {
            dragDistance = Math.Min(dragDistance, 10f);
            while (cueStickParent.rotation.y != targetRot.y)
            {
                cueStickParent.rotation = Quaternion.Slerp(cueStickParent.rotation, targetRot, speed * dragDistance * Time.deltaTime);
                yield return null;
            }
            cueStickParent.rotation = targetRot;
            yield return null;
        }


        IEnumerator ResetGameFlag()
        {
            yield return new WaitForSeconds(0.4f);
            isStrikeCompleted = true;
            cueStick.gameObject.SetActive(false);
            if (gameManager.CurrentGameState != GameManagerLuckyShot.GameState.isItCueballBreak)
            {
                gameManager.CurrentGameState = GameManagerLuckyShot.GameState.isStrikeDone;
            }
            else
            {
                gameManager.CurrentGameState = GameManagerLuckyShot.GameState.isItCueballBreakDone;
            }
        }

        public void UpdateCueStickProperties()
        {
            cueStickDataManager.ReducePlayerCueStickCharge();
            CheckCharge();
        }

        public void CheckCharge()
        {
            if (cueStickDataManager.GetCharge() <= 0)
            {
                //print(cueStickDataManager.GetCharge());
                if (cueStickDataManager.GetAutoRecharge() == 1)
                {
                    if (GameData.Instance().PlayerBalance >= cueStickDataManager.GetRechargePriceInNumbers())
                    {
                        cueStickDataManager.ResetPlayerCueStickCharge();
                    }
                }
                else
                {
                    UpdateForceCueStickProperty(0);
                    UpdateSpinCueStickProperty(0);
                    UpdateAimCueStickProperty(0);
                    UpdateTimeCueStickProperty(0);
                }
            }
        }

        public void ResetCueStick()
        {
            //if (cueBall.velocity.magnitude <= 0.01f)
            {
                //boardManager.CheckHitGameRules();

                //If breakshot cue stick rotation should also be reset
                if (gameManager.CurrentGameState == GameManagerLuckyShot.GameState.isItCueballBreak)
                {
                    cueStickParent.eulerAngles = Vector3.zero;
                }
                cueBallManager.ResetCueBallSpawnPosition();

                cueStickParent.position = new Vector3(cueBall.GetComponent<Transform>().position.x, cueStickParent.position.y,
                        cueBall.GetComponent<Transform>().position.z);
                isStrikeCompleted = false;

                forceUpdater.ResetTheForceSliderValues();
                forceFromCueStick = constantForceFromCueStick;
                cueStick.localPosition = startCueStickPosition;

                //Reset Cue spin position  and aiming wheel
                gameManager.hudManager.ResetCueSpinPosition();
                gameManager.hudManager.ResetAimingWheelPosition();

                //Reset Guidelines
                guideLines.SetActive(false);
                ballReflectLine.SetPosition(0, Vector3.zero);
                ballReflectLine.SetPosition(1, Vector3.zero);

                cueBallReflectLine.SetPosition(0, Vector3.zero);
                cueBallReflectLine.SetPosition(1, Vector3.zero);

                guideLine.SetPosition(0, Vector3.zero);
                guideLine.SetPosition(1, Vector3.zero);

                ShowGuidelineFlag = true;
                /*cueBall.GetComponent<Rigidbody>().isKinematic = false;
                for (int i = 0; i < balls.Length; i++)
                {
                    balls[i].GetComponent<Rigidbody>().isKinematic = true;
                }*/
                FirstHitDoneBall = true;
                FirstHitDoneCueBall = true;
                guideLines.SetActive(true);
                cueStick.gameObject.SetActive(true);
                gameManager.hudManager.StopTimer = false;
            }
        }

        public int GetTapToAimFlag()
        {
            //0 - disabled property
            //1- enabled
            return cueStickDataManager.TapToAimFlag;
        }

        public void SetTapToAimFlag(int val)
        {
            cueStickDataManager.TapToAimFlag = val;
        }

        public int GetDisableGuidelineInLocalFlag()
        {
            //0 - disabled property
            //1- enabled
            return cueStickDataManager.DisableGuidelineInLocalFlag;
        }

        public void SetDisableGuidelineInLocalFlag(int val)
        {
            cueStickDataManager.DisableGuidelineInLocalFlag = val;
            if (val == 0)
            {
                disableGuidelineInLocalFlag = false;
            }
            else
            {
                disableGuidelineInLocalFlag = true;
            }
        }

        public int GetAimingWheelFlag()
        {
            //0 - disabled property
            //1- enabled
            return cueStickDataManager.AimingWheelFlag;
        }

        public void SetAimingWheelFlag(int val)
        {
            cueStickDataManager.AimingWheelFlag = val;
        }

        public int GetCueSensitivity()
        {
            //0 - slow
            //1- normal
            //2 - fast
            return cueStickDataManager.CueSensitivity;
        }

        public void SetCueSensitivity(int val)
        {
            cueStickDataManager.CueSensitivity = val;
            if (val == 0) //slow
            {
                speed = 2f;
            }
            else if (val == 2) //fast
            {
                speed = 2f;
            }
            else //normal
            {
                speed = 2f;
            }
        }

        public int GetPowerBarLocationFlag()
        {
            //0 - left
            //1- right
            return cueStickDataManager.PowerBarLocationFlag;
        }

        public void SetPowerBarLocationFlag(int val)
        {
            cueStickDataManager.PowerBarLocationFlag = val;
        }

        public int GetPowerBarOrientationFlag()
        {
            //0 - vertical
            //1- horizontal
            return cueStickDataManager.PowerBarOrientationFlag;
        }

        public void SetPowerBarOrientationFlag(int val)
        {
            cueStickDataManager.PowerBarOrientationFlag = val;
        }

        public void UpdateForceCueStickProperty(int force)
        {
            forceFromCueStick = initalForce + (70 * force);
            constantForceFromCueStick = initalForce + (70 * force);
        }

        public void UpdateSpinCueStickProperty(int spin)
        {
            cueBallCollisionHandler.UpdateSpinValue(spin);
        }

        public void UpdateAimCueStickProperty(int aim)
        {
            maxLength = initialMaxLength + (0.05f * aim);
        }

        public void UpdateTimeCueStickProperty(int time)
        {
            gameManager.UpdateMaxTimePerShot(time);
        }

    }
}

