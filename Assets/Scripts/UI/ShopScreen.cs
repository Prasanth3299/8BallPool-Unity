using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;
namespace RevolutionGames.UI
{

    public class ShopScreen : MonoBehaviour
    {
        public UIManager uiManager;
        public GameObject shopMainScreen;
        public GameObject surpriseBoxes;
        public GameObject surpriseBoxPurchaseCompleteScreen;
        public GameObject scratchers;
        public GameObject CuesMainScreen;
        public GameObject shopCoinScreen;
        public GameObject shopCashScreen;
        public GameObject promotionScreen;
        public GameObject standardScreen;
        public GameObject victoryScreen;
        public GameObject surpriseScreen;
        public GameObject countryScreen;
        public GameObject ownedScreen;
        public GameObject socialMainScreen;
        public GameObject avatarScreen;
        public GameObject chatPaksScreen;
        public GameObject stickersScreen;
        public Transform standardCueContent;
        public GameObject standardCueData;
        public Transform victoryCueContent;
        public GameObject victoryCueData;
        public Transform surpriseCueContent;
        public GameObject surpriseCueData;
        public Transform countryCueContent;
        public GameObject countryCueData;
        public Transform ownedCueContent;
        public GameObject ownedCueData;

        //Coins Screen
        public GameObject shopCoinsContent;
        public GameObject shopCoinsData;
        public GameObject insufficientCoinsPopup;
        public Text shopCoinText;

        public Transform shopAvatarcontent;
        public GameObject shopAvatarPrefab;
        public Transform shopStickersContent;
        public GameObject shopStickersPrefab;
        public GameObject shopCashContent;
        public GameObject shopCashPrefab;
        public GameObject shopChatPacksPrefab;
        public Transform ShopChatPacksContent;
        public GameObject cuesInfo1Screen;
        public GameObject cuesInfo2Screen;
        public GameObject cuesInfo3Screen;
        public Text buyBoxPopupText;
        public Text purchaseCompletePopupText;
        public GameObject buyEpicBoxPopup;
        public GameObject purchaseCompleteEpicBoxPopup;
        public GameObject socialPurchasePopupScreen;
        public GameObject cuesPurchaseCompletePopup;
        public GameObject surpriseCueBuyPopup;
        public Text surpriseCueNameText;
        public Text socialPurchaseText;
        public GameObject getFreeCoinsPopup;
        public GameObject chatPackBuyButton1;
        public GameObject chatPackBuyButton2;
        public Text shopMainCoinText;
        public Text surpriseBoxCoinText;
        public Text scratchersCoinText;
        public Text cuesMainCoinText, cueNamePurchaseText;
        public Text socialMainCoinText;
        
        public Image cuePurchaseImage;
        private string screenName;
        //surpriseBoxScreen
        public Text rareBoxCountText;
        public Image rareBoxCountImage;
        public Text epicBoxCountText;
        public Image epicBoxCountImage;
        public Text legendaryBoxCountText;
        public Image legendaryBoxCountImage;
        public Text purchaseCompleteBoxNameText;
        public Image purchaseCompleteBoxImage;
        public Image rareBoxImage;
        public Image epicBoxImage;
        public Image legendaryBoxImage;
        private int rareBoxCount;
        private int epicBoxCount;
        private int legendaryBoxCount;
        public GameObject socialAvatarpurchaseScreen;
        public Image avatarImage;
        public Text avatarNameText;
        public Text avatarNameHeaderText;
        //scratch screen

        public Text scratchPackName;
        public GameObject scratchePurchasePopupScreen;
        public GameObject cuesInfoMainScreen;
        public Transform cuesInfoContent;
        public Image cuesInfoButton1;
        public Image cuesInfoButton2;
        public Image cuesInfoButton3;
        public int testCurrentLevel = 0;

        //cues screen

        public GameObject surpriseBoxCuesPurchaseScreen;

        private CueStickDataManager cueStickDataManager;
        public Sprite[] avatarSprite = new Sprite[5];
        public Sprite[] stickersSprite = new Sprite[5];
        private List<GameObject> avatarList = new List<GameObject>();
        private List<GameObject> stickersList = new List<GameObject>();
        private List<GameObject> standardCueList = new List<GameObject>();
        private List<GameObject> victoryCueList = new List<GameObject>();
        private List<GameObject> ownedCueList = new List<GameObject>();
        private List<GameObject> countryCueList = new List<GameObject>();
        private List<GameObject> surpriseCueList = new List<GameObject>();

        private int playerCueStickIndex;

        private int countryUsingCueStickIndex = -1;

        //public GameObject avatarBuyButton;
        //public GameObject avatrUseButton;


        public void Start()
        {
            cueStickDataManager = uiManager.dataManager.GetComponent<CueStickDataManager>();


            /*shopMainCoinText.text = PlayerPrefs.GetInt("coin amount").ToString();
            surpriseBoxCoinText.text = PlayerPrefs.GetInt("coin amount").ToString();
            cuesMainCoinText.text = PlayerPrefs.GetInt("coin amount").ToString();
            socialMainCoinText.text = PlayerPrefs.GetInt("coin amount").ToString();
            scratchersCoinText.text = PlayerPrefs.GetInt("coin amount").ToString();
            socialMainCoinText.text = PlayerPrefs.GetInt("coin amount").ToString();
            shopCoinText.text = PlayerPrefs.GetInt("coin amount").ToString();*/




            /*for (int i = 0; i < 1; i++)
            {
                GameObject cues = Instantiate(standardCueData, standardCueContent.transform);
                GameObject victory = Instantiate(victoryCueData, victoryCueContent.transform);
                GameObject surprise = Instantiate(surpriseCueData, surpriseCueContent.transform);
                GameObject country = Instantiate(counterCueData, countryCueContent.transform);
                GameObject owned = Instantiate(ownedCueData, ownedCueContent.transform);
                GameObject coins = Instantiate(shopCoinsData, shopCoinsContent.transform);
                GameObject cavatars = Instantiate(shopAvatarPrefab, shopAvatarcontent.transform);
                GameObject stickers = Instantiate(shopStickersPrefab, shopStickersContent.transform);
                GameObject chatpack = Instantiate(shopChatPacksPrefab, ShopChatPacksContent.transform);
            }*/


            for (int i = 0; i < 5; i++)
            {
                PlayerPrefs.SetInt("avatarbuy" + i, 0);
                PlayerPrefs.SetInt("stickerbuy" + i, 0);
            }
            loadVictoryCuesList();
            loadSurpriseCuesList();
            loadCountryCuesList();
        }

        public void Update()
        {
            if (cuesInfoMainScreen.activeSelf)
            {
                if (cuesInfoContent.localPosition.x < -570)
                {
                    CuesInfo2ButtonColorchange();
                }
                 if (cuesInfoContent.localPosition.x < -1800)
                {
                    CuesInfo3ButtonColorchange();
                }
                if(cuesInfoContent.localPosition.x>-570)
                {
                    CuesInfo1ButtonColorchange();
                }

            }
        }

        public void OnEnable()
        {
            rareBoxCount = PlayerPrefs.GetInt("rareboxcount");
            epicBoxCount = PlayerPrefs.GetInt("epicboxcount");
            legendaryBoxCount = PlayerPrefs.GetInt("legendaryboxcount");
            if (rareBoxCount == 0)
            {

                rareBoxCountImage.gameObject.SetActive(false);
            }
            else
            {
                rareBoxCountImage.gameObject.SetActive(true);
                rareBoxCountText.text = rareBoxCount.ToString();
            }
            if (epicBoxCount == 0)
            {
                epicBoxCountImage.gameObject.SetActive(false);

            }
            else
            {
                epicBoxCountImage.gameObject.SetActive(true);
                epicBoxCountText.text = epicBoxCount.ToString();
            }
            if (legendaryBoxCount == 0)
            {
                legendaryBoxCountImage.gameObject.SetActive(false);
            }
            else
            {
                legendaryBoxCountImage.gameObject.SetActive(true);
                legendaryBoxCountText.text = legendaryBoxCount.ToString();
            }
            //if(cueStickDataManager == null)
            //{
            //    cueStickDataManager = uiManager.dataManager.GetComponent<CueStickDataManager>();
            //}
            //cueStickDataManager.SetVictoryCueStickCurrentLevel(0, testCurrentLevel);
        }


        public void ScratchersButton()
        {
            surpriseBoxes.SetActive(false);
            scratchers.SetActive(true);
        }

        public void HomeShopButtonClicked()
        {
            shopMainScreen.SetActive(true);
        }

        public void HomeCoinButtonClicked()
        {
            screenName = "homeScreen";
            shopCoinScreen.SetActive(true);
        }

        public void HomeCuesButtonClicked()
        {
            //shopMainScreen.SetActive(false);
            screenName = "homeScreen";
            CuesMainScreen.SetActive(true);
            if(cueStickDataManager == null)
            {
                cueStickDataManager = uiManager.dataManager.GetComponent<CueStickDataManager>();
            }
            OwnedButton();
        }

        public void ProfileEditButtonClicked()
        {
            screenName = "ProfileScreen";
            OnMainSocialButtonClicked();
        }

        public void GiftCoinButtonClicked()
        {
            screenName = "giftscreen";
            shopCoinScreen.SetActive(true);
        }

        // surprise api(to change board api)
        public void OnSurpriseBoxButtonClicked()
        {
            //uiManager.apiManager.APIGetShopBoards();
            OnSurpriseBoxButtonCallBack();
        }

        public void OnSurpriseBoxButtonCallBack()
        {
            shopMainScreen.SetActive(false);
            surpriseBoxes.SetActive(true);
            scratchers.SetActive(false);
        }

