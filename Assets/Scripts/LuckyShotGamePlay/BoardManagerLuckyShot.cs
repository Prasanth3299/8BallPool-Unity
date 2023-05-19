using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RevolutionGames.UI;

namespace RevolutionGames.Game
{
    public class BoardManagerLuckyShot : MonoBehaviour
    {
        public GameManagerLuckyShot gameManager;
        public LuckyShotManager luckyShotManager;
        public Transform pottedStartPosition;
        public Transform pottedEndPosition;

        private Vector3 pottedTarget;
        private int pottedBallcounter;
        private float pottedBallRadius;
        private CueStickManagerLuckyShot cueStickManager;

        private List<GameObject> solidBallList = new List<GameObject>();
        private List<GameObject> stripeBallList = new List<GameObject>();
        private List<Rigidbody> remainingBallsInPlay = new List<Rigidbody>();

        //List to get only the balls potted between each strike
        private List<GameObject> currentlyPottedBalls = new List<GameObject>();
        private List<GameObject> pottedBalls = new List<GameObject>();

        private GameObject firstHitBall; //Ball that is hit first using the cue ball
        private int hitBallsCount = 0;
        private bool cueBallPotted = false;
        private bool BallPotted = false;
        private bool playerGroupBallPotted = false;

        //GameObject local for 8ball
        private GameObject eightBallObject;

        // Start is called before the first frame update
        void Start()
        {
            /*
            cueStickManager = gameManager.cueStickManager;
            for (int i = 0; i < gameManager.balls.Length; i++)
            {
                if (i < 7)
                {
                    solidBallList.Add(gameManager.balls[i]);
                }
                else if (i > 7)
                {
                    stripeBallList.Add(gameManager.balls[i]);
                }
                remainingBallsInPlay.Add(gameManager.balls[i].GetComponent<Rigidbody>());
            }
            remainingBallsInPlay.Add(gameManager.cueBall.GetComponent<Rigidbody>());
            eightBallObject = gameManager.balls[7];
            pottedBallcounter = 0;
            pottedBallRadius = gameManager.balls[0].GetComponent<SphereCollider>().bounds.extents.x;
            */
        }

        // Update is called once per frame
        void Update()
        {
            /*
            //if strike has been completed, then waiting for all balls on the table to stop moving
            if (gameManager.CurrentGameState == GameManagerLuckyShot.GameState.isItCueballBreakDone)
            {
                bool isTurnCompleted = true;
                for (int i = 0; i < remainingBallsInPlay.Count; i++)
                {
                    if (remainingBallsInPlay[i].velocity.magnitude >= 0.005f)
                    {
                        isTurnCompleted = false;
                        break;
                    }
                }
                if (isTurnCompleted)
                {
                    cueStickManager.UpdateCueStickProperties();
                    CheckBreakShotRules();
                    gameManager.cueStickManager.ResetCueStick();
                    if (!(gameManager.CurrentGameState == GameManagerLuckyShot.GameState.isItResetCueballOn))
                    {
                        gameManager.CurrentGameState = GameManagerLuckyShot.GameState.isReadyForStrike;
                    }
                    StartCoroutine(SetBallPositionInTray());
                    cueBallPotted = false;
                    playerGroupBallPotted = false;

                }
            }
            else if (gameManager.CurrentGameState == GameManagerLuckyShot.GameState.isStrikeDone)
            {
                bool isTurnCompleted = true;
                for (int i = 0; i < remainingBallsInPlay.Count; i++)
                {
                    if (remainingBallsInPlay[i].velocity.magnitude >= 0.005f)
                    {
                        isTurnCompleted = false;
                        break;
                    }
                }
                if (isTurnCompleted)
                {
                    cueStickManager.UpdateCueStickProperties();
                    if (!CheckForGameOver())
                    {
                        //If all remaining balls are idle, change state to ready
                        //bool islegalHit = CheckHitGameRules();
                        //if(islegalHit)
                        CheckGameRules();
                        if (gameManager.CurrentPlayer.ballGroupType == "")
                        {
                            UpdateBallTypeForPlayers();
                        }
                        else
                        {
                            for (int i = 0; i < currentlyPottedBalls.Count; i++)
                            {
                                UpdateRemainingBallListForPlayers(currentlyPottedBalls[i].transform);
                            }
                        }
                        gameManager.cueStickManager.ResetCueStick();
                        if (!(gameManager.CurrentGameState == GameManagerLuckyShot.GameState.isItResetCueballOn))
                        {
                            gameManager.CurrentGameState = GameManagerLuckyShot.GameState.isReadyForStrike;
                        }
                        StartCoroutine(SetBallPositionInTray());
                        cueBallPotted = false;
                        playerGroupBallPotted = false;
                        //currentlyPottedBalls.Clear();
                    }
                    else
                    {
                        gameManager.CurrentGameState = GameManagerLuckyShot.GameState.isGameOver;
                        gameManager.GameOver();
                    }

                }
            }
            */
        }

