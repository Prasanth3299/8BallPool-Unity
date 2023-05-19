using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;
using System;

namespace RevolutionGames.UI
{


    public class SpinWheelScreen : MonoBehaviour
    {
        public UIManager uiManager;
        int randval;
        private float timeInterval;
        private bool spinWheelCoroutine;
        private int freeSpinWheelFinalAngle;
        private int paidSpinWheelFinalAngle;
        public Text freeWinText;
        public Text paidWinText;
        public int freeSpinWheeelSection;
        public int paidSpinWheelSection;
        float freeSpinWheelTotalAngle;
        float paidSpinWheelTotalAngle;
        public Text[] freeSpinRewards;
        public Text[] paidSpinRewards;
        public GameObject spinWheelPopup;
        public GameObject freeSpinWheel;
        public GameObject paidSpinWheel;
        public Transform freeSpinWheelCircle;
        public Transform paidSpinWheelCircle;
        public GameObject surpriseBoxScreen;
        public GameObject spinWheelScreen;
        public GameObject scratchScreen;
        public GameObject paidSpinWheelPopup;
        public Text freeSpinCount;
        public GameObject freeSpinArrowImage;
        public GameObject paidSpinArrowImage;
        public Text freeSpinTimeText;
        public Text FreeWheelBoxCountText;
        public Text PaidWhaeelBoxCountText;

        public Image boxcountImage;
        public Text boxCountText;
        public Image scratchCountImage;
        public Text scratchCountListText;
        public Image spinCountImage;
        public Text spinCountText;
        public Image boxcountImagePaid;
        public Text boxCountTextPaid;
        public Image scratchCountImagePaid;
        public Text scratchCountListTextPaid;
        public Image spinCountImagePaid;
        public Text spinCountTextPaid;
        public Image spinBackGroundImage;
        public Image scratchBackGroundImage;
        public Image surpriseBackGroundImage;

        private int scratchCountList;
        private int spinCountList;
        private int boxCountList;
        private string screenName = "";

        //public bool freeSpinbutton = false;

        // Start is called before the first frame update




        void Start()
        {


            //int spinCount = PlayerPrefs.GetInt("spincount");
            spinWheelCoroutine = true;
            freeSpinWheelTotalAngle = 360 / freeSpinWheeelSection;
            paidSpinWheelTotalAngle = 360 / paidSpinWheelSection;

        }
        public void OnEnable()
        {

            FreeSpinWheelUpdaate();

            scratchCountList = PlayerPrefs.GetInt("scratchcount");
            boxCountList = PlayerPrefs.GetInt("boxcount");
            spinCountList = PlayerPrefs.GetInt("spincount");


            if (scratchCountList == 0)
            {
                scratchCountImage.gameObject.SetActive(false);
                scratchCountImagePaid.gameObject.SetActive(false);
            }
            else
            {
                scratchCountImage.gameObject.SetActive(true);
                scratchCountImagePaid.gameObject.SetActive(true);
                scratchCountListText.text = scratchCountList.ToString();
                scratchCountListTextPaid.text = scratchCountList.ToString();
            }
            if (spinCountList == 0)
            {
                spinCountImage.gameObject.SetActive(false);
                spinCountImagePaid.gameObject.SetActive(false);
            }
            else
            {
                spinCountImage.gameObject.SetActive(true);
                spinCountImagePaid.gameObject.SetActive(true);
                spinCountText.text = spinCountList.ToString();
                spinCountTextPaid.text = spinCountList.ToString();

            }
            if (boxCountList == 0)
            {
                boxcountImage.gameObject.SetActive(false);
                boxcountImagePaid.gameObject.SetActive(false);
            }
            else
            {
                boxcountImage.gameObject.SetActive(true);
                boxcountImagePaid.gameObject.SetActive(true);
                boxCountText.text = boxCountList.ToString();
                boxCountTextPaid.text = boxCountList.ToString();
            }
        }
        public void Update()
        {
            //SpinWheelScreenUpdate();
            if (paidSpinWheelPopup.activeSelf)
            {
                SpinWheelScreenUpdate();
            }
        }

