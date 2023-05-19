using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;
namespace RevolutionGames.UI
{
    public class LeaderBoardScreen : MonoBehaviour
    {
        public UIManager uiManager;
        public GameObject myLeagueScreen;
        public GameObject friendsScreen;
        public GameObject countryScreen;
        public GameObject worldScreen;
        public GameObject leaderBoardLeague;
        public GameObject lastWeekResultScreen;
        public GameObject leagueInfoHeaderScreen;
        public GameObject leagueInfo1;
        public GameObject leagueInfo2;
        public GameObject leagueInfo3;
        public GameObject leagueInfo4;
        public GameObject friendsInfoScreen;
        public GameObject countryInfoScreen;
        public GameObject worldInfoScreen;
        public GameObject myLeagueContent;
        public GameObject myLeaguePrefab;
        public GameObject friendsContent;
        public GameObject friendsPrefab;
        public GameObject countryContent;
        public GameObject countryPrefab;
        public GameObject worldContent;
        public GameObject worldPrefab;
        public GameObject challengeButton;
        public GameObject sendImage;
        public Image profileImageMyLeague, profileImageFriends, profileImageCountry, profileImageWorld;

        public void Start()
        {
        }

        public void OnEnable()
        {
            myLeagueScreen.SetActive(true);
            friendsScreen.SetActive(false);
            countryScreen.SetActive(false);
            worldScreen.SetActive(false);
        }
        public void OnLeaderBoardMyLeagueButtonClicked()
        {
            //uiManager.apiManager.APIGetLeaderBoardWorldData();

            myLeagueScreen.SetActive(true);
            friendsScreen.SetActive(false);
            countryScreen.SetActive(false);
            worldScreen.SetActive(false);
        }
        public void OnLeaderBoardMyLeagueButtonCallBack(List<Leaderboard> myLeagues)
        {
            /*for (int i = 0; i <myLeagueContent.transform.childCount; i++)
            {
                Destroy(myLeagueContent.transform.GetChild(i));
            }
            for (int i = 0; i < myLeagues.Count; i++)
            {
                GameObject myLeague = Instantiate(myLeaguePrefab, myLeagueContent.transform);
                myLeague.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = myLeagues[i].player_level.ToString();
                myLeague.transform.GetChild(0).GetChild(7).GetComponent<Text>().text = myLeagues[i].player_name;
                myLeague.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = myLeagues[i].player_balance.ToString();
            }*/
            myLeagueScreen.SetActive(true);
            friendsScreen.SetActive(false);
            countryScreen.SetActive(false);
            worldScreen.SetActive(false);
        }
        public void OnLeaderBoardFriendsButtonClicked()
        {

            /*for (int i = 0; i < friendsContent.transform.childCount; i++)
            {
                Destroy(friendsContent.transform.GetChild(i));
            }*/

            myLeagueScreen.SetActive(false);
            friendsScreen.SetActive(true);
            countryScreen.SetActive(false);
            worldScreen.SetActive(false);
            //uiManager.apiManager.APIGetLeaderBoardCountryData();
        }
        public void OnLeaderBoardFriendsButtonCallBack(List<Leaderboard> friends)
        {

            for (int i = 0; i < friends.Count; i++)
            {
                GameObject friend = Instantiate(friendsPrefab, friendsContent.transform);
                friend.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = friends[i].player_level.ToString();
                friend.transform.GetChild(0).GetChild(6).GetComponent<Text>().text = friends[i].player_name;
                friend.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = friends[i].player_balance.ToString();
            }
            myLeagueScreen.SetActive(false);
            friendsScreen.SetActive(true);
            countryScreen.SetActive(false);
            worldScreen.SetActive(false);
        }
        public void OnLeaderBoardCountryButtonClicked()
        {
            /*Ifor (int i = 0; i < countryContent.transform.childCount; i++)
            {
                Destroy(countryContent.transform.GetChild(i));
            }*/
            //uiManager.apiManager.APIGetLeaderBoardCountryData();

            myLeagueScreen.SetActive(false);
            friendsScreen.SetActive(false);
            countryScreen.SetActive(true);
            worldScreen.SetActive(false);
        }
        public void OnLeaderBoardCountryButtonCallBack(List<Leaderboard> leaderboards)
        {
            for (int i = 0; i < leaderboards.Count; i++)
            {
                GameObject country = Instantiate(countryPrefab, countryContent.transform);
                country.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = leaderboards[i].player_level.ToString();
                country.transform.GetChild(0).GetChild(7).GetComponent<Text>().text = leaderboards[i].player_name;
                country.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = leaderboards[i].player_balance.ToString();

            }
            myLeagueScreen.SetActive(false);
            friendsScreen.SetActive(false);
            countryScreen.SetActive(true);
            worldScreen.SetActive(false);

        }
        public void OnLeaderBoardWorldButtonClicked()
        {
            /*for (int i = 0; i < worldContent.transform.childCount; i++)
            {
                Destroy(worldContent.transform.GetChild(i));
            }*/
            //uiManager.apiManager.APIGetLeaderBoardWorldData();

            myLeagueScreen.SetActive(false);
            friendsScreen.SetActive(false);
            countryScreen.SetActive(false);
            worldScreen.SetActive(true);
        }
        public void OnLeaderBoardWorldButtonCallBack(List<Leaderboard> worldBoards)
        {
            for (int i = 0; i < worldBoards.Count; i++)
            {
                GameObject world = Instantiate(worldPrefab, worldContent.transform);
                world.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = worldBoards[i].player_level.ToString();
                world.transform.GetChild(0).GetChild(7).GetComponent<Text>().text = worldBoards[i].player_name;
                world.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = worldBoards[i].player_balance.ToString();
            }
            myLeagueScreen.SetActive(false);
            friendsScreen.SetActive(false);
            countryScreen.SetActive(false);
            worldScreen.SetActive(true);
        }
        public void OnBackButtonClicked()
        {
            uiManager.leaderBoardScreenParent.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }
        
       
        
