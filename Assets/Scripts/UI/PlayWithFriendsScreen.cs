using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;

namespace RevolutionGames.UI
{
    public class PlayWithFriendsScreen : MonoBehaviour
    {
        public UIManager uiManager;
        public GameObject playWithFriendsMainScreen;
        public GameObject playWithFriendsScreen;
        public GameObject playWithFriendsOfFriendsScreen;
        public GameObject friendsActivityScreen;
        public GameObject selectCityScreen;
        public GameObject findFriendsScreen;
        public Transform friendsChallengeContent;
        public Transform friendsChallengeContentEmpty;
        public GameObject friendsChallengePrefab;
        public Transform friendsOfFriendsChallengeContent;
        public GameObject friendsOfFriendsChallengePrefab;
        public Transform friendsActivityContent;
        public GameObject friendsActivityPrefab;
        public GameObject selectCityContent;
        public GameObject selectCityPrefab;
        public Transform findFriendsContent;
        public GameObject findFriendsPrefab;
        public GameObject findFriendsMeassageText;
        public GameObject addFriendsPopup;
        public GameObject findFriendsScrollView;
   
        private GameObject friendsChallenge;
        private GameObject friendsOffFriendsChallenge;
        private GameObject friendsActivity;
        private GameObject findFriends;
        List<GameObject> friendsChallengeList = new List<GameObject>();
        List<GameObject> friendsOfFriendsChallengeList = new List<GameObject>();
        List<GameObject> friendsActivityList = new List<GameObject>();
        List<GameObject> findFriendsList = new List<GameObject>();


        public void Start()
        {
            for(int i=0;i<3;i++)
            {
                GenerateFriendsList(i);
                GenerateFriendsOfFriendsList(i);
                GenerateFriendsActivityList(i);
                GeneraateFindFriendsList(i);
            }
           
        }

        public void GenerateFriendsList(int numberOfFriends)
        {
            friendsChallenge = Instantiate(friendsChallengePrefab, friendsChallengeContent);
            friendsChallengeList.Add(friendsChallenge);
            friendsChallengeList[0].transform.GetChild(4).GetComponent<Text>().text = "Sree Kumar RS";
            friendsChallenge.transform.GetChild(11).GetComponent<Button>().onClick.AddListener(() => FriendsChallengeButton(numberOfFriends));
        }
        public void GenerateFriendsOfFriendsList(int numberOffFriends)
        {
            friendsOffFriendsChallenge = Instantiate(friendsOfFriendsChallengePrefab, friendsOfFriendsChallengeContent);
            friendsOfFriendsChallengeList.Add(friendsOffFriendsChallenge);
            friendsOfFriendsChallengeList[0].transform.GetChild(2).GetComponent<Text>().text = "Sree Kumar RS";
            friendsOffFriendsChallenge.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(()=>FriendsOfFriendsChallengeButton(numberOffFriends));
            friendsOffFriendsChallenge.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(()=>FriendsOfFriendsAddButton(numberOffFriends));

        }
        public void GenerateFriendsActivityList(int numberOfFriends)
        {
            friendsActivity = Instantiate(friendsActivityPrefab, friendsActivityContent);
            friendsActivityList.Add(friendsActivity);
            friendsActivity.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => FriendsActivityChallengeButton(numberOfFriends));
        }
        public void GeneraateFindFriendsList(int numberOfFriends)
        {
            findFriends = Instantiate(findFriendsPrefab, findFriendsContent);
            findFriendsList.Add(findFriends);
            findFriendsList[0].transform.GetChild(2).GetComponent<Text>().text = "Sree Kumar RS";
            findFriends.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(() => AddFriendsButtonClicked(numberOfFriends));

        }