        public void PocketedBallDetails(Collision collision)
        {
            switch (collision.collider.tag)
            {
                case "CueBall":
                    cueBallPotted = true;
          
                    break;

                case "Ball":
                    {
                       
                        BallPotted = true;
                        //print("collision" + collision.collider.name);
                        currentlyPottedBalls.Add(collision.gameObject);
                        if (!collision.gameObject.name.Contains("8Ball"))
                        {
                            pottedBalls.Add(collision.gameObject);
                        }
                        if (gameManager.CurrentGameState != GameManagerLuckyShot.GameState.isItCueballBreakDone)
                        {
                            UpdatePottedBallInTray(collision.collider.transform);
                        }
                        else
                        {
                            UpdatePottedBallInTrayForBreakShot(collision.collider.transform);
                        }
                    }
                    break;

                default:
                    break;
            }

            luckyShotManager.BallPottedUpdateDetails(cueBallPotted,BallPotted);
            
        }

        public void CheckGameRules()
        {
            //check if player group ball is potted or not
            for (int i = 0; i < currentlyPottedBalls.Count; i++)
            {
                if (gameManager.CheckGroupType(currentlyPottedBalls[i].name))
                    playerGroupBallPotted = true;
            }

            if (cueBallPotted)
            {
                gameManager.SwitchPlayerTurn();
                gameManager.cueBallManager.ResetCueBall();
            }
            else
            {
                if (CheckHitGameRules())
                {
                    if (!playerGroupBallPotted)
                        gameManager.SwitchPlayerTurn();
                    else
                        gameManager.RestartTimer();
                }
                else
                {
                    gameManager.SwitchPlayerTurn();
                    gameManager.SetCueBallInHand();
                }
            }

        }

        public void CheckBreakShotRules()
        {
            if (cueBallPotted)
            {
                gameManager.SwitchPlayerTurn();
                gameManager.cueBallManager.ResetCueBall();
            }
            else
            {
                if (CheckHitGameRules())
                {
                    if (!playerGroupBallPotted)
                        gameManager.SwitchPlayerTurn();
                    else
                        gameManager.RestartTimer();
                }
                else
                {
                    gameManager.SwitchPlayerTurn();
                    gameManager.SetCueBallInHand();
                }
            }
            for (int i = 0; i < currentlyPottedBalls.Count; i++)
            {
                if (currentlyPottedBalls[i].name.Contains("8Ball"))
                {
                    currentlyPottedBalls[i].SetActive(true);
                    gameManager.Reset8Ball();
                    currentlyPottedBalls.RemoveAt(i);
                }
            }
        }

        public void AddHitBallsCount(GameObject hitBall)
        {
            hitBallsCount++;
            if (hitBallsCount == 1)
            {
                firstHitBall = hitBall;
            }
        }