        //standard cues API(to change dice api)
        public void OnCuesButtonClicked()
        {
            /*for (int i = 0; i < standardCueContent.transform.childCount; i++)
            {
                Destroy(standardCueContent.transform.GetChild(i));
            }*/
            //uiManager.apiManager.APIGetShopDices();
            shopMainScreen.SetActive(false);
            CuesMainScreen.SetActive(true);
            ownedScreen.SetActive(true);
            countryScreen.SetActive(false);
            victoryScreen.SetActive(false);
            standardScreen.SetActive(false);
            surpriseScreen.SetActive(false);
            OwnedButton();
        }
        public void OncuesButtonCallBack(List<ShopDice> standardCues)
        {
            for (int i = 0; i < standardCues.Count; i++)
            {
                GameObject cues = Instantiate(standardCueData, standardCueContent.transform);
                shopMainScreen.SetActive(false);
                CuesMainScreen.SetActive(true);
            }
        }

        //avatars api
        public void OnMainSocialButtonClicked()
        {
            avatarScreen.SetActive(true);
            chatPaksScreen.SetActive(false);
            stickersScreen.SetActive(false);
            //print("shopAvatarcontent.transform.childCount 00" + shopAvatarcontent.transform.childCount);
            for (int i = 0; i < shopAvatarcontent.childCount; i++)
            {
                Destroy(shopAvatarcontent.GetChild(i).gameObject);
            }
            avatarList.Clear();
            //uiManager.apiManager.APIGetShopAvatars();
            shopMainScreen.SetActive(false);
            socialMainScreen.SetActive(true);
            OnSocialButtonCallBack(avatarSprite);
        }
        public void OnMainSocialButtonCallBack(List<ShopAvatar> shopAvatars)
        {
            /*for (int i = 0; i < shopAvatars.Count; i++)
            {
                string status = shopAvatars[i].status;
                int avatarId = shopAvatars[i].avatar_id;
               
                GameObject avatar = Instantiate(shopAvatarPrefab, shopAvatarcontent.transform);
                avatar.transform.name = shopAvatars[i].avatar_id.ToString();
                avatar.transform.GetChild(0).GetComponent<Text>().text = shopAvatars[i].avatar_name;

                if (status == "purchased")
                {
                    print("purchased Button");

                    avatar.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                    avatar.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    avatar.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { OnAvatarUseButtonClicked(avatar); });

                }
                else
                {
                    print("Nameee.." + avatar.name);
                    print("Avatar Id..." + avatarId);
                    avatar.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                    avatar.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().text = shopAvatars[i].cost.ToString();
                    avatar.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { OnAvatarBuyButtonClicked(avatar.name); });

                }

                shopMainScreen.SetActive(false);
                socialMainScreen.SetActive(true);
            }*/

        }

