using RevolutionGames.Data;
using RevolutionGames.Hud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolutionGames.Game
{
    public class Player
    {
        public string name = "";
        public string ballGroupType = "";
        public List<GameObject> remainingBalls = new List<GameObject>();
        public int timer = 0;
    }

    public class GameManager : MonoBehaviour
    {
        public GameObject cueBall;
        public GameObject guideLines;
        public GameObject guideLine, circleLine, ballReflectLine, cueBallReflectLine;
        public GameObject circleRestrictedImage, circleImage, lineImage, ballLineImage, cueBallLineImage;
        public GameObject cueBallInHandCircle, handImage, cueStickParent, headStringArea, floorArea, headStringAreaVisible;
        public GameObject[] balls;
        public CueStickManager cueStickManager;
        public CueBallManager cueBallManager;
        public CueBallCollisionHandler cueBallCollisionHandler;
        public HudManager hudManager;
        public GameRules gameRules;
        public BoardManager boardManager;
        public Transform centerObject;
        public Transform trayParentObject;
        public SpriteRenderer tray;
        public Transform trayStartPosition, trayEndPosition;

        public enum GameState {isGameOn, isGamePaused, isReadyForStrike, isItResetCueballOn, isItCueballBreak, isGameOver, isStrikeDone,
            isItCueballBreakDone};

        private GameState currentGameState;
        private Player firstPlayer, secondPlayer, currentPlayer, winner;
        private Vector3[] ballPositions;
        private Quaternion ballRotation;
        private List<int> ballNumbers;
        private int leftCornerPos = 0, rightCornerPos = 8, eighBallPos = 7;
        private int range1 = 0, range2 = 0, range = 0;
        private int[] ballInPos;
        private int maxTimePerShot = 30;
        private int initialMaxTimePerShot = 30;

        // Start is called before the first frame update
        void Start()
        {
            ballPositions = new Vector3[balls.Length];
            ballRotation = new Quaternion();
            ballInPos = new int[balls.Length];
            ballRotation = balls[eighBallPos].transform.rotation;
            for (int i = 0; i < balls.Length; i++)
            {
                ballPositions[i] = balls[i].transform.position;
            }
            InitializeGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentPlayer.timer == 0)
            {
                SwitchPlayerTurn();
                if (CurrentGameState != GameState.isItCueballBreak && CurrentGameState != GameState.isItCueballBreakDone)
                {
                    //SetCueBallInHand();
                    CurrentGameState = GameState.isReadyForStrike;
                }
                else
                {
                    cueBallManager.ShowHandImage(true);
                }
            }
        }

        public GameState CurrentGameState
        {
            get => currentGameState; set => currentGameState = value;
        }
        public Player CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public Player FirstPlayer { get => firstPlayer; set => firstPlayer = value; }
        public Player SecondPlayer { get => secondPlayer; set => secondPlayer = value; }
        public Player Winner { get => winner; set => winner = value; }
        public int[] BallInPos { get => ballInPos; set => ballInPos = value; }

        public void InitializeGame()
        {
            //DataManager.Instance().SpawnSurpriseBox();
            //Player details
            winner = null;
            CurrentGameState = GameState.isGameOn;
            firstPlayer = new Player();
            secondPlayer = new Player();
            firstPlayer.name = "Player1";
            GameData.Instance().PlayerName = firstPlayer.name;
            firstPlayer.timer = maxTimePerShot;
            secondPlayer.name = "Player2";
            secondPlayer.timer = maxTimePerShot;

            var random = new System.Random();
            //initialze ball numbers to randomize ball position and angles during breakshot
            
            ballNumbers = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };


            //Determine the first player using randomize
            range = Random.Range(1, 3);
            if (range == 1)
            {
                currentPlayer = firstPlayer;
            }
            else
            {
                currentPlayer = secondPlayer;
            }

            //Set positions and angles of balls for break shot
            range = Random.Range(0, 2); //first randomize solid or stripe to be placed in left corner 0 -solid in left corner, 1 -stripe
            if (range == 0)
            {
                range1 = Random.Range(0, 7); //setting a solid ball at corner
                range2 = Random.Range(8, 15);
                //print(range);
            }
            else
            {
                range1 = Random.Range(8, 15); //setting a solid ball at corner
                range2 = Random.Range(0, 7); //setting a stripe ball at another corner
            }

            //setting a ball at left corner
            balls[range1].transform.position = ballPositions[leftCornerPos];
            balls[range1].transform.rotation = ballRotation;
            ballInPos[leftCornerPos] = range1;
            ballNumbers.Remove(range1);

            //setting a ball at right corner
            balls[range2].transform.position = ballPositions[rightCornerPos];
            balls[range2].transform.rotation = ballRotation;
            ballInPos[rightCornerPos] = range2;
            ballNumbers.Remove(range2);

            //setting 8 ball position
            balls[eighBallPos].transform.position = ballPositions[eighBallPos];
            balls[eighBallPos].transform.rotation = ballRotation;
            ballInPos[eighBallPos] = eighBallPos;
            ballNumbers.Remove(eighBallPos);
            
            //other balls positions
            for (int i = 0; i < balls.Length; i++)
            {
                if (i == leftCornerPos || i == rightCornerPos || i == eighBallPos)
                    continue;
                if (ballNumbers.Count == 0)
                    break;
                range = random.Next(ballNumbers.Count);
                balls[ballNumbers[range]].transform.position = ballPositions[i];
                balls[ballNumbers[range]].transform.rotation = ballRotation;
                ballInPos[i] = ballNumbers[range];
                ballNumbers.RemoveAt(range);
            }
            handImage.SetActive(true);

            CurrentGameState = GameState.isItCueballBreak;
            hudManager.DisplayPlayerName(firstPlayer.name, 1);
            hudManager.DisplayPlayerName(secondPlayer.name, 2);
            hudManager.DisplayTimer(currentPlayer);
            hudManager.DisplayBreakShotDetails(currentPlayer.name);
        }

        public void Reset8Ball()
        {
            balls[eighBallPos].transform.position = ballPositions[eighBallPos];
            balls[eighBallPos].transform.rotation = ballRotation;
        }

        //return true if passed ball is player's group or if player doesnt have any group assigned
        public bool CheckGroupType(string ballName) 
        {
            if(ballName.Contains("8Ball"))
            {
                if(currentPlayer.remainingBalls.Count == 1 && currentPlayer.remainingBalls[0].name.Contains("8Ball"))
                {
                    return true;
                }
                return false;
            }
            if (currentPlayer.ballGroupType == "" || ballName.Contains(currentPlayer.ballGroupType))
            {
                return true;
            }
            return false;
        }

        public void SwitchPlayerTurn()
        {
            if (currentPlayer == firstPlayer)
            {
                currentPlayer = secondPlayer;
            }
            else
            {
                currentPlayer = firstPlayer;
            }
            currentPlayer.timer = maxTimePerShot;
            cueStickManager.ResetCueStick();
            hudManager.DisplayTimer(currentPlayer);
        }

        public void RestartTimer()
        {
            currentPlayer.timer = maxTimePerShot;
            hudManager.DisplayTimer(currentPlayer);
        }

        //Additional 1 sec
        public void UpdateMaxTimePerShot(int time)
        {
            maxTimePerShot = initialMaxTimePerShot + (time * 1);
            hudManager.UpdateMaxTimePerShot(maxTimePerShot);
        }

        public void SetCueBallInHand()
        {
            cueBallManager.ShowHandImage(true);
            CurrentGameState = GameState.isItResetCueballOn;
            hudManager.DisplayCueBallInHandMessage(currentPlayer.name);
        }

        public void GameOver()
        {
            hudManager.DisplayGameOverDetails(winner);
            if(winner.name == GameData.Instance().PlayerName)
            {
                DataManager.Instance().SpawnSurpriseBox();
            }
        }

        //public void SetCueSensitivity(int flag)
        //{
        //    cueStickManager.SetCueSensitivity(flag);
        //}

        //public void DisableGuidelineInLocal(int flag)
        //{
        //    cueStickManager.DisableGuidelineInLocal(flag);
        //}
    }

}