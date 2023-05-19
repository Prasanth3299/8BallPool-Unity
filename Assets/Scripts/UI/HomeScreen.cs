using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;
using System.Collections.Generic;
using System;


namespace RevolutionGames.UI
{
    public class HomeScreen : MonoBehaviour
    {
        public UIManager uiManager;

        public Image ProgressBarImage;
        public Text NameText, IDText, StarText, CoinsText;
        public Image profileImage;
        public Image perkImage1, perkImage2, perkImage3, perkImage4;

        public Image miniGamesListImage;
        public Text miniGamesListcount;
        public Image[] boxSlots;
        public Sprite surpriseBoxSprite;
        public Sprite surpriseBoxEmptySprite;
        public GameObject surpriseBoxPopup;
        public Text cueNameText;
        public Image cueStickImage;
        public GameObject playMinigamesButtonScreen;
        public Text luckyShotTimeText,luckyShotButtonText;
        public GameObject luckyShotTimeImage;
        public Text spinTimeText,spinButtonText;
        public GameObject spinTimeImage;
        public GameObject luckyShotListImage;
        public Text luckyShotListText;
        public GameObject spinListImage;
        public Text spinListText;
        public GameObject boxListImage;
        public Text boxListText;
        public GameObject scratchListImage;
        public Text scratchListText;
        public Image luckyShotBAckGroundImage;
        public Image spinWheelBackGroundImage;
        public Image scratchBackGroundImage;
        public Image surpriseBackGroundImage;
        private bool luckushotBoolean;

        private int totalCount;
        private int scratchCountList;
        private int spinCountList;
        private int boxCountList;
        private int luckyShotList;
        private int luckyShotfreeCount;
        private int totalLuckyShotCount;

        public void Start()
        {
            //PlayerPrefs.DeleteAll();
            SpinWheelUpdate();
            int spinWheelshow = PlayerPrefs.GetInt("spin wheel show");
            int luckyShotshow = PlayerPrefs.GetInt("lucky shot show");
           // print("spinWheelshow"+ spinWheelshow);
            luckushotBoolean = true;
            
            TimeSpan timeDifference;
            //PlayerPrefs.GetInt("spincount", 1);
            if (PlayerPrefs.GetString("systime").Length == 0)
            {

                spinTimeImage.SetActive(false);
                spinButtonText.text = "Spin\n" + "and Win";

                PlayerPrefs.SetInt("spincount", 1);
                if (spinWheelshow == 0)
                {
                    uiManager.spinWheelScreenParent.transform.GetComponent<SpinWheelScreen>().FreeSpinWheelUpdaate();
                }
            }
            if (spinWheelshow == 0)
            {
                uiManager.LuckyShotScreenPArent.transform.GetComponent<LuckyShotScreen>().LuckyShotCountUpdate();
            }
           
           // print("time" + PlayerPrefs.GetString("lucky shot systime").Length);
            if (PlayerPrefs.GetString("lucky shot systime").Length == 0)
            {
                PlayerPrefs.SetInt("luckyshotfreecount", 1);
                luckyShotTimeImage.SetActive(false);
                luckyShotButtonText.text = "Lucky\n" + "Shot";
                if (luckyShotshow == 0)
                {
                    uiManager.LuckyShotScreenPArent.transform.GetComponent<LuckyShotScreen>().LuckyShotUpdate();
                }
               
            }

            if (spinCountList > 0)
            {
                if (spinWheelshow == 0)
                {
                    uiManager.spinWheelScreenParent.transform.GetComponent<SpinWheelScreen>().FreeSpinWheelUpdaate();
                }
               
            }
            //CoinsText.text = PlayerPrefs.GetInt("coin amount").ToString();
        }