        public void OnSocialButtonCallBack(Sprite[] avatarSprite)
        {
            int avatarButton = PlayerPrefs.GetInt("avatr button");
            for (int i = 0; i < avatarSprite.Length; i++)
            {

                int count = i;
                GameObject avatar = Instantiate(shopAvatarPrefab, shopAvatarcontent);
                avatarList.Add(avatar);
                print("Countt" + count);
                avatarList[i].transform.GetChild(1).GetComponent<Image>().sprite = avatarSprite[i];
                avatar.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnAvatarBuyButtonClicked(count));
                avatar.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnAvatarUseButtonClicked(count));
            }
            for (int i = 0; i < avatarList.Count; i++)
            {
                if (PlayerPrefs.GetInt("avatarbuy" + i) == 0)
                {
                    avatarList[i].transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                    avatarList[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                    avatarList[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);

                }
                else if (PlayerPrefs.GetInt("avatarbuy" + i) == 1)
                {
                    avatarList[i].transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    avatarList[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                    avatarList[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                }
                else if (PlayerPrefs.GetInt("avatarbuy" + i) == 2)
                {
                    avatarList[i].transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    avatarList[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                    avatarList[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                }
            }
        }
        
        public void OnAvatarBuyButtonClicked(int avatarNumber)
        {
            PlayerPrefs.SetInt("avatarbuy" + avatarNumber, 1);
            avatarList[avatarNumber].transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
            avatarList[avatarNumber].transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            uiManager.SubtractCoinData(avatarList[avatarNumber].transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().text);
            socialAvatarpurchaseScreen.SetActive(true);
            avatarImage.sprite = avatarList[avatarNumber].transform.GetChild(1).GetComponent<Image>().sprite;
            avatarNameText.text = "Defalut";
            avatarNameHeaderText.text = "Defalut";
            //uiManager.apiManager.APIPurchaseAvatar(avatarId);

            //PlayerPrefs.SetString("avatarbuy", "avatarbuy"+ avatarNumber);
            //print("prehhsj"+ PlayerPrefs.GetString("avatarbuy"));
        }
        public void OnAvatarBuyButtonCallBack()
        {

        }
        public void OnAvatarUseButtonClicked(int avatarNumber)
        {
            for (int i = 0; i < avatarList.Count; i++)
            {
                if (PlayerPrefs.GetInt("avatarbuy" + i) == 2)
                {
                    PlayerPrefs.SetInt("avatarbuy" + i, 1);
                }
            }
            PlayerPrefs.SetInt("avatarbuy" + avatarNumber, 2);
            for (int i = 0; i < avatarList.Count; i++)
            {

                avatarList[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                avatarList[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            }
            avatarList[avatarNumber].transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            avatarList[avatarNumber].transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            uiManager.ProfilePictureUpdate(avatarList[avatarNumber].transform.GetChild(1).GetComponent<Image>().sprite);
            //uiManager.apiManager.APISetCurrentAvatar(avatar.name);

        }
        public void OnAvatarUseButtonCallBack()
        {

        }

        public void OnAvatarPurchaseUseButtonClicked()
        {
            socialAvatarpurchaseScreen.SetActive(false);
        }

        // Coins APi
        public void OnMainShopCoinsButtonClicked()
        {
            /*for (int i = 0; i < shopCoinsContent.transform.childCount; i++)
            {
                Destroy(shopCoinsContent.transform.GetChild(i));
            }*/
            //uiManager.apiManager.APIGetShopCoins();
            shopMainScreen.SetActive(false);
            shopCoinScreen.SetActive(true);

        }
        public void OnMainShopCoinsButtonCallBack(List<ShopCoin> shopCoins)
        {
            print("Shop items1..." + shopCoins.Count);


            for (int i = 0; i < shopCoins.Count; i++)
            {
                int coinsId = shopCoins[i].coins_id;
                int gameId = shopCoins[i].game_id;
                string coinsTitle = shopCoins[i].coins_title;
                string coinsDesscription = shopCoins[i].coins_description;
                int coinsCount = shopCoins[i].coins_count;
                float coinsCost = shopCoins[i].coins_cost;
                int isHidden = shopCoins[i].is_hidden;
                string createdDate = shopCoins[i].created_date;
                GameObject coins = Instantiate(shopCoinsData, shopCoinsContent.transform);
                coins.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = coinsCount + "M";
                coins.transform.GetChild(0).GetChild(7).GetChild(0).GetComponent<Text>().text = coinsCost + "$";
                coins.transform.GetChild(0).GetChild(7).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { OnCoinsBuybuttonClicked(coinsId.ToString()); });


            }
            shopMainScreen.SetActive(false);
            shopCoinScreen.SetActive(true);
        }

        public void InsufficientCoinsPopupCallBack(List<ShopCoin> shopCoins)
        {
            for (int i = 0; i < shopCoins.Count; i++)
            {
                int coinsId = shopCoins[i].coins_id;
                int gameId = shopCoins[i].game_id;
                string coinsTitle = shopCoins[i].coins_title;
                string coinsDesscription = shopCoins[i].coins_description;
                int coinsCount = shopCoins[i].coins_count;
                float coinsCost = shopCoins[i].coins_cost;
                int isHidden = shopCoins[i].is_hidden;
                string createdDate = shopCoins[i].created_date;
                GameObject coins = Instantiate(shopCoinsData, shopCoinsContent.transform);
                coins.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = coinsCount + "M";
                coins.transform.GetChild(0).GetChild(7).GetChild(0).GetComponent<Text>().text = coinsCost + "$";
                coins.transform.GetChild(0).GetChild(7).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { OnCoinsBuybuttonClicked(coinsId.ToString()); });
            }
        }

        public void DisplayInsufficientCoinsPopup()
        {
            insufficientCoinsPopup.SetActive(true);
        }

        public void OnInsufficientCoinsCloseButtonClicked()
        {
            insufficientCoinsPopup.SetActive(false);
        }

        public void OnCoinsBuybuttonClicked(string coinsId)
        {
            uiManager.apiManager.APIPurchaseCoins(coinsId);
        }

        //Stickers Api (to change board api)
        public void OnStickersButtonClicked()
        {
            for (int i = 0; i < shopStickersContent.childCount; i++)
            {
                Destroy(shopStickersContent.transform.GetChild(i).gameObject);
            }
            //uiManager.apiManager.APIGetShopBoards();
            stickersList.Clear();
            avatarScreen.SetActive(false);
            chatPaksScreen.SetActive(false);
            stickersScreen.SetActive(true);

            OnStickersButtonCallBack(stickersSprite);
        }
        public void OnStickersButtonCallBack(Sprite[] stickersSprite)
        {
            for (int i = 0; i < stickersSprite.Length; i++)
            {
                int count = i;
                GameObject stickers = Instantiate(shopStickersPrefab, shopStickersContent);
                stickersList.Add(stickers);
                stickersList[i].transform.GetChild(1).GetComponent<Image>().sprite = stickersSprite[i];
                stickers.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnStickersBuyButtonClicked(count));
                stickers.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnStickersUseButtonClicked(count));

            }


            for (int i = 0; i < stickersList.Count; i++)
            {
                if (PlayerPrefs.GetInt("stickerbuy" + i) == 0)
                {
                    stickersList[i].transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                    stickersList[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                    stickersList[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);

                }
                else if (PlayerPrefs.GetInt("stickerbuy" + i) == 1)
                {
                    stickersList[i].transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    stickersList[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                    stickersList[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                }
                else if (PlayerPrefs.GetInt("stickerbuy" + i) == 2)
                {
                    stickersList[i].transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    stickersList[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                    stickersList[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                }
            }
            /* print("call back");
             for(int i=0;i<shopStickers.Count;i++)
             {
                 string status = shopStickers[i].status;
                 int boardId = shopStickers[i].board_id;

                 GameObject stickers = Instantiate(shopStickersPrefab, shopStickersContent.transform);
                 stickers.transform.name = shopStickers[i].board_id.ToString();
                 stickers.transform.GetChild(0).GetComponent<Text>().text = shopStickers[i].board_name;

                 if (status == "purchased")
                 {
                     print("purchased Button");

                     stickers.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                     stickers.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                     stickers.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { OnStickersUseButtonClicked(stickers); });

                 }
                 else
                 {
                     stickers.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                     stickers.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().text = shopStickers[i].coin_count.ToString();
                    stickers.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { OnStickersBuyButtonClicked(stickers.name); });

                 }
                 avatarScreen.SetActive(false);
                 chatPaksScreen.SetActive(false);
                 stickersScreen.SetActive(true);
             }*/

        }
        public void OnStickersBuyButtonClicked(int stickesNumber)
        {
            PlayerPrefs.SetInt("stickerbuy" + stickesNumber, 1);

            //uiManager.apiManager.APIPurchaseBoard(boardId);
            stickersList[stickesNumber].transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
            stickersList[stickesNumber].transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            uiManager.SubtractCoinData(stickersList[stickesNumber].transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().text);

        }
        public void OnStickersBuyButtonCallBack()
        {

        }
        public void OnStickersUseButtonClicked(int stickersNumber)

        {

            for (int i = 0; i < stickersList.Count; i++)
            {
                if (PlayerPrefs.GetInt("stickerbuy" + i) == 2)
                {
                    //print("ppppppp");
                    PlayerPrefs.SetInt("stickerbuy" + i, 1);
                }

            }
            PlayerPrefs.SetInt("stickerbuy" + stickersNumber, 2);
            //avatar.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            //uiManager.apiManager.APISetCurrentBoard(stickers.name);
            for (int i = 0; i < avatarList.Count; i++)
            {
                stickersList[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                stickersList[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            }
            stickersList[stickersNumber].transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            stickersList[stickersNumber].transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            //uiManager.ProfilePictureUpdate(avatarList[stickersNumber].transform.GetChild(1).GetComponent<Image>().sprite);
        }
        public void OnStickersUseButtonCallBack()
        {

        }

        // cash Api(to change token api)
        public void OnCashButtonClicked()
        {
            for (int i = 0; i < shopCashContent.transform.childCount; i++)
            {
                Destroy(shopCashContent.transform.GetChild(i));
            }
            uiManager.apiManager.APIGetShopTokens();
            //shopMainScreen.SetActive(false);
            //shopCash.SetActive(true);
        }
        public void OnCashButtonCallBack(List<ShopToken> shopCash)
        {
            for (int i = 0; i < shopCash.Count; i++)
            {
                GameObject cash = Instantiate(shopCashPrefab, shopCashContent.transform);
                cash.transform.GetChild(2).GetChild(7).GetChild(0).GetComponent<Text>().text = shopCash[i].cost + "$";
                // cash.transform.GetChild(2).GetChild(7).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { OncashBuyButtonClicked(shopCash[i].token_id.ToString()); });
            }
            shopMainScreen.SetActive(false);
            shopCashScreen.SetActive(true);

        }
        public void OncashBuyButtonClicked(string tokenid)
        {
            uiManager.apiManager.APIPurchaseToken(tokenid);
        }
        public void OncashBuyButtonCallBack()
        {

        }


        //api chatpacks (to change dice api)
        public void OnChatPacksButtonClicked()
        {
            /*for (int i = 0; i < ShopChatPacksContent.transform.childCount;i++)
            { 
                Destroy(ShopChatPacksContent.transform.GetChild(i));
            }*/
            // uiManager.apiManager.APIGetShopDices();

            avatarScreen.SetActive(false);
            chatPaksScreen.SetActive(true);
            stickersScreen.SetActive(false);

        }
        public void OnChatPacksButtonCallBack(List<ShopDice> chatPacks)
        {
            for (int i = 0; i < chatPacks.Count; i++)
            {
                GameObject chatPack = Instantiate(shopChatPacksPrefab, ShopChatPacksContent.transform);
            }
            avatarScreen.SetActive(false);
            chatPaksScreen.SetActive(true);
            stickersScreen.SetActive(false);
        }

        public void OnChatPackBuyButtonClicked()
        {

        }
        public void OnChatBuyButtonCallBack()
        {

        }
        public void PromotionButton()
        {
            shopMainScreen.SetActive(false);
            promotionScreen.SetActive(true);
        }
        public void OnBackButtonClicked()
        {
            if (screenName == "HomeScreen")
            {
                this.gameObject.SetActive(false);
                shopMainScreen.SetActive(false);
                uiManager.homeScreenParent.SetActive(true);
            }
            else if (screenName == "ShopScreen")
            {
                shopMainScreen.SetActive(true);
                surpriseBoxes.SetActive(false);
                scratchers.SetActive(false);
                CuesMainScreen.SetActive(false);
                socialMainScreen.SetActive(false);
                shopCoinScreen.SetActive(false);
                shopCashScreen.SetActive(false);
                promotionScreen.SetActive(false);
            }
            else
            {
                uiManager.homeScreenParent.SetActive(true);
                this.gameObject.SetActive(false);
            }




        }
        public void OnShopMainBackButtonClicked()
        {
            uiManager.shopScreenParent.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }
        public void StandardScreen()
        {

            standardScreen.SetActive(true);
            victoryScreen.SetActive(false);
            surpriseScreen.SetActive(false);
            countryScreen.SetActive(false);
            ownedScreen.SetActive(false);

            loadStandardCuesList();
        }

        public void loadStandardCuesList()
        {
            for (int i = 0; i < standardCueContent.childCount; i++)
            {
                Destroy(standardCueContent.transform.GetChild(i).gameObject);
            }
            StandardButtonCallBack();
        }

        public void StandardButtonCallBack()
        {
            standardCueList.Clear();
            //standardCueList.RemoveAll(standardCue);
            for (int i = 0; i < cueStickDataManager.GetStandardCueStickCount(); i++)
            {
                int count = i;
                GameObject standardCue = Instantiate(standardCueData, standardCueContent);
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
            if (isCoinsSufficient(cueStickDataManager.GetStandardCueStickPriceInNumber(itemNumber)))
            {
                /*standardCueList[itemNumber].transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                standardCueList[itemNumber].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                standardCueList[itemNumber].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                standardCueList[itemNumber].transform.GetChild(5).gameObject.SetActive(false);
                standardCueList[itemNumber].transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
                standardCueList[itemNumber].transform.GetChild(0).GetComponent<Image>().enabled = false;
                standardCueList[itemNumber].transform.GetChild(2).gameObject.SetActive(false);
                standardCueList[itemNumber].transform.SetParent(ownedCueContent);
                cuePurchaseImage.sprite = standardCueList[itemNumber].transform.GetChild(7).GetComponent<Image>().sprite;
                cueNamePurchaseText.text = standardCueList[itemNumber].transform.GetChild(4).GetComponent<Text>().text;
                uiManager.SubtractCoinData(250.ToString());
                cuesPurchaseCompletePopup.SetActive(true);
                ownedCueList.Add(standardCueList[itemNumber]);*/
                GameData.Instance().PlayerBalance -= cueStickDataManager.GetStandardCueStickPriceInNumber(itemNumber);
                uiManager.UpdateCoinData(GameData.Instance().PlayerBalance);
                standardCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(4).gameObject.SetActive(true);
                standardCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);
                cueNamePurchaseText.text = standardCueList[itemNumber].transform.GetChild(0).GetChild(4).GetComponent<Text>().text;
                cuesPurchaseCompletePopup.SetActive(true);
                cueStickDataManager.ObtainedStandardCueStick(itemNumber);
                //StandardButtonCallBack();
            }
            else
            {
                DisplayInsufficientCoinsPopup();
            }
            // standardCueList.RemoveAt(itemNumber);
        }

        public void VictoryButton()
        {
            standardScreen.SetActive(false);
            victoryScreen.SetActive(true);
            surpriseScreen.SetActive(false);
            countryScreen.SetActive(false);
            ownedScreen.SetActive(false);

            //loadVictoryCuesList();
        }

        public void loadVictoryCuesList()
        {
            for (int i = 0; i < victoryCueContent.childCount; i++)
            {
                Destroy(victoryCueContent.transform.GetChild(i).gameObject);
            }
            VictoryButtonCallBack();
        }

        public void VictoryButtonCallBack()
        {
            victoryCueList.Clear();
            //standardCueList.RemoveAll(standardCue);
            for (int i = 0; i < cueStickDataManager.GetVictoryCueStickCount(); i++)
            {
                int count = i;
                GameObject victoryCue = Instantiate(victoryCueData, victoryCueContent);

                victoryCue.transform.GetChild(0).GetChild(6).GetComponent<Image>().sprite = cueStickDataManager.GetVictoryCueStickImage(i);
                victoryCue.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = cueStickDataManager.GetVictoryCueStickName(i);
                victoryCue.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = 
                    "Play in " + cueStickDataManager.GetVictoryCueStickCity(i) + " to get pieces";
                for (int j = cueStickDataManager.GetVictoryCueStickForce(i); j < 10; j++)
                {
                    victoryCue.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetVictoryCueStickAim(i); j < 10; j++)
                {
                    victoryCue.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetVictoryCueStickSpin(i); j < 10; j++)
                {
                    victoryCue.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetVictoryCueStickTime(i); j < 10; j++)
                {
                    victoryCue.transform.GetChild(0).GetChild(1).GetChild(3).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                victoryCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetVictoryCueStickType(i);
                victoryCue.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = 
                    cueStickDataManager.GetVictoryCueStickUnlockedPieces(i) + "/" + cueStickDataManager.GetVictoryCueStickTotalPieces(i);
                victoryCue.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = cueStickDataManager.GetVictoryCueStickUnlockedPieces(i) / (float)cueStickDataManager.GetVictoryCueStickTotalPieces(i);
                for (int k = 0; k < (4 - cueStickDataManager.GetVictoryCueStickUnlockedPieces(i)); k++)
                {
                    victoryCue.transform.GetChild(0).GetChild(7).GetChild(k).gameObject.SetActive(true);
                }

                if (cueStickDataManager.GetVictoryCueStickIsUnlockedFlag(i) == 1)
                {

                    victoryCue.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnVictoryCueExpandButtonClicked(count));
                    victoryCue.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnVictoryCueExpandButtonClicked(count));
                    if (cueStickDataManager.GetVictoryCueStickCurrentLevel(i) >= (cueStickDataManager.GetVictoryCueStickMaxLevel(i) + 1))
                    {
                        victoryCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetVictoryCueStickType(i) +
                        " Level MAX";
                    }
                    else
                    {
                        victoryCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetVictoryCueStickType(i) +
                        " Level " + cueStickDataManager.GetVictoryCueStickCurrentLevel(i);
                    }
                    victoryCue.transform.GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(true);
                    victoryCue.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
                    victoryCue.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text =
                        cueStickDataManager.GetVictoryCueStickCurrentSubLevel(i) + "/" + cueStickDataManager.GetVictoryCueStickMaxSubLevel(i);
                    victoryCue.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = cueStickDataManager.GetVictoryCueStickCurrentSubLevel(i) / (float)cueStickDataManager.GetVictoryCueStickMaxSubLevel(i);
                    if (cueStickDataManager.GetPlayerCueStickName() == cueStickDataManager.GetVictoryCueStickName(i))
                    {

                        victoryCue.transform.GetChild(1).GetChild(5).gameObject.SetActive(false);
                        victoryCue.transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
                    }
                    else
                    {

                        victoryCue.transform.GetChild(1).GetChild(5).gameObject.SetActive(true);
                        victoryCue.transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
                    }
                }


                victoryCue.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>().text = (cueStickDataManager.GetVictoryCueStickRechargePrice(i)).ToString();
                if (cueStickDataManager.GetVictoryCueStickCharge(i) >= 0)
                {
                    victoryCue.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetVictoryCueStickCharge(i) + "/50";
                }
                else
                {
                    victoryCue.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "0/50";
                }
                victoryCue.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Recharge";
                victoryCue.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 120, 0, 255);
                victoryCue.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnVictoryRechargeButtonClicked(count));
                int chargeBlocks = cueStickDataManager.GetVictoryCueStickCharge(i) / 10;
                Color chargeBlockColor;
                if (chargeBlocks >= 4)
                {
                    if (chargeBlocks == 5)
                    {
                        victoryCue.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Charged";
                        victoryCue.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                        victoryCue.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveListener(() => OnVictoryRechargeButtonClicked(count));
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
                        victoryCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = chargeBlockColor;
                    }
                    else
                    {
                        victoryCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(false);
                    }
                }
                if (chargeBlocks <= 0)
                {
                    victoryCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                    victoryCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
                }
                if (cueStickDataManager.GetVictoryCueStickAutoRecharge(i) == 1)
                {
                    victoryCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    victoryCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
                }
                victoryCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnVictoryAutoRechargeButtonClicked(count));
                victoryCue.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnVictoryAutoRechargeButtonClicked(count));
                victoryCue.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(() => VictoryCuePlayButtonClicked(count));
                victoryCue.transform.GetChild(1).GetChild(5).GetComponent<Button>().onClick.AddListener(() => OnVictoryCuesUseButonClicked(count));
                //standardCue.transform.GetChild(0).GetChild(5).GetComponent<Button>().onClick.AddListener(() => StandardCueBuyButtonClicked(count));
                victoryCueList.Add(victoryCue);
            }
        }

        public void OnVictoryCueExpandButtonClicked(int index)
        {
            if (victoryCueList[index].transform.GetChild(1).gameObject.activeSelf)
            {
                victoryCueList[index].transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
                victoryCueList[index].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                victoryCueList[index].transform.GetChild(0).GetChild(8).gameObject.SetActive(false);
                victoryCueList[index].transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        public void OnVictoryAutoRechargeButtonClicked(int index)
        {
            GameObject temp = victoryCueList[index].transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject;
            if (temp.activeSelf)
            {
                temp.SetActive(false);
                cueStickDataManager.SetVictoryCueStickAutoRecharge(index, 0);
            }
            else
            {
                temp.SetActive(true);
                cueStickDataManager.SetVictoryCueStickAutoRecharge(index, 1);
            }
        }

        public void OnVictoryRechargeButtonClicked(int index)
        {
            if (GameData.Instance().PlayerBalance >= cueStickDataManager.GetVictoryCueStickRechargePriceInNumbers(index))
            {
                GameData.Instance().PlayerBalance -= cueStickDataManager.GetVictoryCueStickRechargePriceInNumbers(index);
                uiManager.UpdateCoinData(GameData.Instance().PlayerBalance);
                cueStickDataManager.ResetVictoryCueStickCharge(index);
                victoryCueList[index].transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Charged";
                victoryCueList[index].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                victoryCueList[index].transform.GetChild(6).GetChild(0).GetComponent<Button>().onClick.RemoveListener(() => OnVictoryRechargeButtonClicked(index));
                victoryCueList[index].transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetVictoryCueStickCharge(index) + "/50";
                victoryCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
                for (int k = 0; k < 5; k++)
                {
                    victoryCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(true);
                    victoryCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0);
                }
            }
            else
            {
                DisplayInsufficientCoinsPopup();
            }
        }

        public void VictoryCuePlayButtonClicked(int itemNumber)
        {
            uiManager.play1On1ScreenParent.SetActive(true);
            //victoryCueList[itemNumber].transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            //victoryCueList[itemNumber].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            //victoryCueList[itemNumber].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            //victoryCueList[itemNumber].transform.GetChild(5).gameObject.SetActive(false);
            //victoryCueList[itemNumber].transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            //victoryCueList[itemNumber].transform.GetChild(0).GetComponent<Image>().enabled = false;
            //victoryCueList[itemNumber].transform.GetChild(2).gameObject.SetActive(false);
            //victoryCueList[itemNumber].transform.SetParent(ownedCueContent);
            //cuePurchaseImage.sprite = victoryCueList[itemNumber].transform.GetChild(7).GetComponent<Image>().sprite;
            //cueNamePurchaseText.text = victoryCueList[itemNumber].transform.GetChild(4).GetComponent<Text>().text;
            //uiManager.SubtractCoinData(250.ToString());
            //cuesPurchaseCompletePopup.SetActive(true);
            //ownedCueList.Add(victoryCueList[itemNumber]);
            //// standardCueList.RemoveAt(itemNumber);
            //print("Countttt222" + victoryCueList.Count);
        }

        public void OnVictoryCuesUseButonClicked(int index)
        {
            victoryCueList[index].transform.GetChild(1).GetChild(5).gameObject.SetActive(false);
            victoryCueList[index].transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
            cueStickDataManager.SetPlayerCueStick(victoryCueList[index].transform.GetChild(0).GetChild(4).GetComponent<Text>().text);
            playerCueStickIndex = cueStickDataManager.GetPlayerCueStick();
        }

        public void SurpriseButton()
        {
            standardScreen.SetActive(false);
            victoryScreen.SetActive(false);
            surpriseScreen.SetActive(true);
            countryScreen.SetActive(false);
            ownedScreen.SetActive(false);

            //loadSurpriseCuesList();
        }

        public void loadSurpriseCuesList()
        {
            for (int i = 0; i < surpriseCueContent.childCount; i++)
            {
                Destroy(surpriseCueContent.GetChild(i).gameObject);
            }
            SurpriseButtonCallBack();
        }

        public void SurpriseButtonCallBack()
        {
            surpriseCueList.Clear();
            for (int i = 0; i < cueStickDataManager.GetSurpriseCueStickCount(); i++)
            {
                int count = i;
                GameObject surpriseCue = Instantiate(surpriseCueData, surpriseCueContent);

                surpriseCue.transform.GetChild(0).GetChild(6).GetComponent<Image>().sprite = cueStickDataManager.GetSurpriseCueStickImage(i);
                surpriseCue.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = cueStickDataManager.GetSurpriseCueStickName(i);
                //surpriseCue.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = cueStickDataManager.GetSurpriseCueStickPrice(i);
                for (int j = cueStickDataManager.GetSurpriseCueStickForce(i); j < 10; j++)
                {
                    surpriseCue.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetSurpriseCueStickAim(i); j < 10; j++)
                {
                    surpriseCue.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetSurpriseCueStickSpin(i); j < 10; j++)
                {
                    surpriseCue.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetSurpriseCueStickTime(i); j < 10; j++)
                {
                    surpriseCue.transform.GetChild(0).GetChild(1).GetChild(3).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                surpriseCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetSurpriseCueStickType(i);

                surpriseCue.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text =
                cueStickDataManager.GetSurpriseCueStickUnlockedPieces(i) + "/" + cueStickDataManager.GetSurpriseCueStickTotalPieces(i);
                surpriseCue.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = cueStickDataManager.GetSurpriseCueStickUnlockedPieces(i) / (float)cueStickDataManager.GetSurpriseCueStickTotalPieces(i);
                for (int k = 0; k < (4 - cueStickDataManager.GetSurpriseCueStickUnlockedPieces(i)); k++)
                {
                    surpriseCue.transform.GetChild(0).GetChild(7).GetChild(k).gameObject.SetActive(true);
                }
                if (cueStickDataManager.GetSurpriseCueStickIsUnlockedFlag(i) == 1)
                {
                    surpriseCue.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnSurpriseCueExpandButtonClicked(count));
                    surpriseCue.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnSurpriseCueExpandButtonClicked(count));
                    if (cueStickDataManager.GetSurpriseCueStickCurrentLevel(i) >= (cueStickDataManager.GetSurpriseCueStickMaxLevel(i) + 1))
                    {
                        surpriseCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetSurpriseCueStickType(i) +
                        " Level MAX";
                    }
                    else
                    {
                        surpriseCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetSurpriseCueStickType(i) +
                        " Level " + cueStickDataManager.GetSurpriseCueStickCurrentLevel(i);
                    }
                    surpriseCue.transform.GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(true);
                    surpriseCue.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
                    surpriseCue.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text =
                        cueStickDataManager.GetSurpriseCueStickCurrentSubLevel(i) + "/" + cueStickDataManager.GetSurpriseCueStickMaxSubLevel(i);
                    surpriseCue.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = cueStickDataManager.GetSurpriseCueStickCurrentSubLevel(i) / (float)cueStickDataManager.GetSurpriseCueStickMaxSubLevel(i);
                    if (cueStickDataManager.GetPlayerCueStickName() == cueStickDataManager.GetSurpriseCueStickName(i))
                    {

                        surpriseCue.transform.GetChild(1).GetChild(5).gameObject.SetActive(false);
                        surpriseCue.transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
                    }
                    else
                    {
                        surpriseCue.transform.GetChild(1).GetChild(5).gameObject.SetActive(true);
                        surpriseCue.transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
                    }
                }
                surpriseCue.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>().text = (cueStickDataManager.GetSurpriseCueStickRechargePrice(i)).ToString();
                if (cueStickDataManager.GetSurpriseCueStickCharge(i) >= 0)
                {
                    surpriseCue.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetSurpriseCueStickCharge(i) + "/50";
                }
                else
                {
                    surpriseCue.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "0/50";
                }
                surpriseCue.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Recharge";
                surpriseCue.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 120, 0, 255);
                surpriseCue.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnSurpriseRechargeButtonClicked(count));
                int chargeBlocks = cueStickDataManager.GetSurpriseCueStickCharge(i) / 10;
                Color chargeBlockColor;
                if (chargeBlocks >= 4)
                {
                    if (chargeBlocks == 5)
                    {
                        surpriseCue.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Charged";
                        surpriseCue.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                        surpriseCue.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveListener(() => OnSurpriseRechargeButtonClicked(count));
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
                        surpriseCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = chargeBlockColor;
                    }
                    else
                    {
                        surpriseCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(false);
                    }
                }
                if (chargeBlocks <= 0)
                {
                    surpriseCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                    surpriseCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
                }
                if (cueStickDataManager.GetSurpriseCueStickAutoRecharge(i) == 1)
                {
                    surpriseCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    surpriseCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
                }
                surpriseCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnSurpriseAutoRechargeButtonClicked(count));
                surpriseCue.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnSurpriseAutoRechargeButtonClicked(count));

                surpriseCueList.Add(surpriseCue);
                surpriseCue.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(() => OnSurpriseCueBuyButtonClicked(count));
                surpriseCue.transform.GetChild(1).GetChild(5).GetComponent<Button>().onClick.AddListener(() => OnSurpriseCuesUseButonClicked(count));

            }
        }

