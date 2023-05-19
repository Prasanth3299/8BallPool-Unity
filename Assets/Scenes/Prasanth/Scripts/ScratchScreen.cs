using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RevolutionGames.UI
{
    public class ScratchScreen : MonoBehaviour
    {
        public UIManager uiManager;

        public GameObject scratchGuard;
        public GameObject scratchWin;
        public GameObject scratchUpto;
        public GameObject moreScratchersWinPopup;
        public int numberOfScratchers;
        public int numberOfButtons;
        public GameObject[] scratchButtons;
        public Text[] scratchRewards;
        ArrayList rewards = new ArrayList();
        ArrayList rewardsSwap = new ArrayList();
        List<string> scratchtext = new List<string>();
        //
       // List<GameObject> buttons=new List<GameObject>();
        private string[] final = new string[12];
        private int count = 0;
        private int valueOfIndex;
        public Text scratchCountText;

        public Image boxcountImage;
        public Text boxCountText;
        public Image scratchCountImage;
        public Text scratchCountListText;
        public Image spinCountImage;
        public Text spinCountText;
        private int scratchCount;

        private int scratchCountList;
        private int spinCountList;
        private int boxCountList;
        private string screenName = "";
        public void Start()
        {
           
            ScratchRewards();
            //boxCountText.text = PlayerPrefs.GetInt("boxcount").ToString();
            //scratchCountText.text = PlayerPrefs.GetInt("scratchcount").ToString();
            //scratchCountListText.text= PlayerPrefs.GetInt("scratchcount").ToString();
            //spinCountText.text = PlayerPrefs.GetInt("spincount").ToString();

        }
        public void OnEnable()
        {

            //PlayerPrefs.SetInt("scratchers", 0);
            scratchCountText.text = PlayerPrefs.GetInt("scratchcount").ToString();
            int count = PlayerPrefs.GetInt("scratchers");
            
            scratchCountList = PlayerPrefs.GetInt("scratchcount");
            boxCountList = PlayerPrefs.GetInt("boxcount");
            spinCountList = PlayerPrefs.GetInt("spincount");
            

            if (scratchCountList == 0)
            {
                scratchCountImage.gameObject.SetActive(false);
            }
            else
            {
                scratchCountImage.gameObject.SetActive(true);
                scratchCountListText.text = scratchCountList.ToString();
            }
            if(spinCountList==0)
            {
                spinCountImage.gameObject.SetActive(false);
            }
            else
            {
                spinCountImage.gameObject.SetActive(true);
                spinCountText.text = spinCountList.ToString();

            }
            if (boxCountList == 0)
            {
                boxcountImage.gameObject.SetActive(false);
            }
            else
            {
                boxcountImage.gameObject.SetActive(true);
                boxCountText.text = boxCountList.ToString();
            }

            for (int i = 0; i < numberOfButtons; i++)
            {
                print("on enable" + PlayerPrefs.GetInt("scratchbutton" + i));
               //PlayerPrefs.SetInt("scratchbutton"+i, 0);
                if (PlayerPrefs.GetInt("scratchbutton"+i) == 0)
                {

                    scratchButtons[i].SetActive(true);
                }

                else
                {
                    scratchButtons[i].SetActive(false);
                }
            }
        }
        public void ScratchRewards()
        {
            rewards.Add("100");
            rewards.Add("200");
            rewards.Add("300");
            rewards.Add("400");
            rewards.Add("500");
            rewards.Add("600");
            rewards.Add("700");
            rewards.Add("800");
            rewards.Add("900");
            rewards.Add("1000");
            rewards.Add("2000");
            rewards.Add("3000");
            for (int i = 0; i < 12; i++)
            {
                rewardsSwap.Add("0");
            }

            valueOfIndex = Random.Range(0, rewards.Count);

            int range1 = Random.Range(0, rewardsSwap.Count);
            rewardsSwap[range1] = rewards[valueOfIndex].ToString();
            int range2 = Random.Range(0, rewardsSwap.Count);
            while (range2 == range1)
            {
                range2 = Random.Range(0, rewardsSwap.Count);
            }
            rewardsSwap[range2] = rewards[valueOfIndex].ToString();
            int range3 = Random.Range(0, rewardsSwap.Count);
            while ((range3 == range1) || (range3 == range2))
            {
                range3 = Random.Range(0, rewardsSwap.Count);
            }
            rewardsSwap[range3] = rewards[valueOfIndex].ToString();
            rewards.RemoveAt(valueOfIndex);

            for (int j = 0; j < rewardsSwap.Count; j++)
            {
                if (rewardsSwap[j].Equals("0"))
                {
                    valueOfIndex = Random.Range(0, rewards.Count);
                    rewardsSwap[j] = rewards[valueOfIndex].ToString();
                    rewards.RemoveAt(valueOfIndex);

                }
            }
            for (int k = 0; k < rewardsSwap.Count; k++)
            {
                print("text" + k);
                scratchRewards[k].text = rewardsSwap[k].ToString();
                //PlayerPrefs.SetString("scratchtext" + k, rewardsSwap[k].ToString());
                string value = rewardsSwap[k].ToString();
                scratchtext.Add(value);
            }

        }


        public void ScratchButton(GameObject scratchObj)
        {
            scratchCount = PlayerPrefs.GetInt("scratchcount");
            print("scjopsd"+scratchCount);
            if(scratchCount>0)
            {
                int count = PlayerPrefs.GetInt("scratchers");
                print("count"+ count);
                scratchCount = scratchCount - 1;
                scratchCountText.text = scratchCount.ToString();
                scratchCountListText.text= scratchCount.ToString();
                PlayerPrefs.SetInt("scratchcount", scratchCount);
                scratchObj.SetActive(false);
                for (int i = 0; i < numberOfButtons; i++)
                {
                    if (scratchButtons[i] == scratchObj)
                    {
                        PlayerPrefs.SetInt("scratchbutton"+i, 1);
                        
                        final[i] = scratchRewards[i].text;
                        PlayerPrefs.SetInt("scratchers", count+1);
                        print("increment");
                        //count++;
                    }
                }
                if (PlayerPrefs.GetInt("scratchers") == 12)
                {
                    print("final");
                    for (int i = 0; i < final.Length; i++)
                    {
                        for (int j = i + 1; j < final.Length; j++)
                        {
                            for (int k = j + 1; k < final.Length; k++)
                            {
                                if (final[i] == final[j] && final[i] == final[k])
                                {
                                    //count = 0;

                                    
                                    PlayerPrefs.SetInt("scratchers", 0);
                                    scratchWin.SetActive(true);
                                    scratchUpto.SetActive(false);
                                    print("k valuree"+ final[k]);
                                   scratchWin.GetComponent<Text>().text = final[k];
                                    //uiManager.UpdateCoinData(final[k]);

                                    
                                    StartCoroutine(ScratchButtonEnable());
                                    
                                }
                            }
                        }
                    }
                }
            }
            
        }
        public IEnumerator ScratchButtonEnable()
        {
            yield return new WaitForSeconds(5);
            scratchWin.SetActive(false);
            scratchUpto.SetActive(true);
            //ScratchRewards();

            for (int i = 0; i < scratchButtons.Length; i++)
            {
                PlayerPrefs.SetInt("scratchbutton"+i, 0);
                scratchButtons[i].SetActive(true);
            }
        }
        public void OnSpinButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.spinWheelScreenParent.SetActive(true);
        }

        
        public void OnBoxesButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.surpriseBoxScreenParent.SetActive(true);
        }

        public void ScratchersPlayMinigame()
        {
            screenName = "shop scratchers screen";
        }
        public void SurpriseBoxPlayMinigame()
        {
            screenName = "shop surprise boxs screen";

        }

        public void OnCloseMiniGamesButtonClicked()
        {
            if (screenName == "shop scratchers screen")
            {
                screenName = "";
                this.gameObject.SetActive(false);
                uiManager.shopScreenParent.SetActive(true);
                uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().ScratchersButton();
            }
            else if(screenName == "shop surprise boxs screen")
            {
                print("surprise");
                screenName = "";
                this.gameObject.SetActive(false);
                uiManager.shopScreenParent.SetActive(true);
                uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().OnSurpriseBoxButtonClicked();

            }
            else
            {
                uiManager.homeScreenParent.SetActive(true);
                uiManager.spinWheelScreenParent.SetActive(false);
                uiManager.surpriseBoxScreenParent.SetActive(false);
                uiManager.scratchScreenParent.SetActive(false);
            }
            
            
        }
        public void GetMoreScratchButton()
        {
          
            this.gameObject.SetActive(false);
            uiManager.shopScreenParent.SetActive(true);
        }
        public void GetMoreScratchersButtonClicked()
        {
            
            int count = PlayerPrefs.GetInt("scratchcount");
            PlayerPrefs.SetInt("scratchcount", count + 5);
            moreScratchersWinPopup.SetActive(true);
            OnEnable();
        }
        public void GetMoreScratchersPopupCloseButton()
        {
            moreScratchersWinPopup.SetActive(false);
        }
    }
}


