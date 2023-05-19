using RevolutionGames.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RevolutionGames.UI
{
    public class SettingsScreen : MonoBehaviour
    {
        public UIManager uIManager;

        public GameObject RestoreImage, SoundOffButton, TableStickersOffButton, VibratingOffButton, TapToAimOnButton, AimingWheelOnButton,
            LocalModeGuidelineOnButton, InGameChatOffButton, ShowOnlineStatusOffButton, FriendsOnlineNotificationsOffButton,
            VisibleToFoFOffButton, loginEmailButton,loginGooglePlayButton,loginFacebookButton,loginAppleButton,loginGoogleField,loginAppleField;

        public GameObject CueSensitivitySlowSelectedImage, CueSensitivityNormalSelectedImage, CueSensitivityFastSelectedImage,
            PowerBarLocationLeftSelectedImage, PowerBarLocationRightSelectedImage,
            PowerBarOrientationHorizontalSelectedImage, PowerBarOrientationVerticalSelectedImage,
            AllowAddFriendEveryoneSelectedImage, AllowAddFriendNoneSelectedImage,
            AllowChallengesEveryoneSelectedImage, AllowChallengesNoneSelectedImage;

        public Text VersionText;
        public GameObject powerBar;
        public Transform settingsContent;

        private int soundFlag,tabbleStickersFlag,cueSensitivityFlag,vibratingFlag,tapToAimFlag,aimingWheelFlag,localGuidelineFlag,powerBarLocationFlag,
                     powerBarOrientationFlag,gameChatFlag,addFriendFlag,allowChallengesFlag,onlineStatusFlag,notificationFriendsFlag,visibleFriendsFlag;
        public GameObject termsAndConditionScreen;
        public GameObject privacyPolicyScreen;
        public GameObject chatOrderScreen;
        public GameObject chatOrderPrefab;
        public Transform chatOrderContent;

        private CueStickDataManager cueStickDataManager;
        private Vector3 settingsInitialPos;
       
        List<GameObject> chatOrderList = new List<GameObject>();
        List<string> chatText = new List<string>();


        // Start is called before the first frame update
        void Start()
        {

#if UNITY_IOS
            Debug.Log("Ios platform");
            loginAppleField.SetActive(true);
            loginGoogleField.SetActive(false);

#else
            loginAppleField.gameObject.SetActive(false);
            loginGoogleField.SetActive(true);
            Debug.Log("Any other platform");

#endif

            cueStickDataManager = DataManager.Instance().GetComponent<CueStickDataManager>();
            settingsInitialPos = settingsContent.position;

            chatText.Add("Good luck");
            chatText.Add("Nice shot");
            chatText.Add("well played");
            chatText.Add("You're good!");
            chatText.Add("Thanks");


            if (Application.platform == RuntimePlatform.Android)
                RestoreImage.SetActive(false);
            soundFlag = PlayerPrefs.GetInt("sound");
            tabbleStickersFlag = PlayerPrefs.GetInt("table stickers");
            cueSensitivityFlag = cueStickDataManager.CueSensitivity;
            vibratingFlag = PlayerPrefs.GetInt("vibrating");
            tapToAimFlag = cueStickDataManager.TapToAimFlag;
            aimingWheelFlag = cueStickDataManager.AimingWheelFlag;
            localGuidelineFlag = cueStickDataManager.DisableGuidelineInLocalFlag;
            powerBarLocationFlag = cueStickDataManager.PowerBarLocationFlag;
            powerBarOrientationFlag = cueStickDataManager.PowerBarOrientationFlag;
            gameChatFlag = PlayerPrefs.GetInt("game chat");
            addFriendFlag = PlayerPrefs.GetInt("add friend");
            allowChallengesFlag = PlayerPrefs.GetInt("allow challenge");
            onlineStatusFlag = PlayerPrefs.GetInt("online status");
            notificationFriendsFlag = PlayerPrefs.GetInt("friends online notification");
            visibleFriendsFlag = PlayerPrefs.GetInt("visible friends");

            if (soundFlag == 0)
            {
                SoundOffButton.SetActive(true);
            }
            else
            {
                SoundOffButton.SetActive(false);
            }
            if (tabbleStickersFlag == 0)
            {
                TableStickersOffButton.SetActive(true);
            }
            else 
            {
                TableStickersOffButton.SetActive(false);
            }

            if (cueSensitivityFlag == 0)
            {
                CueSensitivitySlowSelectedImage.SetActive(false);
                CueSensitivityNormalSelectedImage.SetActive(true);
                CueSensitivityFastSelectedImage.SetActive(true);
            }
            else if (cueSensitivityFlag == 1)
            {
                CueSensitivitySlowSelectedImage.SetActive(true);
                CueSensitivityNormalSelectedImage.SetActive(false);
                CueSensitivityFastSelectedImage.SetActive(true);
            }
            else
            {
                CueSensitivitySlowSelectedImage.SetActive(true);
                CueSensitivityNormalSelectedImage.SetActive(true);
                CueSensitivityFastSelectedImage.SetActive(false);
            }
            if (vibratingFlag == 0)
            {
                VibratingOffButton.SetActive(true);
            }
            else
            {
                VibratingOffButton.SetActive(false);
            }
            if (tapToAimFlag == 0)
            {
                TapToAimOnButton.SetActive(false);
            }
            else
            {
                TapToAimOnButton.SetActive(true);
            }
            if (aimingWheelFlag == 0)
            {
                AimingWheelOnButton.SetActive(false);
            }
            else
            {
                AimingWheelOnButton.SetActive(true);
            }
            if (localGuidelineFlag == 0)
            {
                LocalModeGuidelineOnButton.SetActive(false);
            }
            else
            {
                LocalModeGuidelineOnButton.SetActive(true);
            }
            if (powerBarLocationFlag == 0)
            {
                PowerBarLocationLeftSelectedImage.SetActive(false);
                PowerBarLocationRightSelectedImage.SetActive(true);
            }
            else 
            {
                PowerBarLocationLeftSelectedImage.SetActive(true);
                PowerBarLocationRightSelectedImage.SetActive(false);
            }
            if (powerBarOrientationFlag == 0)
            {
                PowerBarOrientationHorizontalSelectedImage.SetActive(true);
                PowerBarOrientationVerticalSelectedImage.SetActive(false);
            }
            else
            {
                PowerBarOrientationHorizontalSelectedImage.SetActive(false);
                PowerBarOrientationVerticalSelectedImage.SetActive(true);
            }
            if (gameChatFlag == 0)
            {
                InGameChatOffButton.SetActive(true);
            }
            else
            {
                InGameChatOffButton.SetActive(false);
            }
            if (addFriendFlag == 0)
            {
                AllowAddFriendEveryoneSelectedImage.SetActive(false);
                AllowAddFriendNoneSelectedImage.SetActive(true);
            }
            else
            {
                AllowAddFriendEveryoneSelectedImage.SetActive(true);
                AllowAddFriendNoneSelectedImage.SetActive(false);
            }
            if (allowChallengesFlag == 0)
            {
                AllowChallengesEveryoneSelectedImage.SetActive(false);
                AllowChallengesNoneSelectedImage.SetActive(true);
            }
            else 
            {
                AllowChallengesEveryoneSelectedImage.SetActive(true);
                AllowChallengesNoneSelectedImage.SetActive(false);
            }
            if (onlineStatusFlag == 0)
            {
                ShowOnlineStatusOffButton.SetActive(true);
            }
            else
            {
                ShowOnlineStatusOffButton.SetActive(false);
            }
            if (notificationFriendsFlag == 0)
            {
                FriendsOnlineNotificationsOffButton.SetActive(true);
            }
            else
            {
                FriendsOnlineNotificationsOffButton.SetActive(false);
            }
            if (visibleFriendsFlag == 0)
            {
                VisibleToFoFOffButton.SetActive(true);
            }
            else
            {
                VisibleToFoFOffButton.SetActive(false);
            }


        }

        public void OnEnable()
        {
            if (PlayerPrefs.GetInt("email login") == 0)
            {
                loginEmailButton.transform.GetComponent<Button>().interactable = true;
                //loginEmailButton.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                loginEmailButton.transform.GetChild(0).GetComponent<Text>().text = "Login with Email";
            }
            else
            {
                loginEmailButton.transform.GetComponent<Button>().interactable = false;
                // loginEmailButton.transform.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
                loginEmailButton.transform.GetChild(0).GetComponent<Text>().text = "Logged In";
            }
            if (PlayerPrefs.GetInt("google login") == 0)
            {
                loginGooglePlayButton.transform.GetComponent<Button>().interactable = true;
                //loginGooglePlayButton.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                loginGooglePlayButton.transform.GetChild(0).GetComponent<Text>().text = "Google play";
            }
            else
            {
                loginGooglePlayButton.transform.GetComponent<Button>().interactable = false;
                //loginGooglePlayButton.transform.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
                loginGooglePlayButton.transform.GetChild(0).GetComponent<Text>().text = "Logged In";
            }
            if (PlayerPrefs.GetInt("facebook login") == 0)
            {
                loginFacebookButton.transform.GetComponent<Button>().interactable = true;
                loginFacebookButton.transform.GetChild(0).GetComponent<Text>().text = "Login with Facebook";
            }
            else
            {
                loginFacebookButton.transform.GetComponent<Button>().interactable = false;
                loginFacebookButton.transform.GetChild(0).GetComponent<Text>().text = "Logged In";

            }
            if (PlayerPrefs.GetInt("apple login") == 0)
            {
                loginAppleButton.transform.GetComponent<Button>().interactable = true;
                loginAppleButton.transform.GetChild(0).GetComponent<Text>().text = "Sign in with Apple";
            }
            else
            {
                loginAppleButton.transform.GetComponent<Button>().interactable = false;
                loginAppleButton.transform.GetChild(0).GetComponent<Text>().text = "Logged In";
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnLogoutButtonClicked()
        {
            PlayerPrefs.SetInt("email login", 0);
            this.gameObject.SetActive(false);
            uIManager.loginScreenParent.SetActive(true);
        }
        public void LoginGooglePlayButtonClicked()
        {
            PlayerPrefs.SetInt("google login ", 1);
            loginGooglePlayButton.transform.GetComponent<Button>().interactable = false;
            loginGooglePlayButton.transform.GetChild(0).GetComponent<Text>().text = "Logged In";

        }
        public void LoginAppleButtonClicked()
        {
            PlayerPrefs.SetInt("apple login", 1);
            loginAppleButton.transform.GetComponent<Button>().interactable = false;
            loginAppleButton.transform.GetChild(0).GetComponent<Text>().text = "Logged In";
        }

        public void OnMoreGamesButtonClicked()
        {

        }

        public void OnTermsButtonClicked()
        {
            termsAndConditionScreen.SetActive(true);
        }
        public void TermsCloseButtonClicked()
        {
            termsAndConditionScreen.SetActive(false);
        }
        public void OnPrivacyButtonClicked()
        {
            privacyPolicyScreen.SetActive(true);
        }
        public void PrivacyCloseButton()
        {
            privacyPolicyScreen.SetActive(false);
        }
        public void OnTutorialsButtonClicked()
        {

        }

        public void OnRestorePurchasesButtonClicked()
        {

        }

        public void OnLanguageButtonClicked()
        {

        }


        public void OnSoundOnButtonClicked()
        {
            PlayerPrefs.SetInt("sound", 0);
            SoundOffButton.SetActive(true);
        }

        public void OnSoundOffButtonClicked()
        {
            PlayerPrefs.SetInt("sound", 1);
            SoundOffButton.SetActive(false);
        }

        public void OnTableStickersOnButtonClicked()
        {
            PlayerPrefs.SetInt("table stickers", 0);
            TableStickersOffButton.SetActive(true);
        }

        public void OnTableStickersOffButtonClicked()
        {
            PlayerPrefs.SetInt("table stickers", 1);
            TableStickersOffButton.SetActive(false);
        }

        public void OnCueSensitivitySlowButtonClicked()
        {
            PlayerPrefs.SetInt("cue sensitivity", 0);
            CueSensitivitySlowSelectedImage.SetActive(false);
            CueSensitivityNormalSelectedImage.SetActive(true);
            CueSensitivityFastSelectedImage.SetActive(true);
            cueStickDataManager.CueSensitivity = 0;
        }

        public void OnCueSensitivityNormalButtonClicked()
        {
            PlayerPrefs.SetInt("cue sensitivity", 1);
            CueSensitivitySlowSelectedImage.SetActive(true);
            CueSensitivityNormalSelectedImage.SetActive(false);
            CueSensitivityFastSelectedImage.SetActive(true);
            cueStickDataManager.CueSensitivity = 1;
        }

        public void OnCueSensitivityFastButtonClicked()
        {
            PlayerPrefs.SetInt("cue sensitivity", 2);
            CueSensitivitySlowSelectedImage.SetActive(true);
            CueSensitivityNormalSelectedImage.SetActive(true);
            CueSensitivityFastSelectedImage.SetActive(false);
            cueStickDataManager.CueSensitivity = 2;
        }

        public void OnVibratingOnButtonClicked()
        {
            PlayerPrefs.SetInt("vibrating",0);
            VibratingOffButton.SetActive(true);
        }

        public void OnVibratingOffButtonClicked()
        {
            PlayerPrefs.SetInt("vibrating", 1);
            VibratingOffButton.SetActive(false);
        }

        public void OnTapToAimOnButtonClicked()
        {
            PlayerPrefs.SetInt("tap to aim", 0);
            TapToAimOnButton.SetActive(false);
            cueStickDataManager.TapToAimFlag = 0;
        }

        public void OnTapToAimOffButtonClicked()
        {
            PlayerPrefs.SetInt("tap to aim", 1);
            TapToAimOnButton.SetActive(true);
            cueStickDataManager.TapToAimFlag = 1;
        }

        public void OnAimingWheelOnButtonClicked()
        {
            PlayerPrefs.SetInt("aiming wheel", 0);
            AimingWheelOnButton.SetActive(false);
            cueStickDataManager.AimingWheelFlag = 0;
        }

        public void OnAimingWheelOffButtonClicked()
        {
            PlayerPrefs.SetInt("aiming wheel", 1);
            AimingWheelOnButton.SetActive(true);
            cueStickDataManager.AimingWheelFlag = 1;
        }

        public void OnLocalModeGuidelineOnButtonClicked()
        {
            PlayerPrefs.SetInt("local guideline", 0);
            LocalModeGuidelineOnButton.SetActive(false);
            cueStickDataManager.DisableGuidelineInLocalFlag = 0;
        }

        public void OnLocalModeGuidelineOffButtonClicked()
        {
            PlayerPrefs.SetInt("local guideline", 1);
            LocalModeGuidelineOnButton.SetActive(true);
            cueStickDataManager.DisableGuidelineInLocalFlag = 1;
        }

        public void OnPowerBarLocationLeftButtonClicked()
        {
            PlayerPrefs.SetInt("powerbar location",0);
            PowerBarLocationLeftSelectedImage.SetActive(false);
            PowerBarLocationRightSelectedImage.SetActive(true);
            cueStickDataManager.PowerBarLocationFlag = 0;
        }

        public void OnPowerBarLocationRightButtonClicked()
        { 
            PlayerPrefs.SetInt("powerbar location", 1);
            PowerBarLocationLeftSelectedImage.SetActive(true);
            PowerBarLocationRightSelectedImage.SetActive(false);
            cueStickDataManager.PowerBarLocationFlag = 1;
        }

        public void OnPowerBarOrientationHorizontalButtonClicked()
        {
           
            PlayerPrefs.SetInt("powerbar orientation",1);
            PowerBarOrientationHorizontalSelectedImage.SetActive(false);
            PowerBarOrientationVerticalSelectedImage.SetActive(true);
            cueStickDataManager.PowerBarOrientationFlag = 1;
        }

        public void OnPowerBarOrientationVerticalButtonClicked()
        {
       
            PlayerPrefs.SetInt("powerbar orientation",0);
            PowerBarOrientationHorizontalSelectedImage.SetActive(true);
            PowerBarOrientationVerticalSelectedImage.SetActive(false);
            cueStickDataManager.PowerBarOrientationFlag = 0;
        }

        public void OnInGameChatOnButtonClicked()
        {
            PlayerPrefs.SetInt("game chat", 0);
            InGameChatOffButton.SetActive(true);
        }

        public void OnInGameChatOffButtonClicked()
        {
            PlayerPrefs.SetInt("game chat", 1);
            InGameChatOffButton.SetActive(false);
        }

        public void OnChatOrderButtonClicked()
        {
            for (int i = 0; i < chatOrderContent.childCount; i++)
            {
                Destroy(chatOrderContent.transform.GetChild(i).gameObject);
            }
            ChatOrderButtonCallBack(chatText);
            chatOrderScreen.SetActive(true);        
        }
        public void ChatOrderButtonCallBack(List<string> chatText)
        {
    
            for(int i=0;i<chatText.Count;i++)
            {
                int count = i;
                GameObject chatOrder = Instantiate(chatOrderPrefab, chatOrderContent);
                chatOrderList.Add(chatOrder);
                chatOrderList[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = chatText[i].ToString();
                chatOrder.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => ChatOrderHideButton(count));
                chatOrder.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => ChatOrderUnHideButton(count));
                chatOrder.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => ChatOrderMoveButton(count));
               
            }
            
            
        }
        public void OnChatOrderCloseButton()
        {
            chatOrderScreen.SetActive(false);
        }
        public void ChatOrderHideButton(int itemNumber)
        { 
            chatOrderList[itemNumber].transform.GetChild(1).gameObject.SetActive(true);
            chatOrderList[itemNumber].transform.GetChild(2).gameObject.SetActive(false);
        }
        public void ChatOrderUnHideButton(int itemNumber)
        {
            chatOrderList[itemNumber].transform.GetChild(1).gameObject.SetActive(false);
            chatOrderList[itemNumber].transform.GetChild(2).gameObject.SetActive(true);
        }
        public void ChatOrderMoveButton(int itemNumber)
        { 

        }
        public void OnAllowAddFriendEveryoneButtonClicked()
        {
            PlayerPrefs.SetInt("add friend",0);
            AllowAddFriendEveryoneSelectedImage.SetActive(false);
            AllowAddFriendNoneSelectedImage.SetActive(true);
        }

        public void OnAllowAddFriendNoneButtonClicked()
        {
            PlayerPrefs.SetInt("add friend", 1);
            AllowAddFriendEveryoneSelectedImage.SetActive(true);
            AllowAddFriendNoneSelectedImage.SetActive(false);
        }

        public void OnAllowChallengesEveryoneButtonClicked()
        {
            PlayerPrefs.SetInt("allow challenge",0);
            AllowChallengesEveryoneSelectedImage.SetActive(false);
            AllowChallengesNoneSelectedImage.SetActive(true);
        }

        public void OnAllowChallengesNoneButtonClicked()
        {
            PlayerPrefs.SetInt("allow challenge", 1);
            AllowChallengesEveryoneSelectedImage.SetActive(true);
            AllowChallengesNoneSelectedImage.SetActive(false);
        }

        public void OnShowOnlineStatusOnButtonClicked()
        {
            PlayerPrefs.SetInt("online status",0);
            ShowOnlineStatusOffButton.SetActive(true);
        }

        public void OnShowOnlineStatusOffButtonClicked()
        {
            PlayerPrefs.SetInt("online status", 1);
            ShowOnlineStatusOffButton.SetActive(false);
        }

        public void OnFriendsOnlineNotificationsOnButtonClicked()
        {
            PlayerPrefs.SetInt("friends online notification",0);
            FriendsOnlineNotificationsOffButton.SetActive(true);
        }

        public void OnFriendsOnlineNotificationsOffButtonClicked()
        {
            PlayerPrefs.SetInt("friends online notification", 1);
            FriendsOnlineNotificationsOffButton.SetActive(false);
        }

        public void OnVisibleToFoFOnButtonClicked()
        {
            PlayerPrefs.SetInt("visible friends",0);
            VisibleToFoFOffButton.SetActive(true);
        }

        public void OnVisibleToFoFOffButtonClicked()
        {
            PlayerPrefs.SetInt("visible friends", 1);
            VisibleToFoFOffButton.SetActive(false);
        }

        public void OnCreditsButtonClicked()
        {

        }

        public void OnBackButtonClicked()
        {
            settingsContent.position = settingsInitialPos;
            this.gameObject.SetActive(false);
            uIManager.homeScreenParent.SetActive(true);
            //uIManager.ShowScreen(UIManager.Screens.SettingsScreen, UIManager.Screens.HomeScreen);
        }
        public void OnBackButtonGameSceneClicked()
        {
            this.gameObject.SetActive(false);

        }
        public void OnEmailLoginButtonClicked()
        {
            print("Email");
            uIManager.loginScreenParent.SetActive(true);
            this.gameObject.SetActive(false);
            uIManager.loginScreenParent.transform.GetComponent<LoginScreen>().EmailLoginButton();
        }

        public void OnFacebookLoginButtonClicked()
        {
            PlayerPrefs.SetInt("facebook login", 1);
            loginFacebookButton.transform.GetComponent<Button>().interactable = false;
            loginFacebookButton.transform.GetChild(0).GetComponent<Text>().text = "Logged In";
        }

        /*public void PowerbarLocation()
        {
            print("powerbarrr");
            int powerBarLocationFlag1 = PlayerPrefs.GetInt("powerbar location");
            int powerBarOrientationFlag1 = PlayerPrefs.GetInt("powerbar orientation");
            print("powerBarLocationFlag1" + powerBarLocationFlag1);
            print("powerBarOrientationFlag1" + powerBarOrientationFlag1);
            if (powerBarLocationFlag1==0)
            {
                if (powerBarOrientationFlag1==1)
                {
                    Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                    powerBar.transform.GetComponent<RectTransform>().position = new Vector3(583f, 20f, 0f);
                    powerBar.transform.GetComponent<RectTransform>().rotation = spawnRotation;
                    print("lefthorizontal");
                }
                else if(powerBarOrientationFlag1==0)
                {
                    Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    powerBar.transform.GetComponent<RectTransform>().position = new Vector3(90, 459, 0);
                    powerBar.transform.GetComponent<RectTransform>().rotation = spawnRotation;
                    print("leftvertical");
                }
            }
            else if (powerBarLocationFlag1==1)
            {
                if (powerBarOrientationFlag1==1)
                {

                    Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                    powerBar.transform.GetComponent<RectTransform>().position = new Vector3(1309f, 20f, 0f);
                    powerBar.transform.GetComponent<RectTransform>().rotation = spawnRotation;
                    print("righthorizontal");
                }
                else if (powerBarOrientationFlag1==0)
                {
                    Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    powerBar.transform.GetComponent<RectTransform>().position = new Vector3(1860, 459, 0);
                    powerBar.transform.GetComponent<RectTransform>().rotation = spawnRotation;
                    print("rightvertical");
                }
            }
        }*/
    }
}