        public void OnFriendsButtonCallBack(List<Friend> friends)
        {
            for (int i = 0; i < friendsChallengeContent.transform.childCount; i++)
            {
                Destroy(friendsChallengeContent.transform.GetChild(i));
            }
            for (int i = 0; i < friends.Count; i++)
            {
                int friendsId = friends[i].friend_id;
                GameObject friendsData = Instantiate(friendsChallengePrefab,friendsChallengeContent.transform);
                friendsData.transform.GetChild(4).GetComponent<Text>().text = friends[i].player_name;
                friendsData.transform.GetChild(11).GetComponent<Button>().onClick.AddListener(delegate { OnFriendsChallengeButtonClicked(friendsId.ToString()); });
            }
            playWithFriendsMainScreen.SetActive(true);
            playWithFriendsScreen.SetActive(true);
            playWithFriendsOfFriendsScreen.SetActive(false);
            friendsActivityScreen.SetActive(false);
        }
        public void OnFriendsChallengeButtonClicked(string friendsId)
        {
            uiManager.apiManager.APIInviteFriend(friendsId);
        }
        public void OnFriendsChallengeButtonCallBack()
        {

        }
        public void OnFriendsButtonClickead()
        {
            playWithFriendsMainScreen.SetActive(true);
            playWithFriendsScreen.SetActive(true);
            playWithFriendsOfFriendsScreen.SetActive(false);
            friendsActivityScreen.SetActive(false);
        }
        public void OnFriendsOfFriendsButtonClicked()
        {
            playWithFriendsMainScreen.SetActive(true);
            playWithFriendsScreen.SetActive(false);
            playWithFriendsOfFriendsScreen.SetActive(true);
            friendsActivityScreen.SetActive(false);


        }
        public void OnFriendsOfFriendsButtonCallBack(List<Friend> friends)
        {
            for (int i = 0; i < friendsOfFriendsChallengeContent.transform.childCount; i++)
            {
                Destroy(friendsOfFriendsChallengeContent.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < friends.Count; i++)
            {
                int friendsId = friends[i].friend_id;
                GameObject friendsData = Instantiate(friendsOfFriendsChallengePrefab,friendsOfFriendsChallengeContent.transform);
                friendsData.transform.GetChild(2).GetComponent<Text>().text = friends[i].player_name;
                friendsData.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(delegate { OnFriendsOfFriendsChallengeButtonClicked(friendsId.ToString()); });
                friendsData.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(delegate { OnFriendsOfFriendsAddFriendButtonClicked(friendsId.ToString()); });
            }

           
        }
        public void OnFriendsOfFriendsChallengeButtonClicked(string friendsId)
        {
            uiManager.apiManager.APIInviteFriend(friendsId);
        }
        public void OnFriendsOfFriendsChallengeButtonCallBack()
        {
            
        }
        public void OnFriendsOfFriendsAddFriendButtonClicked(string friendsId)
        {
            uiManager.apiManager.APIAddFriend(friendsId);
        }
        public void OnFriendsOfFriendsAddFriendButtonCallBack()
        {

        }
        public void OnFriendsActivityButtonCallBack(List<Friend> friends)
        {
            for (int i = 0; i < friendsActivityContent.transform.childCount; i++)
            {
                Destroy(friendsActivityContent.transform.GetChild(i));
            }

            for (int i = 0; i < friends.Count; i++)
            {
                int friendsId = friends[i].player_id;
                GameObject friendsData = Instantiate(friendsActivityPrefab, friendsActivityContent.transform);
                friendsData.transform.GetChild(1).GetComponent<Text>().text = friends[i].player_name + "Challenged you!";
                friendsData.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate { OnFriendsActivityChallengeButtonClicked(friendsId.ToString()); });
            }

        }
        public void OnFriendsActivityChallengeButtonClicked(string friendsId)
        {
            uiManager.apiManager.APIInviteFriend(friendsId);
        }
        public void OnFriendsActivityChallengeButtonCallBack()
        {
            
        }
        public void OnFriendsActivityButton()
        {
            playWithFriendsMainScreen.SetActive(true);
            playWithFriendsScreen.SetActive(false);
            playWithFriendsOfFriendsScreen.SetActive(false);
            friendsActivityScreen.SetActive(true);
        }
        
        public void OnBackButtonClicked()
        {
            uiManager.homeScreenParent.SetActive(true);
            uiManager.playWithFriendsScreenParent.SetActive(false);
        }
        public void AddByUniqueIdButtonClickrd()
        {
            findFriendsScreen.SetActive(true);
        }
        public void SearechFriendsPopupCloseButton()
        {
            findFriendsScreen.SetActive(false);
        }
        
