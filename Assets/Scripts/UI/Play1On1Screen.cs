using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;
using City = RevolutionGames.APIData.City;

namespace RevolutionGames.UI
{
    public class Play1On1Screen : MonoBehaviour
    {
        public UIManager uiManager;
        public Image ProgressBarImage;
        public Text NameText, IDText, StarText, CoinsText;

        public GameObject CitySelectionContent, CityButton, MainPanel, PlayersPanel;

        public GameObject noGamesPopup;

        private GameObject newButton;

        public void OnBackButtonClicked()
        {
            uiManager.play1On1ScreenParent.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }

        // Start is called before the first frame update
        void Start()
        {
            InstantiateCitySelection(null);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            noGamesPopup.SetActive(true);
            //MainPanel.SetActive(true);
            //PlayersPanel.SetActive(false);
            //UpdatePlayerData();
            //UpdatePlayerCoinsData();

        }

        public void InstantiateCitySelection(List<City> cities)
        {
            for (int i = 0; i < CitySelectionContent.transform.childCount; i++) // To remove any existing childs
            {
                Destroy(CitySelectionContent.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < 10; i++)
            {
                newButton = Instantiate(CityButton) as GameObject;
                newButton.transform.SetParent(CitySelectionContent.transform, true);
                newButton.GetComponent<Button>().onClick.AddListener(() => OnCityButtonClicked());
            }
        }

        public void OnAvatarButtonClicked()
        {
            //uIManager.SubScreenNavigation = UIManager.SubScreens.ProfileDetailsPanel;
           // uIManager.ShowScreen(UIManager.Screens.Play1On1Screen, UIManager.Screens.ProfileScreen);
        }

        public void OnProfileButtonClicked()
        {
           // uIManager.SubScreenNavigation = UIManager.SubScreens.ProgressPanel;
           // uIManager.ShowScreen(UIManager.Screens.Play1On1Screen, UIManager.Screens.ProfileScreen);
        }

        public void OnCoinsButtonClicked()
        {
            //uIManager.ShowScreen(this.gameObject, UIManager.Screens.ShopScreenPanel);
        }

        public void OnGiftsButtonClicked()
        {

        }

        public void OnPlayWithFriendsButtonClicked()
        {
           /// uIManager.ShowScreen(UIManager.Screens.Play1On1Screen, UIManager.Screens.PlayWithFriendsScreen);
        }

        //Other options
        public void OnCuesButtonClicked()
        {

        }

        public void OnFreeRewardsButtonClicked()
        {

        }

        public void OnRankingsButtonClicked()
        {

        }

        public void OnShopButtonClicked()
        {

        }

        public void OnPromotionsButtonClicked()
        {

        }

        public void OnBoxSlotButtonClicked(Button btn)
        {

        }

        public void OnCityButtonClicked()
        {
            MainPanel.SetActive(false);
            PlayersPanel.SetActive(true);
        }

        //public void OnBackButtonClicked()
        //{
            //if (MainPanel.activeSelf)
               // uIManager.ShowScreen(UIManager.Screens.Play1On1Screen, UIManager.Screens.HomeScreen);
            //else
                //OnEnable();

        //}

        public void OnPopupCloseButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }
    }
}
