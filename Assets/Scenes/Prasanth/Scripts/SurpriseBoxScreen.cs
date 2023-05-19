using RevolutionGames.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RevolutionGames.UI
{
    public class SurpriseBoxScreen : MonoBehaviour
    {
        public UIManager uiManager;

        public GameObject surpriseBox;
        public GameObject SurpriseBoxesInfo2Animation1;
        public GameObject surpriseBoxOpen2;
        public GameObject surpriseBoxOpen5;
        public GameObject surpriseBoxOpen5Popup;
        public GameObject surpriseBoxInfo3;
        public GameObject getRareBoxButton;
        public GameObject openRareBoxButton;
        public GameObject getEpicBoxButton;
        public GameObject openEpicBoxButton;
        public GameObject getLegendaryBoxButton;
        public GameObject openLegendaryBoxButton;
        public GameObject buyBoxPopup;
        public GameObject purchaseCompleteEpicBoxPopup;
        public GameObject openButton;
        public GameObject backButton;
        public Image rareBoxImage;
        public Image epicBoxImage;
        public Image legendaryBoxImage;
        public Image perkImage1, perkImage2, perkImage3, perkImage4;

        //public GameObject closeBoxOpenButton;
        public GameObject PopupInfoButton;
        public Text buyBoxNameText;
        public Text buyBoxCoinCount;
        public Text buyBoxOfferCoinCount;
        public Image buyBoxNoramlImage;
        public Image buyBoxOfferImage;
        public Text purchaseCompleteBoxNameText;
        public Image purchaseCompleteBoxImage;
        public Text cueNameText;
        public Image cueStickImage;
        public Image cueStickProgressImage;
        public GameObject lockIcon;
        public GameObject upgradeIcon;
        public Text cueStickProgressText;

        public Text rareBoxCountText;
        public Image rareBoxCountImage;
        public Text epicBoxCountText;
        public Image epicBoxCountImage;
        public Text legendaryBoxCountText;
        public Image legendaryBoxCountImage;
        private string boxName;
        private int openBoxCount;
        private int rareBoxCount;
        private int epicBoxCount;
        private int legendaryBoxCount;


        public Image boxcountImage;
        public Text boxCountText;
        public Image scratchCountImage;
        public Text scratchCountListText;
        public Image spinCountImage;
        public Text spinCountText;

        private int scratchCountList;
        private int spinCountList;
        private int boxCountList;
        private string screenName = "";
        public Transform surpriseBoxInfoScrollContent;
        public Image info1Button;
        public Image info2Button;

        public void Start()
        {
            //PlayerPrefs.SetInt("boxcount", 0);
            // PlayerPrefs.SetInt("rareboxcount",0);
            //PlayerPrefs.SetInt("epicboxcount",0);
            //PlayerPrefs.SetInt("legendaryboxcount",0);

        }
        public void Update()
        {
            if (SurpriseBoxesInfo2Animation1.activeSelf)
            {
               
                if (surpriseBoxInfoScrollContent.localPosition.x < -700)
                {
                   

                    SurpriseBoxInfo2ButtonColorChange();
                }
                else
                {
                  
                    SurpriseBoxInfo1ButtonColorChange();
                }
                   
            }
        }
        public void OnEnable()
        {

            //PlayerPrefs.SetInt("boxcount", 0);
            //PlayerPrefs.SetInt("rareboxcount", 0);
            //PlayerPrefs.SetInt("epicboxcount", 0);
            // PlayerPrefs.SetInt("legendaryboxcount", 0);

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
            if (spinCountList == 0)
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

            //boxCountText.text = PlayerPrefs.GetInt("boxcount").ToString();
            rareBoxCount = PlayerPrefs.GetInt("rareboxcount");
            epicBoxCount = PlayerPrefs.GetInt("epicboxcount");
            legendaryBoxCount = PlayerPrefs.GetInt("legendaryboxcount");
            if (rareBoxCount == 0)
            {

                rareBoxCountImage.gameObject.SetActive(false);
                getRareBoxButton.SetActive(true);
                openRareBoxButton.SetActive(false);
                print("rare if");
            }
            else
            {
                rareBoxCountImage.gameObject.SetActive(true);
                rareBoxCountText.text = rareBoxCount.ToString();
                getRareBoxButton.SetActive(false);
                openRareBoxButton.SetActive(true);
                print(" rare else");
            }
            if (epicBoxCount == 0)
            {
                epicBoxCountImage.gameObject.SetActive(false);
                getEpicBoxButton.SetActive(true);
                openEpicBoxButton.SetActive(false);

            }
            else
            {
                epicBoxCountImage.gameObject.SetActive(true);
                epicBoxCountText.text = epicBoxCount.ToString();
                getEpicBoxButton.SetActive(false);
                openEpicBoxButton.SetActive(true);
            }
            if (legendaryBoxCount == 0)
            {
                legendaryBoxCountImage.gameObject.SetActive(false);
                getLegendaryBoxButton.SetActive(true);
                openLegendaryBoxButton.SetActive(false);
            }
            else
            {
                legendaryBoxCountImage.gameObject.SetActive(true);
                legendaryBoxCountText.text = legendaryBoxCount.ToString();
                getLegendaryBoxButton.SetActive(false);
                openLegendaryBoxButton.SetActive(true);
            }
        }
        public void GetRareBoxButton()
        {
            buyBoxNameText.text = "Buy Rare Box?";
            buyBoxNoramlImage.sprite = rareBoxImage.sprite;
            buyBoxOfferImage.sprite = rareBoxImage.sprite;
            buyBoxCoinCount.text = "5";
            buyBoxOfferCoinCount.text = "14";
            buyBoxPopup.SetActive(true);
            boxName = "Rare";

        }
        public void GetEpicBoxButtonClicked()
        {
            buyBoxNameText.text = "Buy Epic Box?";
            buyBoxNoramlImage.sprite = epicBoxImage.sprite;
            buyBoxOfferImage.sprite = epicBoxImage.sprite;
            buyBoxCoinCount.text = "16";
            buyBoxOfferCoinCount.text = "44";
            buyBoxPopup.SetActive(true);
            boxName = "Epic";
        }

        public void GetLegendaryBoxButtonClickedd()
        {
            buyBoxNameText.text = "Buy Legendary Box?";
            buyBoxNoramlImage.sprite = legendaryBoxImage.sprite;
            buyBoxOfferImage.sprite = legendaryBoxImage.sprite;
            buyBoxCoinCount.text = "33";
            buyBoxOfferCoinCount.text = "89";
            buyBoxPopup.SetActive(true);
            boxName = "Legendary";
        }

        public void Buy1BoxButtonClicked()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 1);
            boxcountImage.gameObject.SetActive(true);
            boxCountText.text = PlayerPrefs.GetInt("boxcount").ToString();
            if (boxName == "Rare")
            {
                int rareCount = PlayerPrefs.GetInt("rareboxcount");
                PlayerPrefs.SetInt("rareboxcount", rareCount + 1);
                rareBoxCountText.text = PlayerPrefs.GetInt("rareboxcount").ToString();
                //rareBoxCount = 1;
                //rareBoxCountText.text = rareBoxCount.ToString();
                rareBoxCountImage.gameObject.SetActive(true);
                purchaseCompleteBoxNameText.text = "Rare Box";
                purchaseCompleteBoxImage.sprite = buyBoxNoramlImage.sprite;
                purchaseCompleteEpicBoxPopup.SetActive(true);
                getRareBoxButton.SetActive(false);
                openRareBoxButton.SetActive(true);
                uiManager.SubtractCoinData(5.ToString());
                //PlayerPrefs.SetInt("boxcount", openBoxCount + 1);

            }
            else if (boxName == "Epic")
            {
                int epicCount = PlayerPrefs.GetInt("epicboxcount");
                PlayerPrefs.SetInt("epicboxcount", epicCount + 1);
                epicBoxCountText.text = PlayerPrefs.GetInt("epicboxcount").ToString();
                //epicBoxCount = 1;
                //epicBoxCountText.text = epicBoxCount.ToString();
                epicBoxCountImage.gameObject.SetActive(true);
                purchaseCompleteBoxNameText.text = "Epic Box";
                purchaseCompleteBoxImage.sprite = buyBoxNoramlImage.sprite;
                purchaseCompleteEpicBoxPopup.SetActive(true);
                getEpicBoxButton.SetActive(false);
                openEpicBoxButton.SetActive(true);
                uiManager.SubtractCoinData(16.ToString());
                //PlayerPrefs.SetInt("boxcount", openBoxCount + 1);
            }
            else if (boxName == "Legendary")
            {
                int legendaryCount = PlayerPrefs.GetInt("legendaryboxcount");
                PlayerPrefs.SetInt("legendaryboxcount", legendaryCount + 1);
                legendaryBoxCountText.text = PlayerPrefs.GetInt("legendaryboxcount").ToString();
                //legendaryBoxCount = 1;
                //legendaryBoxCountText.text = legendaryBoxCount.ToString();
                legendaryBoxCountImage.gameObject.SetActive(true);
                purchaseCompleteBoxNameText.text = "Legendary";
                purchaseCompleteBoxImage.sprite = buyBoxNoramlImage.sprite;
                purchaseCompleteEpicBoxPopup.SetActive(true);
                getLegendaryBoxButton.SetActive(false);
                openLegendaryBoxButton.SetActive(true);
                uiManager.SubtractCoinData(33.ToString());
                //PlayerPrefs.SetInt("boxcount", openBoxCount + 1);
            }

        }
        public void Buy2BoxButtonClicked()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 3);
            boxcountImage.gameObject.SetActive(true);
            boxCountText.text = PlayerPrefs.GetInt("boxcount").ToString();
            if (boxName == "Rare")
            {
                int rareCount = PlayerPrefs.GetInt("rareboxcount");
                PlayerPrefs.SetInt("rareboxcount", rareCount + 3);
                rareBoxCountText.text = PlayerPrefs.GetInt("rareboxcount").ToString();
                //rareBoxCount = 3;
                //rareBoxCountText.text = rareBoxCount.ToString();
                rareBoxCountImage.gameObject.SetActive(true);
                purchaseCompleteBoxNameText.text = "Rare Box";
                purchaseCompleteBoxImage.sprite = buyBoxOfferImage.sprite;
                purchaseCompleteEpicBoxPopup.SetActive(true);
                getRareBoxButton.SetActive(false);
                openRareBoxButton.SetActive(true);
                uiManager.SubtractCoinData(14.ToString());
            }
            else if (boxName == "Epic")
            {
                int epicCount = PlayerPrefs.GetInt("epicboxcount");
                PlayerPrefs.SetInt("epicboxcount", epicCount + 3);
                epicBoxCountText.text = PlayerPrefs.GetInt("epicboxcount").ToString();
                //epicBoxCount = 3;
                //epicBoxCountText.text = epicBoxCount.ToString();
                epicBoxCountImage.gameObject.SetActive(true);
                purchaseCompleteBoxNameText.text = "Epic Box";
                purchaseCompleteBoxImage.sprite = buyBoxOfferImage.sprite;
                purchaseCompleteEpicBoxPopup.SetActive(true);
                getEpicBoxButton.SetActive(false);
                openEpicBoxButton.SetActive(true);
                uiManager.SubtractCoinData(44.ToString());
            }
            else if (boxName == "Legendary")
            {

                int legendaryCount = PlayerPrefs.GetInt("legendaryboxcount");
                PlayerPrefs.SetInt("legendaryboxcount", legendaryCount + 3);
                legendaryBoxCountText.text = PlayerPrefs.GetInt("legendaryboxcount").ToString();
                //legendaryBoxCount = 3;
                //legendaryBoxCountText.text = legendaryBoxCount.ToString();
                legendaryBoxCountImage.gameObject.SetActive(true);
                purchaseCompleteBoxNameText.text = "Legendary";
                purchaseCompleteBoxImage.sprite = buyBoxOfferImage.sprite;
                purchaseCompleteEpicBoxPopup.SetActive(true);
                getLegendaryBoxButton.SetActive(false);
                openLegendaryBoxButton.SetActive(true);
                uiManager.SubtractCoinData(89.ToString());
            }

        }

        public void CloseBoxPopupButton()
        {
            buyBoxPopup.SetActive(false);
            purchaseCompleteEpicBoxPopup.SetActive(false);
        }

        public void RareBoxOpenButton()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count - 1);
            boxCountText.text = PlayerPrefs.GetInt("boxcount").ToString();
            int rareCount = PlayerPrefs.GetInt("rareboxcount");
            PlayerPrefs.SetInt("rareboxcount", rareCount - 1);
            rareBoxCountText.text = PlayerPrefs.GetInt("rareboxcount").ToString();
            //rareBoxCount = rareBoxCount - 1;
            //rareBoxCountText.text = rareBoxCount.ToString();
            //surpriseBox.SetActive(false);
            buyBoxPopup.SetActive(false);
            purchaseCompleteEpicBoxPopup.SetActive(false);
            surpriseBoxOpen5.SetActive(true);
            StartCoroutine(OpenPerkImages());

            int idx = uiManager.dataManager.GetRareCueStickIndex();
            cueNameText.text = uiManager.dataManager.GetRareCueStickName(idx);
            cueStickImage.sprite = uiManager.dataManager.GetRareCueStickImage(idx);
            uiManager.dataManager.UpdateSurpriseCueStickLevel(idx);
            uiManager.UpdateSurpriseCuesList();

            if (uiManager.dataManager.GetRareCueStickIsUnlockedFlag(idx) == 1)
            {
                cueStickProgressText.text = uiManager.dataManager.GetRareCueStickCurrentSubLevel(idx) + "/" + (float)uiManager.dataManager.GetRareCueStickMaxSubLevel(idx);
                cueStickProgressImage.fillAmount = uiManager.dataManager.GetRareCueStickCurrentSubLevel(idx) / (float)uiManager.dataManager.GetRareCueStickMaxSubLevel(idx);
                lockIcon.SetActive(false);
                upgradeIcon.SetActive(true);
            }
            else
            {
                cueStickProgressText.text = uiManager.dataManager.GetRareCueStickUnlockedPieces(idx) + "/4";
                cueStickProgressImage.fillAmount = uiManager.dataManager.GetRareCueStickUnlockedPieces(idx) / (float)4;
                lockIcon.SetActive(true);
                upgradeIcon.SetActive(false);
            }
            if (PlayerPrefs.GetInt("rareboxcount") == 0)
            {
                getRareBoxButton.SetActive(true);
                openRareBoxButton.SetActive(false);
                rareBoxCountImage.gameObject.SetActive(false);
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

        public void EpicBoxOpenButton()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count - 1);
            boxCountText.text = PlayerPrefs.GetInt("boxcount").ToString();
            int epicCount = PlayerPrefs.GetInt("epicboxcount");
            PlayerPrefs.SetInt("epicboxcount", epicCount - 1);
            epicBoxCountText.text = PlayerPrefs.GetInt("epicboxcount").ToString();
            //epicBoxCount = epicBoxCount - 1;
            //epicBoxCountText.text = epicBoxCount.ToString();
            //surpriseBox.SetActive(false);

            buyBoxPopup.SetActive(false);
            purchaseCompleteEpicBoxPopup.SetActive(false);
            surpriseBoxOpen5.SetActive(true);
            StartCoroutine(OpenPerkImages());

            int idx = uiManager.dataManager.GetEpicCueStickIndex();
            cueNameText.text = uiManager.dataManager.GetEpicCueStickName(idx);
            cueStickImage.sprite = uiManager.dataManager.GetEpicCueStickImage(idx);
            uiManager.dataManager.UpdateSurpriseCueStickLevel(idx);
            uiManager.UpdateSurpriseCuesList();

            if (uiManager.dataManager.GetLegendaryCueStickIsUnlockedFlag(idx) == 1)
            {
                cueStickProgressImage.fillAmount = uiManager.dataManager.GetLegendaryCueStickCurrentSubLevel(idx) / (float)uiManager.dataManager.GetLegendaryCueStickMaxSubLevel(idx);
                lockIcon.SetActive(false);
                upgradeIcon.SetActive(true);
            }
            else
            {
                cueStickProgressImage.fillAmount = uiManager.dataManager.GetLegendaryCueStickUnlockedPieces(idx) / (float)4;
                lockIcon.SetActive(true);
                upgradeIcon.SetActive(false);
            }

            if (PlayerPrefs.GetInt("epicboxcount") == 0)
            {
                getEpicBoxButton.SetActive(true);
                openEpicBoxButton.SetActive(false);
                epicBoxCountImage.gameObject.SetActive(false);
            }

        }
        public void LegendaryBoxOpenButton()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count - 1);
            boxCountText.text = PlayerPrefs.GetInt("boxcount").ToString();
            int legendaryCount = PlayerPrefs.GetInt("legendaryboxcount");
            PlayerPrefs.SetInt("legendaryboxcount", legendaryCount - 1);
            legendaryBoxCountText.text = PlayerPrefs.GetInt("legendaryboxcount").ToString();
            //legendaryBoxCount = legendaryBoxCount - 1;
            //legendaryBoxCountText.text = legendaryBoxCount.ToString();
            //surpriseBox.SetActive(false);

            buyBoxPopup.SetActive(false);
            purchaseCompleteEpicBoxPopup.SetActive(false);
            surpriseBoxOpen5.SetActive(true);
            StartCoroutine(OpenPerkImages());

            int idx = uiManager.dataManager.GetLegendaryCueStickIndex();
            cueNameText.text = uiManager.dataManager.GetLegendaryCueStickName(idx);
            cueStickImage.sprite = uiManager.dataManager.GetLegendaryCueStickImage(idx);
            uiManager.dataManager.UpdateSurpriseCueStickLevel(idx);
            uiManager.UpdateSurpriseCuesList();

            if (uiManager.dataManager.GetLegendaryCueStickIsUnlockedFlag(idx) == 1)
            {
                cueStickProgressImage.fillAmount = uiManager.dataManager.GetLegendaryCueStickCurrentSubLevel(idx) / (float)uiManager.dataManager.GetLegendaryCueStickMaxSubLevel(idx);
                lockIcon.SetActive(false);
                upgradeIcon.SetActive(true);
            }
            else
            {
                cueStickProgressImage.fillAmount = uiManager.dataManager.GetLegendaryCueStickUnlockedPieces(idx) / (float)4;
                lockIcon.SetActive(true);
                upgradeIcon.SetActive(false);
            }


            if (PlayerPrefs.GetInt("legendaryboxcount") == 0)
            {
                getLegendaryBoxButton.SetActive(true);
                openLegendaryBoxButton.SetActive(false);
                legendaryBoxCountImage.gameObject.SetActive(false);
            }

        }


        public void BackButton()
        {

            surpriseBox.SetActive(true);
            surpriseBoxOpen2.SetActive(false);
        }
        public void CloseBoxOpenButton()
        {
            surpriseBox.SetActive(true);
            SurpriseBoxesInfo2Animation1.SetActive(false);
            surpriseBoxInfo3.SetActive(false);
            surpriseBoxOpen5.SetActive(false);


        }
        public void PopupOpenButton()
        {
            //surpriseBoxOpen5.SetActive(false);
            surpriseBoxOpen5Popup.SetActive(true);
        }
        public void PopupCloseButton()
        {
            //surpriseBoxOpen5.SetActive(true);
            surpriseBoxOpen5Popup.SetActive(false);
        }

        public void OnSpinButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.spinWheelScreenParent.SetActive(true);
        }

        public void OnScratchButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.scratchScreenParent.SetActive(true);
        }
        public void SurpriseBoxPlayMinigame()
        {
            screenName = "shop surprise boxs screen";

        }
        public void ScratchersPlayMinigame()
        {
            screenName = "shop scratchers screen";
        }

        public void OnCloseMiniGamesButtonClicked()
        {
            if (screenName == "shop surprise boxs screen")
            {
                print("surprise");
                screenName = "";
                this.gameObject.SetActive(false);
                uiManager.shopScreenParent.SetActive(true);
                uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().OnSurpriseBoxButtonClicked();

            }
            else if (screenName == "shop scratchers screen")
            {
                screenName = "";
                this.gameObject.SetActive(false);
                uiManager.shopScreenParent.SetActive(true);
                uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().ScratchersButton();
            }
            else
            {
                uiManager.homeScreenParent.SetActive(true);
                uiManager.spinWheelScreenParent.SetActive(false);
                uiManager.surpriseBoxScreenParent.SetActive(false);
                uiManager.scratchScreenParent.SetActive(false);
            }

        }
        public void SurpriseBoxInfoButtonClicked()
        {
            SurpriseBoxesInfo2Animation1.SetActive(true);
            SurpriseBoxInfo1ButtonClicked();
        }
        public void surpriseBoxInfoCloseButton()
        {
            SurpriseBoxesInfo2Animation1.SetActive(false);
        }
        public void GetMoreButtonClicked()
        {
            surpriseBoxOpen5.SetActive(false);
            buyBoxPopup.SetActive(true);
        }
        public void SurpriseBoxInfo1ButtonClicked()
        {
            surpriseBoxInfoScrollContent.localPosition = new Vector2(-3.189795e-06f, 2.5749e-05f);
            info1Button.color = new Color32(255,255,255,255);
            info2Button.color = new Color32(255,255,255,100);

        }
        public void SurpriseBoxInfo2ButtonClicked()
        {
            info1Button.color = new Color32(255, 255, 255, 100);
            info2Button.color = new Color32(255, 255, 255, 255);
            surpriseBoxInfoScrollContent.localPosition = new Vector2(-1418.543f, 2.5749e-05f);
        }
        public void SurpriseBoxInfo1ButtonColorChange()
        {
           
            info1Button.color = new Color32(255, 255, 255, 255);
            info2Button.color = new Color32(255, 255, 255, 100);
        }
        public void SurpriseBoxInfo2ButtonColorChange()
        {
           
            info1Button.color = new Color32(255, 255, 255, 100);
            info2Button.color = new Color32(255, 255, 255, 255);
        }



    }
}