        // Update is called once per frame
        public void OnSpinButtonClicked()
        {
            //uiManager.apiManager.APIFreeSpinData();

            //if (Input.GetMouseButtonDown(0) && isCoroutine)
            // {
            // StartCoroutine(FreeSpin());
            //}

        }
        public void OnSpinButtonCallBack(List<SpinData> freeSpinData)
        {
            for (int i = 0; i < freeSpinData.Count; i++)
            {
                for (int j = 0; j < freeSpinRewards.Length; i++)
                {
                    if (i == j)
                    {
                        freeSpinRewards[j].text = freeSpinData[i].ToString();
                    }
                }
            }
        }
        public void OnStartFreeSpinButtonClicked(string spinId)
        {
            print("Calling spin");
            //uiManager.apiManager.APIStartFreeSpin( spinId);
            OnStartFreeSpinButtonCallBack();
        }
        public void OnStartFreeSpinButtonCallBack()
        {
            PlayerPrefs.SetInt("spin wheel show", 0);
            print("spinefnwe" + PlayerPrefs.GetInt("spin wheel show"));
            int spinCount = PlayerPrefs.GetInt("spincount");
            print("call back");
            if (spinCount == 1)
            {
                PlayerPrefs.SetString("systime", System.DateTime.Now.ToString());
                print("systime" + System.DateTime.Now.ToString());

            }

            spinCount = spinCount - 1;
            PlayerPrefs.SetInt("spincount", spinCount);
            print("spinCount" + spinCount);
            FreeSpinCountUpadate(spinCount);
            freeSpinArrowImage.SetActive(false);
            StartCoroutine(FreeSpin());
        }

        public void OnPaidSpinButtonClicked()
        {
            //uiManager.apiManager.APIPaidSpinData();
            OnStartPaidSpinWheelButtonClicked(null);
        }
        public void OnPaidSpinButtonCallBack(List<SpinData> paidSpinData)
        {
            for (int i = 0; i < paidSpinData.Count; i++)
            {
                for (int j = 0; j < paidSpinRewards.Length; i++)
                {
                    if (i == j)
                    {
                        paidSpinRewards[j].text = paidSpinData[i].ToString();
                    }
                }
            }
        }
        public void OnStartPaidSpinWheelButtonClicked(string spiId)
        {
            //uiManager.apiManager.APIStartPaidSpin(spiId);
            OnStartPaidSpinWheelButtonCallBack();
        }
        public void OnStartPaidSpinWheelButtonCallBack()
        {
            paidSpinArrowImage.SetActive(false);
            StartCoroutine(PaidSpin());
        }
        private IEnumerator FreeSpin()
        {
            print("free spin");
            spinWheelCoroutine = false;
            randval = UnityEngine.Random.Range(50, 60);
            timeInterval = 0.0001f * Time.deltaTime * 2;
            print(timeInterval);
            for (int i = 0; i < randval; i++)
            {
                freeSpinWheelCircle.Rotate(0, 0, -(freeSpinWheelTotalAngle / 2));//start

                if (i > Mathf.RoundToInt(randval * 0.2f))//slow wheel
                    timeInterval = 1f * Time.deltaTime;
                //print(timeInterval);
                if (i > Mathf.RoundToInt(randval * 0.8f))
                    timeInterval = 1.5f * Time.deltaTime;
                if (i > Mathf.RoundToInt(randval * 0.9f))
                    timeInterval = 2f * Time.deltaTime;
                if (i > Mathf.RoundToInt(randval * 1f))
                    timeInterval = 2.5f * Time.deltaTime;
                if (i > Mathf.RoundToInt(randval * 2f))
                    timeInterval = 3f * Time.deltaTime;
                yield return new WaitForSeconds(timeInterval);
            }
            if (Mathf.RoundToInt(freeSpinWheelCircle.eulerAngles.z) % freeSpinWheelTotalAngle != 0)
            {
                freeSpinWheelCircle.Rotate(0, 0, freeSpinWheelTotalAngle / 2);
            }
            freeSpinWheelFinalAngle = Mathf.RoundToInt(freeSpinWheelCircle.eulerAngles.z);
            print(freeSpinWheelFinalAngle);
            for (int i = 0; i < freeSpinWheeelSection; i++)
            {
                if (freeSpinWheelFinalAngle == i * freeSpinWheelTotalAngle)
                {
                    freeWinText.text = freeSpinRewards[i].text;
                }
                spinWheelPopup.SetActive(true);
            }
            spinWheelCoroutine = true;
            uiManager.UpdateCoinData(int.Parse(freeWinText.text));
        }
        public void CollectButton()
        {
            this.gameObject.SetActive(false);
            spinWheelPopup.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }
        private IEnumerator PaidSpin()
        {
            spinWheelCoroutine = false;
            randval = UnityEngine.Random.Range(50, 60);
            timeInterval = 0.0001f * Time.deltaTime * 2;
            print(timeInterval);
            for (int i = 0; i < randval; i++)
            {
                paidSpinWheelCircle.Rotate(0, 0, -(paidSpinWheelTotalAngle / 2));//start

                if (i > Mathf.RoundToInt(randval * 0.2f))//slow wheel
                    timeInterval = 1f * Time.deltaTime;
                //print(timeInterval);
                if (i > Mathf.RoundToInt(randval * 0.8f))
                    timeInterval = 1.5f * Time.deltaTime;
                if (i > Mathf.RoundToInt(randval * 0.9f))
                    timeInterval = 2f * Time.deltaTime;
                if (i > Mathf.RoundToInt(randval * 1f))
                    timeInterval = 2.5f * Time.deltaTime;
                if (i > Mathf.RoundToInt(randval * 2f))
                    timeInterval = 3f * Time.deltaTime;
                yield return new WaitForSeconds(timeInterval);
            }
            if (Mathf.RoundToInt(paidSpinWheelCircle.eulerAngles.z) % paidSpinWheelTotalAngle != 0)
            {
                paidSpinWheelCircle.Rotate(0, 0, paidSpinWheelTotalAngle / 2);
            }
            paidSpinWheelFinalAngle = Mathf.RoundToInt(paidSpinWheelCircle.eulerAngles.z);
            print(paidSpinWheelFinalAngle);
            for (int i = 0; i < paidSpinWheelSection; i++)
            {
                if (paidSpinWheelFinalAngle == i * paidSpinWheelTotalAngle)
                {
                    paidWinText.text = paidSpinRewards[i].text;
                }
                spinWheelPopup.SetActive(true);
            }
            uiManager.UpdateCoinData(int.Parse(paidWinText.text));
            spinWheelCoroutine = true;
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
            PlayerPrefs.SetInt("spin wheel show",1);
            if (screenName == "shop scratchers screen")
            {
                screenName = "";
                this.gameObject.SetActive(false);
                uiManager.shopScreenParent.SetActive(true);
                uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().ScratchersButton();
            }
            else if (screenName == "shop surprise boxs screen")
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

        public void OnBoxesButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.surpriseBoxScreenParent.SetActive(true);
        }