        public void Update()
        {
            //SpinWheelUpdate();
            if (playMinigamesButtonScreen.activeSelf)
            {

                LuckyShotTimerUpdate();
                SpinWheelUpdate();
            }
            if (uiManager.homeScreenParent.activeSelf)
            {
                spinCountList = PlayerPrefs.GetInt("spincount");
                totalCount = scratchCountList + boxCountList + spinCountList + luckyShotList + luckyShotfreeCount;

                if (totalCount > 0)
                {
                    miniGamesListcount.text = totalCount.ToString();
                    miniGamesListImage.gameObject.SetActive(true);
                }
                else
                {
                    miniGamesListImage.gameObject.SetActive(false);
                }
                if (totalLuckyShotCount > 0)
                {
                    luckyShotListImage.SetActive(true);
                    luckyShotListText.text = totalLuckyShotCount.ToString();
                }
                else
                {
                    luckyShotListImage.SetActive(false);
                }
                if (scratchCountList > 0)
                {

                    scratchListImage.SetActive(true);
                    scratchListText.text = scratchCountList.ToString();
                }
                else
                {
                    scratchListImage.SetActive(false);
                }

                if (spinCountList > 0)
                {
                    spinListImage.SetActive(true);
                    spinListText.text = spinCountList.ToString();
                }
                else
                {
                    spinListImage.SetActive(false);
                }
                if (boxCountList > 0)
                {
                    boxListImage.SetActive(true);
                    boxListText.text = boxCountList.ToString();
                }
                else
                {
                    boxListImage.SetActive(false);
                }

            }
            
        }
        public void OnEnable()
        {
            scratchCountList = PlayerPrefs.GetInt("scratchcount");
            boxCountList = PlayerPrefs.GetInt("boxcount");
            spinCountList = PlayerPrefs.GetInt("spincount");
            luckyShotList = PlayerPrefs.GetInt("lucky shot count");
            luckyShotfreeCount = PlayerPrefs.GetInt("luckyshotfreecount");
            totalLuckyShotCount = luckyShotList + luckyShotfreeCount;
            totalCount = scratchCountList + boxCountList + spinCountList + luckyShotList+ luckyShotfreeCount;

            if (uiManager.homeScreenParent.activeSelf)
            {
                spinWheelBackGroundImage.color = new Color32(255, 255, 255, 150);
                scratchBackGroundImage.color = new Color32(255, 255, 255, 150);
                surpriseBackGroundImage.color = new Color32(255, 255, 255, 150);
            }
            else
            {
                spinWheelBackGroundImage.color = new Color32(255, 255, 255, 255);
                scratchBackGroundImage.color = new Color32(255, 255, 255, 255);
                surpriseBackGroundImage.color = new Color32(255, 255, 255, 255);
            }

           // scratchCountList = PlayerPrefs.GetInt("scratchcount");
           // boxCountList = PlayerPrefs.GetInt("boxcount");
           // spinCountList = PlayerPrefs.GetInt("spincount");
           // luckyShotList= PlayerPrefs.GetInt("lucky shot count"); 
           // int totalCount = scratchCountList + boxCountList + spinCountList+ luckyShotList;
            if (totalCount > 0)
            {
                miniGamesListcount.text = totalCount.ToString();
                miniGamesListImage.gameObject.SetActive(true);
            }
            else
            {
                miniGamesListImage.gameObject.SetActive(false);
            }
            if (totalLuckyShotCount > 0)
            {
                luckyShotListImage.SetActive(true);
                luckyShotListText.text = totalLuckyShotCount.ToString();
            }
            else
            {
                luckyShotListImage.SetActive(false);
            }
           

            FillBoxSlots();
            SpinWheelUpdate();
        }
        



        public void FillBoxSlots()
        {
            for (int i = 0; i < 4; i++)
            {
                if (i < DataManager.Instance().GetSurpriseBoxListCount())
                {
                    boxSlots[i].sprite = surpriseBoxSprite;
                }
                else
                {
                    boxSlots[i].sprite = surpriseBoxEmptySprite;
                }
            }
        }

      
        public void OpenSurpriseBox(int index)
        {

            if (index < DataManager.Instance().GetSurpriseBoxListCount())
            {
                int idx = uiManager.dataManager.OpenVictorySurpriseBox(index);
                uiManager.dataManager.UpdateVictoryCueStickLevel(idx);
                uiManager.UpdateVictoryCuesList();
                //int idx = uiManager.dataManager.OpenCountrySurpriseBox(index);
                //uiManager.dataManager.UpdateCountryCueStickLevel(idx);
                //uiManager.UpdateCountryCuesList();
                cueNameText.text = uiManager.dataManager.GetVictoryBoxCueStickName(idx);
                cueStickImage.sprite = uiManager.dataManager.GetVictoryBoxCueStickImage(idx);

                //cueNameText.text = uiManager.dataManager.GetCountryBoxCueStickName(idx);
                //cueStickImage.sprite = uiManager.dataManager.GetCountryBoxCueStickImage(idx);
                surpriseBoxPopup.SetActive(true);
                StartCoroutine(OpenPerkImages());
                FillBoxSlots();
            }
        }