        public bool CheckHitGameRules()
        {
            if (hitBallsCount == 0)
            {
                //No ball has been hit by the cueball
                return false;
            }
            else
            {
                /*if (gameManager.CurrentPlayer == gameManager.FirstPlayer)
                {
                    if(gameManager.CurrentPlayer.ballGroupType != "" && !firstHitBall.name.Contains(gameManager.FirstPlayer.ballGroupType))
                    {
                        //Illegal hit - wrong hit by the ccurent player
                        gameManager.SwitchPlayerTurn();
                        gameManager.cueBallManager.ResetCueBall();
                        return false;
                    }
                }
                else
                {
                    if (gameManager.CurrentPlayer.ballGroupType != "" && !firstHitBall.name.Contains(gameManager.SecondPlayer.ballGroupType))
                    {
                        //Illegal hit - wrong hit by the ccurent player
                        gameManager.SwitchPlayerTurn();
                        gameManager.cueBallManager.ResetCueBall();
                        return false;
                    }
                }*/

                if ((gameManager.CurrentPlayer.ballGroupType == "" && firstHitBall.name.Contains("8Ball")))
                {
                    //print("check1");
                    firstHitBall = null;
                    hitBallsCount = 0;
                    return false;
                }

                if (gameManager.CurrentPlayer.ballGroupType != "")
                {
                    if (gameManager.CurrentPlayer.remainingBalls.Count == 1 &&
                        gameManager.CurrentPlayer.remainingBalls[0].name.Contains("8Ball"))
                    {
                        //print("check2" + firstHitBall.name);

                        if (!firstHitBall.name.Contains("8Ball"))
                        {
                            //print("check3");
                            firstHitBall = null;
                            hitBallsCount = 0;
                            return false;
                        }
                        //print("check4");
                        firstHitBall = null;
                        hitBallsCount = 0;
                        return true;
                    }
                    else if (!firstHitBall.name.Contains(gameManager.CurrentPlayer.ballGroupType))
                    {
                        //print("check5");
                        firstHitBall = null;
                        hitBallsCount = 0;
                        return false;
                    }
                }
            }
            //print("check6");
            firstHitBall = null;
            hitBallsCount = 0;
            return true;
        }

        //Remove the velocity of potted ball and then place in the tray
        private void UpdatePottedBallInTray(Transform ballTransform)
        {
            ballTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ballTransform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ballTransform.gameObject.SetActive(false);
            /*if (gameManager.CurrentPlayer.ballGroupType == "")
            {
                UpdateBallTypeForPlayers(ballTransform);
            }
            UpdateRemainingBallListForPlayers(ballTransform);*/
        }
        private void UpdatePottedBallInTrayForBreakShot(Transform ballTransform)
        {
            ballTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ballTransform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ballTransform.gameObject.SetActive(false);
            for (int i = 0; i < stripeBallList.Count; i++)
            {
                if (ballTransform == stripeBallList[i].transform)
                {
                    stripeBallList.RemoveAt(i);
                }
            }
            for (int i = 0; i < solidBallList.Count; i++)
            {
                if (ballTransform == solidBallList[i].transform)
                {
                    solidBallList.RemoveAt(i);
                }
            }
        }

        IEnumerator SetBallPositionInTray()
        {
            for (int i = 0; i < currentlyPottedBalls.Count; i++)
            {
                currentlyPottedBalls[i].transform.position = pottedStartPosition.position;
                currentlyPottedBalls[i].GetComponent<Rigidbody>().useGravity = false;
                currentlyPottedBalls[i].gameObject.SetActive(true);
                Vector3 target = new Vector3(pottedEndPosition.position.x,
                    pottedEndPosition.position.y, pottedEndPosition.position.z + (pottedBallcounter * pottedBallRadius * 2));
                //currentlyPottedBalls[i].transform.LookAt(target);
                currentlyPottedBalls[i].GetComponent<BallCollisionHandlerLuckyShot>().StartTrayMovementForPottedBall(target);
                pottedBallcounter++;
                yield return new WaitForSeconds(0.5f);
            }
            currentlyPottedBalls.Clear();
        }



