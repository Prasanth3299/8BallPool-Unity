using RevolutionGames.Data;
using RevolutionGames.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RevolutionGames.Hud
{
    public class HudManagerLuckyShot : MonoBehaviour
    {
        public GameManagerLuckyShot gameManager;
        public RectTransform canvas;
        public CanvasInputPositionHandler canvasInputPositionHandler;
        public GameObject cueSpinScreen;
        public GameObject forceUpdater;
        public GameObject cueSpinSpot;
        public GameObject cueBallIndicator;
        public Image Player1TimerImage, Player2TimerImage;
        public Text player1NameText, player2NameText, player1BallTypeText, messageText, player1Timer, player2Timer, player2BallTypeText;
        public Image[] player1Balls;
        public Image[] player2Balls;
        public Sprite[] ballSprites;
        public GameObject menuParent;
        public GameObject confirmationPanel;
        public GameObject menuPanel;
        public GameObject rematchScreen;
        public GameObject winnerText1;
        public GameObject winnerText2;
        public GameObject cuesMainScreen;

        public GameObject settingScreen;
        public Transform settingsContent;

        public GameObject profileScreen;

        public GameObject ownedScreen;
        public Transform ownedCueContent;
        public GameObject ownedCueData;
        public GameObject standardCueData;
        public GameObject CoinsScreen;
        public Text coinsText;
        public GameObject purchaseCompletePopup;
        public Text cueNamePurchaseText;
        public GameObject CoinsPurchaseCompleteScreen;
        public GameObject aimingWheel;

        public GameObject SoundOffButton, TableStickersOffButton, VibratingOffButton, TapToAimOnButton, AimingWheelOnButton,
           LocalModeGuidelineOnButton, InGameChatOffButton, ShowOnlineStatusOffButton, FriendsOnlineNotificationsOffButton,
           VisibleToFoFOffButton;

        public GameObject CueSensitivitySlowSelectedImage, CueSensitivityNormalSelectedImage, CueSensitivityFastSelectedImage,
            PowerBarLocationLeftSelectedImage, PowerBarLocationRightSelectedImage,
            PowerBarOrientationHorizontalSelectedImage, PowerBarOrientationVerticalSelectedImage,
            AllowAddFriendEveryoneSelectedImage, AllowAddFriendNoneSelectedImage,
            AllowChallengesEveryoneSelectedImage, AllowChallengesNoneSelectedImage;

        public Text VersionText;
        public GameObject powerBar;
        public RectTransform redSpot; //cue spin spot in HUD
        private int soundFlag, tabbleStickersFlag, cueSensitivityFlag, vibratingFlag, tapToAimFlag, aimingWheelFlag, localGuidelineFlag, powerBarLocationFlag,
                     powerBarOrientationFlag, gameChatFlag, addFriendFlag, allowChallengesFlag, onlineStatusFlag, notificationFriendsFlag, visibleFriendsFlag;






        private CueStickManagerLuckyShot cueStickManager;
        private CueStickDataManager cueStickDataManager;
        private SpriteRenderer cueStickSprite;
        private List<GameObject> ownedCueList = new List<GameObject>();
        private List<GameObject> standardCueList = new List<GameObject>();
        private bool isCueBallPressed = false;
        private CueSpinSpot cueSpinSpotPos;
        private AimingWheelLuckyShot aimingWheelPos;
        private Text TimerText;
        private Image TimerImage;
        private float tempTime = 0;
        private Player currentPlayer;
        private Transform trayParentObject;
        private SpriteRenderer tray;
        private Transform trayStartPosition, trayEndPosition;
        private float screenWidth = 0, screenHeight = 0;
        private float maxTimePerShot = 0;
        private int playerCueStickIndex = 0;
        private Vector3 settingsInitialPos = Vector3.zero;
        private Vector3 cueScreenInitialPos = Vector3.zero;
        //private Text[] player1BallText;
        //private Text[] player2BallText;
        private bool stopTimer = false;
        public bool IsCueBallPressed { get => isCueBallPressed; set => isCueBallPressed = value; }
        public bool StopTimer { get => stopTimer; set => stopTimer = value; }

        // Start is called before the first frame update
        void Start()
        {
            //Save ball child text in a text array to display in HUD for both players
            /*player1BallText = new Text[player1Balls.Length];
            for (int i = 0; i < player1Balls.Length; i++)
            {
                player1BallText[i] = player1Balls[i].transform.GetChild(0).GetComponent<Text>();
            }
            player2BallText = new Text[player2Balls.Length];
            for (int i = 0; i < player2Balls.Length; i++)
            {
                player2BallText[i] = player2Balls[i].transform.GetChild(0).GetComponent<Text>();
            }*/

            cueStickManager = gameManager.cueStickManager;
            cueStickDataManager = cueStickManager.cueStickDataManager;
            trayParentObject = gameManager.trayParentObject;
            tray = gameManager.tray;
            trayStartPosition = gameManager.trayStartPosition;
            trayEndPosition = gameManager.trayEndPosition;
            screenWidth = canvas.rect.width;
            screenHeight = canvas.rect.height;
            cueStickSprite = cueStickManager.cueStick.GetComponent<SpriteRenderer>();
            settingsInitialPos = settingsContent.position;
            cueScreenInitialPos = ownedCueContent.position;

            IsCueBallPressed = false;
            cueSpinSpotPos = cueSpinSpot.GetComponent<CueSpinSpot>();
            aimingWheelPos = aimingWheel.GetComponent<AimingWheelLuckyShot>();
          //  rematchScreen.SetActive(false);

            soundFlag = PlayerPrefs.GetInt("sound");
            tabbleStickersFlag = PlayerPrefs.GetInt("table stickers");
            cueSensitivityFlag = cueStickManager.GetCueSensitivity();//PlayerPrefs.GetInt("cue sensitivity");
            vibratingFlag = PlayerPrefs.GetInt("vibrating");
            tapToAimFlag = cueStickManager.GetTapToAimFlag();//PlayerPrefs.GetInt("tap to aim");
            aimingWheelFlag = cueStickManager.GetAimingWheelFlag();//PlayerPrefs.GetInt("aiming wheel");
            localGuidelineFlag = cueStickManager.GetDisableGuidelineInLocalFlag();//PlayerPrefs.GetInt("local guideline");
            powerBarLocationFlag = cueStickManager.GetPowerBarLocationFlag();//PlayerPrefs.GetInt("powerbar location");
            powerBarOrientationFlag = cueStickManager.GetPowerBarOrientationFlag();//PlayerPrefs.GetInt("powerbar orientation");
            gameChatFlag = PlayerPrefs.GetInt("game chat");
            addFriendFlag = PlayerPrefs.GetInt("add friend");
            onlineStatusFlag = PlayerPrefs.GetInt("online status");
            notificationFriendsFlag = PlayerPrefs.GetInt("friends online notification");
            visibleFriendsFlag = PlayerPrefs.GetInt("visible friends");



            /*

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
                aimingWheel.SetActive(false);
            }
            else
            {
                AimingWheelOnButton.SetActive(true);
                aimingWheel.SetActive(true);
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

            */

            PowerbarLocation();
            SetCueSensitivity();
            DisableGuidelineInLocal();

            cueStickSprite.sprite = cueStickDataManager.GetPlayerCueStickSprite();
        }

        // Update is called once per frame
        void Update()
        {
           /* tempTime += Time.deltaTime;
            if (!stopTimer && tempTime > 1 && !(gameManager.CurrentGameState == GameManager.GameState.isGameOver || gameManager.CurrentGameState == GameManager.GameState.isStrikeDone || gameManager.CurrentGameState == GameManager.GameState.isItCueballBreakDone))
            {
                tempTime = 0;
               // TimerText.text = currentPlayer.timer.ToString();
                TimerImage.fillAmount = ((float)currentPlayer.timer / (float)maxTimePerShot);
                currentPlayer.timer--;
                if (currentPlayer.timer == 0)
                {
                    gameManager.cueStickManager.ResetCueStick();
                    SetMessage("Time Out!");
                    gameManager.CurrentPlayer.timer = 0;

                }
            }*/
        }

        public void InstantiateCueStickContent()
        {
            InstantiateOwnedCueStickContent();
            InstantiateStandardCueStickContent();
        }

        public void InstantiateOwnedCueStickContent()
        {
            ownedCueList.Clear();
            for (int i = 0; i < ownedCueContent.transform.childCount; i++)
            {
                Destroy(ownedCueContent.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < cueStickDataManager.GetOwnedCueStickCount(); i++)
            {
                /*int count = i;
                GameObject ownedCue = Instantiate(ownedCueData);
                ownedCue.transform.SetParent(ownedCueContent, false);
                ownedCue.transform.GetChild(7).GetComponent<Image>().sprite = cueStickDataManager.GetOwnedCueStickImage(i);
                ownedCue.transform.GetChild(4).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickName(i);
                for (int j = cueStickDataManager.GetOwnedCueStickForce(i); j < 10; j++)
                {
                    ownedCue.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetOwnedCueStickAim(i); j < 10; j++)
                {
                    ownedCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetOwnedCueStickSpin(i); j < 10; j++)
                {
                    ownedCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetOwnedCueStickTime(i); j < 10; j++)
                {
                    ownedCue.transform.GetChild(1).GetChild(3).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                ownedCue.transform.GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickType(i);
                ownedCue.transform.GetChild(6).GetChild(3).GetComponent<Text>().text = (cueStickDataManager.GetOwnedCueStickRechargePrice(i)).ToString();
                if (cueStickDataManager.GetOwnedCueStickCharge(i) >= 0)
                {
                    ownedCue.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickCharge(i) + "/50";
                }
                else
                {
                    ownedCue.transform.GetChild(8).GetChild(0).GetComponent<Text>().text = "0/50";
                }
                ownedCue.transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "Recharge";
                ownedCue.transform.GetChild(6).GetComponent<Image>().color = new Color32(255, 120, 0, 255);
                ownedCue.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(() => OnOwnedRechargeButtonClicked(count));
                int chargeBlocks = cueStickDataManager.GetOwnedCueStickCharge(i) / 10;
                Color chargeBlockColor;
                if (chargeBlocks >= 4)
                {
                    if (chargeBlocks == 5)
                    {
                        ownedCue.transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "Charged";
                        ownedCue.transform.GetChild(6).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                        ownedCue.transform.GetChild(6).GetComponent<Button>().onClick.RemoveListener(() => OnOwnedRechargeButtonClicked(count));
                    }
                    chargeBlockColor = new Color(0, 1, 0);
                }
                else if (chargeBlocks == 3)
                {
                    chargeBlockColor = new Color(1, 1, 0);
                }
                else
                {
                    chargeBlockColor = new Color(1, 0, 0);
                }
                for (int k = 0; k < 5; k++)
                {
                    if (k < chargeBlocks)
                    {
                        ownedCue.transform.GetChild(8).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = chargeBlockColor;
                    }
                    else
                    {
                        ownedCue.transform.GetChild(8).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(false);
                    }
                }
                if (chargeBlocks <= 0)
                {
                    ownedCue.transform.GetChild(8).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                    ownedCue.transform.GetChild(8).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
                }
                if (cueStickDataManager.GetOwnedCueStickAutoRecharge(i) == 1)
                {
                    ownedCue.transform.GetChild(9).GetChild(1).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    ownedCue.transform.GetChild(9).GetChild(1).GetChild(0).gameObject.SetActive(false);
                }
                if (cueStickDataManager.GetOwnedCueStickMaxLevel(i) > 0)
                {
                    ownedCue.transform.GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickType(i);
                    ownedCue.transform.GetChild(2).gameObject.SetActive(true);
                    ownedCue.transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                        cueStickDataManager.GetOwnedCueStickCurrentSubLevel(i) + "/" + cueStickDataManager.GetOwnedCueStickMaxSubLevel(i);
                    ownedCue.transform.GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = cueStickDataManager.GetOwnedCueStickCurrentSubLevel(i) / (float)cueStickDataManager.GetOwnedCueStickMaxSubLevel(i);

                    if (cueStickDataManager.GetOwnedCueStickIsUnlockedFlag(i) == 1)
                    {
                        if (cueStickDataManager.GetOwnedCueStickCurrentLevel(i) >= (cueStickDataManager.GetOwnedCueStickMaxLevel(i) + 1))
                        {
                            ownedCue.transform.GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickType(i) +
                            " Level MAX";
                        }
                        else
                        {
                            ownedCue.transform.GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickType(i) +
                            " Level " + cueStickDataManager.GetOwnedCueStickCurrentLevel(i);
                        }
                        ownedCue.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
                        ownedCue.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    }
                }
                ownedCueList.Add(ownedCue);
                ownedCue.transform.GetChild(9).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnOwnedAutoRechargeButtonClicked(count));
                ownedCue.transform.GetChild(9).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnOwnedAutoRechargeButtonClicked(count));
                ownedCue.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnCuesUseButtonClicked(count));
                if (i == playerCueStickIndex)
                {
                    ownedCue.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    ownedCue.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                }*/
                Vector2 autoChargePosition = new Vector2(682.55f, 23);
                Vector3 chargePosition = new Vector3(380f, 23f, 0f);
                int count = i;
                GameObject ownedCue = Instantiate(ownedCueData);
                if (cueStickDataManager.GetOwnedCueStickCategory(i) != "Owned") //Beginner Cue
                {
                    ownedCue.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnOwnedCueExpandButtonClicked(count));
                    ownedCue.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnOwnedCueExpandButtonClicked(count));
                }
                else
                {
                    ownedCue.transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
                }

                ownedCue.transform.SetParent(ownedCueContent, false);
                ownedCue.transform.GetChild(1).GetChild(2).GetComponent<RectTransform>().anchoredPosition = autoChargePosition;
                ownedCue.transform.GetChild(1).GetChild(1).GetComponent<Transform>().localPosition = chargePosition;
                ownedCue.transform.GetChild(0).GetChild(6).GetComponent<Image>().sprite = cueStickDataManager.GetOwnedCueStickImage(i);
                ownedCue.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickName(i);
                for (int j = cueStickDataManager.GetOwnedCueStickForce(i); j < 10; j++)
                {
                    ownedCue.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetOwnedCueStickAim(i); j < 10; j++)
                {
                    ownedCue.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetOwnedCueStickSpin(i); j < 10; j++)
                {
                    ownedCue.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetOwnedCueStickTime(i); j < 10; j++)
                {
                    ownedCue.transform.GetChild(0).GetChild(1).GetChild(3).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                ownedCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickType(i);
                ownedCue.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>().text = (cueStickDataManager.GetOwnedCueStickRechargePrice(i)).ToString();
                if (cueStickDataManager.GetOwnedCueStickCharge(i) >= 0)
                {
                    ownedCue.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickCharge(i) + "/50";
                }
                else
                {
                    ownedCue.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "0/50";
                }
                ownedCue.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Recharge";
                ownedCue.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 120, 0, 255);
                ownedCue.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnOwnedRechargeButtonClicked(count));
                int chargeBlocks = cueStickDataManager.GetOwnedCueStickCharge(i) / 10;
                Color chargeBlockColor;
                if (chargeBlocks >= 4)
                {
                    if (chargeBlocks == 5)
                    {
                        ownedCue.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Charged";
                        ownedCue.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                        ownedCue.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveListener(() => OnOwnedRechargeButtonClicked(count));
                    }
                    chargeBlockColor = new Color(0, 1, 0);
                }
                else if (chargeBlocks == 3)
                {
                    chargeBlockColor = new Color(1, 1, 0);
                }
                else
                {
                    chargeBlockColor = new Color(1, 0, 0);
                }
                for (int k = 0; k < 5; k++)
                {
                    if (k < chargeBlocks)
                    {
                        ownedCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = chargeBlockColor;
                    }
                    else
                    {
                        ownedCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(false);
                    }
                }
                if (chargeBlocks <= 0)
                {
                    ownedCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                    ownedCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
                }
                if (cueStickDataManager.GetOwnedCueStickAutoRecharge(i) == 1)
                {
                    ownedCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    ownedCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
                }

                if (cueStickDataManager.GetOwnedCueStickMaxLevel(i) > 0)
                {
                    ownedCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickType(i);
                    ownedCue.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    ownedCue.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text =
                        cueStickDataManager.GetOwnedCueStickCurrentSubLevel(i) + "/" + cueStickDataManager.GetOwnedCueStickMaxSubLevel(i);
                    ownedCue.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = cueStickDataManager.GetOwnedCueStickCurrentSubLevel(i) / (float)cueStickDataManager.GetOwnedCueStickMaxSubLevel(i);

                    if (cueStickDataManager.GetOwnedCueStickIsUnlockedFlag(i) == 1)
                    {
                        if (cueStickDataManager.GetOwnedCueStickCurrentLevel(i) >= (cueStickDataManager.GetOwnedCueStickMaxLevel(i) + 1))
                        {
                            ownedCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickType(i) +
                            " Level MAX";
                        }
                        else
                        {
                            ownedCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickType(i) +
                            " Level " + cueStickDataManager.GetOwnedCueStickCurrentLevel(i);
                        }
                        ownedCue.transform.GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(true);
                        ownedCue.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
                    }
                }
                ownedCueList.Add(ownedCue);
                ownedCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnOwnedAutoRechargeButtonClicked(count));
                ownedCue.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnOwnedAutoRechargeButtonClicked(count));
                ownedCue.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnCuesUseButtonClicked(count));

                if (i == playerCueStickIndex)
                {
                    ownedCue.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    ownedCue.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
                }
            }

        }

        public void OnOwnedCueExpandButtonClicked(int index)
        {
            if (ownedCueList[index].transform.GetChild(1).gameObject.activeSelf)
            {
                ownedCueList[index].transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
                ownedCueList[index].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                ownedCueList[index].transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
                ownedCueList[index].transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        public void OnOwnedAutoRechargeButtonClicked(int index)
        {
            //GameObject temp = ownedCueList[index].transform.GetChild(9).GetChild(1).GetChild(0).gameObject;
            GameObject temp = ownedCueList[index].transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject;
            if (temp.activeSelf)
            {
                temp.SetActive(false);
                cueStickDataManager.SetOwnedCueStickAutoRecharge(index, 0);
            }
            else
            {
                temp.SetActive(true);
                cueStickDataManager.SetOwnedCueStickAutoRecharge(index, 1);
            }
        }

        public void OnOwnedRechargeButtonClicked(int index)
        {
            //GameData.Instance().PlayerBalance = 2220;
            if (GameData.Instance().PlayerBalance >= cueStickDataManager.GetRechargePriceInNumbers())
            {
                //uiManager.SubtractCoinData(cueStickDataManager.GetRechargePriceInNumbers().ToString());
                /*cueStickDataManager.ResetOwnedCueStickCharge(index);
                ownedCueList[index].transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "Charged";
                ownedCueList[index].transform.GetChild(6).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                ownedCueList[index].transform.GetChild(6).GetComponent<Button>().onClick.RemoveListener(() => OnOwnedRechargeButtonClicked(index));
                ownedCueList[index].transform.GetChild(8).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickCharge(index) + "/50";
                ownedCueList[index].transform.GetChild(8).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
                for (int k = 0; k < 5; k++)
                {
                    ownedCueList[index].transform.GetChild(8).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(true);
                    ownedCueList[index].transform.GetChild(8).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0);
                }*/

                cueStickDataManager.ResetOwnedCueStickCharge(index);
                ownedCueList[index].transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Charged";
                ownedCueList[index].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                ownedCueList[index].transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveListener(() => OnOwnedRechargeButtonClicked(index));
                ownedCueList[index].transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetOwnedCueStickCharge(index) + "/50";
                ownedCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
                for (int k = 0; k < 5; k++)
                {
                    ownedCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(true);
                    ownedCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0);
                }
            }
            else
            {
                CoinsScreen.SetActive(true);
            }
        }

        public void InstantiateStandardCueStickContent()
        {
            standardCueList.Clear();
            //standardCueList.RemoveAll(standardCue);
            for (int i = 0; i < cueStickDataManager.GetStandardCueStickCount(); i++)
            {
                /*int count = i;
                GameObject standardCue = Instantiate(standardCueData, ownedCueContent);
                standardCue.transform.GetChild(7).GetComponent<Image>().sprite = cueStickDataManager.GetStandardCueStickImage(i);
                standardCue.transform.GetChild(4).GetComponent<Text>().text = cueStickDataManager.GetStandardCueStickName(i);
                standardCue.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = cueStickDataManager.GetStandardCueStickPrice(i);
                for (int j = cueStickDataManager.GetStandardCueStickForce(i); j < 10; j++)
                {
                    standardCue.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetStandardCueStickAim(i); j < 10; j++)
                {
                    standardCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetStandardCueStickSpin(i); j < 10; j++)
                {
                    standardCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetStandardCueStickTime(i); j < 10; j++)
                {
                    standardCue.transform.GetChild(1).GetChild(3).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                if (cueStickDataManager.GetStandardCueStickMaxLevel(i) > 0)
                {
                    standardCue.transform.GetChild(2).GetChild(1).GetComponent<Text>().text =
                    cueStickDataManager.GetStandardCueStickCurrentSubLevel(i) + "/" + cueStickDataManager.GetStandardCueStickCurrentMaxSubLevel(i);
                    standardCue.transform.GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetStandardCueStickType(i);
                }
                else
                {
                    standardCue.transform.GetChild(2).gameObject.SetActive(false);
                    standardCue.transform.GetChild(5).gameObject.SetActive(false);
                }
                standardCue.transform.GetChild(6).GetChild(3).GetComponent<Text>().text = (cueStickDataManager.GetStandardCueStickRechargePrice(i)).ToString();

                standardCueList.Add(standardCue);
                standardCue.transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() => StandardCueBuyButtonClicked(count));
                //standardCue.transform.GetChild(0).GetChild(5).GetComponent<Button>().onClick.AddListener(() => StandardCueBuyButtonClicked(count));*/
                int count = i;
                GameObject standardCue = Instantiate(standardCueData, ownedCueContent);
                standardCue.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnStandardCueExpandButtonClicked(count));
                standardCue.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnStandardCueExpandButtonClicked(count));

                standardCue.transform.GetChild(0).GetChild(6).GetComponent<Image>().sprite = cueStickDataManager.GetStandardCueStickImage(i);
                standardCue.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = cueStickDataManager.GetStandardCueStickName(i);
                standardCue.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = cueStickDataManager.GetStandardCueStickPrice(i);

                for (int j = cueStickDataManager.GetStandardCueStickForce(i); j < 10; j++)
                {
                    standardCue.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetStandardCueStickAim(i); j < 10; j++)
                {
                    standardCue.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetStandardCueStickSpin(i); j < 10; j++)
                {
                    standardCue.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetStandardCueStickTime(i); j < 10; j++)
                {
                    standardCue.transform.GetChild(0).GetChild(1).GetChild(3).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                if (cueStickDataManager.GetStandardCueStickMaxLevel(i) > 0)
                {
                    standardCue.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text =
                    cueStickDataManager.GetStandardCueStickCurrentSubLevel(i) + "/" + cueStickDataManager.GetStandardCueStickCurrentMaxSubLevel(i);
                    standardCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetStandardCueStickType(i);
                }
                else
                {
                    standardCue.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                    standardCue.transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
                }
                standardCue.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>().text = (cueStickDataManager.GetStandardCueStickRechargePrice(i)).ToString();


                standardCue.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() => StandardCueBuyButtonClicked(count));
                //standardCue.transform.GetChild(0).GetChild(5).GetComponent<Button>().onClick.AddListener(() => StandardCueBuyButtonClicked(count));
                standardCueList.Add(standardCue);
            }
        }

        public void OnStandardCueExpandButtonClicked(int index)
        {
            if (standardCueList[index].transform.GetChild(1).gameObject.activeSelf)
            {
                standardCueList[index].transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
                standardCueList[index].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                standardCueList[index].transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
                standardCueList[index].transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        public void StandardCueBuyButtonClicked(int itemNumber)
        {
            if (GameData.Instance().PlayerBalance >= cueStickDataManager.GetStandardCueStickPriceInNumber(itemNumber))
            {
                //uiManager.UpdateCoinData(cueStickDataManager.GetStandardCueStickPriceInNumber(itemNumber));
                GameData.Instance().PlayerBalance -= cueStickDataManager.GetStandardCueStickPriceInNumber(itemNumber);
                UpdatePlayerCoinsData();
                //standardCueList[itemNumber].transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                //standardCueList[itemNumber].transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                //ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                //ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                //cueNamePurchaseText.text = standardCueList[itemNumber].transform.GetChild(4).GetComponent<Text>().text;

                standardCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(4).gameObject.SetActive(true);
                standardCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);

                ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
                ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
                cueNamePurchaseText.text = standardCueList[itemNumber].transform.GetChild(0).GetChild(4).GetComponent<Text>().text;

                purchaseCompletePopup.SetActive(true);
                cueStickDataManager.ObtainedStandardCueStick(itemNumber);
                playerCueStickIndex = cueStickDataManager.GetPlayerCueStick();
                cueStickSprite.sprite = cueStickDataManager.GetPlayerCueStickSprite();
                ownedCueList.Add(standardCueList[itemNumber]);
                standardCueList.RemoveAt(itemNumber);
                InstantiateCueStickContent();
                //StandardButtonCallBack();
            }
            else
            {
                CoinsScreen.SetActive(true);
            }
            // standardCueList.RemoveAt(itemNumber);
        }

        public void UpdatePlayerCoinsData()
        {
            coinsText.text = GameData.Instance().PlayerBalance.ToString();
        }

        public void OnCueSpinClicked()
        {
            if (cueSpinScreen.activeSelf)
            {
                cueSpinScreen.SetActive(false);
            }
            else
            {
                cueSpinScreen.SetActive(true);
            }
        }

        public void EnableCueBallIndicator(bool val)
        {
            cueBallIndicator.SetActive(val);
        }

        public void ResetCueSpinPosition()
        {
            cueSpinSpotPos.ResetCueSpinPosition();
        }

        public void DisableAimingWheel()
        {
            aimingWheel.SetActive(false);
        }

        public void ResetAimingWheelPosition()
        {
            if (cueStickDataManager.AimingWheelFlag == 1)
            {
                aimingWheelPos.ResetAimingWheel();
            }
        }

        public void DisplayBreakShotDetails(string name)
        {
            //Enable necessary messages to show game over
            SetMessage(name + " Breaks!");
        }

        public void DisplayBallGroupTypeDetails(string ballGroupType)
        {
            //Enable necessary messages to show player ball group - solid/stripes
            SetMessage("You are " + ballGroupType + "s");
        }

        public void DisplayCueBallInHandMessage(string name)
        {
            SetMessage(name + " has cue ball in hand!");
        }

        public void DisplayGameOverDetails(Player winner)
        {
            //Enable necessary messages to show game over
            //SceneManager.LoadScene("MainMenuScene");
            rematchScreen.SetActive(true);
            SetMessage(winner.name + " Wins!");
            if (winner.name == "Player1")
            {
                winnerText1.SetActive(true);
            }
            else
            {
                winnerText2.SetActive(true);
            }
            //print("name  ;"+winner.name);
        }

        public void DisplayPlayerName(string name, int playerno)
        {
            if (playerno == 1)
                player1NameText.text = name;
            else
                player2NameText.text = name;
        }

        public void DisplayTimer(Player currentPlayer)
        {
            this.currentPlayer = currentPlayer;
            if (currentPlayer == gameManager.FirstPlayer)
            {
                TimerText = player1Timer;
                TimerImage = Player1TimerImage;
                Player2TimerImage.fillAmount = 1;
                player2Timer.text = "-";
            }
            else
            {
                TimerText = player2Timer;
                TimerImage = Player2TimerImage;
                Player1TimerImage.fillAmount = 1;
                player1Timer.text = "-";
            }
        }

        private void SetMessage(string message)
        {
            messageText.text = message;
            StartCoroutine(DisplayMessage());
        }

        IEnumerator DisplayMessage()
        {
            messageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            messageText.gameObject.SetActive(false);
        }

        public void UpdatePlayer1BallType(string type)
        {
            player1BallTypeText.text = type;
        }
        public void UpdatePlayer2BallType(string type)
        {
            player2BallTypeText.text = type;
        }
        public void UpdatePlayer1BallListDisplay()
        {
            for (int i = 0; i < player1Balls.Length; i++)
            {
                if (i < gameManager.FirstPlayer.remainingBalls.Count)
                {
                    if (gameManager.FirstPlayer.remainingBalls[i].name == "8BallParent")
                    {
                        //player1BallText[i].text = ballNumber[1];
                        player1Balls[i].sprite = ballSprites[7];
                    }
                    else
                    {
                        string[] ballNumber = gameManager.FirstPlayer.remainingBalls[i].name.Split("_"[0]);
                        //player1BallText[i].text = ballNumber[1];
                        player1Balls[i].sprite = ballSprites[int.Parse(ballNumber[1]) - 1];
                    }
                    player1Balls[i].color = new Color(1, 1, 1, 1);
                }
                else
                {
                    //player1BallText[i].text = "";
                    player1Balls[i].sprite = null;
                    player1Balls[i].color = new Color(1, 1, 1, 0);
                }
            }
        }
        public void UpdatePlayer2BallListDisplay()
        {
            for (int i = 0; i < player2Balls.Length; i++)
            {
                if (i < gameManager.SecondPlayer.remainingBalls.Count)
                {
                    if (gameManager.SecondPlayer.remainingBalls[i].name == "8BallParent")
                    {
                        //player1BallText[i].text = ballNumber[1];
                        player2Balls[i].sprite = ballSprites[7];
                    }
                    else
                    {
                        string[] ballNumber = gameManager.SecondPlayer.remainingBalls[i].name.Split("_"[0]);
                        //player2BallText[i].text = ballNumber[1];
                        player2Balls[i].sprite = ballSprites[int.Parse(ballNumber[1]) - 1];
                    }
                    player2Balls[i].color = new Color(1, 1, 1, 1);
                }
                else
                {
                    //player2BallText[i].text = "";
                    player2Balls[i].sprite = null;
                    player2Balls[i].color = new Color(1, 1, 1, 0);
                }
            }
        }

        public void OnMenuButtonClicked()
        {
            menuParent.SetActive(true);
            menuPanel.SetActive(true);
            confirmationPanel.SetActive(false);
        }

        //Menu panel options
        public void OnCloseButtonClicked()
        {
            menuParent.SetActive(false);
        }

        public void OnOptionsButtonClicked()
        {
            OnCloseButtonClicked();
            settingScreen.SetActive(true);
        }
        public void SettingBackButtonClicked()
        {
            settingScreen.SetActive(false);
            //Reset settings sccroll to top
            settingsContent.position = settingsInitialPos;
        }

        public void OnCuesButtonClicked()
        {
            OnCloseButtonClicked();
            playerCueStickIndex = cueStickDataManager.GetPlayerCueStick();
            InstantiateCueStickContent();
            cuesMainScreen.SetActive(true);
        }
        public void CuesBackButtonClicked()
        {
            cuesMainScreen.SetActive(false);
            //Reset cues screen scroll to top
            ownedCueContent.position = cueScreenInitialPos;
        }
        public void ProfileButtonClicked()
        {
            profileScreen.SetActive(true);
        }
        public void ProfileBackButtonClicked()
        {
            profileScreen.SetActive(false);
        }

        public void OnLeaveButtonClicked()
        {
            menuPanel.SetActive(false);
            confirmationPanel.SetActive(true);

        }

        //Confirmation Panel
        public void OnKeepPlayingButtonClicked()
        {
            menuParent.SetActive(false);
        }

        public void OnLeaveGameButtonClicked()
        {
            menuParent.SetActive(false);
            //GameData.Instance().ToScreenName = "home";
            SceneManager.LoadScene("MainMenuScene");
        }

        public void RematchButtonClicked()
        {
            SceneManager.LoadScene("GameplayScene");
        }
        public void RematchBackButtonClicked()
        {

            GameData.Instance().ToScreenName = "home";
            SceneManager.LoadScene("MainMenuScene");
            //rematchScreen.SetActive(false);
        }
        public void OnCuesUseButtonClicked(int index)
        {
            /*ownedCueList[index].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            ownedCueList[index].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            cueStickDataManager.SetPlayerCueStick(ownedCueList[index].transform.GetChild(4).GetComponent<Text>().text);
            playerCueStickIndex = index;*/
            ownedCueList[index].transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
            ownedCueList[index].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
            cueStickDataManager.SetPlayerCueStick(ownedCueList[index].transform.GetChild(0).GetChild(4).GetComponent<Text>().text);
            playerCueStickIndex = index;
            cueStickSprite.sprite = cueStickDataManager.GetPlayerCueStickSprite();
        }

        /*public void BuyButton1Clicked()
        {
            print(BuyCoinText1.text);
            if (BuyCoinText1.text == "1.5B")
            {
                cueNamePurchaseText.text = cueNameText1.text;
                buyButton1.SetActive(false);
                useButton2.SetActive(true);
                purchaseCompleteScreen.SetActive(true);
            }
            else
            {
                CoinsScreen.SetActive(true);
            }
        }
        public void BuyButton2Clicked()
        {
            print(BuyCoinText2.text);
            if (BuyCoinText2.text == "1.5B")
            {
                cueNamePurchaseText.text = cueNameText2.text;
                purchaseCompleteScreen.SetActive(true);
            }
            else 
            {
                CoinsScreen.SetActive(true);
            }
        }*/

        public void PurchaseCompletepopupCloseButton()
        {
            purchaseCompletePopup.SetActive(false);
        }
        public void CoinsScreenCloseButton()
        {
            CoinsScreen.SetActive(false);
        }
        public void BuyCoinsButtonClicked()
        {
            CoinsPurchaseCompleteScreen.SetActive(true);
        }
        public void CoinsPurchasePopupCloseButton()
        {
            CoinsPurchaseCompleteScreen.SetActive(false);
        }
        public void ProfileCuesButtonClicked()
        {
            cuesMainScreen.SetActive(true);
            profileScreen.SetActive(false);
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
            SetCueSensitivity();
        }

        public void OnCueSensitivityNormalButtonClicked()
        {
            PlayerPrefs.SetInt("cue sensitivity", 1);
            CueSensitivitySlowSelectedImage.SetActive(true);
            CueSensitivityNormalSelectedImage.SetActive(false);
            CueSensitivityFastSelectedImage.SetActive(true);
            SetCueSensitivity();
        }

        public void OnCueSensitivityFastButtonClicked()
        {
            PlayerPrefs.SetInt("cue sensitivity", 2);
            CueSensitivitySlowSelectedImage.SetActive(true);
            CueSensitivityNormalSelectedImage.SetActive(true);
            CueSensitivityFastSelectedImage.SetActive(false);
            SetCueSensitivity();
        }

        public void OnVibratingOnButtonClicked()
        {
            PlayerPrefs.SetInt("vibrating", 0);
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
            cueStickManager.SetTapToAimFlag(0);
        }

        public void OnTapToAimOffButtonClicked()
        {
            PlayerPrefs.SetInt("tap to aim", 1);
            TapToAimOnButton.SetActive(true);
            cueStickManager.SetTapToAimFlag(1);


        }

        public void OnAimingWheelOnButtonClicked()
        {
            PlayerPrefs.SetInt("aiming wheel", 0);
            AimingWheelOnButton.SetActive(false);
            aimingWheel.SetActive(false);
            cueStickManager.SetAimingWheelFlag(0);
        }

        public void OnAimingWheelOffButtonClicked()
        {
            PlayerPrefs.SetInt("aiming wheel", 1);
            AimingWheelOnButton.SetActive(true);
            aimingWheel.SetActive(true);
            cueStickManager.SetAimingWheelFlag(1);
        }

        public void OnLocalModeGuidelineOnButtonClicked()
        {
            PlayerPrefs.SetInt("local guideline", 0);
            LocalModeGuidelineOnButton.SetActive(false);
            DisableGuidelineInLocal();
        }

        public void OnLocalModeGuidelineOffButtonClicked()
        {
            PlayerPrefs.SetInt("local guideline", 1);
            LocalModeGuidelineOnButton.SetActive(true);
            DisableGuidelineInLocal();
        }

        public void OnPowerBarLocationLeftButtonClicked()
        {
            PlayerPrefs.SetInt("powerbar location", 0);
            PowerBarLocationLeftSelectedImage.SetActive(false);
            PowerBarLocationRightSelectedImage.SetActive(true);
            PowerbarLocation();
            cueStickManager.SetPowerBarLocationFlag(0);
        }

        public void OnPowerBarLocationRightButtonClicked()
        {
            PlayerPrefs.SetInt("powerbar location", 1);
            PowerBarLocationLeftSelectedImage.SetActive(true);
            PowerBarLocationRightSelectedImage.SetActive(false);
            PowerbarLocation();
            cueStickManager.SetPowerBarLocationFlag(1);
        }

        public void OnPowerBarOrientationHorizontalButtonClicked()
        {

            PlayerPrefs.SetInt("powerbar orientation", 1);
            PowerBarOrientationHorizontalSelectedImage.SetActive(false);
            PowerBarOrientationVerticalSelectedImage.SetActive(true);
            PowerbarLocation();
            cueStickManager.SetPowerBarOrientationFlag(1);
        }

        public void OnPowerBarOrientationVerticalButtonClicked()
        {

            PlayerPrefs.SetInt("powerbar orientation", 0);
            PowerBarOrientationHorizontalSelectedImage.SetActive(true);
            PowerBarOrientationVerticalSelectedImage.SetActive(false);
            PowerbarLocation();
            cueStickManager.SetPowerBarOrientationFlag(0);
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

        }

        public void OnAllowAddFriendEveryoneButtonClicked()
        {
            PlayerPrefs.SetInt("add friend", 0);
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
            PlayerPrefs.SetInt("allow challenge", 0);
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
            PlayerPrefs.SetInt("online status", 0);
            ShowOnlineStatusOffButton.SetActive(true);
        }

        public void OnShowOnlineStatusOffButtonClicked()
        {
            PlayerPrefs.SetInt("online status", 1);
            ShowOnlineStatusOffButton.SetActive(false);
        }

        public void OnFriendsOnlineNotificationsOnButtonClicked()
        {
            PlayerPrefs.SetInt("friends online notification", 0);
            FriendsOnlineNotificationsOffButton.SetActive(true);
        }

        public void OnFriendsOnlineNotificationsOffButtonClicked()
        {
            PlayerPrefs.SetInt("friends online notification", 1);
            FriendsOnlineNotificationsOffButton.SetActive(false);
        }

        public void OnVisibleToFoFOnButtonClicked()
        {
            PlayerPrefs.SetInt("visible friends", 0);
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

        public void PowerbarLocation()
        {
            //print("powerbarrr");
            int powerBarLocationFlag1 = PlayerPrefs.GetInt("powerbar location");
            int powerBarOrientationFlag1 = PlayerPrefs.GetInt("powerbar orientation");

            //print("powerBarLocationFlag1" + powerBarLocationFlag1);
            //print("powerBarOrientationFlag1" + powerBarOrientationFlag1);
            if (powerBarLocationFlag1 == 0)
            {

                if (powerBarOrientationFlag1 == 1)
                {
                    Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
                    powerBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-330, -((screenHeight / 2) - 60), 0f);
                    powerBar.transform.GetComponent<RectTransform>().rotation = spawnRotation;
                    if (trayParentObject.localPosition.z < 0)
                    {
                        trayParentObject.localPosition = new Vector3(trayParentObject.localPosition.x, trayParentObject.localPosition.y, -trayParentObject.localPosition.z + 0.05f);
                        trayStartPosition.localPosition = new Vector3(-trayStartPosition.localPosition.x, trayStartPosition.localPosition.y, trayStartPosition.localPosition.z);
                        trayEndPosition.localPosition = new Vector3(-trayEndPosition.localPosition.x, trayEndPosition.localPosition.y, trayEndPosition.localPosition.z);
                        gameManager.boardManager.changePottedBallsPosition(trayEndPosition.position.x);
                    }
                    tray.flipX = false;
                    //print("lefthorizontal");
                }
                else if (powerBarOrientationFlag1 == 0)
                {
                    Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    powerBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-((screenWidth / 2) - 90), -35, 0);
                    powerBar.transform.GetComponent<RectTransform>().rotation = spawnRotation;
                    if (trayParentObject.localPosition.z < 0)
                    {
                        trayParentObject.localPosition = new Vector3(trayParentObject.localPosition.x, trayParentObject.localPosition.y, -trayParentObject.localPosition.z + 0.05f);
                        trayStartPosition.localPosition = new Vector3(-trayStartPosition.localPosition.x, trayStartPosition.localPosition.y, trayStartPosition.localPosition.z);
                        trayEndPosition.localPosition = new Vector3(-trayEndPosition.localPosition.x, trayEndPosition.localPosition.y, trayEndPosition.localPosition.z);
                        gameManager.boardManager.changePottedBallsPosition(trayEndPosition.position.x);

                    }
                    tray.flipX = false;
                    //print("leftvertical");
                }
                aimingWheel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3((screenWidth / 2 - 90), -35, 0);
                if (Camera.main.ScreenToWorldPoint(aimingWheel.transform.position).x - 0.5f < trayParentObject.GetChild(0).gameObject.transform.position.x)
                {
                    aimingWheel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3((screenWidth / 2 - 40), -35, 0);
                }
                aimingWheelPos.GetInitialPosition();
            }
            else if (powerBarLocationFlag1 == 1)
            {

                aimingWheelPos.GetInitialPosition();
                if (powerBarOrientationFlag1 == 1)
                {

                    Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                    powerBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(330, -((screenHeight / 2) - 60), 0f);
                    powerBar.transform.GetComponent<RectTransform>().rotation = spawnRotation;
                    if (trayParentObject.localPosition.z < 0)
                    {
                        trayParentObject.localPosition = new Vector3(trayParentObject.localPosition.x, trayParentObject.localPosition.y, -trayParentObject.localPosition.z + 0.05f);
                        trayStartPosition.localPosition = new Vector3(-trayStartPosition.localPosition.x, trayStartPosition.localPosition.y, trayStartPosition.localPosition.z);
                        trayEndPosition.localPosition = new Vector3(-trayEndPosition.localPosition.x, trayEndPosition.localPosition.y, trayEndPosition.localPosition.z);
                        gameManager.boardManager.changePottedBallsPosition(trayEndPosition.position.x);
                    }
                    tray.flipX = false;
                    //print("righthorizontal");
                }
                else if (powerBarOrientationFlag1 == 0)
                {
                    Quaternion spawnRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    powerBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(((screenWidth / 2) - 90), -35, 0);
                    powerBar.transform.GetComponent<RectTransform>().rotation = spawnRotation;
                    if (trayParentObject.localPosition.z > 0)
                    {
                        trayParentObject.localPosition = new Vector3(trayParentObject.localPosition.x, trayParentObject.localPosition.y, (-trayParentObject.localPosition.z + 0.05f));
                        trayStartPosition.localPosition = new Vector3(-trayStartPosition.localPosition.x, trayStartPosition.localPosition.y, trayStartPosition.localPosition.z);
                        trayEndPosition.localPosition = new Vector3(-trayEndPosition.localPosition.x, trayEndPosition.localPosition.y, trayEndPosition.localPosition.z);
                        gameManager.boardManager.changePottedBallsPosition(trayEndPosition.position.x);
                    }
                    tray.flipX = true;
                    //print("rightvertical");
                }
                aimingWheel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(screenWidth / 2 - 90), -35, 0);
                if (Camera.main.ScreenToWorldPoint(aimingWheel.transform.position).x + 0.5f > trayParentObject.GetChild(0).gameObject.transform.position.x)
                {
                    aimingWheel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(screenWidth / 2 - 40), -35, 0);
                }
            }
        }

        public void SetCueSensitivity()
        {
            cueStickManager.SetCueSensitivity(PlayerPrefs.GetInt("cue sensitivity", 1));
        }

        public void DisableGuidelineInLocal()
        {
            cueStickManager.SetDisableGuidelineInLocalFlag(PlayerPrefs.GetInt("local guideline"));
        }

        public void UpdateMaxTimePerShot(int time)
        {
            maxTimePerShot = time;
        }

        public void LuckyShotBackButtonClicked()
        {
            print("main scene");
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}

