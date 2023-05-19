using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using RevolutionGames.API;
using RevolutionGames.Data;

namespace RevolutionGames.UI
{
    public class UIManager : MonoBehaviour
    {
        public APIManager apiManager;
        public DataManager dataManager;

        // Prasanth

        public GameObject loginScreenParent;
        public GameObject homeScreenParent;
        public GameObject shopScreenParent;
        public GameObject leaderBoardScreenParent;
        public GameObject surpriseBoxScreenParent;
        public GameObject spinWheelScreenParent;
        public GameObject scratchScreenParent;
        public GameObject playLocalScreenParent;
        public GameObject playTournamentScreenParent;
        public GameObject play1On1ScreenParent;
        public GameObject playWithFriendsScreenParent;
        public GameObject specialEventScreenParent;
        public GameObject freeRewardsScreenParent;
        public GameObject giftsScreenParent;
        public GameObject profileScreenParent;
        public GameObject settingsScreenParent;
        public GameObject rematchGameScreenParent;
        public GameObject LuckyShotScreenPArent; 

        private HomeScreen homeScreen;
        private ShopScreen shopScreen;
        private GiftsScreen giftScreen;
        private LeaderBoardScreen leaderBoardScreen;
        private ProfileScreen profileScreen;

        private CueStickDataManager cueStickDataManager;

        private void Start()
        {
            dataManager = DataManager.Instance();
            cueStickDataManager = dataManager.GetComponent<CueStickDataManager>();
            homeScreen=homeScreenParent.transform.GetComponent<HomeScreen>();
            giftScreen = giftsScreenParent.transform.GetComponent<GiftsScreen>();
            shopScreen = shopScreenParent.transform.GetComponent<ShopScreen>();
            leaderBoardScreen = leaderBoardScreenParent.transform.GetComponent<LeaderBoardScreen>();
            profileScreen = profileScreenParent.transform.GetComponent<ProfileScreen>();

            cueStickDataManager.TapToAimFlag = PlayerPrefs.GetInt("tap to aim");
            cueStickDataManager.AimingWheelFlag = PlayerPrefs.GetInt("aiming wheel");
            cueStickDataManager.CueSensitivity = PlayerPrefs.GetInt("cue sensitivity");
            cueStickDataManager.DisableGuidelineInLocalFlag = PlayerPrefs.GetInt("local guideline");
            cueStickDataManager.PowerBarLocationFlag = PlayerPrefs.GetInt("powerbar location");
            cueStickDataManager.PowerBarOrientationFlag = PlayerPrefs.GetInt("powerbar orientation");
        }

        private void OnEnable()
        {
            if(GameData.Instance()!=null && GameData.Instance().ToScreenName == "local")
            {
                loginScreenParent.SetActive(false);
                playLocalScreenParent.SetActive(true);
            }
            if (GameData.Instance() != null && GameData.Instance().ToScreenName == "home")
            {
                loginScreenParent.SetActive(false);
                homeScreenParent.SetActive(true);
            }
            if (GameData.Instance() != null && GameData.Instance().ToScreenName == "login")
            {
                loginScreenParent.SetActive(true);
                homeScreenParent.SetActive(false);
            }
            if (GameData.Instance() != null && GameData.Instance().ToScreenName == "LuckyShot")
            {
                loginScreenParent.SetActive(false);
                LuckyShotScreenPArent.SetActive(false);
                homeScreenParent.SetActive(true);
            }
        }

        public void UpdateCoinData(long count)
        {
            //homeScreenParent.transform.GetComponent<HomeScreen>();
            homeScreen.CoinsUpdate(count);
            shopScreen.coinsUpdate();
            giftScreen.coinsUpdate(count);
        }

        public void SubtractCoinData(string coinCount)
        {
            int count = int.Parse(coinCount);
            homeScreen.CoinsSubtract(count);
            shopScreen.SubtractCoinsData(count);
            giftScreen.SubtractCoinsData(count);

        }
        public void ProfilePictureUpdate(Sprite avatar)
        {
            homeScreen.ProfilePictureUpdate(avatar);
            profileScreen.ProfilePictureUpdate(avatar);
            leaderBoardScreen.ProfilePictureUpdate(avatar);
        }

        public void UpdateSurpriseCuesList()
        {
            shopScreen.loadSurpriseCuesList();
        }

        public void UpdateCountryCuesList()
        {
            shopScreen.loadCountryCuesList();
        }

        public void UpdateVictoryCuesList()
        {
            shopScreen.loadVictoryCuesList();
        }




        public void surpriseBoxButton()
        {
            
        }
        public void UpdatePlayerDetails()
        {
            
        }