        public IEnumerator OpenPerkImages()
        {
            yield return new WaitForSeconds(2.5f);
            if (perkImage4.enabled == true)
            {
                perkImage4.enabled = false;
            }
            else if (perkImage3.enabled == true)
            {
                perkImage3.enabled = false;
            }
            else if (perkImage2.enabled == true)
            {
                perkImage2.enabled = false;
            }
            else
            {
                perkImage1.enabled = false;
            }
        }

        public void OnSurpriseBoxCloseButtonClicked()
        {
            surpriseBoxPopup.SetActive(false);
        }

        public void SpinWheelUpdate()
        {
            TimeSpan timeDifference;
            if (PlayerPrefs.GetString("systime").Length != 0)
            {
                DateTime currentDate = System.DateTime.Now;
                DateTime tempTime = Convert.ToDateTime(PlayerPrefs.GetString("systime"));
                long temp = Convert.ToInt64(tempTime.ToBinary());
                DateTime oldDate = DateTime.FromBinary(temp);
                oldDate = oldDate.AddDays(1); 
                timeDifference = oldDate.Subtract(currentDate);
                //print("time :" + timeDifference.Hours);
                spinTimeText.text = timeDifference.Hours + "h " + timeDifference.Minutes + "m " + timeDifference.Seconds + "s ";
                if (timeDifference.Minutes <= 0 && timeDifference.Seconds < 0)
                {
                    spinTimeImage.SetActive(false);
                    spinButtonText.text = "Spin\n" + "and Win";
                    if (PlayerPrefs.GetInt("spincount") == 0)
                    {
                        PlayerPrefs.SetInt("spincount", (PlayerPrefs.GetInt("spincount") + 1));
                       
                    }
                    uiManager.spinWheelScreenParent.transform.GetComponent<SpinWheelScreen>().FreeSpinCountUpadate(PlayerPrefs.GetInt("spincount"));
                }
                else
                {
                    spinTimeImage.SetActive(true);
                    spinButtonText.text = "Spin\n" + "and Win"+"\n";
                }
                //print(PlayerPrefs.GetInt("spincount"));

            }

        }

        //Lucky shot 
        public void LuckyShotTimerUpdate()
        {
            TimeSpan timeDifference;
            if (PlayerPrefs.GetString("lucky shot systime").Length != 0)
            {
                DateTime currentDate = System.DateTime.Now;
                DateTime tempTime = Convert.ToDateTime(PlayerPrefs.GetString("lucky shot systime"));
                long temp = Convert.ToInt64(tempTime.ToBinary());
                DateTime oldDate = DateTime.FromBinary(temp);
                oldDate = oldDate.AddDays(1);
                timeDifference = oldDate.Subtract(currentDate);
               // print(timeDifference.Minutes);
               // print(timeDifference.Seconds);

                luckyShotTimeText.text = timeDifference.Hours + "h " + timeDifference.Minutes + "m " + timeDifference.Seconds + "s ";
                if (timeDifference.Minutes <= 0 && timeDifference.Seconds < 0)
                {
                    if (luckyShotfreeCount == 0)
                    {
                        PlayerPrefs.SetInt("luckyshotfreecount", 1);
                        luckyShotList = PlayerPrefs.GetInt("lucky shot count");
                        luckyShotfreeCount = PlayerPrefs.GetInt("luckyshotfreecount");
                        totalLuckyShotCount = luckyShotList + luckyShotfreeCount;
                        totalCount = scratchCountList + boxCountList + spinCountList + luckyShotList + luckyShotfreeCount;
                        luckyShotListImage.SetActive(true);
                        luckyShotListText.text = totalLuckyShotCount.ToString();
                    }
                    luckyShotTimeImage.SetActive(false);
                    luckyShotButtonText.text = "Lucky\n" + "Shot";
                    luckushotBoolean = false;
                }
                else
                {
                    luckyShotTimeImage.SetActive(true);

                    luckyShotButtonText.text = "Lucky\n" + "Shot\n" + "\n";
                    luckushotBoolean = true;
                }
            }
            if (totalLuckyShotCount > 0)
            {
                luckyShotListImage.SetActive(true);
                luckyShotListText.text = totalLuckyShotCount.ToString();
                luckyShotTimeImage.SetActive(false);
                luckyShotButtonText.text = "Lucky\n" + "Shot";
            }
            else
            {
                luckyShotListImage.SetActive(false);
            }

        }

