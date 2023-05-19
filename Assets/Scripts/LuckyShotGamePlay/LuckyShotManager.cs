using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using RevolutionGames.Game;

namespace RevolutionGames.UI
{

    public class LuckyShotManager: MonoBehaviour
    {
        //public BoardManagerLuckyShot boardManagerLuckyShot;
        public Transform prizeAreaPosition;
        public Transform cueBallPosition;
        public Transform ballPosition;
        public Transform cueStickParent;
        public Transform canvas;
        public Transform targetPosition;
        public GameObject luckyShotPrizeWinPopup;
        public GameObject freeLuckyShotPrizeDetails;
        public GameObject paidLuckyShotPrizeDetails;
        public BallCollisionHandlerLuckyShot ballCollisionHandlerLuckyShot;
        public CueBallCollisionHandlerLuckyShot cueBallCollisionHandlerLuckyShot;
        public Text winText;
        private int freeLuckyShotCount;
        private string betterLuckText = "Better luck next time";

        //prize area circle position
        private float prizeAreaMinXvalue , prizeAreaMaxXvalue, prizeAreaMinZvalue , prizeAreaMaxZvalue;
        //prize circle top left area position(range 1)
        private float topLeftPrizeAreaMinXvalue=-5.5f, topLeftPrizeAreaMaxXvalue=-3.65f, topLeftPrizeAreaMinZvalue=-0.48f, topLeftPrizeAreaMaxZvalue=0.56f;
        //prize circle bottom left area position(range 2)
        private float bottomLeftPrizeAreaMinXvalue = -5.5f, bottomLeftPrizeAreaMaxXvalue = -3.65f, bottomLeftPrizeAreaMinZvalue = -0.48f, bottomLeftPrizeAreaMaxZvalue = -2.06f;
        //prize circle top right area position(range 3)
        private float topRightPrizeAreaMinXvalue = 3f, topRightPrizeAreaMaxXvalue = 4.59f, topRightPrizeAreaMinZvalue = -0.48f, topRightPrizeAreaMaxZvalue = 0.56f;
        //prize circle bottom right area position(range 4)
        private float bottomRightPrizeAreaMinXvalue = 3f, bottomRightPrizeAreaMaxXvalue = 4.59f, bottomRightPrizeAreaMinZvalue = -0.48f, bottomRightPrizeAreaMaxZvalue = -2.06f;
        //right side ball palce position area value
        private float ballMinXValue, ballMaxXValue , ballMinZValue , ballMaxZValue;
        // ball bottom right position area value
        private float bottomRightBallMinXValue=0.90f, bottomRightBallMaxXValue=2.13f, bottomRightBallMinZValue=-0.66f, bottomRightBallMaxZValue=-3.5f;
        // ball top right  position area value
        private float topRightBallMinXValue = 0.90f, topRightBallMaxXValue = 2.13f, topRightBallMinZValue = -0.66f, topRightBallMaxZValue = 2f;
        // ball bottom left  position area value
        private float bottomLeftBallMinXValue =-3f, bottomLeftBallMaxXValue =-1.77f, bottomLeftBallMinZValue = -0.66f, bottomLeftBallMaxZValue = -3.5f;
        // ball top left  position area value
        private float topLeftBallMinXValue = -3f, topLeftBallMaxXValue = -1.77f, topLeftBallMinZValue = -0.66f, topLeftBallMaxZValue = 2f;
        // cue ball palce position area value
        private float cueBallMinXValue, cueBallMaxXValue, cueBallMinZValue, cueBallMaxZValue;
        // cue ball bottom right position area value
        private float bottomRightCueBallMinXValue=3.54f, bottomRightCueBallMaxXValue=6.57f, bottomRightCueBallMinZValue=-0.66f, bottomRightCueBallMaxZValue=-3.5f;
        // cue ball top right position area value
        private float topRightCueBallMinXValue = 3.54f,topRightCueBallMaxXValue = 6.57f, topRightCueBallMinZValue = -0.66f, topRightCueBallMaxZValue = 2f;
        //cue ball bottom left  position area value
        private float bottomLeftCueBallMinXValue = -7.51f, bottomLeftCueBallMaxXValue = -4.54f, bottomLeftCueBallMinZValue = -0.66f, bottomLeftCueBallMaxZValue = -3.5f;
        //cue ball top left  position area value
        private float topLeftCueBallMinXValue = -7.51f, topLeftCueBallMaxXValue = -4.54f, topLeftCueBallMinZValue = -0.66f, topLeftCueBallMaxZValue = 2f;
        //random prize area posion variable
        private float randomPrizeAreaXValue, randomPrizeAreaZValue;
        //random right side ball posion variable
        private float randomRightBallXValue, randomRightBallZValue;
        //random right side cue ball posion variable
        private float randomCueBallXValue, randomCueBallZValue;
        //prize area circle radius data;
        private float yellowCircleRadius = 0.60f, redCircleRadius = 1.15f, blueCircleRadius = 1.76f, blackCircleRadius = 2.36f;