        public void OnSurpriseCueExpandButtonClicked(int index)
        {
            if (surpriseCueList[index].transform.GetChild(1).gameObject.activeSelf)
            {
                surpriseCueList[index].transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
                surpriseCueList[index].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                surpriseCueList[index].transform.GetChild(0).GetChild(8).gameObject.SetActive(false);
                surpriseCueList[index].transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        public void OnSurpriseCueBuyButtonClicked(int itemNumber)
        {
            surpriseCueNameText.text = "Buy " + surpriseCueList[itemNumber].transform.GetChild(0).GetChild(5).GetComponent<Text>().text + " Box ?";
            surpriseCueBuyPopup.SetActive(true);

        }

        public void OnSurpriseAutoRechargeButtonClicked(int index)
        {
            GameObject temp = surpriseCueList[index].transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject;
            if (temp.activeSelf)
            {
                temp.SetActive(false);
                cueStickDataManager.SetSurpriseCueStickAutoRecharge(index, 0);
            }
            else
            {
                temp.SetActive(true);
                cueStickDataManager.SetSurpriseCueStickAutoRecharge(index, 1);
            }
        }

        public void OnSurpriseRechargeButtonClicked(int index)
        {
            if (GameData.Instance().PlayerBalance >= cueStickDataManager.GetSurpriseCueStickRechargePriceInNumbers(index))
            {
                GameData.Instance().PlayerBalance -= cueStickDataManager.GetSurpriseCueStickRechargePriceInNumbers(index);
                uiManager.UpdateCoinData(GameData.Instance().PlayerBalance);
                cueStickDataManager.ResetSurpriseCueStickCharge(index);
                surpriseCueList[index].transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Charged";
                surpriseCueList[index].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                surpriseCueList[index].transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveListener(() => OnSurpriseRechargeButtonClicked(index));
                surpriseCueList[index].transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetSurpriseCueStickCharge(index) + "/50";
                surpriseCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
                for (int k = 0; k < 5; k++)
                {
                    surpriseCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(true);
                    surpriseCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0);
                }
            }
            else
            {
                DisplayInsufficientCoinsPopup();
            }
        }

        public void OnSurpriseCuesUseButonClicked(int index)
        {
            surpriseCueList[index].transform.GetChild(1).GetChild(5).gameObject.SetActive(false);
            surpriseCueList[index].transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            cueStickDataManager.SetPlayerCueStick(surpriseCueList[index].transform.GetChild(0).GetChild(4).GetComponent<Text>().text);
            playerCueStickIndex = cueStickDataManager.GetPlayerCueStick();
        }

        public void SurpriseCueBuy1ButtonClicked()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 1);
            int legendaryCount = PlayerPrefs.GetInt("legendaryboxcount");
            PlayerPrefs.SetInt("legendaryboxcount", legendaryCount + 1);
            surpriseBoxCuesPurchaseScreen.SetActive(true);
            uiManager.SubtractCoinData(33.ToString());
        }

        public void SurpriseCueBuy2ButtonClicked()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 3);
            int legendaryCount = PlayerPrefs.GetInt("legendaryboxcount");
            PlayerPrefs.SetInt("legendaryboxcount", legendaryCount + 3);
            surpriseBoxCuesPurchaseScreen.SetActive(true);
            uiManager.SubtractCoinData(89.ToString());
        }
        public void surpriseCueBuyPopupCloseButtton()
        {
            surpriseCueBuyPopup.SetActive(false);
            surpriseBoxCuesPurchaseScreen.SetActive(false);
        }

        public void SurpriseBoxCuePlayMinigameButtonClicked()
        {
            surpriseCueBuyPopup.SetActive(false);
            surpriseBoxCuesPurchaseScreen.SetActive(false);
            this.gameObject.SetActive(false);
            uiManager.surpriseBoxScreenParent.SetActive(true);
            CuesMainScreen.SetActive(false);
        }

        public void CountryButton()
        {
            standardScreen.SetActive(false);
            victoryScreen.SetActive(false);
            surpriseScreen.SetActive(false);
            countryScreen.SetActive(true);
            ownedScreen.SetActive(false);

            //loadCountryCuesList();
        }

        public void loadCountryCuesList()
        {
            for (int i = 0; i < countryCueContent.childCount; i++)
            {
                Destroy(countryCueContent.transform.GetChild(i).gameObject);

            }
            CountryButtonCallBack();
        }

        public void CountryButtonCallBack()
        {
            countryCueList.Clear();
            for (int i = 0; i < cueStickDataManager.GetCountryCueStickCount(); i++)
            {
                int count = i;
                GameObject countryCue = Instantiate(countryCueData,countryCueContent);
                countryCue.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnCountryCueExpandButtonClicked(count));
                countryCue.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnCountryCueExpandButtonClicked(count));

                countryCue.transform.GetChild(0).GetChild(6).GetComponent<Image>().sprite = cueStickDataManager.GetCountryCueStickImage(i);
                countryCue.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = cueStickDataManager.GetCountryCueStickName(i);
                countryCue.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = cueStickDataManager.GetCountryCueStickPrice(i);
                for (int j = cueStickDataManager.GetCountryCueStickForce(i); j < 10; j++)
                {
                    countryCue.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetCountryCueStickAim(i); j < 10; j++)
                {
                    countryCue.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetCountryCueStickSpin(i); j < 10; j++)
                {
                    countryCue.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                for (int j = cueStickDataManager.GetCountryCueStickTime(i); j < 10; j++)
                {
                    countryCue.transform.GetChild(0).GetChild(1).GetChild(3).GetChild(1).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
                countryCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetCountryCueStickType(i);
                if (cueStickDataManager.GetCountryCueStickIsUnlockedFlag(i) == 1)
                {
                    if (cueStickDataManager.GetCountryCueStickCurrentLevel(i) >= (cueStickDataManager.GetCountryCueStickMaxLevel(i) + 1))
                    {
                        countryCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetCountryCueStickType(i) +
                        " Level MAX";
                    }
                    else
                    {
                        countryCue.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetCountryCueStickType(i) +
                    " Level " + cueStickDataManager.GetCountryCueStickCurrentLevel(i);
                    }
                    countryCue.transform.GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(true);
                    countryCue.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
                    countryCue.transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
                    countryCue.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.SetActive(true);
                    countryCue.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text =
                        cueStickDataManager.GetCountryCueStickCurrentSubLevel(i) + "/" + cueStickDataManager.GetCountryCueStickMaxSubLevel(i);
                    countryCue.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = cueStickDataManager.GetCountryCueStickCurrentSubLevel(i) / (float)cueStickDataManager.GetCountryCueStickMaxSubLevel(i);

                    countryCue.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = 
                        "Play in " + cueStickDataManager.GetCountryCueStickCity(i) + " to get pieces";
                    countryCue.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    if (cueStickDataManager.GetPlayerCueStickName() == cueStickDataManager.GetCountryCueStickName(i))
                    {
                        countryUsingCueStickIndex = i;
                        countryCue.transform.GetChild(1).GetChild(5).gameObject.SetActive(false);
                        countryCue.transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
                    }
                    else
                    {

                        countryCue.transform.GetChild(1).GetChild(5).gameObject.SetActive(true);
                        countryCue.transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
                    }
                }
                countryCue.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>().text = (cueStickDataManager.GetCountryCueStickRechargePrice(i)).ToString();
                if (cueStickDataManager.GetCountryCueStickCharge(i) >= 0)
                {
                    countryCue.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetCountryCueStickCharge(i) + "/50";
                }
                else
                {
                    countryCue.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "0/50";
                }
                countryCue.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Recharge";
                countryCue.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 120, 0, 255);
                countryCue.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnCountryRechargeButtonClicked(count));
                int chargeBlocks = cueStickDataManager.GetCountryCueStickCharge(i) / 10;
                Color chargeBlockColor;
                if (chargeBlocks >= 4)
                {
                    if (chargeBlocks == 5)
                    {
                        countryCue.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Charged";
                        countryCue.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                        countryCue.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveListener(() => OnCountryRechargeButtonClicked(count));
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
                        countryCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = chargeBlockColor;
                    }
                    else
                    {
                        countryCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(false);
                    }
                }
                if (chargeBlocks <= 0)
                {
                    countryCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                    countryCue.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
                }
                if (cueStickDataManager.GetCountryCueStickAutoRecharge(i) == 1)
                {
                    countryCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    countryCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
                }
                countryCue.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnCountryAutoRechargeButtonClicked(count));
                countryCue.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnCountryAutoRechargeButtonClicked(count));
                countryCueList.Add(countryCue);
                countryCue.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(() => OnCountryCuePlayButtonClicked(count));
                countryCue.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnCountryCueBuyButtonClicked(count));
                countryCue.transform.GetChild(1).GetChild(5).GetComponent<Button>().onClick.AddListener(() => OnCountryCuesUseButtonClicked(count));
            }
        }

        public void OnCountryCueExpandButtonClicked(int index)
        {
            if (countryCueList[index].transform.GetChild(1).gameObject.activeSelf)
            {
                countryCueList[index].transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
                countryCueList[index].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                countryCueList[index].transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
                countryCueList[index].transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        public void OnCountryCuePlayButtonClicked(int itemNumber)
        {
            uiManager.play1On1ScreenParent.SetActive(true);
        }

        public void OnCountryAutoRechargeButtonClicked(int index)
        {
            GameObject temp = countryCueList[index].transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject;
            if (temp.activeSelf)
            {
                temp.SetActive(false);
                cueStickDataManager.SetCountryCueStickAutoRecharge(index, 0);
            }
            else
            {
                temp.SetActive(true);
                cueStickDataManager.SetCountryCueStickAutoRecharge(index, 1);
            }
        }

        public void OnCountryRechargeButtonClicked(int index)
        {
            if (GameData.Instance().PlayerBalance >= cueStickDataManager.GetCountryCueStickRechargePriceInNumbers(index))
            {
                GameData.Instance().PlayerBalance -= cueStickDataManager.GetCountryCueStickRechargePriceInNumbers(index);
                uiManager.UpdateCoinData(GameData.Instance().PlayerBalance);
                cueStickDataManager.ResetCountryCueStickCharge(index);
                countryCueList[index].transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().text = "Charged";
                countryCueList[index].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(102, 99, 129, 255);
                countryCueList[index].transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveListener(() => OnCountryRechargeButtonClicked(index));
                countryCueList[index].transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = cueStickDataManager.GetCountryCueStickCharge(index) + "/50";
                countryCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
                for (int k = 0; k < 5; k++)
                {
                    countryCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).gameObject.SetActive(true);
                    countryCueList[index].transform.GetChild(1).GetChild(1).GetChild(1).GetChild(k).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0);
                }
            }
            else
            {
                DisplayInsufficientCoinsPopup();
            }
        }

        public void OnCountryCueBuyButtonClicked(int itemNumber)
        {
            if (isCoinsSufficient(cueStickDataManager.GetCountryCueStickPriceInNumber(itemNumber)))
            {
                /*countryCueList[itemNumber].transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                countryCueList[itemNumber].transform.GetChild(5).gameObject.SetActive(false);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
                countryCueList[itemNumber].transform.GetChild(0).GetComponent<Image>().enabled = false;
                countryCueList[itemNumber].transform.GetChild(2).gameObject.SetActive(false);
                countryCueList[itemNumber].transform.SetParent(ownedCueContent);
                cuePurchaseImage.sprite = countryCueList[itemNumber].transform.GetChild(7).GetComponent<Image>().sprite;
                cueNamePurchaseText.text = countryCueList[itemNumber].transform.GetChild(4).GetComponent<Text>().text;
                uiManager.SubtractCoinData(40.ToString());
                cuesPurchaseCompletePopup.SetActive(true);
                ownedCueList.Add(countryCueList[itemNumber]);*/
                //countryCueList.RemoveAt(itemNumber);

                GameData.Instance().PlayerBalance -= cueStickDataManager.GetCountryCueStickPriceInNumber(itemNumber);
                uiManager.UpdateCoinData(GameData.Instance().PlayerBalance);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(4).gameObject.SetActive(true);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);
                cueNamePurchaseText.text = countryCueList[itemNumber].transform.GetChild(0).GetChild(4).GetComponent<Text>().text;
                cuesPurchaseCompletePopup.SetActive(true);
                cueStickDataManager.ObtainedCountryCueStick(itemNumber);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(5).GetComponent<Text>().text = cueStickDataManager.GetCountryCueStickType(itemNumber) +
                    " Level " + cueStickDataManager.GetCountryCueStickCurrentLevel(itemNumber);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(true);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
                countryCueList[itemNumber].transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(4).gameObject.SetActive(true);
                countryCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text =
                    "Play in " + cueStickDataManager.GetCountryCueStickCity(itemNumber) + " to get pieces";
                countryCueList[itemNumber].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

                if (countryUsingCueStickIndex != -1)
                {
                    countryCueList[countryUsingCueStickIndex].transform.GetChild(1).GetChild(5).gameObject.SetActive(true);
                    countryCueList[countryUsingCueStickIndex].transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
                }
                countryUsingCueStickIndex = itemNumber;
                countryCueList[countryUsingCueStickIndex].transform.GetChild(1).GetChild(5).gameObject.SetActive(false);
                countryCueList[countryUsingCueStickIndex].transform.GetChild(1).GetChild(4).gameObject.SetActive(true);

                countryCueList[itemNumber].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text =
                    cueStickDataManager.GetCountryCueStickCurrentSubLevel(itemNumber) + "/" + cueStickDataManager.GetCountryCueStickMaxSubLevel(itemNumber);
            }
            else
            {
                DisplayInsufficientCoinsPopup();
            }
        }

        public void OnCountryCuesUseButtonClicked(int index)
        {
            countryCueList[index].transform.GetChild(1).GetChild(5).gameObject.SetActive(false);
            countryCueList[index].transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
            cueStickDataManager.SetPlayerCueStick(countryCueList[index].transform.GetChild(0).GetChild(4).GetComponent<Text>().text);
            playerCueStickIndex = cueStickDataManager.GetPlayerCueStick();
            if (countryUsingCueStickIndex != -1)
            {
                countryCueList[countryUsingCueStickIndex].transform.GetChild(1).GetChild(5).gameObject.SetActive(true);
                countryCueList[countryUsingCueStickIndex].transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
            }
            countryUsingCueStickIndex = index;
        }

        public void OwnedButton()
        {
            standardScreen.SetActive(false);
            victoryScreen.SetActive(false);
            surpriseScreen.SetActive(false);
            countryScreen.SetActive(false);
            ownedScreen.SetActive(true);
            //print(ownedCueContent.childCount+"owned");
            for (int i = 0; i < ownedCueContent.childCount; i++)
            {
                Destroy(ownedCueContent.transform.GetChild(i).gameObject);
                
            }
            playerCueStickIndex = cueStickDataManager.GetPlayerCueStick();
            OwendButtonCallBack();
        }

        public void OwendButtonCallBack()
        {
            ownedCueList.Clear();
            //print(cueStickDataManager.GetOwnedCueStickCount());
            for (int i = 0; i < cueStickDataManager.GetOwnedCueStickCount(); i++)
            {
                Vector2 autoChargePosition =new  Vector2 (682.55f, 23);
                Vector3 chargePosition =new  Vector3 (380f, 23f,0f);
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

                ownedCue.transform.SetParent(ownedCueContent,false);
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
                if(cueStickDataManager.GetOwnedCueStickCharge(i)>=0)
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
                if(chargeBlocks >= 4)
                {
                    if(chargeBlocks == 5)
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
                if(chargeBlocks <= 0)
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
                ownedCue.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnOwnedCuesUseButonClicked(count));

                if(i == playerCueStickIndex)
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
            if (GameData.Instance().PlayerBalance >= cueStickDataManager.GetRechargePriceInNumbers())
            {
                uiManager.SubtractCoinData(cueStickDataManager.GetRechargePriceInNumbers().ToString());
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
                DisplayInsufficientCoinsPopup();
            }
        }

        public void OnOwnedCuesUseButonClicked(int itemNumber)
        {
            ownedCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
            ownedCueList[itemNumber].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
            ownedCueList[playerCueStickIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
            cueStickDataManager.SetPlayerCueStick(ownedCueList[itemNumber].transform.GetChild(0).GetChild(4).GetComponent<Text>().text);
            playerCueStickIndex = itemNumber;
            loadVictoryCuesList();
            loadCountryCuesList();
            loadSurpriseCuesList();
        }

        public void AvatarButton()
        {
            avatarScreen.SetActive(true);
            chatPaksScreen.SetActive(false);
            stickersScreen.SetActive(false);
        }

        public void ChatPacksButton()
        {
            avatarScreen.SetActive(false);
            chatPaksScreen.SetActive(true);
            stickersScreen.SetActive(false);
        }

        public void StickersButton()
        {
            avatarScreen.SetActive(false);
            chatPaksScreen.SetActive(false);
            stickersScreen.SetActive(true);
        }

        public void OnCoinsButtonClicked()
        {
            OnMainShopCoinsButtonClicked();
        }
        public void GetRareBoxButton()
        {
            buyEpicBoxPopup.SetActive(true);

        }
        public void GetEpicBoxButton()
        {
            buyBoxPopupText.text = "Buy Epic Box?";
            purchaseCompletePopupText.text = "Epic Box";
            buyEpicBoxPopup.SetActive(true);

        }

        public void GetLegendaryBoxButton()
        {
            buyBoxPopupText.text = "Buy Legendary Box?";
            purchaseCompletePopupText.text = "Legendaary Box";
            buyEpicBoxPopup.SetActive(true);

        }
        public void GetScratcherPackButton()
        {
            buyBoxPopupText.text = "Buy Scratcher Pack?";
            purchaseCompletePopupText.text = "Scratcher Pack";
            buyEpicBoxPopup.SetActive(true);
        }
        public void JumboScratcPackButton()
        {
            buyBoxPopupText.text = "Buy JumboScratcher Pack?";
            purchaseCompletePopupText.text = "Scratcher Pack";
            buyEpicBoxPopup.SetActive(true);
        }
        public void CuesPruchasePopupCloseButton()
        {
            cuesPurchaseCompletePopup.SetActive(false);
        }
        public void BuyRareBoxButtonClicked()
        {
            buyEpicBoxPopup.SetActive(false);
            purchaseCompleteEpicBoxPopup.SetActive(true);
        }
        public void BuyBoxCloseButton()
        {
            buyEpicBoxPopup.SetActive(false);
        }
        public void PurchaseCompleteCloseButton()
        {
            buyEpicBoxPopup.SetActive(false);
            purchaseCompleteEpicBoxPopup.SetActive(false);

        }
        public void CuesInfoButtonClicked()
        {
            cuesInfoMainScreen.SetActive(true);
            CuesInfo1ButtonClicked();
           // cuesInfo1Screen.SetActive(true);
        }
        public void CuesInfoCloseButtonClicked()
        {
            cuesInfoMainScreen.SetActive(false);
            //cuesInfo1Screen.SetActive(false);
            //cuesInfo2Screen.SetActive(false);
            //cuesInfo3Screen.SetActive(false);
        }
        public void PlyMiniGameButtonClicked()
        {
            uiManager.surpriseBoxScreenParent.transform.GetComponent<SurpriseBoxScreen>().SurpriseBoxPlayMinigame();
            uiManager.spinWheelScreenParent.transform.GetComponent<SpinWheelScreen>().SurpriseBoxPlayMinigame();
            uiManager.scratchScreenParent.transform.GetComponent<ScratchScreen>().SurpriseBoxPlayMinigame();
            this.gameObject.SetActive(false);
            purchaseCompleteEpicBoxPopup.SetActive(false);
            surpriseBoxes.SetActive(false);
            uiManager.surpriseBoxScreenParent.SetActive(true);
            shopMainScreen.SetActive(true);
        }
        public void SocialPurchaseOkButton()
        {
            socialPurchasePopupScreen.SetActive(false);
        }
        public void AvatarBuyButton()
        {
            socialPurchaseText.text = "Avatars Successfully purchased";
            socialPurchasePopupScreen.SetActive(true);
        }
        public void ChatPacksBuyButton1Clicked()
        {
            chatPackBuyButton1.SetActive(false);
            uiManager.SubtractCoinData(1000.ToString());
            socialPurchaseText.text = "Chatpacks Successfully purchased";
            socialPurchasePopupScreen.SetActive(true);
        }
        public void ChatPackBuyButton2Clicked()
        {
            uiManager.SubtractCoinData(1500.ToString());
            chatPackBuyButton2.SetActive(false);
            socialPurchaseText.text = "Chatpacks Successfully purchased";
            socialPurchasePopupScreen.SetActive(true);
        }

        public void StickersButButton()
        {
            socialPurchaseText.text = "Stickers Successfully purchased";
            socialPurchasePopupScreen.SetActive(true);
        }
        public void CoinBuyButton1Clickrd()
        {
            GameData.Instance().PlayerBalance += 8900;
            uiManager.UpdateCoinData(GameData.Instance().PlayerBalance);
            socialPurchaseText.text = "Coins Successfully purchased";
            socialPurchasePopupScreen.SetActive(true);
        }
        public void CoinBuyButton2Clickrd()
        {
            GameData.Instance().PlayerBalance += 200000000;
            uiManager.UpdateCoinData(GameData.Instance().PlayerBalance);
            socialPurchaseText.text = "Coins Successfully purchased";
            socialPurchasePopupScreen.SetActive(true);
        }
        public void CuesInfo1ButtonClicked()
        {
            cuesInfoContent.localPosition = new Vector2(0f, -0.0009519864f);
            cuesInfoButton1.color = new Color32(255, 255, 255, 255);
            cuesInfoButton2.color = new Color32(142, 142, 142, 255);
            cuesInfoButton3.color = new Color32(142, 142, 142, 255);
            //cuesInfo1Screen.SetActive(true);
           // cuesInfo2Screen.SetActive(false);
            //cuesInfo3Screen.SetActive(false);
        }
        public void CuesInfo2ButtonClicked()
        {
            cuesInfoContent.localPosition = new Vector2(-1167.408f, -0.0009519864f);
            cuesInfoButton1.color = new Color32(142, 142, 142, 255);
            cuesInfoButton2.color = new Color32(255, 255, 255, 255);
            cuesInfoButton3.color = new Color32(142, 142, 142, 255);

            //cuesInfo1Screen.SetActive(false);
            //cuesInfo2Screen.SetActive(true);
            //cuesInfo3Screen.SetActive(false);
        }
        public void CuesInfo3ButtonClicked()
        {
            cuesInfoContent.localPosition = new Vector2(-2307.65f, -0.0009519864f);
            cuesInfoButton1.color = new Color32(142, 142, 142, 255);
            cuesInfoButton2.color = new Color32(142, 142, 142, 255);
            cuesInfoButton3.color = new Color32(255, 255, 255, 255);
            
        }
        public void CuesInfo1ButtonColorchange()
        {
            
            cuesInfoButton1.color = new Color32(255, 255, 255, 255);
            cuesInfoButton2.color = new Color32(142, 142, 142, 255);
            cuesInfoButton3.color = new Color32(142, 142, 142, 255);
           
        }
        public void CuesInfo2ButtonColorchange()
        {
            
            cuesInfoButton1.color = new Color32(142, 142, 142, 255);
            cuesInfoButton2.color = new Color32(255, 255, 255, 255);
            cuesInfoButton3.color = new Color32(142, 142, 142, 255);

           
        }
        public void CuesInfo3ButtonColorchange()
        {
           
            cuesInfoButton1.color = new Color32(142, 142, 142, 255);
            cuesInfoButton2.color = new Color32(142, 142, 142, 255);
            cuesInfoButton3.color = new Color32(255, 255, 255, 255);
            
        }




        public void GetFreeCoinButtonClicked()
        {
            getFreeCoinsPopup.SetActive(true);
        }
        public void GetFreeCoinPopupCloseButton()
        {
            getFreeCoinsPopup.SetActive(false);
        }
        public void coinsUpdate()
        {
            //long count = coinCount + int.Parse(shopMainCoinText.text);
            shopMainCoinText.text = GameData.Instance().PlayerBalance.ToString();
            surpriseBoxCoinText.text = GameData.Instance().PlayerBalance.ToString();
            scratchersCoinText.text = GameData.Instance().PlayerBalance.ToString();
            cuesMainCoinText.text = GameData.Instance().PlayerBalance.ToString();
            socialMainCoinText.text = GameData.Instance().PlayerBalance.ToString();
            shopCoinText.text = GameData.Instance().PlayerBalance.ToString();
            //PlayerPrefs.SetInt("coin amount", count);
        }
        public void SubtractCoinsData(int coinCount)
        {
            int count =int.Parse(shopMainCoinText.text)-coinCount;
            shopMainCoinText.text = count.ToString();
            surpriseBoxCoinText.text = count.ToString();
            scratchersCoinText.text = count.ToString();
            cuesMainCoinText.text = count.ToString();
            socialMainCoinText.text = count.ToString();
            shopCoinText.text = count.ToString();
            //PlayerPrefs.SetInt("coin amount", count);
        }

        public void OnSocialBackButtonClicked()
        {
            if (screenName == "ProfileScreen")
            {
                uiManager.profileScreenParent.SetActive(true);
                this.gameObject.SetActive(false);
                socialMainScreen.SetActive(false);
                screenName = "";
            }
            else 
            {
                shopMainScreen.SetActive(true);
                socialMainScreen.SetActive(false);
            }
        }
        public void OnShopCoinsBackButtonClicked()
        {
            if (screenName == "homeScreen")
            {
                shopCoinScreen.SetActive(false);
                this.gameObject.SetActive(false);
                uiManager.homeScreenParent.SetActive(true);
                screenName = "";
            }
            else if (screenName == "giftscreen")
            {
                uiManager.giftsScreenParent.SetActive(true);
                this.gameObject.SetActive(false);
                shopCoinScreen.SetActive(false);
                screenName = "";
            }
            else
            {
                shopMainScreen.SetActive(true);
                shopCoinScreen.SetActive(false);
            }
        }
        public void OnCuesBackButtonClicked()
        {
            if(screenName== "homeScreen")
            {
                this.gameObject.SetActive(false);
                uiManager.homeScreenParent.SetActive(true);
                CuesMainScreen.SetActive(false);
                screenName = "";

            }
            else
            {
                CuesMainScreen.SetActive(false);
                shopMainScreen.SetActive(true);
            }
        }
        public void OnSurpriseBoxBackButtonClicked()
        {
            shopMainScreen.SetActive(true);
            surpriseBoxes.SetActive(false);
        }
        public void OnScratchBackButtonClicked()
        {
            shopMainScreen.SetActive(true);
            scratchers.SetActive(false);
        }




        // surprise box screen

        public void RareBoxBuyButton1Clicked()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 1);
            int rareCount = PlayerPrefs.GetInt("rareboxcount");
            PlayerPrefs.SetInt("rareboxcount", rareCount + 1);
            rareBoxCountText.text = PlayerPrefs.GetInt("rareboxcount").ToString();
            //rareBoxCount = 1;
            //rareBoxCountText.text = rareBoxCount.ToString();
            rareBoxCountImage.gameObject.SetActive(true);
            purchaseCompleteBoxNameText.text = "Rare Box";
            purchaseCompleteBoxImage.sprite = rareBoxImage.sprite;
            purchaseCompleteEpicBoxPopup.SetActive(true);
            uiManager.SubtractCoinData(5.ToString());

        }
        public void RareBoxBuyButton2Clicked()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 3);
            int rareCount = PlayerPrefs.GetInt("rareboxcount");
            PlayerPrefs.SetInt("rareboxcount", rareCount + 3);
            rareBoxCountText.text = PlayerPrefs.GetInt("rareboxcount").ToString();
            //rareBoxCount = 1;
            //rareBoxCountText.text = rareBoxCount.ToString();
            rareBoxCountImage.gameObject.SetActive(true);
            purchaseCompleteBoxNameText.text = "Rare Box";
            purchaseCompleteBoxImage.sprite = rareBoxImage.sprite;
            surpriseBoxPurchaseCompleteScreen.SetActive(true);
            uiManager.SubtractCoinData(14.ToString());
        }
        public void EpicBoxBuy1ButtonClicked()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 1);
            int epicCount = PlayerPrefs.GetInt("epicboxcount");
            PlayerPrefs.SetInt("epicboxcount", epicCount + 1);
            epicBoxCountText.text = PlayerPrefs.GetInt("epicboxcount").ToString();
            //epicBoxCount = 1;
            //epicBoxCountText.text = epicBoxCount.ToString();
            epicBoxCountImage.gameObject.SetActive(true);
            purchaseCompleteBoxNameText.text = "Epic Box";
            purchaseCompleteBoxImage.sprite =epicBoxImage.sprite;
            surpriseBoxPurchaseCompleteScreen.SetActive(true);
            uiManager.SubtractCoinData(16.ToString());
        }
        public void EpicBoxBuy2ButtonClicked()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 3);
            int epicCount = PlayerPrefs.GetInt("epicboxcount");
            PlayerPrefs.SetInt("epicboxcount", epicCount + 3);
            epicBoxCountText.text = PlayerPrefs.GetInt("epicboxcount").ToString();
            //epicBoxCount = 1;
            //epicBoxCountText.text = epicBoxCount.ToString();
            epicBoxCountImage.gameObject.SetActive(true);
            purchaseCompleteBoxNameText.text = "Epic Box";
            purchaseCompleteBoxImage.sprite = epicBoxImage.sprite;
            surpriseBoxPurchaseCompleteScreen.SetActive(true);
            uiManager.SubtractCoinData(44.ToString());
        }
        public void LegendaryBoxBuy1ButttonClicked()
        {
            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 1);
            int legendaryCount = PlayerPrefs.GetInt("legendaryboxcount");
            PlayerPrefs.SetInt("legendaryboxcount", legendaryCount + 1);
            legendaryBoxCountText.text = PlayerPrefs.GetInt("legendaryboxcount").ToString();
            //legendaryBoxCount = 1;
            //legendaryBoxCountText.text = legendaryBoxCount.ToString();
            legendaryBoxCountImage.gameObject.SetActive(true);
            purchaseCompleteBoxNameText.text = "Legendary";
            purchaseCompleteBoxImage.sprite = legendaryBoxImage.sprite;
            surpriseBoxPurchaseCompleteScreen.SetActive(true);
            uiManager.SubtractCoinData(33.ToString());
        }
        public void LegendaryBoxBuy2ButttonClicked()
        {

            int count = PlayerPrefs.GetInt("boxcount");
            PlayerPrefs.SetInt("boxcount", count + 3);
            int legendaryCount = PlayerPrefs.GetInt("legendaryboxcount");
            PlayerPrefs.SetInt("legendaryboxcount", legendaryCount + 3);
            legendaryBoxCountText.text = PlayerPrefs.GetInt("legendaryboxcount").ToString();
            //legendaryBoxCount = 1;
            //legendaryBoxCountText.text = legendaryBoxCount.ToString();
            legendaryBoxCountImage.gameObject.SetActive(true);
            purchaseCompleteBoxNameText.text = "Legendary";
            purchaseCompleteBoxImage.sprite = legendaryBoxImage.sprite;
            surpriseBoxPurchaseCompleteScreen.SetActive(true);
            uiManager.SubtractCoinData(89.ToString());
        }
        public void SurpriseBoxPurchaseCloseButton()
        {
            surpriseBoxPurchaseCompleteScreen.SetActive(false);
        }
        public void surpriseBoxPlayminiGameButtonClicked()
        {
            uiManager.surpriseBoxScreenParent.transform.GetComponent<SurpriseBoxScreen>().SurpriseBoxPlayMinigame();
            uiManager.spinWheelScreenParent.transform.GetComponent<SpinWheelScreen>().SurpriseBoxPlayMinigame();
            uiManager.scratchScreenParent.transform.GetComponent<ScratchScreen>().SurpriseBoxPlayMinigame();
            surpriseBoxes.SetActive(false);
            this.gameObject.SetActive(false);
            surpriseBoxPurchaseCompleteScreen.SetActive(false);
            uiManager.surpriseBoxScreenParent.SetActive(true);
            
        }


        //Scratch scree
        public void ScratchBuyButtonClicked()
        {
            scratchPackName.text = "Scratcher Pack";
            scratchePurchasePopupScreen.SetActive(true);
            int count = PlayerPrefs.GetInt("scratchcount");
            PlayerPrefs.SetInt("scratchcount", count + 1);
            uiManager.SubtractCoinData(20.ToString());
        }
        public void ScratchJumboBuyButtonClicked()
        {
            scratchPackName.text = "Jumbo Scratcher Pack";
            scratchePurchasePopupScreen.SetActive(true);
            int count = PlayerPrefs.GetInt("scratchcount");
            PlayerPrefs.SetInt("scratchcount", count + 3);
            uiManager.SubtractCoinData(50.ToString());
        }
        public void ScratcherPurchaseCloseButtonClicked()
        {
            scratchePurchasePopupScreen.SetActive(false);
        }
        public void ScratcherPlayMiniGameButtonClicked()
        {
            uiManager.scratchScreenParent.transform.GetComponent<ScratchScreen>().ScratchersPlayMinigame();
            uiManager.surpriseBoxScreenParent.transform.GetComponent<SurpriseBoxScreen>().ScratchersPlayMinigame();
            uiManager.spinWheelScreenParent.transform.GetComponent<SpinWheelScreen>().ScratchersPlayMinigame();
            this.gameObject.SetActive(false);
            scratchePurchasePopupScreen.SetActive(false);
            scratchers.SetActive(false);
            uiManager.scratchScreenParent.SetActive(true);
        }

        //Function to check if coins are sufficent
        public bool isCoinsSufficient(long requiredCoins)
        {
            if(GameData.Instance().PlayerBalance >= requiredCoins)
            {
                return true;
            }
            return false;
        }
    }
}