       /* public GameObject LoginScreenParent, HomeScreenParent, ProfileScreenParent, ProfileSummaryScreenParent, Play1On1ScreenParent, 
            PlayWithFriendsScreenParent, PlayLocalScreenParent, SettingsScreenParent;
        public enum Screens
        {
            LoginScreen, HomeScreen, ProfileScreen, ProfileSummaryScreen, Play1On1Screen, PlayWithFriendsScreen, PlayLocalScreen, 
            SettingsScreen
        }

        public enum SubScreens
        {
            ProfileDetailsPanel, ProgressPanel
        }

        private GameObject nextScreenObject, previousScreenObject;
        private Stack<GameObject> previousScreensList;

        private SubScreens subScreenNavigation;

        public SubScreens SubScreenNavigation { get => subScreenNavigation; set => subScreenNavigation = value; }

       

        // Start is called before the first frame update
        void Start()
        {
            //ShowLoginScreen();
        }

        public void Update()
        {
            /*if(noInternet)
            {
                ShowPopUpScreen("Your internet is slow or disconnected.\nTrying to reconnect...");
            }
        }

        private void Awake()
        {


        }

        public void ShowLoginScreen()
        {
            HomeScreenParent.SetActive(true);
            ProfileScreenParent.SetActive(false);
            ProfileSummaryScreenParent.SetActive(false);
            Play1On1ScreenParent.SetActive(false);
            PlayWithFriendsScreenParent.SetActive(false);
            PlayLocalScreenParent.SetActive(false);
            SettingsScreenParent.SetActive(false);
        }

        public void ShowScreen(Screens previousScreen, Screens nextScreen, bool retainPreviousScreen = false)
        {
            switch (previousScreen)
            {
                case Screens.LoginScreen:
                    previousScreenObject = LoginScreenParent;
                    break;

                case Screens.HomeScreen:
                    previousScreenObject = HomeScreenParent;
                    break;

                case Screens.ProfileScreen:
                    previousScreenObject = ProfileScreenParent;
                    break;

                case Screens.Play1On1Screen:
                    previousScreenObject = Play1On1ScreenParent;
                    break;

                case Screens.PlayWithFriendsScreen:
                    previousScreenObject = PlayWithFriendsScreenParent;
                    break;

                case Screens.PlayLocalScreen:
                    previousScreenObject = PlayLocalScreenParent;
                    break;

                case Screens.SettingsScreen:
                    previousScreenObject = SettingsScreenParent;
                    break;

                default:
                    previousScreenObject = HomeScreenParent;
                    break;
            }

            switch (nextScreen)
            {
                case Screens.LoginScreen:
                    nextScreenObject = LoginScreenParent;
                    break;

                case Screens.HomeScreen:
                    nextScreenObject = HomeScreenParent;
                    break;

                case Screens.ProfileScreen:
                    nextScreenObject = ProfileScreenParent;
                    break;

                case Screens.Play1On1Screen:
                    nextScreenObject = Play1On1ScreenParent;
                    break;

                case Screens.PlayWithFriendsScreen:
                    nextScreenObject = PlayWithFriendsScreenParent;
                    break;

                case Screens.PlayLocalScreen:
                    nextScreenObject = PlayLocalScreenParent;
                    break;

                case Screens.SettingsScreen:
                    nextScreenObject = SettingsScreenParent;
                    break;

                default:
                    nextScreenObject = HomeScreenParent;
                    break;
            }
            StartCoroutine(ScreenNavigation(previousScreenObject, nextScreenObject, retainPreviousScreen));
        }

        //Animation Handlers
        IEnumerator ScreenNavigation(GameObject fromObject = null, GameObject toObject = null, bool retainPreviousScreen = false)
        {
            toObject.SetActive(true);
            yield return new WaitForSeconds(0.01f);
            if (!retainPreviousScreen)
                fromObject.SetActive(false);
        }

        /*public void UpdatePlayerData()
        {
            HomeScreenPanel.transform.GetComponent<HomeScreen>().UpdatePlayerData();
            ProfileScreenPanel.transform.GetComponent<ProfileScreen>().UpdatePlayerData();
            Play1On1ScreenPanel.transform.GetComponent<Play1On1Screen>().UpdatePlayerData();
            SettingsScreenPanel.transform.GetComponent<SettingsScreen>().UpdatePlayerData();
            PlayTournamentScreenPanel.transform.GetComponent<PlayTournamentScreen>().UpdatePlayerData();
        }

        public void UpdatePlayerCoinsData()
        {
            HomeScreenPanel.transform.GetComponent<HomeScreen>().UpdatePlayerCoinsData();
            ProfileScreenPanel.transform.GetComponent<ProfileScreen>().UpdatePlayerCoinsData();
            ShopScreenPanel.transform.GetComponent<ShopScreen>().UpdatePlayerCoinsData();
            RankingsScreenPanel.transform.GetComponent<RankingsScreen>().UpdatePlayerCoinsData();
            CategoryScreenPanel.transform.GetComponent<CategoryScreen>().UpdatePlayerCoinsData();
            Play1On1ScreenPanel.transform.GetComponent<Play1On1Screen>().UpdatePlayerCoinsData();
            PlayTournamentScreenPanel.transform.GetComponent<PlayTournamentScreen>().UpdatePlayerCoinsData();
            SettingsScreenPanel.transform.GetComponent<SettingsScreen>().UpdatePlayerCoinsData();
            PlayFriendsScreenPanel.transform.GetComponent<PlayFriendsScreen>().UpdatePlayerCoinsData();

        }*/

        //  Prasanth

        
        }
    
   
}