        private float ballDistance;
        private int prizeAreaRange;
        private bool ballHit;


        public void Start()
        {

            //free lucky ui enable
            freeLuckyShotCount = PlayerPrefs.GetInt("luckyshotfreecount");
            {
                if (freeLuckyShotCount == 1)
                {
                    freeLuckyShotPrizeDetails.SetActive(true);
                    paidLuckyShotPrizeDetails.SetActive(false);
                    //freeLuckyShotCount -= 1;
                    //PlayerPrefs.SetInt("lucky shot freecount", freeLuckyShotCount);
                }
                else
                {
                    freeLuckyShotPrizeDetails.SetActive(false);
                    paidLuckyShotPrizeDetails.SetActive(true);
                }
            }
            //get position ball,cue ball and prize area 
            SetBallCueBallPrizeAreaPosition();
            //set randomize position
            SetRandomizePosition();

        }
        public void Update()
        {
            // to check ball hit condition
            ballHit = ballCollisionHandlerLuckyShot.ballHit;

            //Calculate ball distance
            ballDistance = Vector3.Distance(targetPosition.position, ballPosition.position);
        }
        public void SetBallCueBallPrizeAreaPosition()
        {
            prizeAreaRange = UnityEngine.Random.Range(1, 4);
            // print(prizeAreaRange);
            if (prizeAreaRange == 1)
            {
                // print("range  1");
                prizeAreaMinXvalue = topLeftPrizeAreaMinXvalue;
                prizeAreaMaxXvalue = topLeftPrizeAreaMaxXvalue;
                prizeAreaMinZvalue = topLeftPrizeAreaMinZvalue;
                prizeAreaMaxZvalue = topLeftPrizeAreaMaxZvalue;

                //cue ball position
                cueBallMinXValue = bottomRightCueBallMinXValue;
                cueBallMaxXValue = bottomRightCueBallMaxXValue;
                cueBallMinZValue = bottomRightCueBallMinZValue;
                cueBallMaxZValue = bottomRightCueBallMaxZValue;

                ballMinXValue = bottomRightBallMinXValue;
                ballMaxXValue = bottomRightBallMaxXValue;
                ballMinZValue = bottomRightBallMinZValue;
                ballMaxZValue = bottomRightBallMaxZValue;


            }
            if (prizeAreaRange == 2)
            {
                // print("range  2");
                prizeAreaMinXvalue = bottomLeftPrizeAreaMinXvalue;
                prizeAreaMaxXvalue = bottomLeftPrizeAreaMaxXvalue;
                prizeAreaMinZvalue = bottomLeftPrizeAreaMinZvalue;
                prizeAreaMaxZvalue = bottomLeftPrizeAreaMaxZvalue;


                //cue ball position
                cueBallMinXValue = topRightCueBallMinXValue;
                cueBallMaxXValue = topRightCueBallMaxXValue;
                cueBallMinZValue = topRightCueBallMinZValue;
                cueBallMaxZValue = topRightCueBallMaxZValue;


                ballMinXValue = topRightBallMinXValue;
                ballMaxXValue = topRightBallMaxXValue;
                ballMinZValue = topRightBallMinZValue;
                ballMaxZValue = topRightBallMaxZValue;

            }
            if (prizeAreaRange == 3)
            {
                //print("range  3");
                prizeAreaMinXvalue = topRightPrizeAreaMinXvalue;
                prizeAreaMaxXvalue = topRightPrizeAreaMaxXvalue;
                prizeAreaMinZvalue = topRightPrizeAreaMinZvalue;
                prizeAreaMaxZvalue = topRightPrizeAreaMaxZvalue;

                //cue ball position
                cueBallMinXValue = bottomLeftCueBallMinXValue;
                cueBallMaxXValue = bottomLeftCueBallMaxXValue;
                cueBallMinZValue = bottomLeftCueBallMinZValue;
                cueBallMaxZValue = bottomLeftCueBallMaxZValue;


                ballMinXValue = bottomLeftBallMinXValue;
                ballMaxXValue = bottomLeftBallMaxXValue;
                ballMinZValue = bottomLeftBallMinZValue;
                ballMaxZValue = bottomLeftBallMaxZValue;


            }
            if (prizeAreaRange == 4)
            {
                // print("range  4");
                prizeAreaMinXvalue = bottomRightPrizeAreaMinXvalue;
                prizeAreaMaxXvalue = bottomRightPrizeAreaMaxXvalue;
                prizeAreaMinZvalue = bottomRightPrizeAreaMinZvalue;
                prizeAreaMaxZvalue = bottomRightPrizeAreaMaxZvalue;

                //cue ball position
                cueBallMinXValue = topLeftCueBallMinXValue;
                cueBallMaxXValue = topLeftCueBallMaxXValue;
                cueBallMinZValue = topLeftCueBallMinZValue;
                cueBallMaxZValue = topLeftCueBallMaxZValue;

                ballMinXValue = topLeftBallMinXValue;
                ballMaxXValue = topLeftBallMaxXValue;
                ballMinZValue = topLeftBallMinZValue;
                ballMaxZValue = topLeftBallMaxZValue;
            }

        }

