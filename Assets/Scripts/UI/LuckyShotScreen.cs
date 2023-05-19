using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace RevolutionGames.UI
{

    public class LuckyShotScreen : MonoBehaviour
    {

        public UIManager uiManager;
        public GameObject freeLuckyShotScreen;
        public GameObject paidLuckyShotScreen;
        public Text playButtonText,countText;
        public GameObject countImage;
        public GameObject PaidButton;
        public GameObject playButton;
        public GameObject purchasesPopup;
        public GameObject luckyShotTimeImage;
        private int luckyShotcount;
        private int luckyShotFreecount;
        private int totalCount;
        // Start is called before the first frame update
        void Start()
        {
            //LuckyShotUpdate();
        }
        public void OnEnable()
        {

            luckyShotcount = PlayerPrefs.GetInt("lucky shot count");
            luckyShotFreecount= PlayerPrefs.GetInt("luckyshotfreecount");
            totalCount = luckyShotcount + luckyShotFreecount;
            LuckyShotCountUpdate();
            if (totalCount > 0)
            {
                playButton.SetActive(true);
                PaidButton.SetActive(false);
                countImage.SetActive(true);
                countText.text = totalCount.ToString();

            }
            else
            {
                PaidButton.SetActive(true);
                playButton.SetActive(false);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void LuckyShotUpdate()
        {
            this.gameObject.SetActive(true);
            freeLuckyShotScreen.SetActive(true);
            paidLuckyShotScreen.SetActive(false);

        }

        public void LuckyShotCountUpdate()
        {

            luckyShotcount = PlayerPrefs.GetInt("lucky shot count");
            TimeSpan timeDifference;
            if (PlayerPrefs.GetString("lucky shot systime").Length != 0)
            {
                DateTime currentDate = System.DateTime.Now;
                DateTime tempTime = Convert.ToDateTime(PlayerPrefs.GetString("lucky shot systime"));
                long temp = Convert.ToInt64(tempTime.ToBinary());
                DateTime oldDate = DateTime.FromBinary(temp);
                oldDate = oldDate.AddDays(1);
                timeDifference = oldDate.Subtract(currentDate);
                //print(timeDifference.Minutes);
                //print(timeDifference.Seconds);
                //print("time :" + timeDifference.Hours + "Min :" + timeDifference.Minutes);
                if (timeDifference.Minutes <= 0 && timeDifference.Seconds < 0)
                {
                  
                    this.gameObject.SetActive(true);
                    freeLuckyShotScreen.SetActive(true);
                    paidLuckyShotScreen.SetActive(false);
                    // luckyShotCount += 1;
                }
                else
                {
                    freeLuckyShotScreen.SetActive(false);
                    paidLuckyShotScreen.SetActive(true);
                }

                //PlayerPrefs.SetInt("lucky shot count", luckyShotCount);
            }

            //print("luckyShotCount" + luckyShotCount);
           /* if (luckyShotCount > 0)
            {
                this.gameObject.SetActive(true);
                freeLuckyShotScreen.SetActive(true);
                paidLuckyShotScreen.SetActive(false);
            }
            else
            {
                //this.gameObject.SetActive(true);
                paidLuckyShotScreen.SetActive(true);
                freeLuckyShotScreen.SetActive(false);
            }*/
        }
            public void OnPlayFreeButtonClicked()
        {
            PlayerPrefs.SetInt("lucky shot show", 1);
            GameData.Instance().ToScreenName = "LuckyShot";
            //PlayerPrefs.SetString("lucky shot systime", System.DateTime.Now.ToString());
            luckyShotTimeImage.SetActive(false);
            //this.gameObject.SetActive(false);
            //luckyShotFreecount -= 1;
            //PlayerPrefs.SetInt("lucky shot freecount", luckyShotFreecount);

            //int luckyShotCount1 = PlayerPrefs.GetInt("lucky shot count");
            //luckyShotCount1 = luckyShotCount1 - 1;

           // countText.text = luckyShotCount1.ToString();
            //PlayerPrefs.SetInt("lucky shot count", luckyShotCount1);
            //print("luckyShotCount" + luckyShotCount);
            SceneManager.LoadScene("LuckyShotScene");
           // this.gameObject.SetActive(false);
           
           

        }
        public void OnPlayPaidButtonClicked()
        {
            GameData.Instance().ToScreenName = "LuckyShot";
            SceneManager.LoadScene("LuckyShotScene");
            int  luckyShotCount1 = PlayerPrefs.GetInt("lucky shot count");
           
           //.luckyShotCount1 = luckyShotCount1-1;
           
            countText.text = luckyShotCount1.ToString();
            //PlayerPrefs.SetInt("lucky shot count", luckyShotCount1);
            //this.gameObject.SetActive(false);
        }
        public void OnPaidLuckyShotCloseButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }
        public void OnFreeLuckyShotCloseButtonClicked()
        {
            PlayerPrefs.SetInt("lucky shot show", 1);
            this.gameObject.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }
        public void OnPaidButtonClicked()
        {
            luckyShotcount = PlayerPrefs.GetInt("lucky shot count");
            luckyShotcount += 3;
            countText.text = luckyShotcount.ToString();
            PlayerPrefs.SetInt("lucky shot count", luckyShotcount);
            purchasesPopup.SetActive(true);
            playButton.SetActive(true);
            PaidButton.SetActive(false);
        }
        public void OnPurchaseCloseButtonClicked()
        {
            purchasesPopup.SetActive(false);
        }
    }
}