        public void OnShopCoinsButtonClicked()
        {
            gameObject.SetActive(false);
            uiManager.shopScreenParent.SetActive(true);
            uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().HomeCoinButtonClicked();
        }
        public void OnShopButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.shopScreenParent.SetActive(true);
            uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().HomeShopButtonClicked();
        }
        public void OnPlayMiniGamesButtonClicked()
        {
            playMinigamesButtonScreen.SetActive(true);
            spinWheelBackGroundImage.color = new Color32(255, 255, 255, 255);
            scratchBackGroundImage.color = new Color32(255, 255, 255, 255);
            surpriseBackGroundImage.color = new Color32(255, 255, 255, 255);
            //SpinWheelUpdate();
            // gameObject.SetActive(false);
            // uiManager.spinWheelScreenParent.SetActive(true);
            // uiManager.spinWheelScreenParent.GetComponent<SpinWheelScreen>().SpinWheelScreenUpdate();
        }

        public void OnLeadersBoardButtonClicked()
        {
            gameObject.SetActive(false);
            uiManager.leaderBoardScreenParent.SetActive(true);
            //uiManager.apiManager.APIGetLeaderBoardWorldData();
        }

        public void OnPlayLocalButtonClicked()
        {
            //uiManager.apiManager.APIGetLocalGamesList();
            OnPlayLocalButtonCallBack(null);
        }
        public void OnPlayLocalButtonCallBack(List<GameMode> localGames)
        {
            gameObject.SetActive(false);
            uiManager.playLocalScreenParent.SetActive(true);
        }

        public void OnTournamentButtonClicked()
        {
            //uiManager.apiManager.APIGetTournamentGamesList();
            //gameObject.SetActive(false);
            uiManager.playTournamentScreenParent.SetActive(true);
        }
        public void OnTournamentButtonCallBack(List<GameMode> tournamentGames)
        {
            gameObject.SetActive(false);
            uiManager.playTournamentScreenParent.SetActive(true);
        }

        public void OnPlay1On1ButtonClicked()
        {
            //uiManager.apiManager.APIGetOnlineGamesList();
            //gameObject.SetActive(false);
            uiManager.play1On1ScreenParent.SetActive(true);
        }

        public void OnPlay1On1ButtonCallBack(List<GameMode> onlineGames)
        {
            uiManager.play1On1ScreenParent.SetActive(true);
            gameObject.SetActive(false);
        }

        /*public void OnPlayWithFriendsButtonClicked()
        {
            uiManager.apiManager.APIGetComputerGamesList();
           
        }*/

        public void OnPlayWithFriendsButtonCallBack(List<GameMode> gameModes)
        {
            uiManager.apiManager.APIGetFriendsList();
        }
        public void OnSpecialEventsButtonClicked()
        {
            //uiManager.apiManager.APIGetSpecialGamesList(); 
            //gameObject.SetActive(false);
            uiManager.specialEventScreenParent.SetActive(true);
        }
        public void OnSpecialEventsButtonCallBack(List<GameMode> eventGmaes)
        {
            gameObject.SetActive(false);
            uiManager.specialEventScreenParent.SetActive(true);
        }