        public void SetRandomizePosition()
        {
            float ballYValue = ballPosition.localPosition.y;
            float cueBallYValue = cueBallPosition.localPosition.y;
            float prizeAreaYValue = prizeAreaPosition.localPosition.y;

            //randomize prize area position
            randomPrizeAreaXValue = UnityEngine.Random.Range(prizeAreaMinXvalue, prizeAreaMaxXvalue);
            randomPrizeAreaZValue = UnityEngine.Random.Range(prizeAreaMinZvalue, prizeAreaMaxZvalue);
            prizeAreaPosition.localPosition = new Vector3(randomPrizeAreaXValue, prizeAreaYValue, randomPrizeAreaZValue);

            //randomize ball area position
            randomRightBallXValue = UnityEngine.Random.Range(ballMinXValue, ballMaxXValue);
            randomRightBallZValue = UnityEngine.Random.Range(ballMinZValue, ballMaxZValue);
            ballPosition.localPosition = new Vector3(randomRightBallXValue, ballYValue, randomRightBallZValue);


            //randomize cue ball area position
            randomCueBallXValue = UnityEngine.Random.Range(cueBallMinXValue, cueBallMaxXValue);
            randomCueBallZValue = UnityEngine.Random.Range(cueBallMinZValue, cueBallMaxZValue);
            cueBallPosition.localPosition = new Vector3(randomCueBallXValue, cueBallYValue, randomCueBallZValue);


            cueStickParent.localPosition = cueBallPosition.localPosition;
        }
       


        public void LuckyShotPrizeDetailsUpdate()
        {
            if(freeLuckyShotPrizeDetails.activeSelf)
            {
                if (ballDistance < yellowCircleRadius)
                {
                    //print("Prize1");
                    winText.text = "You have won 5000 Coins";

                }
                else if (ballDistance > yellowCircleRadius && ballDistance < redCircleRadius)
                {
                    //print("Prize2");
                    winText.text = "You have won one surprise box";
                }
                else if (ballDistance > redCircleRadius && ballDistance < blueCircleRadius)
                {
                    //print("Prize3");
                    winText.text = "You have won 1000 Coins";
                }
                else if (ballDistance > blueCircleRadius && ballDistance < blackCircleRadius)
                {
                    //print("Prize4");
                    winText.text = "You have won 500 Coins";
                }

                else
                {
                    winText.text = betterLuckText;
                }
                luckyShotPrizeWinPopup.SetActive(true);
            }
            else
            {
                if (ballDistance < yellowCircleRadius)
                {
                    //print("Prize1");
                    winText.text = "You have Four surprise box";

                }
                else if (ballDistance > yellowCircleRadius && ballDistance < redCircleRadius)
                {
                    //print("Prize2");
                    winText.text = "You have won 100,000 Coins";
                }
                else if (ballDistance > redCircleRadius && ballDistance < blueCircleRadius)
                {
                    //print("Prize3");
                    winText.text = "You have won 75000 Coins";
                }
                else if (ballDistance > blueCircleRadius && ballDistance < blackCircleRadius)
                {
                    //print("Prize4");
                    winText.text = "You have won 50000 Coins";
                }

                else
                {
                    //print("else");
                    winText.text = betterLuckText;
                }
                luckyShotPrizeWinPopup.SetActive(true);
            }
           
            
        }

        public void BallPottedUpdateDetails(bool cueBallPotted,bool ballPotted)
        {
            
            if (cueBallPotted== true && ballHit == false|| ballPotted == true)
            {
                winText.text = betterLuckText;
                luckyShotPrizeWinPopup.SetActive(true);
                
            }
        }
        public void BollNotHitUpdateDetails()
        {
           
            if (ballHit == false && cueBallCollisionHandlerLuckyShot.cueBallStop == true)
            {
                    winText.text = betterLuckText;
                luckyShotPrizeWinPopup.SetActive(true);
            }

        }

        public void OnPopupOkButtonCLicked()
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
