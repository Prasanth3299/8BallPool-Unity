
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RevolutionGames.Game
{
    public class CueBallManagerQuickFireMode : MonoBehaviour
    {
        public GameManagerQuickFire gameManager;
        public Transform cueBall;

        private GameObject cueBallInHandCircle, cueStickParent, headStringAreaVisible, headStringArea,
            floorArea, guideLines, handImage;
        private GameObject[] balls;
        private SpriteRenderer headStringAreaSprite, floorAreaSprite;
        private bool isMouseButtonPressed = false, noObstaclesFlag = true;
        private float cueBallYPos = 0, cueBallRadius = 0;
        private Vector3 mousePos = Vector3.zero, tempPos = Vector3.zero;
        private Vector3[] ballPositions;
        private SphereCollider[] ballSphereColliders;
        private Transform centerObject;
        private Vector3 initialCenterObjectPosition;
        private Vector3 cueBallSpawnPosition = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            cueBallInHandCircle = gameManager.cueBallInHandCircle;
            cueStickParent = gameManager.cueStickParent;
            headStringArea = gameManager.headStringArea;
            headStringAreaVisible = gameManager.headStringAreaVisible;
            floorArea = gameManager.floorArea;
            handImage = gameManager.handImage;
            guideLines = gameManager.guideLines;
            balls = gameManager.balls;
            centerObject = gameManager.centerObject;
            initialCenterObjectPosition = gameManager.centerObject.position;
            cueBallSpawnPosition = cueBall.transform.position;

            ballPositions = new Vector3[balls.Length];
            //setting the position of 8 ball
            //ballPositions[7] = balls[7].transform.position;
            //Store positions of each object ball
            for (int i = 0; i < balls.Length; i++)
            {
                ballPositions[i] = balls[i].transform.position;
            }

            ballSphereColliders = new SphereCollider[balls.Length];
            headStringAreaSprite = headStringArea.GetComponent<SpriteRenderer>();
            floorAreaSprite = floorArea.GetComponent<SpriteRenderer>();
            cueBallInHandCircle.SetActive(false);
            headStringAreaVisible.SetActive(false);
            cueBallYPos = cueBall.transform.TransformPoint(cueBall.GetComponent<SphereCollider>().center).y;
            tempPos = cueBall.position;
            cueBallRadius = cueBall.GetComponent<SphereCollider>().bounds.extents.x;
            for (int i = 0; i < balls.Length; i++)
            {
                ballSphereColliders[i] = balls[i].GetComponent<SphereCollider>();

            }
        }

        // Update is called once per frame
        void Update()
        {
            cueBallInHandCircle.transform.position = new Vector3(cueBall.transform.position.x, cueBallInHandCircle.transform.position.y, cueBall.transform.position.z);
            handImage.transform.position = cueBallInHandCircle.transform.position;
        }

        //Functions to be called from CanvasInputPositionHandler when touching or clicking on the canvas
        public void OnPointerDownFunction(PointerEventData pointerEventData)
        {
            ShowHandImage(false);
            if (gameManager.CurrentGameState == GameManagerQuickFire.GameState.isItResetCueballOn ||
                gameManager.CurrentGameState == GameManagerQuickFire.GameState.isItCueballBreak)
            {
                //Store positions of each object ball
                for (int i = 0; i < balls.Length; i++)
                {
                    ballPositions[i] = balls[i].transform.position;
                }

                guideLines.SetActive(false);
                isMouseButtonPressed = true;
                cueBallInHandCircle.SetActive(true);
                cueStickParent.SetActive(false);
                if (gameManager.CurrentGameState == GameManagerQuickFire.GameState.isItCueballBreak)
                {
                    headStringAreaVisible.SetActive(true);
                }

                //Ray ray = Camera.main.ScreenPointToRay(pointerEventData.position);
                //RaycastHit hit;

                //if (Physics.Raycast(ray, out hit, 100))
                //{
                //    // whatever tag you are looking for on your game object
                //    if (hit.collider.tag == "CueBallInHand" || hit.collider.tag == "CueBall")
                //    {
                //        guideLines.SetActive(false);
                //        isMouseButtonPressed = true;
                //        cueBallInHandCircle.SetActive(true);
                //        cueStickParent.SetActive(false);
                //        if (gameManager.CurrentGameState == GameManager.GameState.isItCueballBreak)
                //            headStringAreaVisible.SetActive(true);
                //    }
                //}
            }
        }

        public void OnPointerUpFunction(PointerEventData pointerEventData)
        {
            if (gameManager.CurrentGameState == GameManagerQuickFire.GameState.isItResetCueballOn ||
                gameManager.CurrentGameState == GameManagerQuickFire.GameState.isItCueballBreak)
            {
                isMouseButtonPressed = false;
                cueBallInHandCircle.SetActive(false);
                headStringAreaVisible.SetActive(false);
                cueStickParent.SetActive(true);
                cueBall.position = new Vector3(cueBall.position.x, cueBallYPos, cueBall.position.z);
                cueStickParent.transform.position = new Vector3(cueBall.transform.position.x, cueStickParent.transform.position.y,
                        cueBall.transform.position.z);
                cueBall.GetComponent<Rigidbody>().useGravity = true;
                guideLines.SetActive(true);
            }
        }

        //Reset to spawn position if timer runs out and cue ball break shor
        public void ResetCueBallSpawnPosition()
        {
            if (gameManager.CurrentGameState == GameManagerQuickFire.GameState.isItCueballBreak)
            {
                cueBall.position = cueBallSpawnPosition;
            }
            isMouseButtonPressed = false;
            cueBallInHandCircle.SetActive(false);
            headStringAreaVisible.SetActive(false);
            cueStickParent.SetActive(true);
            cueBall.GetComponent<Rigidbody>().useGravity = true;
            guideLines.SetActive(true);
        }

        public void OnDragFunction(PointerEventData data)
        {
            if (isMouseButtonPressed)
            {
                noObstaclesFlag = true;
                mousePos = Camera.main.ScreenToWorldPoint(data.position);
                mousePos = new Vector3(mousePos.x, headStringArea.transform.position.y, mousePos.z);

                if (gameManager.CurrentGameState == GameManagerQuickFire.GameState.isItCueballBreak)
                {
                    if (headStringAreaSprite.bounds.Contains(mousePos))
                    {
                        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        cueBall.position = new Vector3(mousePos.x, cueBallYPos + 1f, mousePos.z);
                        cueBallInHandCircle.transform.position = cueBall.transform.position;
                        cueBall.GetComponent<Rigidbody>().useGravity = false;
                    }
                    else
                    {
                        if ((mousePos.x < headStringAreaSprite.bounds.min.x)) //&& (mousePos.z > headStringAreaSprite.bounds.min.z) && (mousePos.z < headStringAreaSprite.bounds.max.z))
                        {
                            if (mousePos.z >= 0)
                                cueBall.position = new Vector3(headStringAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Min(headStringAreaSprite.bounds.max.z, mousePos.z));
                            else
                                cueBall.position = new Vector3(headStringAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Max(headStringAreaSprite.bounds.min.z, mousePos.z));

                        }
                        else if ((mousePos.x > headStringAreaSprite.bounds.max.x)) //&& (mousePos.z > headStringAreaSprite.bounds.min.z) && (mousePos.z < headStringAreaSprite.bounds.max.z))
                        {
                            if (mousePos.z >= 0)
                                cueBall.position = new Vector3(headStringAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Min(headStringAreaSprite.bounds.max.z, mousePos.z));
                            else
                                cueBall.position = new Vector3(headStringAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Max(headStringAreaSprite.bounds.min.z, mousePos.z));
                        }
                        else if ((mousePos.z < headStringAreaSprite.bounds.min.z) && (mousePos.x > headStringAreaSprite.bounds.min.x) && (mousePos.x < headStringAreaSprite.bounds.max.x))
                        {
                            cueBall.position = new Vector3(mousePos.x, cueBallYPos + 1f, headStringAreaSprite.bounds.min.z);
                        }
                        else if ((mousePos.z > headStringAreaSprite.bounds.max.z) && (mousePos.x > headStringAreaSprite.bounds.min.x) && (mousePos.x < headStringAreaSprite.bounds.max.x))
                        {
                            cueBall.position = new Vector3(mousePos.x, cueBallYPos + 1f, headStringAreaSprite.bounds.max.z);
                        }
                        cueBallInHandCircle.transform.position = new Vector3(cueBall.position.x, cueBallYPos + 1f, cueBall.position.z);
                        cueBall.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
                else
                {
                    CheckCueBallPlacement();
                    if (noObstaclesFlag)
                    {
                        tempPos = new Vector3(tempPos.x, cueBallYPos + 1f, tempPos.z);
                        cueBall.position = tempPos;
                        cueBallInHandCircle.transform.position = new Vector3(tempPos.x, cueBallYPos + 1f, tempPos.z);
                        cueBall.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            }
        }

        public void CheckCueBallPlacement()
        {
            if (floorAreaSprite.bounds.Contains(mousePos))
            {
                tempPos = new Vector3(mousePos.x, cueBallYPos + 1f, mousePos.z);
            }
            else
            {
                if ((mousePos.x < floorAreaSprite.bounds.min.x)) //&& (mousePos.z > floorAreaSprite.bounds.min.z) && (mousePos.z < floorAreaSprite.bounds.max.z))
                {
                    if (mousePos.z >= 0)
                        tempPos = new Vector3(floorAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Min(floorAreaSprite.bounds.max.z, mousePos.z));
                    else
                        tempPos = new Vector3(floorAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Max(floorAreaSprite.bounds.min.z, mousePos.z));
                }
                else if ((mousePos.x > floorAreaSprite.bounds.max.x)) //&& (mousePos.z > floorAreaSprite.bounds.min.z) && (mousePos.z < floorAreaSprite.bounds.max.z))
                {
                    if (mousePos.z >= 0)
                        tempPos = new Vector3(floorAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Min(floorAreaSprite.bounds.max.z, mousePos.z));
                    else
                        tempPos = new Vector3(floorAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Max(floorAreaSprite.bounds.min.z, mousePos.z));

                }
                else if ((mousePos.z < floorAreaSprite.bounds.min.z) && (mousePos.x > floorAreaSprite.bounds.min.x) && (mousePos.x < floorAreaSprite.bounds.max.x))
                {
                    tempPos = new Vector3(mousePos.x, cueBallYPos + 1f, floorAreaSprite.bounds.min.z);
                }
                else if ((mousePos.z > floorAreaSprite.bounds.max.z) && (mousePos.x > floorAreaSprite.bounds.min.x) && (mousePos.x < floorAreaSprite.bounds.max.x))
                {
                    tempPos = new Vector3(mousePos.x, cueBallYPos + 1f, floorAreaSprite.bounds.max.z);
                }
            }

            //check for obstacles before placing the moved cue ball
            tempPos = new Vector3(tempPos.x, cueBallYPos, tempPos.z);

            for (int i = 0; i < balls.Length; i++)
            {

                if (Math.Round((double)Vector3.Distance(tempPos, ballPositions[i]), 2) <= Math.Round((double)(cueBallRadius + (ballSphereColliders[7].bounds.extents.x)), 2) + 0.03f) //tolerance
                {
                    //print(Math.Round((double)Vector3.Distance(tempPos, ballPositions[i]), 2) + ":" + Math.Round((double)(cueBallRadius + (ballSphereColliders[7].bounds.extents.x)), 2));
                    noObstaclesFlag = false;
                }
            }
        }

        public void ResetCueBall()
        {
            cueBall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            centerObject.position = initialCenterObjectPosition; //Resetting position back to center
            for (int i = 0; i < balls.Length; i++)
            {
                ballPositions[i] = balls[i].transform.position;
            }
            gameManager.SetCueBallInHand(); //Set Gamestate
            cueBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            cueBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        A:

            noObstaclesFlag = true;
            int j = 0;
            float diff = 0;
            for (int i = 0; i < balls.Length; i++)
            {

                if (Math.Round((double)Vector3.Distance(centerObject.position, ballPositions[i]), 7) <= Math.Round((double)(cueBallRadius + (ballSphereColliders[8].bounds.extents.x)), 7))
                {
                    noObstaclesFlag = false;
                    j = i;
                    diff = (cueBallRadius * 2) - (Vector3.Distance(centerObject.position, ballPositions[i]));
                    break;
                }
            }
            if (noObstaclesFlag)
            {
                cueBall.position = centerObject.position;
            }
            else
            {
                centerObject.position = new Vector3(centerObject.position.x, centerObject.position.y, centerObject.position.z + diff + 0.005f);
                goto A;
            }
            gameManager.cueStickManager.ResetCueStick();
        }

        public void ShowHandImage(bool val)
        {
            handImage.SetActive(val);
        }
    }
}

