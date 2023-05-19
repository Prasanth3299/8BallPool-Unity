using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RevolutionGames.UI
{
    public class GiftsScreen : MonoBehaviour
    {
        public UIManager uIManager;

        public GameObject giftsMainScreen;
        public GameObject sendGiftsScreen;
        public GameObject sendFreeCoinsScreen;
        public GameObject requestGiftsScreen;
        public GameObject giftsInboxScreen;
        public GameObject noGiftInboxScreen;
        public Transform requestGiftFriendsContent;
        public GameObject requestGiftFriendsInfoData;
        public Transform sendFreeCoinsFriendContent;
        public Transform sendFreeCoinsFriendContentEmpty;
        public GameObject sendFreeCoinsFriendData;
        public Transform inboxFriendsContent;
        public GameObject inboxFriendsInfoData;
        public GameObject notSelectImageRequestGift;
        public GameObject selectImageRequestGift;
        public GameObject notSelectImageSendFreeCoin;
        public GameObject selectImageSendFreeCoin;
        public GameObject GiftPopupScreen;
        public Text meassageText;
        public Text sendGiftCoinText;
        public Text giftInfoxCoinText;
        private GameObject requestGift;
        private GameObject sendFreeCoins;
        private int requestGiftCount=0;
        private int sendFreeCoinCount=0;
        private int searchCount=0;
        List<GameObject> requestGiftList=new List<GameObject>();
        List<GameObject> sendFreeCoinList=new List<GameObject>();

        public void Start()
        {

            //sendGiftCoinText.text = PlayerPrefs.GetInt("coin amount").ToString();
            //giftInfoxCoinText.text = PlayerPrefs.GetInt("coin amount").ToString();

            for (int i = 0; i < 9; i++)
            {
                GenerateRequestGiftPrefab(i);
                //requestGift= Instantiate(requestGiftFriendsInfoData, requestGiftFriendsContent.transform);
                //requestGiftList.Add(requestGift);
                //requestGift.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { NotSelectRequestGiftButtonClicked(); });
                //requestGift.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { SelectRequestGiftButtonClicked(); });
                
            }
            for (int i = 0; i < 9; i++)
            {
                GenerateSendFreeCoinPrefab(i);
                //sendFreeCoins = Instantiate(sendFreeCoinsFriendData, sendFreeCoinsFriendContent.transform);
                //sendFreeCoinList.Add(sendFreeCoins);
                //sendFreeCoins.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { NotSelectSendFreeCoinButtonClicked(i); });
                //sendFreeCoins.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { SelectSendFreeCoinButtonClicked( i); });
            }
            for (int i = 0; i < 9; i++)
            {
                GameObject inbox = Instantiate(inboxFriendsInfoData, inboxFriendsContent);
                inbox.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { GiftAcceptButtonClicked(); });

            }
        }
        public void Update()
        {
            // SelectAllRequestGiftButton();
        }
        public void GenerateSendFreeCoinPrefab(int itemnumber)
        {
            sendFreeCoins = Instantiate(sendFreeCoinsFriendData,sendFreeCoinsFriendContent);
            //sendFreeCoins.transform.SetParent(sendFreeCoinsFriendContent,false);
            sendFreeCoinList.Add(sendFreeCoins);
            sendFreeCoinList [0].transform.GetChild(1).GetComponent<Text>().text="prasanth";
            sendFreeCoins.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => NotSelectSendFreeCoinButtonClicked(itemnumber));
            sendFreeCoins.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => SelectSendFreeCoinButtonClicked(itemnumber));
        }
        public void GenerateRequestGiftPrefab(int itemnumber)
        {
            requestGift = Instantiate(requestGiftFriendsInfoData, requestGiftFriendsContent);
            requestGiftList.Add(requestGift);
            //requestGiftListName.Add(requestGift.transform.GetChild(1).GetComponent<Text>());
            requestGiftList[0].transform.GetChild(1).GetComponent<Text>().text = "prasanth";
            requestGift.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => NotSelectRequestGiftButtonClicked(itemnumber));
            requestGift.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => SelectRequestGiftButtonClicked(itemnumber));
        }
        public void SendGiftsButton()
        {
            giftsMainScreen.SetActive(true);
            sendGiftsScreen.SetActive(true);
            giftsInboxScreen.SetActive(false);
        }
        public void InboxButton()
        {
            giftsInboxScreen.SetActive(true);
            sendGiftsScreen.SetActive(false);
        }
        public void SendFreeCoinButton()
        {
            sendFreeCoinsScreen.SetActive(true);
            sendGiftsScreen.SetActive(false);
        }
        public void RequestGiftButton()
        {
            requestGiftsScreen.SetActive(true);
            sendGiftsScreen.SetActive(false);
        }
        public void SendGiftCloseButton()
        {
            this.gameObject.SetActive(false);
            uIManager.homeScreenParent.SetActive(true);
        }
        public void CloseButton()
        {
            this.gameObject.SetActive(true);
            //uIManager.homeScreenParent.SetActive(true);
            sendGiftsScreen.SetActive(true);
            requestGiftsScreen.SetActive(false);
            sendFreeCoinsScreen.SetActive(false);
            giftsInboxScreen.SetActive(false);
            noGiftInboxScreen.SetActive(false);

        }
        public void RequestButton()
        {
            GiftPopupScreen.SetActive(true);
            meassageText.text = "Your request has been sent.";
        }
        public void SelectAllRequestGiftButton()
        {
 
            if (selectImageRequestGift.activeSelf)
            {
                requestGiftCount = 0;
                notSelectImageRequestGift.SetActive(true);
                selectImageRequestGift.SetActive(false);
                for (int i = 0; i < requestGiftList.Count; i++)
                {
                    requestGiftList[i].transform.GetChild(3).gameObject.SetActive(false);
                    requestGiftList[i].transform.GetChild(2).gameObject.SetActive(true);
                }
                //requestGift.transform.GetChild(3).gameObject.SetActive(false);
                //requestGift.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                requestGiftCount =requestGiftList.Count;
                print("slect all");
                notSelectImageRequestGift.SetActive(false);
                selectImageRequestGift.SetActive(true);
                for (int i = 0; i < requestGiftList.Count; i++)
                {
                    requestGiftList[i].transform.GetChild(3).gameObject.SetActive(true);
                    requestGiftList[i].transform.GetChild(2).gameObject.SetActive(false);
                }
                //requestGift.transform.GetChild(3).gameObject.SetActive(true);
                //requestGift.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        public void SelectRequestGiftButtonClicked(int number)
        {
         
            requestGiftCount--;
            requestGiftList[number].transform.GetChild(3).gameObject.SetActive(false);
            requestGiftList[number].transform.GetChild(2).gameObject.SetActive(true);
            notSelectImageRequestGift.SetActive(true);
            selectImageRequestGift.SetActive(false);
    
           
        }
        public void NotSelectRequestGiftButtonClicked(int number)
        {

            requestGiftCount++;
            requestGiftList[number].transform.GetChild(3).gameObject.SetActive(true);
                requestGiftList[number].transform.GetChild(2).gameObject.SetActive(false);
            if (requestGiftCount == requestGiftList.Count)
            {
                selectImageRequestGift.SetActive(true);
                notSelectImageRequestGift.SetActive(false);

            }
               
        }
       
        public void SlectAllSendFreeCoinButtonClicked()
        {
            if (selectImageSendFreeCoin.activeSelf)
            {
                sendFreeCoinCount = 0;
                notSelectImageSendFreeCoin.SetActive(true);
                selectImageSendFreeCoin.SetActive(false);
                for (int i = 0; i < sendFreeCoinList.Count; i++)
                {
                    sendFreeCoinList[i].transform.GetChild(3).gameObject.SetActive(false);
                    sendFreeCoinList[i].transform.GetChild(2).gameObject.SetActive(true);
                }
            }

            else
              {
                sendFreeCoinCount = sendFreeCoinList.Count;
                notSelectImageSendFreeCoin.SetActive(false);
                selectImageSendFreeCoin.SetActive(true);
                for (int i = 0; i < sendFreeCoinList.Count; i++)
                {
                    sendFreeCoinList[i].transform.GetChild(3).gameObject.SetActive(true);
                    sendFreeCoinList[i].transform.GetChild(2).gameObject.SetActive(false);
                }
            }

        }
        public void SelectSendFreeCoinButtonClicked(int number)
        {
            sendFreeCoinCount--;
            sendFreeCoinList[number].transform.GetChild(3).gameObject.SetActive(false);
            sendFreeCoinList[number].transform.GetChild(2).gameObject.SetActive(true);
            notSelectImageSendFreeCoin.SetActive(true);
            selectImageSendFreeCoin.SetActive(false);

        }
        public void NotSelectSendFreeCoinButtonClicked( int number)
        {
            sendFreeCoinCount++;
            sendFreeCoinList[number].transform.GetChild(3).gameObject.SetActive(true);
            sendFreeCoinList[number].transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < sendFreeCoinList.Count; i++)
            {
                if (sendFreeCoinList[i].transform.GetChild(3).gameObject.activeSelf)
                {
                    if (sendFreeCoinCount == sendFreeCoinList.Count)
                    {
                        selectImageSendFreeCoin.SetActive(true);
                        notSelectImageSendFreeCoin.SetActive(false);
                    }

                }
            }
    
               
        }
        public void SlectAllFreeCoinButtonEnable()
        {
            
        }
        public void SendCoinButton()
        {
            GiftPopupScreen.SetActive(true);
            meassageText.text = "Your free coin has been sent.";
        }
        public void SelectAllSendCoinButton()
        {
            
        }
        public void AcceptAllInboxButton()
        {
            GiftPopupScreen.SetActive(true);
            meassageText.text = "All gifts have been accepted.";
        }
        public void GiftAcceptButtonClicked()
        {
            GiftPopupScreen.SetActive(true);
            meassageText.text = "Gifts has been accepted.";
        }
        public void RequestGiftsInboxButton()
        {
           
        }
        public void InviteFriendsButton()
        {

        }

        public void OnCoinsButtonClicked()
        {
            this.gameObject.SetActive(false);
            uIManager.shopScreenParent.SetActive(true);
            uIManager.shopScreenParent.GetComponent<ShopScreen>().GiftCoinButtonClicked();
        }
        public void GiftPopupCloseButton()
        {
            GiftPopupScreen.SetActive(false);
        }
        public void coinsUpdate(long coinCount)
        {
            long count = coinCount + long.Parse(sendGiftCoinText.text);
            sendGiftCoinText.text = count.ToString();
            giftInfoxCoinText.text = count.ToString();
            //PlayerPrefs.SetInt("coin amount", count);
        }
        public void SubtractCoinsData(int coinCount)
        {
            int count =  int.Parse(sendGiftCoinText.text)-coinCount;
            sendGiftCoinText.text = count.ToString();
            giftInfoxCoinText.text = count.ToString();
            //PlayerPrefs.SetInt("coin amount", count);
        }
        public void SendFreeCoinSearchField(string name)
        {
            //string nameSearch = name;
            //print(nameSearch);
            //print("length :" + name.Length);
            for (int i = 0; i < sendFreeCoinList.Count; i++)
            {
               
                //print(sendFreeCoinList[i].transform.GetChild(1).GetComponent<Text>().text);
                if (sendFreeCoinList[i].transform.GetChild(1).GetComponent<Text>().text.ToLower().StartsWith(name.ToLower()))
                {
                   
                    print("i value" + i);
                  
                    sendFreeCoinList[i].transform.gameObject.SetActive(true);
                    sendFreeCoinList[i].transform.SetParent(sendFreeCoinsFriendContent, false);
                    
                    //Destroy(sendFreeCoinsFriendContent.transform.GetChild(i).gameObject);
                    print("Yes");
                   
                }
                else if(name.Length==0)
                {
                    print("length");
                    sendFreeCoinList[i].transform.SetParent(sendFreeCoinsFriendContent,false);
                    sendFreeCoinList[i].transform.gameObject.SetActive(true);
                    
                }
                else
                {
                    print("else");
                    //sendFreeCoinsFriendContent= null;
                    sendFreeCoinList[i].transform.SetParent(sendFreeCoinsFriendContentEmpty, false);
                    sendFreeCoinList[i].transform.gameObject.SetActive(false);

                }

            }
        }
        public void RequestGiftSearchField(string name)
        {
            for(int i=0;i<requestGiftList.Count;i++)
            {
                if (requestGiftList[i].transform.GetChild(1).GetComponent<Text>().text.ToLower().StartsWith(name.ToLower()))
                {
                    requestGiftList[i].transform.gameObject.SetActive(true);
                    requestGiftList[i].transform.SetParent(requestGiftFriendsContent, false);
                }
                else if (name.Length == 0)
                {
                    requestGiftList[i].transform.gameObject.SetActive(true);
                    requestGiftList[i].transform.SetParent(requestGiftFriendsContent, false);
                }
                else
                {
                    requestGiftList[i].transform.gameObject.SetActive(false);
                    requestGiftList[i].transform.SetParent(sendFreeCoinsFriendContentEmpty, false);
                }
            }
        }
    }
}