        private void UpdateBallTypeForPlayers(Transform ballTransform)
        {
            bool isStripeBallPresent = false;
            bool isSolidBallPresent = false;
            for (int i = 0; i < currentlyPottedBalls.Count; i++)
            {
                if (currentlyPottedBalls[i].name.Contains("Solid"))
                {
                    isSolidBallPresent = true;
                }
                if (currentlyPottedBalls[i].name.Contains("Stripe"))
                {
                    isStripeBallPresent = true;
                }
            }
            if (isStripeBallPresent && isSolidBallPresent)
            {
                return;
            }
            if (ballTransform.name.Contains("Solid"))
            {
                if (gameManager.CurrentPlayer == gameManager.FirstPlayer)
                {
                    gameManager.CurrentPlayer.ballGroupType = "Solid";
                    gameManager.CurrentPlayer.remainingBalls = solidBallList;
                    gameManager.SecondPlayer.ballGroupType = "Stripe";
                    gameManager.SecondPlayer.remainingBalls = stripeBallList;
                    gameManager.hudManager.UpdatePlayer1BallType("Solid");
                    gameManager.hudManager.UpdatePlayer2BallType("Stripe");
                }
                else
                {
                    gameManager.CurrentPlayer.ballGroupType = "Solid";
                    gameManager.CurrentPlayer.remainingBalls = solidBallList;
                    gameManager.FirstPlayer.ballGroupType = "Stripe";
                    gameManager.FirstPlayer.remainingBalls = stripeBallList;
                    gameManager.hudManager.UpdatePlayer1BallType("Stripe");
                    gameManager.hudManager.UpdatePlayer2BallType("Solid");
                }
            }
            else if (ballTransform.name.Contains("Stripe"))
            {
                if (gameManager.CurrentPlayer == gameManager.FirstPlayer)
                {
                    gameManager.CurrentPlayer.ballGroupType = "Stripe";
                    gameManager.CurrentPlayer.remainingBalls = stripeBallList;
                    gameManager.SecondPlayer.ballGroupType = "Solid";
                    gameManager.SecondPlayer.remainingBalls = solidBallList;
                    gameManager.hudManager.UpdatePlayer1BallType("Stripe");
                    gameManager.hudManager.UpdatePlayer2BallType("Solid");
                }
                else
                {
                    gameManager.CurrentPlayer.ballGroupType = "Stripe";
                    gameManager.CurrentPlayer.remainingBalls = stripeBallList;
                    gameManager.FirstPlayer.ballGroupType = "Solid";
                    gameManager.FirstPlayer.remainingBalls = solidBallList;
                    gameManager.hudManager.UpdatePlayer1BallType("Solid");
                    gameManager.hudManager.UpdatePlayer2BallType("Stripe");
                }
            }
            gameManager.hudManager.DisplayBallGroupTypeDetails(gameManager.CurrentPlayer.ballGroupType);
        }
        private void UpdateBallTypeForPlayers()
        {
            bool isStripeBallPresent = false;
            bool isSolidBallPresent = false;
            for (int i = 0; i < currentlyPottedBalls.Count; i++)
            {
                if (currentlyPottedBalls[i].name.Contains("Solid"))
                {
                    for (int j = 0; j < solidBallList.Count; j++)
                    {
                        if (currentlyPottedBalls[i].transform == solidBallList[j].transform)
                        {
                            solidBallList.RemoveAt(j);
                        }
                    }
                    isSolidBallPresent = true;
                }
                if (currentlyPottedBalls[i].name.Contains("Stripe"))
                {
                    for (int j = 0; j < stripeBallList.Count; j++)
                    {
                        if (currentlyPottedBalls[i].transform == stripeBallList[j].transform)
                        {
                            stripeBallList.RemoveAt(j);
                        }
                    }
                    isStripeBallPresent = true;
                }
            }
            if (isStripeBallPresent && isSolidBallPresent)
            {
                return;
            }
            else if (isSolidBallPresent)
            {
                if (gameManager.CurrentPlayer == gameManager.FirstPlayer)
                {
                    gameManager.CurrentPlayer.ballGroupType = "Solid";
                    gameManager.CurrentPlayer.remainingBalls = solidBallList;
                    gameManager.SecondPlayer.ballGroupType = "Stripe";
                    gameManager.SecondPlayer.remainingBalls = stripeBallList;
                    gameManager.hudManager.UpdatePlayer1BallType("Solid");
                    gameManager.hudManager.UpdatePlayer2BallType("Stripe");
                }
                else
                {
                    gameManager.CurrentPlayer.ballGroupType = "Solid";
                    gameManager.CurrentPlayer.remainingBalls = solidBallList;
                    gameManager.FirstPlayer.ballGroupType = "Stripe";
                    gameManager.FirstPlayer.remainingBalls = stripeBallList;
                    gameManager.hudManager.UpdatePlayer1BallType("Stripe");
                    gameManager.hudManager.UpdatePlayer2BallType("Solid");
                }
            }
            else if (isStripeBallPresent)
            {
                if (gameManager.CurrentPlayer == gameManager.FirstPlayer)
                {
                    gameManager.CurrentPlayer.ballGroupType = "Stripe";
                    gameManager.CurrentPlayer.remainingBalls = stripeBallList;
                    gameManager.SecondPlayer.ballGroupType = "Solid";
                    gameManager.SecondPlayer.remainingBalls = solidBallList;
                    gameManager.hudManager.UpdatePlayer1BallType("Stripe");
                    gameManager.hudManager.UpdatePlayer2BallType("Solid");
                }
                else
                {
                    gameManager.CurrentPlayer.ballGroupType = "Stripe";
                    gameManager.CurrentPlayer.remainingBalls = stripeBallList;
                    gameManager.FirstPlayer.ballGroupType = "Solid";
                    gameManager.FirstPlayer.remainingBalls = solidBallList;
                    gameManager.hudManager.UpdatePlayer1BallType("Solid");
                    gameManager.hudManager.UpdatePlayer2BallType("Stripe");
                }
            }
            if (gameManager.CurrentPlayer.ballGroupType != "")
            {
                gameManager.hudManager.DisplayBallGroupTypeDetails(gameManager.CurrentPlayer.ballGroupType);
            }
            for (int i = 0; i < currentlyPottedBalls.Count; i++)
            {
                UpdateRemainingBallListForPlayers(currentlyPottedBalls[i].transform);
            }
        }