        public void FriendsChallengeButton(int friendsNumber)
        {
            friendsChallengeList[friendsNumber].transform.GetChild(11).gameObject.SetActive(false);
            friendsChallengeList[friendsNumber].transform.GetChild(12).gameObject.SetActive(true);
        }
        public void FriendsOfFriendsChallengeButton(int friendaNumber)
        {
            friendsOfFriendsChallengeList[friendaNumber].transform.GetChild(8).gameObject.SetActive(true);
            friendsOfFriendsChallengeList[friendaNumber].transform.GetChild(7).gameObject.SetActive(false);
        }
        public void FriendsOfFriendsAddButton(int friendsNumber)
        {
            friendsOfFriendsChallengeList[friendsNumber].transform.GetChild(9).gameObject.SetActive(true);
            friendsOfFriendsChallengeList[friendsNumber].transform.GetChild(6).gameObject.SetActive(false);
        }
        public void FriendsActivityChallengeButton(int friendsNumber)
        {
            friendsActivityList[friendsNumber].transform.GetChild(5).gameObject.SetActive(false);
            friendsActivityList[friendsNumber].transform.GetChild(6).gameObject.SetActive(true);
        }
        public void AddFriendsButtonClicked(int friendsNumber)
        {
            addFriendsPopup.SetActive(true);
        }
        public void AddFriendPopupCloseButton()
        {
            addFriendsPopup.SetActive(false);
        }


        public void FriendsSearchField(string name)
        {
            for(int i=0;i<friendsChallengeList.Count;i++)
            {
                if (friendsChallengeList[i].transform.GetChild(4).GetComponent<Text>().text.ToLower().Contains(name.ToLower()))
                {
                    friendsChallengeList[i].transform.gameObject.SetActive(true);
                    friendsChallengeList[i].transform.SetParent(friendsChallengeContent);
                }
                else if (name.Length == 0)
                {
                    friendsChallengeList[i].transform.gameObject.SetActive(true);
                    friendsChallengeList[i].transform.SetParent(friendsChallengeContent);
                }
                else
                {
                    friendsChallengeList[i].transform.gameObject.SetActive(false);
                    friendsChallengeList[i].transform.SetParent(friendsChallengeContentEmpty);
                }
            }
        }
        public void FriendsOfFriendsSearchField(string name)
        {
            for (int i = 0; i < friendsOfFriendsChallengeList.Count; i++)
            {
                if (friendsOfFriendsChallengeList[i].transform.GetChild(2).GetComponent<Text>().text.ToLower().StartsWith(name.ToLower()))
                {
                    friendsOfFriendsChallengeList[i].transform.gameObject.SetActive(true);
                    friendsOfFriendsChallengeList[i].transform.SetParent(friendsOfFriendsChallengeContent);
                }
                else if (name.Length == 0)
                {
                    friendsOfFriendsChallengeList[i].transform.gameObject.SetActive(true);
                    friendsOfFriendsChallengeList[i].transform.SetParent(friendsOfFriendsChallengeContent);
                }
                else 
                {
                    friendsOfFriendsChallengeList[i].transform.gameObject.SetActive(false);
                    friendsOfFriendsChallengeList[i].transform.SetParent(friendsChallengeContentEmpty);
                }
            }
        }
        public void FindFriendsSearchField(string name)
        {
            print(name); 
            for(int i=0;i<findFriendsList.Count;i++)
            {
                
                if (name.Length == 0)
                {
                    findFriendsScrollView.SetActive(false);
                    findFriendsMeassageText.SetActive(true);

                }
               else if (findFriendsList[i].transform.GetChild(2).GetComponent<Text>().text.ToLower().StartsWith(name.ToLower()))
                {
                    findFriendsScrollView.SetActive(true);
                    findFriendsMeassageText.SetActive(false);
                    findFriendsList[i].transform.gameObject.SetActive(true);
                    findFriendsList[i].transform.SetParent(findFriendsContent);
                }
                else
                {

                    findFriendsList[i].transform.gameObject.SetActive(false);
                    findFriendsList[i].transform.SetParent(friendsChallengeContentEmpty);
                }
            }
        }

    }
}
