using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RevolutionGames.UI
{
    public class CueStick : MonoBehaviour
    {
        public LineRenderer GuideLine, CircleLine, BallReflectLine, CueBallReflectLine;
        public Rigidbody cueBall;
        public GameObject CubeDummy;
        public Slider forceSlider;
        private float forceFromCueStick = 700f;
        private float constantForceFromCueStick = 700f;
        Ray ray;
        RaycastHit hit, leftHit, rightHit, centerHit;
        bool isMouseDown = false, showGuidelineFlag = true;
        Vector2 mouseStartPosition = Vector2.zero;
        float tempY = 0;
        Transform colliderTransform = null;
        bool isStrikeCompleted = true;
        float prevSliderValue = 0;
        Vector3 startCueStickPosition;
        Quaternion rotate90;
        float spherecastRadius = 0.6f;
        Vector3 cueBallHitPosition = Vector3.zero, cueBallFinalPos = Vector3.zero;
        Vector3 ballCenter = Vector3.zero, ballFinalPos = Vector3.zero;

        int dir = 0;
        float maxLength = 0, cueBallReflectLength = 0, ballReflectLength = 0, ballAngle = 0, sign = 0;

        private void OnCollisionEnter(Collision collision)
        {
            //print("Collision object : " + collision.collider.name);
            if (collision.transform.tag == "CueBall")
            {
                Vector3 direction = collision.contacts[0].point - transform.position;
                direction.Normalize();
                collision.rigidbody.AddForce(direction * forceFromCueStick);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            //print("Trigger object : " + other.name);
            if (other.tag == "CueBall")
            {
                other.transform.GetComponent<Rigidbody>().AddRelativeForce(-transform.up * forceFromCueStick);
            }
        }

        private void Start()
        {
            showGuidelineFlag = true; //flag to show/hide guideline
            rotate90 = Quaternion.AngleAxis(-90, Vector3.up); //for rotation wrt z axis
            prevSliderValue = forceSlider.value;
            startCueStickPosition = transform.localPosition;
            //line.SetPosition(0, cueBall.GetComponent<Transform>().localPosition);
            //line.SetPosition(1, cueBall.GetComponent<Transform>().localPosition * -2);
            dir = 1;//direction of the cueball relfectline
            maxLength = 1.25f;
        }
        void Update()
        {
            if (showGuidelineFlag)
            {
                CircleLine.gameObject.SetActive(true);
                GuideLine.gameObject.SetActive(true);
                BallReflectLine.gameObject.SetActive(true);
                CueBallReflectLine.gameObject.SetActive(true);

                //Raycast left
                Physics.Raycast(transform.parent.position + (transform.parent.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x), (CubeDummy.transform.position + (CubeDummy.transform.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x)) - (transform.parent.position + (transform.parent.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x)), out leftHit, Screen.width);
                Debug.DrawRay(transform.parent.position + (transform.parent.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x), (CubeDummy.transform.position + (CubeDummy.transform.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x) + (CubeDummy.transform.right * 15f)) - (transform.parent.position + (transform.parent.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x)));
                //Raycast right
                Physics.Raycast(transform.parent.position + (-transform.parent.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x), (CubeDummy.transform.position + (-CubeDummy.transform.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x)) - (transform.parent.position + (-transform.parent.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x)), out rightHit, Screen.width);
                Debug.DrawRay(transform.parent.position + (-transform.parent.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x), (CubeDummy.transform.position + (-CubeDummy.transform.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x) + (CubeDummy.transform.right * 15f)) - (transform.parent.position + (-transform.parent.forward * cueBall.GetComponent<SphereCollider>().bounds.extents.x)));
                //Raycast center
                Physics.Raycast(transform.parent.position, CubeDummy.transform.position - transform.parent.position, out centerHit, Screen.width);
                Debug.DrawRay(transform.parent.position, CubeDummy.transform.position + (CubeDummy.transform.right * 15f) - transform.parent.position);
                hit = centerHit;

                cueBallHitPosition = hit.point - ((CubeDummy.transform.position - transform.parent.position).normalized * cueBall.GetComponent<SphereCollider>().bounds.extents.x);
               
                if (leftHit.collider != null && rightHit.collider != null && leftHit.collider.tag == "Ball" && leftHit.collider.name == rightHit.collider.name)
                {
                    print("all same");
                    hit = centerHit;
                    cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * cueBall.GetComponent<SphereCollider>().bounds.extents.x);
                }
                else
                {
                    if (leftHit.collider != null && rightHit.collider != null && leftHit.distance <= rightHit.distance && leftHit.collider.tag == "Ball")
                    {
                        print("lefthit");
                        hit = leftHit;
                        cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * cueBall.GetComponent<SphereCollider>().bounds.extents.x);
                        cueBallHitPosition = cueBall.GetComponent<Transform>().position + ((CubeDummy.transform.position - transform.parent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
                    }
                    else if (leftHit.collider != null && rightHit.collider != null && leftHit.distance > rightHit.distance && rightHit.collider.tag == "Ball")
                    {
                        print("righthit");
                        hit = rightHit;
                        cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * cueBall.GetComponent<SphereCollider>().bounds.extents.x);
                        cueBallHitPosition = cueBall.GetComponent<Transform>().position + ((CubeDummy.transform.position - transform.parent.position).normalized * (cueBallHitPosition - cueBall.GetComponent<Transform>().position).magnitude);
           
                    }
                    if (centerHit.collider != null && centerHit.distance <= hit.distance && centerHit.collider.tag == "Ball")
                    {
                        print("centerhit");
                        hit = centerHit;
                        cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * cueBall.GetComponent<SphereCollider>().bounds.extents.x);
                    }
                    //if(leftHit.collider != null && centerHit.collider != null && centerHit.distance <= rightHit.distance && leftHit.collider.tag == "Ball" && leftHit.collider.name == centerHit.collider.name)
                    //{
                    //    print("left center same");
                    //    hit = centerHit;
                    //    cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * cueBall.GetComponent<SphereCollider>().bounds.extents.x);
                    //}
                    //else if (rightHit.collider != null && centerHit.collider != null && centerHit.distance <= leftHit.distance &&  rightHit.collider.tag == "Ball" && rightHit.collider.name == centerHit.collider.name)
                    //{
                    //    print("right center same");
                    //    hit = centerHit;
                    //    cueBallHitPosition = hit.point - ((hit.point - cueBall.GetComponent<Transform>().position).normalized * cueBall.GetComponent<SphereCollider>().bounds.extents.x);
                    //    //hit.normal = -hit.point -hit.normal * cueBall.GetComponent<SphereCollider>().bounds.extents.x;
                    //}
                }

                if (hit.collider != null)
                {
                    //hit.normal = cueBallHitPosition + ((hit.collider.transform.position - cueBallHitPosition).normalized * 15f);
                    DisplayGuidelines(hit, cueBallHitPosition);
                }

            }


            if (isStrikeCompleted)
            {
                if (cueBall.velocity.magnitude <= 0.1f)
                {
                    transform.parent.position = new Vector3(cueBall.GetComponent<Transform>().position.x, transform.parent.position.y,
                        cueBall.GetComponent<Transform>().position.z);
                    isStrikeCompleted = false;
                    forceSlider.value = 0;
                    forceFromCueStick = constantForceFromCueStick;
                    transform.localPosition = startCueStickPosition;
                    
                    BallReflectLine.SetPosition(0, Vector3.zero);
                    BallReflectLine.SetPosition(1, Vector3.zero);

                    CueBallReflectLine.SetPosition(0, Vector3.zero);
                    CueBallReflectLine.SetPosition(1, Vector3.zero);

                    GuideLine.SetPosition(0, Vector3.zero);
                    GuideLine.SetPosition(1, Vector3.zero);

                    CircleLine.gameObject.SetActive(false);
                    showGuidelineFlag = true;
                }
            }
        }

        public void DisplayGuidelines(RaycastHit hit, Vector3 cueBallHitPosition)
        {
            if (hit.collider.tag == "Ball") // only show reflect lines if it is an object ball
            {


                float radius = cueBall.GetComponent<SphereCollider>().bounds.extents.x;


                ballCenter = hit.collider.GetComponent<Transform>().position;

                ballFinalPos = cueBallHitPosition + ((hit.collider.transform.position - cueBallHitPosition).normalized * 15f);

                //cueBallHitPosition + (Quaternion.AngleAxis(-180f, Vector3.up) * (hit.normal) * scale); //Vector3.Reflect(Vector3.right, hit.normal);

                //To make the y same as that of the point 1 of line
                ballFinalPos = new Vector3(ballFinalPos.x, ballCenter.y, ballFinalPos.z);
                //Determine the angle to find length and direction of the reflect lines
                ballAngle = Vector3.Angle((CubeDummy.transform.position - transform.parent.position).normalized, ballFinalPos);
                sign = Mathf.Sign(Vector3.Dot((CubeDummy.transform.position - transform.parent.position), Vector3.Cross(Vector3.up, ballFinalPos)));
                ballAngle = ballAngle * sign;


               
                if (ballAngle >= 0)
                {
                    //dir = -1;
                    ballReflectLength = ((70 - ballAngle) / 70) * maxLength;
                    cueBallReflectLength = maxLength - ballReflectLength;

                    cueBallFinalPos = cueBallHitPosition + (rotate90 * (-1 * ballFinalPos) * radius);
                    
                    //Ballreflect line length capping
                    Vector3 direction = ballFinalPos - ballCenter;
                    direction = direction.normalized;
                    ballFinalPos = ballCenter + (direction * ballReflectLength);

                    //CueBallreflect line length capping
                    direction = cueBallFinalPos - cueBallHitPosition;
                    direction = direction.normalized;
                    cueBallFinalPos = cueBallHitPosition + (direction * cueBallReflectLength);

                }
                else
                {
                    //dir = 1;
                    ballReflectLength = ((70 + ballAngle) / 70) * maxLength;
                    cueBallReflectLength = maxLength - ballReflectLength;
                    /*cueBallReflectLength = ((ballAngle - 90) / 90) * maxLength;
                    ballReflectLength = maxLength - cueBallReflectLength;*/
                    //print(ballReflectLength + " : " + cueBallReflectLength);
                    
                    cueBallFinalPos = cueBallHitPosition + (rotate90 * (1 * ballFinalPos) * radius);

                    //Ballreflect line length capping
                    Vector3 direction = ballFinalPos - ballCenter;
                    direction = direction.normalized;
                    ballFinalPos = ballCenter + (direction * ballReflectLength);


                    //CueBallreflect line length capping
                    direction = cueBallFinalPos - cueBallHitPosition;
                    direction = direction.normalized;
                    cueBallFinalPos = cueBallHitPosition + (direction * cueBallReflectLength);
                }
                //Cue Stick Guideline
                Debug.DrawLine(transform.parent.position, cueBallHitPosition, Color.black);
                GuideLine.SetPosition(0, cueBall.GetComponent<Transform>().position);
                GuideLine.SetPosition(1, cueBallHitPosition);
                DrawCircle(radius, cueBallHitPosition, 0.05f, 0.05f);

                // Ball direction
                BallReflectLine.SetPosition(0, ballCenter);
                BallReflectLine.SetPosition(1, ballFinalPos);

                // Cue Ball Direction
                CueBallReflectLine.SetPosition(0, cueBallHitPosition);
                CueBallReflectLine.SetPosition(1, cueBallFinalPos);
            }
            else // if not object ball
            {         

                // Cue Stick Guideline
                Debug.DrawLine(transform.parent.position, cueBallHitPosition, Color.black);
                GuideLine.SetPosition(0, transform.parent.position);
                GuideLine.SetPosition(1, cueBallHitPosition);
                DrawCircle(cueBall.GetComponent<SphereCollider>().bounds.extents.x, cueBallHitPosition, 0.05f, 0.05f);

                //Resetting guidelines
                BallReflectLine.SetPosition(0, Vector3.zero);
                BallReflectLine.SetPosition(1, Vector3.zero);

                CueBallReflectLine.SetPosition(0, Vector3.zero);
                CueBallReflectLine.SetPosition(1, Vector3.zero);
            }
        }

        public void UpdateForceValue()
        {
            forceFromCueStick = constantForceFromCueStick * forceSlider.value;

            if (prevSliderValue <= forceSlider.value)
            {
                transform.localPosition += Vector3.right * -5 * (forceSlider.value - prevSliderValue);
                prevSliderValue = forceSlider.value;
            }
            else
            {
                transform.localPosition += Vector3.right * 5 * (prevSliderValue - forceSlider.value);
                prevSliderValue = forceSlider.value;
            }
        }

        public void StrikeTheBall()
        {
            showGuidelineFlag = false;

            //Remove guidelines
            CircleLine.gameObject.SetActive(false);
            GuideLine.gameObject.SetActive(false);
            BallReflectLine.gameObject.SetActive(false);
            CueBallReflectLine.gameObject.SetActive(false);

            cueBall.AddForce(-transform.up * forceFromCueStick);
            transform.localPosition = startCueStickPosition;
            StartCoroutine(ResetGameFlag());
        }

        public void DrawCircle(float radius, Vector3 centerPos, float startWidth, float endWidth) //To draw circle related to guideline
        {
            var segments = 360;
            CircleLine.startWidth = startWidth;
            CircleLine.endWidth = endWidth;
            CircleLine.positionCount = segments + 1;

            var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
            var points = new Vector3[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 360f / segments);
                points[i] = new Vector3(Mathf.Sin(rad) * radius, 0.25f, Mathf.Cos(rad) * radius) + centerPos;
            }

            CircleLine.SetPositions(points);
        }

        IEnumerator ResetGameFlag()
        {
            yield return new WaitForSeconds(0.2f);
            isStrikeCompleted = true;
        }
    }
}
