using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;
namespace RevolutionGames.UI
{

    public class FreeRewardsScreen : MonoBehaviour
    {
        public UIManager uiManager;
        public GameObject freeRewardsScreen;
        public GameObject freeRewardsMainScreen;
        public GameObject freeRewardsFreeCoinsScreen;
        public GameObject freeRewardsVideoCoins1Screen;
        public GameObject freeRewardsVideoCoins2Screen;
        public GameObject freeRewardsCashScreen;
        public GameObject freeRewardsCashReceivedScreen;
        public Text winCoinText;
        public Text watchingVideoText;
        public Text freeCoinTimeText;
        public GameObject freeCoinButton;
        public GameObject freeCoinCollectButton;





        public void Update()
        {
            if (this.gameObject.activeSelf)
            {
                FreeCoinsButtonEnable();
            }

        }
        public void DailyMissionCloseButton()
        {
            uiManager.freeRewardsScreenParent.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
            
        }
        public void CollectRewardButton()
        {

        }
        public void PlayButtonClicked()
        {
            gameObject.SetActive(false);
            uiManager.playLocalScreenParent.SetActive(true);
        }

        public void OnFreeRewardsCoinButtonClicked()
        {
            //uiManager.apiManager.APIGetFreeRewardsData();
            //OnFreeFreewardsCoinButtonCallBack(30);
            winCoinText.text = "50 Coins";
            watchingVideoText.text = "";
            uiManager.UpdateCoinData(50);
            PlayerPrefs.SetString("freecoinsystime", System.DateTime.Now.ToString());
            freeRewardsVideoCoins2Screen.SetActive(true);
        }
        public void OnFreeFreewardsCoinButtonCallBack(int coinCount)
        {
            //freeRewardsFreeCoinsScreen.SetActive(true);
            //uiManager.apiManager.APICollectFreeCoins(coinCount);
        }
        public void OnFreeRewardsCoinScreenCloseButtonClicked()
        {
           freeRewardsFreeCoinsScreen.SetActive(false);
        }
      
        public void OnVideoRewardsCoinButtonClicked()
        {
            

            //uiManager.apiManager.APIGetVideoAdsListing();
            winCoinText.text = "15 Coins";
            uiManager.UpdateCoinData(15);
            watchingVideoText.text = "for watching a video!";
            freeRewardsVideoCoins2Screen.SetActive(true);


        }
        public void OnVideoRewardsCoinButtonCallBack(List<Video> videos)
        { 
            int videoId = videos[0].video_ads_id;
            freeRewardsVideoCoins2Screen.SetActive(true);
            uiManager.apiManager.APICollectVideoAdsCoins(videoId.ToString());
        }
        public void OnVideoRewardsCoins2CloseButton()
        {
            freeRewardsVideoCoins2Screen.SetActive(false);
            FreeCoinsButtonEnable();
        }
        public void FreeCashButton()
        {

        }
        public void CloseButton()
        {
            freeRewardsMainScreen.SetActive(true);
            freeRewardsFreeCoinsScreen.SetActive(false);
            freeRewardsVideoCoins1Screen.SetActive(false);
            freeRewardsVideoCoins2Screen.SetActive(false);
            freeRewardsCashScreen.SetActive(false);
            freeRewardsCashReceivedScreen.SetActive(false);
        }
        public void WatchVideoButton()
        {

        }
        public void LaterButton()
        {

        }
        public void YesButton()
        {

        }
        public void NeedMoreCoinButton()
        {

        }
        public void NeedMoreCashButton()
        {

        }
        public void FreeCoinsButtonEnable()
        {
            TimeSpan timeDifference;
            if (PlayerPrefs.GetString("freecoinsystime").Length != 0)
            {
                DateTime currentDate = System.DateTime.Now;
                DateTime tempTime = Convert.ToDateTime(PlayerPrefs.GetString("freecoinsystime"));
                long temp = Convert.ToInt64(tempTime.ToBinary());
                DateTime oldDate = DateTime.FromBinary(temp);
                oldDate = oldDate.AddHours(4);
                timeDifference = oldDate.Subtract(currentDate);
                //print("time :" + timeDifference.Hours + "Min :" + timeDifference.Minutes);
                if (timeDifference.Minutes<=0 && timeDifference.Seconds<0)
                {
                    //freeCoinCount = 1; 
                    freeCoinButton.SetActive(true);
                    freeCoinCollectButton.SetActive(false);
                }
                else {
                    freeCoinButton.SetActive(false);
                    freeCoinCollectButton.SetActive(true);
                    freeCoinTimeText.text = "Collect in: \n" +"  "+ timeDifference.Hours + " Hr " + timeDifference.Minutes + " Min ";
                }
            }
        }
    }
}