        public void OnProfileButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.profileScreenParent.SetActive(true);
        }

        public void OnSettingsButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.settingsScreenParent.SetActive(true);
        }



        // Start is called before the first frame update
        /*void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            //UpdatePlayerData();
            //UpdatePlayerCoinsData();

        }
        public void ShopButton()
        {
            
        }

        public void OnAvatarButtonClicked()
        {
            uIManager.SubScreenNavigation = UIManager.SubScreens.ProfileDetailsPanel;
            uIManager.ShowScreen(UIManager.Screens.HomeScreen, UIManager.Screens.ProfileScreen);
        }

        public void OnProfileButtonClicked()
        {
            uIManager.SubScreenNavigation = UIManager.SubScreens.ProgressPanel;
            uIManager.ShowScreen(UIManager.Screens.HomeScreen, UIManager.Screens.ProfileScreen);
        }

        public void OnCoinsButtonClicked()
        {
            //uIManager.ShowScreen(this.gameObject, UIManager.Screens.ShopScreenPanel);
        }

        public void OnSettingsButtonClicked()
        {
            uIManager.ShowScreen(UIManager.Screens.HomeScreen, UIManager.Screens.SettingsScreen);
        }*/

        public void OnGiftsButtonClicked()
        {
            gameObject.SetActive(false);
            uiManager.giftsScreenParent.SetActive(true);

        }

        /*//Game modes
        public void OnPlay1On1ButtonClicked()
        {
            uIManager.ShowScreen(UIManager.Screens.HomeScreen, UIManager.Screens.Play1On1Screen);
        }*/

        public void OnPlayWithFriendsButtonClicked()
        {
            //uIManager.ShowScreen(UIManager.Screens.HomeScreen, UIManager.Screens.PlayWithFriendsScreen);
            gameObject.SetActive(false);
            uiManager.playWithFriendsScreenParent.SetActive(true);

        }

        /*public void OnPlayTournamentsButtonClicked()
        {

        }

        public void OnPlaySpecialEventsButtonClicked()
        {

        }

        public void OnPlayLocalButtonClicked()
        {
            uIManager.ShowScreen(UIManager.Screens.HomeScreen, UIManager.Screens.PlayLocalScreen, true);
        }

        public void OnPlayMinigamesButtonClicked()
        {

        }*/

        //Other options
        public void OnCuesButtonClicked()
        {
            gameObject.SetActive(false);
            uiManager.shopScreenParent.SetActive(true);
            uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().HomeCuesButtonClicked();
        }

        public void OnFreeRewardsButtonClicked()
        {
            gameObject.SetActive(true);
            uiManager.freeRewardsScreenParent.SetActive(true);
            uiManager.freeRewardsScreenParent.GetComponent<FreeRewardsScreen>().FreeCoinsButtonEnable();

        }
        public void CoinsUpdate(long coinCount)
        {
            CoinsText.text = (coinCount + long.Parse(CoinsText.text)).ToString();

        }
        public void CoinsSubtract(int coinCount)
        {
            CoinsText.text = (int.Parse(CoinsText.text) - coinCount).ToString();

        }
        public void ProfilePictureUpdate(Sprite avatar)
        {
            profileImage.sprite = avatar;
        }

        public void OnLuckyShotButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.LuckyShotScreenPArent.SetActive(true) ;
            luckyShotBAckGroundImage.color = new Color32(255, 255, 255, 255);
            playMinigamesButtonScreen.SetActive(false);
        }
        public void OnSpinWheelButtonClicked()
        {
            playMinigamesButtonScreen.SetActive(false);
            this.gameObject.SetActive(false);
            uiManager.spinWheelScreenParent.SetActive(true);
            uiManager.spinWheelScreenParent.transform.GetComponent<SpinWheelScreen>().FreeSpinWheelUpdaate();

        }
        public void OnSurpriseBoxButtonClicked()
        {
            playMinigamesButtonScreen.SetActive(false);
            this.gameObject.SetActive(false);
            uiManager.surpriseBoxScreenParent.SetActive(true);
                
        }
        public void OnScratchButtonClickedd()
        {
            playMinigamesButtonScreen.SetActive(false);
            this.gameObject.SetActive(false);
            uiManager.scratchScreenParent.SetActive(true);
        }
        public void OnCloseMinigameButtonScreen()
        {
            playMinigamesButtonScreen.SetActive(false);
        }

        /*public void OnRankingsButtonClicked()
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

        //Fetching player data
        public void UpdatePlayerData()
        {
            NameText.text = GameData.Instance().PlayerName;
            IDText.text = "ID: " + GameData.Instance().PlayerId;
            StarText.text = GameData.Instance().PlayerLevel.ToString();
            ProgressBarImage.fillAmount = 0.5f;
        }

        public void UpdatePlayerCoinsData()
        {
            CoinsText.text = GameData.Instance().PlayerBalance.ToString();
        }

        //Animation handlers
        IEnumerator PopUpPositioning(GameObject toObject = null, string animationName = null)
        {
            toObject.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            toObject.transform.GetComponent<DOTweenAnimation>().DORestartById(animationName);
            toObject.transform.GetComponent<DOTweenAnimation>().DOPlayById(animationName);
            if (animationName == "ScaleDown")
            {
                yield return new WaitForSeconds(0.5f);
                SoundPanel.SetActive(false);
            }
        }*/
    }
}