        public void OnScratchButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.scratchScreenParent.SetActive(true);
        }
        public void SpinWheelScreenUpdate()
        {
            int count = PlayerPrefs.GetInt("spincount");
            TimeSpan timeDifference;
            DateTime currentDate = System.DateTime.Now;

            //PlayerPrefs.SetString("systime", currentDate.ToString());
            DateTime tempTime = Convert.ToDateTime(PlayerPrefs.GetString("systime"));
            long temp = Convert.ToInt64(tempTime.ToBinary());
            DateTime oldDate = DateTime.FromBinary(temp);
            oldDate = oldDate.AddDays(1);
            timeDifference = oldDate.Subtract(currentDate);
            if (timeDifference.Minutes <= 0 && timeDifference.Seconds < 0)
            {
                if (count == 0)
                {
                    PlayerPrefs.SetInt("spincount", count + 1);

                }
            }
                if (count > 0)
            {
                print("free spin");
                //this.gameObject.SetActive(true);
                freeSpinArrowImage.SetActive(true);
                freeSpinWheel.SetActive(true);
                paidSpinWheel.SetActive(false);
                paidSpinWheelPopup.SetActive(false);
            }
            else
            {
                print("paid spin");
                int hours = timeDifference.Hours;
                int minutes = timeDifference.Minutes;
                int seconds = timeDifference.Seconds;
                print("time" + hours);
                print("time" + minutes);
                print("time" + seconds);
                freeSpinTimeText.text = "Free spin wheel will be enabled after \n" + hours + "hrs : " + minutes + "mins : " + seconds + "secs\n" +
                "Press ok for a paid spin wheel for just 0.89$";
                paidSpinArrowImage.SetActive(true);
                paidSpinWheel.SetActive(true);
                paidSpinWheelPopup.SetActive(true);
                freeSpinWheel.SetActive(false);
            }
        }
        public void FreeSpinWheelUpdaate()
        {
            if (PlayerPrefs.GetInt("spincount") > 0)
            {
                //print("free spin");
                //this.gameObject.SetActive(true);
                freeSpinArrowImage.SetActive(true);
                this.gameObject.SetActive(true);
                freeSpinWheel.SetActive(true);
                paidSpinWheel.SetActive(false);
                paidSpinWheelPopup.SetActive(false);
            }
            else
            {
                
                paidSpinArrowImage.SetActive(true);
                paidSpinWheel.SetActive(true);
                paidSpinWheelPopup.SetActive(true);
                freeSpinWheel.SetActive(false);
            }
        }
        public void PaidSpinWheelPopupCloseButton()
        {
            this.gameObject.SetActive(false);
            paidSpinWheel.SetActive(false);
            paidSpinWheelPopup.SetActive(false);
            freeSpinWheel.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }
        public void PaidSpinOkbuttonClicked()
        {
            paidSpinWheelPopup.SetActive(false);
        }
        public void FreeSpinCountUpadate(int count)
        {
            //freeSpinCount.text = (count + int.Parse(freeSpinCount.text)).ToString();
            freeSpinCount.text = count.ToString();
        }

        public void FreeSpinWheelPopupEnable()
        {
            
        }
    }
}