        private void UpdateRemainingBallListForPlayers(Transform ballTransform)
        {
            for (int i = 0; i < gameManager.FirstPlayer.remainingBalls.Count; i++)
            {
                if (ballTransform == gameManager.FirstPlayer.remainingBalls[i].transform)
                {
                    gameManager.FirstPlayer.remainingBalls.RemoveAt(i);
                    if (gameManager.FirstPlayer.remainingBalls.Count == 0)
                    {
                        gameManager.FirstPlayer.remainingBalls.Add(eightBallObject);
                    }
                }
            }

            for (int i = 0; i < gameManager.SecondPlayer.remainingBalls.Count; i++)
            {
                if (ballTransform == gameManager.SecondPlayer.remainingBalls[i].transform)
                {
                    gameManager.SecondPlayer.remainingBalls.RemoveAt(i);
                    if (gameManager.SecondPlayer.remainingBalls.Count == 0)
                    {
                        gameManager.SecondPlayer.remainingBalls.Add(eightBallObject);
                    }
                }
            }
            gameManager.hudManager.UpdatePlayer1BallListDisplay();
            gameManager.hudManager.UpdatePlayer2BallListDisplay();
        }

        private bool CheckForGameOver()
        {
            if (gameManager.CurrentPlayer.remainingBalls.Count == 1 && gameManager.CurrentPlayer.remainingBalls[0].name.Contains("8Ball"))
            {
                if (currentlyPottedBalls.Contains(eightBallObject) && currentlyPottedBalls.Contains(gameManager.cueBall))
                {
                    if (gameManager.CurrentPlayer == gameManager.FirstPlayer)
                    {
                        gameManager.Winner = gameManager.SecondPlayer;
                    }
                    else
                    {
                        gameManager.Winner = gameManager.FirstPlayer;
                    }
                    return true;
                }
                else if (currentlyPottedBalls.Contains(eightBallObject))
                {
                    gameManager.Winner = gameManager.CurrentPlayer;
                    return true;
                }
            }
            else if (currentlyPottedBalls.Contains(eightBallObject))
            {
                if (gameManager.CurrentPlayer == gameManager.FirstPlayer)
                {
                    gameManager.Winner = gameManager.SecondPlayer;
                }
                else
                {
                    gameManager.Winner = gameManager.FirstPlayer;
                }
                return true;
            }
            return false;
        }

        public void changePottedBallsPosition(float posX)
        {
            for (int i = 0; i < pottedBalls.Count; i++)
            {
                pottedBalls[i].transform.position = new Vector3(posX, pottedBalls[i].transform.position.y, pottedBalls[i].transform.position.z);
            }
        }
    }
}

