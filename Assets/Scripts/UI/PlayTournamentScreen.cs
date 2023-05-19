using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;
namespace RevolutionGames.UI
{
    public class PlayTournamentScreen : MonoBehaviour
    {
        public UIManager uiManager;
        public Text quarterFinal1StartingText;
        public Image quarterFinal1Player1Image;
        public Text quaterFinal1Player1LevelText;
        public Image quarterFinal1Player2Image;
        public Text quaterFinal1Player2LevelText;
        public Text quarterFinal2StartingText;
        public Image quarterFinal2Player1Image;
        public Text quaterFinal2Player1LevelText;
        public Image quarterFinal2Player2Image;
        public Text quaterFinal2Player2LevelText;
        public Text quarterFinal3StartingText;
        public Image quarterFinal3Player1Image;
        public Text quaterFinal3Player1LevelText;
        public Image quarterFinal3Player2Image;
        public Text quaterFinal3Player2LevelText;
        public Text quarterFinal4StartingText;
        public Image quarterFinal4Player1Image;
        public Text quaterFinal4Player1LevelText;
        public Image quarterFinal4Player2Image;
        public Text quaterFinal4Player2LevelText;
        public Text semiFinal1Text;
        public Image semiFinal1Player1Image;
        public Text semiFinal1Player1InfoText;
        public Image semiFinal1Player2Image;
        public Text semiFinal1Player2InfoText;
        public Text semiFinal2Text;
        public Image semiFinal2Player1Image;
        public Text semiFinal2Player1InfoText;
        public Image semiFinal2Player2Image;
        public Text semiFinal2Player2InfoText;
        public Text finalText;
        public Image finalPlayer1Image;
        public Text finalPlayer1InfoText;
        public Image finalPlayer2Image;
        public Text finalPlayer2InfoText;
        public Text prizeText;

        public GameObject noGamesPopup;

        public void Start()
        {
            quarterFinal1StartingText.text = "Start";
            quarterFinal2StartingText.text = "Wait";
            quarterFinal3StartingText.text = "Start";
            quarterFinal4StartingText.text = "Starting.";
            semiFinal1Player1InfoText.text = "AAA";
            semiFinal1Player2InfoText.text = "BBB";
            semiFinal2Player1InfoText.text = "CCC";
            semiFinal2Player2InfoText.text = "DDD";
            finalPlayer1InfoText.text = "EEE";
            finalPlayer2InfoText.text = "FFF";

        }

        private void OnEnable()
        {
            noGamesPopup.SetActive(true);
        }

        public void OnBackButtonClicked()
        {
            uiManager.playTournamentScreenParent.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }
    }
}