        public void LastWeekResultButton()
        {
            lastWeekResultScreen.SetActive(true);
            /*myLeagueData.SetActive(false);
            friendsScreen.SetActive(false);
            countryScreen.SetActive(false);
            worldScreen.SetActive(false);*/
        }

        public void CloseLastWeekButton()
        {
            lastWeekResultScreen.SetActive(false);
        }
        public void MyLeagueInfoButton()
        {
            leagueInfoHeaderScreen.SetActive(true);
            leagueInfo1.SetActive(true);
        }
        public void FriendsInfoButton()
        {
            friendsInfoScreen.SetActive(true);
        }
        public void CountryInfoButton()
        {
            countryInfoScreen.SetActive(true);
        }
        public void WorldInfoButton()
        {
            worldInfoScreen.SetActive(true);
        }
        public void CloseInfoButton()
        {
            leagueInfoHeaderScreen.SetActive(false);
            leagueInfo1.SetActive(false);
            leagueInfo2.SetActive(false);
            leagueInfo3.SetActive(false);
            leagueInfo4.SetActive(false);
            friendsInfoScreen.SetActive(false);
            countryInfoScreen.SetActive(false);
            worldInfoScreen.SetActive(false);
        }
        public void OverViewButton()
        {
            leagueInfoHeaderScreen.SetActive(true);
            leagueInfo1.SetActive(true);
            leagueInfo2.SetActive(false);
            leagueInfo3.SetActive(false);
            leagueInfo4.SetActive(false);
        }
        public void InfoButton()
        {
            leagueInfoHeaderScreen.SetActive(true);
            leagueInfo1.SetActive(false);
            leagueInfo2.SetActive(true);
            leagueInfo3.SetActive(false);
            leagueInfo4.SetActive(false);
        }
        public void RulesButton()
        {
            leagueInfoHeaderScreen.SetActive(true);
            leagueInfo1.SetActive(false);
            leagueInfo2.SetActive(false);
            leagueInfo3.SetActive(true);
            leagueInfo4.SetActive(false);
        }
        public void ResultButton()
        {
            leagueInfoHeaderScreen.SetActive(true);
            leagueInfo1.SetActive(false);
            leagueInfo2.SetActive(false);
            leagueInfo3.SetActive(false);
            leagueInfo4.SetActive(true);
        }
        public void FriendsChallengeButtonclicked()
        {
            challengeButton.SetActive(false);
            sendImage.SetActive(true);
        }
        public void ProfilePictureUpdate(Sprite avatar)
        {
            profileImageMyLeague.sprite = avatar;
            profileImageFriends.sprite = avatar;
            profileImageCountry.sprite = avatar;
            profileImageWorld.sprite = avatar;
        }

    }
}
