using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
namespace RevolutionGames.Game
{
    public class QuickFireModeManager : MonoBehaviour
    {
        public BoardManagerQuickFireMode boardManagerQuickFire;
        public Text timerText, scoreText, NumberOfRackText, multiplierText;
        public GameObject gameOverScreenPopup;
        // Game over details text
        public Text numberOfShotsText, numberOfBallPotedText, accuracyText, numberOfLongestShotText, aimTimeText, totalTimeText,
            numberOfRackReachedText, scoreTextGameOver, highScoreText;

        //potted balls
        public Image[] pottedBalls;

        private float playerTimeSeconds = 120f;
        private float totalPlayingTimeSeconds = 0f;
        private int score, highScore, consecutivePotsCount = 0;
        private float multiplier;
        private string timeUpdate;
        private bool gameOverDetailsFlag;
        // Start is called before the first frame update
        public void Start()
        {
            score = 0;
            multiplier = 1.0f;
            highScore = PlayerPrefs.GetInt("quickfirehighscore");
            totalPlayingTimeSeconds = playerTimeSeconds;  //total time player playing
            gameOverDetailsFlag = true;


        }
        public void Update()
        {
            if (playerTimeSeconds > 0)
            {
                playerTimeSeconds -= Time.deltaTime;
                FormatTime(playerTimeSeconds);
            }
        }
        public string FormatTime(float time)
        {
            //int minutes = (int)time / 60000;
            //int seconds = (int)time / 1000 - 60 * minutes;

            int minutes = (int)time / 60;
            int seconds = (int)time - 60 * minutes;
            //print("minutes"+ minutes);
            //print("seconds" + seconds);
            //int milliseconds = (int)time - minutes * 60000 - 1000 * seconds;
            timeUpdate = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timeUpdate;
            if (playerTimeSeconds <= 0)
            {
                if (gameOverDetailsFlag == true)
                {
                    GameOverDetailsUpdate();
                }

            }
            //print("secons dec" + startTimeSeconds);
            //print("time" + timeUpdate);
            return string.Format("{0:00}:{1:00}", minutes, seconds);


        }

        public void GameOverDetailsUpdate()
        {

            int minutes = (int)totalPlayingTimeSeconds / 60;
            int seconds = (int)totalPlayingTimeSeconds - 60 * minutes;
            string totalMinutes = string.Format("{0:0}", minutes);
            string totalSeconds = string.Format("{0:00}", seconds);
            gameOverScreenPopup.SetActive(true);
            numberOfRackReachedText.text = NumberOfRackText.text;
            scoreTextGameOver.text = scoreText.text;
            totalTimeText.text = totalMinutes + "m" + " " + totalSeconds + "s";
            if (highScore < score)
            {
                highScore = score;
                PlayerPrefs.SetInt("quickfirehighscore", highScore);
            }
            highScoreText.text = highScore.ToString();
            gameOverDetailsFlag = false;

        }
        public void BallPottedFunction()
        {
            totalPlayingTimeSeconds += 10f;
            playerTimeSeconds += 10f;
            score += 100;
            scoreText.text = score.ToString();
        }
        public void FiveConsecutivePottedFunction()
        {
                multiplier += 0.5f;
                multiplierText.text = multiplier.ToString() + "x";

        }
        public void CueBallPottedFunction()
        {
            // print("cue ball");
            playerTimeSeconds -= 30f;
            if (playerTimeSeconds <= 0)
            {
                GameOverDetailsUpdate();
                playerTimeSeconds = 0;
            }
            if (multiplier > 1.0f)
            {
                multiplier -= 0.5f;
                multiplierText.text = multiplier.ToString() + "x";
            }

        }
        public void UpdatePlayerPottedBallImages()
        {
            for (int i = 0; i < boardManagerQuickFire.pottedBalls.Count; i++)
            { 
                //pottedBalls[i].sprite= boardManagerQuickFire.pottedBalls[i]
            }
        }

        public void MainMenuButtonClicked()
        {
            SceneManager.LoadScene("MainMenuScene");
        }
        public void PlayAgainButtonClicked()
        {
            SceneManager.LoadScene("QuickFireScene");
            //GameData.Instance().ToScreenName = "home";
        }
    }
}
