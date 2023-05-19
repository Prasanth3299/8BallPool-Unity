using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RevolutionGames.APIData;
using RevolutionGames.Data;
using RevolutionGames.UI;
namespace RevolutionGames.API
{
    class APIResponseCode
    {
        public static int SUCCESS = 200;
        public static int INPUT_ERROR = 300;
        public static int DB_ERROR = 400;
        public static int SERVER_ERROR = 500;
        public static int FAILURE = 404;
    }

    public class APIManager : MonoBehaviour
    {
        public UIManager uiManager;

        #region API URLs
        string apiBaseUrl = "http://34.193.55.212:3002/revgames_apiserver/api/v1/";
        string loginEmailPlayerUrl = "player/login/login_email_player";
        string validateEmailPlayerUrl = "player/login/validate_email_player";
        string registerEmailPlayerUrl = "player/login/register_email_player";
        string loginGuestPlayerUrl = "player/login/login_guest_player";
        string forgotPasswordEmailPlayerUrl = "player/login/forgot_password";
        string freeSpinDataUrl = "player/spin/get_free_spin_data";
        string paidSpinDataUrl = "player/spin/get_paid_spin_data";
        string startFreeSpinUrl = "player/spin/start_free_spin";
        string startPaidSpinUrl = "player/spin/start_paid_spin";
        string getShopCoinsDataUrl = "player/shops/get_shop_coins_data";
        string purchaseCoinsUrl = "player/shops/purchase_coins";
        string getShopAvatarsDataUrl = "player/shops/get_shop_avatars_data";
        string purchaseAvatarUrl = "player/shops/purchase_avatar";
        string setCurrentAvatarUrl = "player/shops/set_current_avatar";
        string getShopBoardsDataUrl = "player/shops/get_shop_boards_data";
        string purchaseBoardUrl = "player/shops/purchase_board";
        string setCurrentBoardUrl = "player/shops/set_current_board";
        string getShopTokensDataUrl = "player/shops/get_shop_tokens_data";
        string purchaseTokenUrl = "player/shops/purchase_token";
        string setCurrentTokenUrl = "player/shops/set_current_token";
        string getShopDiceDataUrl = "player/shops/get_shop_dice_data";
        string purchaseDiceUrl = "player/shops/purchase_dice";
        string setCurrentDiceUrl = "player/shops/set_current_dice";
        string getOnlineGamesListUrl = "player/gamelists/get_online_games_list";
        string getLocalGamesListUrl = "player/gamelists/get_local_games_list";
        string getComputerGamesListUrl = "player/gamelists/get_computer_games_list";
        string getTournamentGamesListUrl = "player/gamelists/get_tournaments_list";
        string getSpecialGamesListUrl = "player/gamelists/get_special_games_list";
        string getFriendsListUrl = "player/friends/get_friends_list";
        string addFriendUrl = "player/friends/add_friend";
        string inviteFriendUrl = "player/friends/invite_friend";
        string getFbFriendsListUrl = "player/friends/get_facebook_friendlist";
        string getAllAvatarsUrl = "items/avatars/get_all_avatars";
        string getFreeRewardsDataUrl = "player/freereward/get_free_rewards_data";
        string collectFreeCoinsUrl = "player/freereward/collect_free_coins";
        string getVideoAdsListingUrl = "player/freereward/get_video_ads_listing";
        string collectVideoAdsCoinsUrl = "player/freereward/collect_video_ads_coins";
        string collectShareRewardCoinsUrl = "player/freereward/collect_share_reward_coins";
        string getLeaderBoardCountryDataUrl = "player/leaderboard/get_leaderboard_country_data";
        string getLeaderBoardWorldDataUrl = "player/leaderboard/get_leaderboard_world_data";
        #endregion

        #region Login APIs
        //Check with email if the player already exists
        public void APIValidateEmailPlayer()
        {
            try
            {
                string url = apiBaseUrl + validateEmailPlayerUrl;
                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIValidateEmailPlayer);
                request.IsKeepAlive = false;
                request.AddField("player_email", GameData.Instance().PlayerEmailId);
                request.AddField("player_name", GameData.Instance().PlayerName);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);


            }
        }
        void ResponseForAPIValidateEmailPlayer(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        Debug.Log(jsonObject);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            uiManager.loginScreenParent.transform.GetComponent<LoginScreen>().LoginEmailContinueButtonCallBack(
                                response_body["is_user_exists"].Value<bool>());
                        }
                        else
                        {
                            if (response.DataAsText.Length > 0)
                            {
                                //show printed error
                                jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                                var response_body = jsonObject["response_body"].Value<JArray>();

                                Debug.Log(response_body[0]["error"].Value<string>());
                            }
                            else
                            {
                                //show the error message "Server is not responding now. Please try again" 
                            }
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }

        //If the player already exists, login with password
        public void APILoginEmailPlayer()
        {
            try
            {
                string url = apiBaseUrl + loginEmailPlayerUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPILoginEmailPlayer);
                request.IsKeepAlive = false;
                request.AddField("player_email", GameData.Instance().PlayerEmailId);
                request.AddField("player_password", GameData.Instance().PlayerPassword);
                request.AddField("divice_name", GameData.Instance().DeviceName);
                request.AddField("device_token", GameData.Instance().DeviceToken);
                request.AddField("software_info", GameData.Instance().SoftwareInfo);
                request.AddField("hardware_info", GameData.Instance().HardwareInfo);
                request.Send();


            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);


            }
        }
        void ResponseForAPILoginEmailPlayer(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        string errorMessage = "";
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            try
                            {
                                var player_data = response_body["player_data"].Value<JObject>();
                                Debug.Log(player_data);
                                //uiManager.loginScreen.transform.GetComponent<LoginScreen>().EmailPasswordContinueButtonCallBack(errorMessage);
                                GameData.Instance().UpdatePlayerDetails(player_data["player_id"].Value<string>(), player_data["player_uuid"].Value<string>(),
                                    player_data["player_name"].Value<string>(), player_data["player_email"].Value<string>(), player_data["social_id"].Value<string>(),
                                    player_data["player_login_mode"].Value<string>(), player_data["player_balance"].Value<int>(), player_data["player_level"].Value<int>(), player_data["expertise_id"].Value<int>(),
                                    player_data["player_id"].Value<int>(), player_data["total_games_won"].Value<int>(), player_data["total_tournaments_played"].Value<int>(),
                                    player_data["total_tournaments_won"].Value<int>(), player_data["total_specialevents_played"].Value<int>(), player_data["total_specialevents_won"].Value<int>(),
                                    player_data["avatar_id"].Value<int>(), player_data["country_id"].Value<int>(), player_data["dice_id"].Value<int>(),
                                    player_data["board_id"].Value<int>(), player_data["token_id"].Value<int>(), player_data["created_date"].Value<string>(),
                                    player_data["avatar_url"].Value<string>(), player_data["country_flag_url"].Value<string>(), player_data["country_name"].Value<string>(),
                                    player_data["security_key"].Value<string>());
                               
                            }
                            catch (Exception e)
                            {
                                errorMessage = jsonObject["response_message"].Value<string>();
                            }
                            uiManager.loginScreenParent.transform.GetComponent<LoginScreen>().EmailOldUserPasswordContinueButtonCallBack(errorMessage);
                        }
                        else
                        {
                            errorMessage = "Server is not responding now. Please try again";
                        }
                        
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }

        //If the player does not exist, register the player in the server
        public void APIRegisterEmailPlayer()
        {
            try
            {
                string url = apiBaseUrl + registerEmailPlayerUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIRegisterEmailPlayer);
                request.IsKeepAlive = false;
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.AddField("player_email", GameData.Instance().PlayerEmailId);
                request.AddField("player_name", GameData.Instance().PlayerName);
                request.AddField("player_password", GameData.Instance().PlayerPassword);
                request.AddField("avatar_id", GameData.Instance().PlayerAvatarId.ToString());
                request.AddField("country_id", GameData.Instance().PlayerCountryId.ToString());
                request.AddField("divice_name", GameData.Instance().DeviceName);
                request.AddField("device_token", GameData.Instance().DeviceToken);
                request.AddField("software_info", GameData.Instance().SoftwareInfo);
                request.AddField("hardware_info", GameData.Instance().HardwareInfo);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIRegisterEmailPlayer(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            var player_data = response_body["player_data"].Value<JObject>();
                            Debug.Log(player_data);
                            GameData.Instance().UpdatePlayerDetails(player_data["player_id"].Value<string>(), player_data["player_uuid"].Value<string>(),
                                player_data["player_name"].Value<string>(), player_data["player_email"].Value<string>(), player_data["social_id"].Value<string>(),
                                player_data["player_login_mode"].Value<string>(), player_data["player_balance"].Value<int>(), player_data["player_level"].Value<int>(), player_data["expertise_id"].Value<int>(),
                                player_data["player_id"].Value<int>(), player_data["total_games_won"].Value<int>(), player_data["total_tournaments_played"].Value<int>(),
                                player_data["total_tournaments_won"].Value<int>(), player_data["total_specialevents_played"].Value<int>(), player_data["total_specialevents_won"].Value<int>(),
                                player_data["avatar_id"].Value<int>(), player_data["country_id"].Value<int>(), player_data["dice_id"].Value<int>(),
                                player_data["board_id"].Value<int>(), player_data["token_id"].Value<int>(), player_data["created_date"].Value<string>(),
                                player_data["avatar_url"].Value<string>(), player_data["country_flag_url"].Value<string>(), player_data["country_name"].Value<string>(),
                                player_data["security_key"].Value<string>());
                            uiManager.loginScreenParent.transform.GetComponent<LoginScreen>().EmailNewUserPasswordContinueButtonCallBack();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }

        //Register the guest user with name
        public void APILoginGuestPlayer()
        {
            try
            {
                string url = apiBaseUrl + loginGuestPlayerUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPILoginGuestPlayer);
                request.IsKeepAlive = false;
                /*request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.AddField("player_name", GameData.Instance().PlayerName);
                request.AddField("player_password", GameData.Instance().PlayerPassword);
                request.AddField("player_divice_id", GameData.Instance().GameUuid);
                request.AddField("avatar_id", GameData.Instance().PlayerAvatarId.ToString());
                request.AddField("country_id", GameData.Instance().PlayerCountryId.ToString());
                request.AddField("divice_name", GameData.Instance().DeviceName);
                request.AddField("device_token", GameData.Instance().DeviceToken);
                request.AddField("software_info", GameData.Instance().SoftwareInfo);
                request.AddField("hardware_info", GameData.Instance().HardwareInfo);*/
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.AddField("player_name", "test");
                request.AddField("player_password", "12345");
                request.AddField("player_divice_id", "test");
                request.AddField("avatar_id", GameData.Instance().PlayerAvatarId.ToString());
                request.AddField("country_id", GameData.Instance().PlayerCountryId.ToString());
                request.AddField("divice_name", "iPhone");
                request.AddField("device_token", "qwq");
                request.AddField("software_info", "qwqw");
                request.AddField("hardware_info", "qweewq");
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);


            }
        }
        void ResponseForAPILoginGuestPlayer(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            var player_data = response_body["player_data"].Value<JObject>();
                            Debug.Log(player_data);
                            GameData.Instance().UpdatePlayerDetails(player_data["player_id"].Value<string>(), player_data["player_uuid"].Value<string>(),
                                player_data["player_name"].Value<string>(), player_data["player_email"].Value<string>(), player_data["social_id"].Value<string>(),
                                player_data["player_login_mode"].Value<string>(), player_data["player_balance"].Value<long>(), player_data["player_level"].Value<int>(), player_data["expertise_id"].Value<int>(),
                                player_data["player_id"].Value<int>(), player_data["total_games_won"].Value<int>(), player_data["total_tournaments_played"].Value<int>(),
                                player_data["total_tournaments_won"].Value<int>(), player_data["total_specialevents_played"].Value<int>(), player_data["total_specialevents_won"].Value<int>(),
                                player_data["avatar_id"].Value<int>(), player_data["country_id"].Value<int>(), player_data["dice_id"].Value<int>(),
                                player_data["board_id"].Value<int>(), player_data["token_id"].Value<int>(), player_data["created_date"].Value<string>(),
                                player_data["avatar_url"].Value<string>(), player_data["country_flag_url"].Value<string>(), player_data["country_name"].Value<string>(),
                                player_data["security_key"].Value<string>());
                            uiManager.loginScreenParent.transform.GetComponent<LoginScreen>().guestLoginContinueButtonCallBack();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }

        //If the player already exists, but has forgotten the password
        public void APIForgotPasswordEmailPlayer()
        {
            try
            {
                string url = apiBaseUrl + forgotPasswordEmailPlayerUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIForgotPasswordEmailPlayer);
                request.IsKeepAlive = false;
                request.AddField("player_email", GameData.Instance().PlayerEmailId);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);


            }
        }
        void ResponseForAPIForgotPasswordEmailPlayer(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        uiManager.loginScreenParent.transform.GetComponent<LoginScreen>().EmailForgotPasswordButtonCallBack();
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }

        //Get all the avatars for selection
        public void APIGetAllAvatars()
        {
            try
            {
                string url = apiBaseUrl + getAllAvatarsUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetAllAvatars);
                request.IsKeepAlive = false;
                request.AddField("game_uuid", "guuid-2e55910a-7159-4ed7-95bb-c3976187991c-2182020155410615");
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetAllAvatars(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        print(response.DataAsText);
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        #endregion

        #region Spinwheel APIs

        //Free spin wheel data
        public void APIFreeSpinData()
        {
            try
            {
                string url = apiBaseUrl + freeSpinDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIFreeSpinData);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().PlayerUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIFreeSpinData(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            print(response_body["spin_short_id"].Value<String>());
                            List<SpinData> freeSpin = response_body["free spin data"].ToObject<List<SpinData>>();
                            uiManager.spinWheelScreenParent.transform.GetComponent<SpinWheelScreen>().OnSpinButtonCallBack(freeSpin);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }

        //Paid spin wheel data
        public void APIPaidSpinData()
        {
            try
            {
                string url = apiBaseUrl + paidSpinDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIPaidSpinData);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIPaidSpinData(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            print(response_body["spin_short_id"].Value<String>());
                            List<SpinData> paidSpin = response_body["free spin data"].ToObject<List<SpinData>>();
                            uiManager.shopScreenParent.transform.GetComponent<SpinWheelScreen>().OnPaidSpinButtonCallBack(paidSpin);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }

        //Start free spin wheel API call
        public void APIStartFreeSpin(string spinId)
        {
            try
            {
                string url = apiBaseUrl + startFreeSpinUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIStartFreeSpin);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("spin_short_id", spinId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIStartFreeSpin(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        print(response.DataAsText);
                        //uiManager.spinWheelScreen.transform.GetComponent<SpinWheelScreen>().OnStartFreeSpinButtonCallBack();
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }

        //Start paid spin wheel api call
        public void APIStartPaidSpin(string spinId)
        {
            try
            {
                string url = apiBaseUrl + startPaidSpinUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIStartPaidSpin);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("spin_short_id", spinId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIStartPaidSpin(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        print(response.DataAsText);
                        uiManager.spinWheelScreenParent.transform.GetComponent<SpinWheelScreen>().OnStartPaidSpinWheelButtonCallBack();
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }
        #endregion

        #region Shop APIs

        //Api for shops
        public void APIGetShopCoins()
        {
            try
            {
                string url = apiBaseUrl + getShopCoinsDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetShopCoins);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetShopCoins(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<ShopCoin> shopcoin = response_body["coin_for_shop"].ToObject<List<ShopCoin>>();
                            //uiManager..shopScreen.transform.GetComponent<ShopScreen>().OnMainShopCoinsButtonCallBack(shopcoin);
                            uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().OnMainShopCoinsButtonCallBack(shopcoin);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }
        public void APIPurchaseCoins(string coinsId)
        {
            try
            {
                string url = apiBaseUrl + purchaseCoinsUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIPurchaseCoins);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.AddField("coins_id", coinsId);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIPurchaseCoins(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            var purchase_coin_details = response_body["purchase_coin_details"].Value<JObject>();
                            Debug.Log(purchase_coin_details);
                            GameData.Instance().PlayerBalance = purchase_coin_details["player_balance"].Value<int>();
                            uiManager.UpdatePlayerDetails();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }

        //Api for Avatars
        public void APIGetShopAvatars()
        {
            try
            {
                string url = apiBaseUrl + getShopAvatarsDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetShopAvatars);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetShopAvatars(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<ShopAvatar> shopavatar = response_body["avatar_details"].ToObject<List<ShopAvatar>>();
                            uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().OnMainSocialButtonCallBack(shopavatar);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }
        public void APIPurchaseAvatar(string avatarId)
        {
            try
            {
                string url = apiBaseUrl + purchaseAvatarUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIPurchaseAvatar);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("avatar_id", avatarId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIPurchaseAvatar(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<int>();
                            uiManager.UpdatePlayerDetails();
                           // uiManager.shopScreen.transform.GetComponent<ShopScreen>().OnAvatarBuyButtonCallBack();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APISetCurrentAvatar(string avatarId)
        {
            try
            {
                string url = apiBaseUrl + setCurrentAvatarUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPISetCurrentAvatar);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("avatar_id", avatarId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPISetCurrentAvatar(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<int>();
                            GameData.Instance().PlayerLevel = response_body["player_level"].Value<int>();
                            GameData.Instance().PlayerExpertiseId = response_body["expertise_id"].Value<int>();
                            GameData.Instance().TotalGamesPlayed = response_body["total_games_played"].Value<int>();
                            GameData.Instance().TotalGamesWon = response_body["total_games_won"].Value<int>();
                            GameData.Instance().TotalTournamentsPlayed = response_body["total_tournaments_played"].Value<int>();
                            GameData.Instance().TotalTournamentsWon = response_body["total_tournaments_won"].Value<int>();
                            GameData.Instance().TotalSpecialEventsPlayed = response_body["total_specialevents_played"].Value<int>();
                            GameData.Instance().TotalSpecialEventsWon = response_body["total_specialevents_won"].Value<int>();
                            GameData.Instance().PlayerAvatarId = response_body["avatar_id"].Value<int>();
                            GameData.Instance().PlayerCountryId = response_body["country_id"].Value<int>();
                            GameData.Instance().PlayerDiceId = response_body["dice_id"].Value<int>();
                            GameData.Instance().PlayerBoardId = response_body["board_id"].Value<int>();
                            GameData.Instance().PlayerTokenId = response_body["token_id"].Value<int>();
                            uiManager.UpdatePlayerDetails();
                            //uiManager.shopScreen.transform.GetComponent<ShopScreen>().OnAvatarUseButtonCallBack();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }

        //Api for boards
        public void APIGetShopBoards()
        {
            print("Calling APIgetBoards..");
            try
            {
                string url = apiBaseUrl + getShopBoardsDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetShopBoards);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetShopBoards(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<ShopBoard> shopboard = response_body["board_details"].ToObject<List<ShopBoard>>();
                            //uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().OnStickersButtonCallBack(shopboard);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }
        public void APIPurchaseBoard(string boardId)
        {
            try
            {
                string url = apiBaseUrl + purchaseBoardUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIPurchaseBoard);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("board_id", boardId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIPurchaseBoard(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<int>();
                            uiManager.UpdatePlayerDetails();
                            //uiManager.shopScreen.transform.GetComponent<ShopScreen>().OnStickersBuyButtonCallBack();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APISetCurrentBoard(string boardId)
        {
            try
            {
                string url = apiBaseUrl + setCurrentBoardUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPISetCurrentBoard);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("board_id", boardId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPISetCurrentBoard(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<int>();
                            GameData.Instance().PlayerLevel = response_body["player_level"].Value<int>();
                            GameData.Instance().PlayerExpertiseId = response_body["expertise_id"].Value<int>();
                            GameData.Instance().TotalGamesPlayed = response_body["total_games_played"].Value<int>();
                            GameData.Instance().TotalGamesWon = response_body["total_games_won"].Value<int>();
                            GameData.Instance().TotalTournamentsPlayed = response_body["total_tournaments_played"].Value<int>();
                            GameData.Instance().TotalTournamentsWon = response_body["total_tournaments_won"].Value<int>();
                            GameData.Instance().TotalSpecialEventsPlayed = response_body["total_specialevents_played"].Value<int>();
                            GameData.Instance().TotalSpecialEventsWon = response_body["total_specialevents_won"].Value<int>();
                            GameData.Instance().PlayerAvatarId = response_body["avatar_id"].Value<int>();
                            GameData.Instance().PlayerCountryId = response_body["country_id"].Value<int>();
                            GameData.Instance().PlayerDiceId = response_body["dice_id"].Value<int>();
                            GameData.Instance().PlayerBoardId = response_body["board_id"].Value<int>();
                            GameData.Instance().PlayerTokenId = response_body["token_id"].Value<int>();
                            uiManager.UpdatePlayerDetails();
                            //uiManager.shopScreen.transform.GetComponent<ShopScreen>().OnStickersUseButtonCallBack();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }

        //API for tokens
        public void APIGetShopTokens()
        {
            try
            {
                string url = apiBaseUrl + getShopTokensDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetShopTokens);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetShopTokens(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<ShopToken> shoptoken = response_body["token_details"].ToObject<List<ShopToken>>();
                            uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().OnCashButtonCallBack(shoptoken);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }
        public void APIPurchaseToken(string tokenId)
        {
            try
            {
                string url = apiBaseUrl + purchaseTokenUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIPurchaseToken);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("token_id", tokenId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIPurchaseToken(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<int>();
                            uiManager.UpdatePlayerDetails();
                            //uiManager.shopScreen.transform.GetComponent<ShopScreen>().OncashBuyButtonCallBack();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APISetCurrentToken(string tokenId)
        {
            try
            {
                string url = apiBaseUrl + setCurrentTokenUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPISetCurrentToken);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("token_id", tokenId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPISetCurrentToken(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<int>();
                            GameData.Instance().PlayerLevel = response_body["player_level"].Value<int>();
                            GameData.Instance().PlayerExpertiseId = response_body["expertise_id"].Value<int>();
                            GameData.Instance().TotalGamesPlayed = response_body["total_games_played"].Value<int>();
                            GameData.Instance().TotalGamesWon = response_body["total_games_won"].Value<int>();
                            GameData.Instance().TotalTournamentsPlayed = response_body["total_tournaments_played"].Value<int>();
                            GameData.Instance().TotalTournamentsWon = response_body["total_tournaments_won"].Value<int>();
                            GameData.Instance().TotalSpecialEventsPlayed = response_body["total_specialevents_played"].Value<int>();
                            GameData.Instance().TotalSpecialEventsWon = response_body["total_specialevents_won"].Value<int>();
                            GameData.Instance().PlayerAvatarId = response_body["avatar_id"].Value<int>();
                            GameData.Instance().PlayerCountryId = response_body["country_id"].Value<int>();
                            GameData.Instance().PlayerDiceId = response_body["dice_id"].Value<int>();
                            GameData.Instance().PlayerBoardId = response_body["board_id"].Value<int>();
                            GameData.Instance().PlayerTokenId = response_body["token_id"].Value<int>();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }

        //API for dice
        public void APIGetShopDices()
        {
            try
            {
                string url = apiBaseUrl + getShopDiceDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetShopDices);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetShopDices(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<ShopDice> shopdice = response_body["dice_details"].ToObject<List<ShopDice>>();
                            uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().OncuesButtonCallBack(shopdice);
                            uiManager.shopScreenParent.transform.GetComponent<ShopScreen>().OnChatPacksButtonCallBack(shopdice);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }
        public void APIPurchaseDice(string diceId)
        {
            try
            {
                string url = apiBaseUrl + purchaseDiceUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIPurchaseDice);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("dice_id", diceId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIPurchaseDice(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<int>();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APISetCurrentDice(string diceId)
        {
            try
            {
                string url = apiBaseUrl + setCurrentDiceUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPISetCurrentDice);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("dice_id", diceId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPISetCurrentDice(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<int>();
                            GameData.Instance().PlayerLevel = response_body["player_level"].Value<int>();
                            GameData.Instance().PlayerExpertiseId = response_body["expertise_id"].Value<int>();
                            GameData.Instance().TotalGamesPlayed = response_body["total_games_played"].Value<int>();
                            GameData.Instance().TotalGamesWon = response_body["total_games_won"].Value<int>();
                            GameData.Instance().TotalTournamentsPlayed = response_body["total_tournaments_played"].Value<int>();
                            GameData.Instance().TotalTournamentsWon = response_body["total_tournaments_won"].Value<int>();
                            GameData.Instance().TotalSpecialEventsPlayed = response_body["total_specialevents_played"].Value<int>();
                            GameData.Instance().TotalSpecialEventsWon = response_body["total_specialevents_won"].Value<int>();
                            GameData.Instance().PlayerAvatarId = response_body["avatar_id"].Value<int>();
                            GameData.Instance().PlayerCountryId = response_body["country_id"].Value<int>();
                            GameData.Instance().PlayerDiceId = response_body["dice_id"].Value<int>();
                            GameData.Instance().PlayerBoardId = response_body["board_id"].Value<int>();
                            GameData.Instance().PlayerTokenId = response_body["token_id"].Value<int>();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        #endregion

        #region Game Mode APIs
        public void APIGetOnlineGamesList()
        {
            try
            {
                string url = apiBaseUrl + getOnlineGamesListUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetOnlineGamesList);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetOnlineGamesList(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<GameMode> onlineGame = response_body["online_game_list"].ToObject<List<GameMode>>();
                            uiManager.homeScreenParent.transform.GetComponent<HomeScreen>().OnPlay1On1ButtonCallBack(onlineGame);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APIGetLocalGamesList()
        {
            try
            {
                string url = apiBaseUrl + getLocalGamesListUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetLocalGamesList);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetLocalGamesList(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<GameMode> localGame = response_body["local_game_list"].ToObject<List<GameMode>>();
                           uiManager.homeScreenParent.transform.GetComponent<HomeScreen>().OnPlayLocalButtonCallBack(localGame);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APIGetComputerGamesList()
        {
            try
            {
                string url = apiBaseUrl + getComputerGamesListUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetComputerGamesList);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetComputerGamesList(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<GameMode> computerGame = response_body["computer_game_list"].ToObject<List<GameMode>>();
                            uiManager.homeScreenParent.transform.GetComponent<HomeScreen>().OnPlayWithFriendsButtonCallBack(computerGame);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APIGetTournamentGamesList()
        {
            try
            {
                string url = apiBaseUrl + getTournamentGamesListUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetTournamentGamesList);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetTournamentGamesList(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<GameMode> tournamentGame = response_body["tournament_game_list"].ToObject<List<GameMode>>();
                            uiManager.homeScreenParent.transform.GetComponent<HomeScreen>().OnTournamentButtonCallBack(tournamentGame);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APIGetSpecialGamesList()
        {
            try
            {
                string url = apiBaseUrl + getSpecialGamesListUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetSpecialGamesList);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetSpecialGamesList(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<GameMode> eventGame = response_body["event_game_list"].ToObject<List<GameMode>>();
                            uiManager.homeScreenParent.transform.GetComponent<HomeScreen>().OnSpecialEventsButtonCallBack(eventGame);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }

        #endregion

        #region Friend Details APIs
        public void APIGetFriendsList()
        {
            try
            {
                string url = apiBaseUrl + getFriendsListUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetFriendsList);
                request.IsKeepAlive = false;
                //request.AddField("player_id", GameData.Instance().PlayerId);
                //request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("player_id", "3");
                request.AddField("security_id", "3-bf164a8d-a536-4eb7-a133-3249a1dd9b5a-392020115243844");
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetFriendsList(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        print("calling");
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            print("Calling...");
                            print(response_body);
                            List<Friend> friends = response_body["friends_list"].ToObject<List<Friend>>();
                            print("Call...");
                            uiManager.playWithFriendsScreenParent.transform.GetComponent<PlayWithFriendsScreen>().OnFriendsActivityButtonCallBack(friends);
                           uiManager.playWithFriendsScreenParent.transform.GetComponent<PlayWithFriendsScreen>().OnFriendsOfFriendsButtonCallBack(friends);
                           uiManager.playWithFriendsScreenParent.transform.GetComponent<PlayWithFriendsScreen>().OnFriendsButtonCallBack(friends);
                          
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APIAddFriend(string friendId)
        {
            try
            {
                string url = apiBaseUrl + addFriendUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIAddFriend);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("friend_id", friendId);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIAddFriend(HTTPRequest request, HTTPResponse response)
        {

            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        print(response.DataAsText);
                        //uiManager.playWithFriendsScreenParent.transform.GetComponent<PlayWithFriendsScreen>().OnFriendsOfFriendsAddFriendButtonCallBack();
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APIInviteFriend(string friendId)
        {
            try
            {
                string url = apiBaseUrl + inviteFriendUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIInviteFriend);
                request.IsKeepAlive = false;
               // request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("player_id", "3");
                request.AddField("friend_id", friendId);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIInviteFriend(HTTPRequest request, HTTPResponse response)
        {

            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        print(response.DataAsText);
                        //uiManager.playLocalScreenParent.transform.GetComponent<PlayWithFriendsScreen>().OnFriendsChallengeButtonCallBack();
                        //uiManager.playWithFriendsScreenParent.transform.GetComponent<PlayWithFriendsScreen>().OnFriendsOfFriendsChallengeButtonCallBack();
                        //uiManager.playWithFriendsScreenParent.transform.GetComponent<PlayWithFriendsScreen>().OnFriendsActivityChallengeButtonCallBack();
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APIGetFbFriendsList()
        {
            try
            {
                string url = apiBaseUrl + getFbFriendsListUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetFbFriendsList);
                request.IsKeepAlive = false;
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetFbFriendsList(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<FbFriend> fbFriend = response_body["friends_list"].ToObject<List<FbFriend>>();
                            //uiManager.shopScreen.transform.GetComponent<ShopScreen>().OnMainShopCoinsButtonCallBack(fbFriend);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        #endregion

        #region Free rewards API
        public void APIGetFreeRewardsData()
        {
            try
            {
                string url = apiBaseUrl + getFreeRewardsDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetFreeRewardsData);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetFreeRewardsData(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            int coinCount = response_body["daily_free_coin_count"].Value<int>();
                            print(response_body["daily_free_coin_count"].Value<int>());
                            uiManager.freeRewardsScreenParent.transform.GetComponent<FreeRewardsScreen>().OnFreeFreewardsCoinButtonCallBack(coinCount);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }

        public void APICollectFreeCoins(int coinCount)
        {
            try
            {
                string url = apiBaseUrl + collectFreeCoinsUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPICollectFreeCoins);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.AddField("coin_count", coinCount.ToString());
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPICollectFreeCoins(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<long>();
                            uiManager.freeRewardsScreenParent.transform.GetComponent<FreeRewardsScreen>().OnFreeRewardsCoinScreenCloseButtonClicked();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        #endregion

        #region Video ad APIs
        public void APIGetVideoAdsListing()
        {
            try
            {
                string url = apiBaseUrl + getVideoAdsListingUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetVideoAdsListing);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetVideoAdsListing(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            print("Success...");
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<Video> video = response_body["video_list"].ToObject<List<Video>>();
                             //GameData.Instance().PlayerBalance = response_body["player_balance"].Value<long>();
                            uiManager.freeRewardsScreenParent.transform.GetComponent<FreeRewardsScreen>().OnVideoRewardsCoinButtonCallBack(video);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APICollectVideoAdsCoins(string videoId)
        {
            try
            {
                string url = apiBaseUrl + collectVideoAdsCoinsUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPICollectVideoAdsCoins);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.AddField("video_ads_id", videoId);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPICollectVideoAdsCoins(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        print("Success...");
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<long>();
                            uiManager.freeRewardsScreenParent.transform.GetComponent<FreeRewardsScreen>().OnVideoRewardsCoins2CloseButton();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        #endregion

        #region Share reward API
        public void APICollectShareRewardCoins()
        {
            try
            {
                string url = apiBaseUrl + collectShareRewardCoinsUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPICollectShareRewardCoins);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPICollectShareRewardCoins(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            GameData.Instance().PlayerBalance = response_body["player_balance"].Value<int>();
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {
                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }
                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        #endregion

        #region Leaderboard APIs
        public void APIGetLeaderBoardCountryData()
        {
            try
            {
                string url = apiBaseUrl + getLeaderBoardCountryDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetLeaderBoardCountryData);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.AddField("country_id", GameData.Instance().PlayerCountryId.ToString());
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetLeaderBoardCountryData(HTTPRequest request, HTTPResponse response)
        {

            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<Leaderboard> countryBoard = response_body["country_leaderboard_data"].ToObject<List<Leaderboard>>();
                           uiManager.leaderBoardScreenParent.transform.GetComponent<LeaderBoardScreen>().OnLeaderBoardCountryButtonCallBack(countryBoard);
                            uiManager.leaderBoardScreenParent.transform.GetComponent<LeaderBoardScreen>().OnLeaderBoardFriendsButtonCallBack(countryBoard);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        public void APIGetLeaderBoardWorldData()
        {
            try
            {
                string url = apiBaseUrl + getLeaderBoardWorldDataUrl;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIGetLeaderBoardWorldData);
                request.IsKeepAlive = false;
                request.AddField("player_id", GameData.Instance().PlayerId);
                request.AddField("security_id", GameData.Instance().PlayerSecurityKey);
                request.AddField("game_uuid", GameData.Instance().GameUuid);
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIGetLeaderBoardWorldData(HTTPRequest request, HTTPResponse response)
        {

            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                            var response_body = jsonObject["response_body"].Value<JObject>();
                            Debug.Log(response_body);
                            List<Leaderboard> worldBoard = response_body["world_leaderboard_data"].ToObject<List<Leaderboard>>();
                            uiManager.leaderBoardScreenParent.transform.GetComponent<LeaderBoardScreen>().OnLeaderBoardWorldButtonCallBack(worldBoard);
                            uiManager.leaderBoardScreenParent.transform.GetComponent<LeaderBoardScreen>().OnLeaderBoardMyLeagueButtonCallBack(worldBoard);
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                        }
                    }
                    else
                    {

                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));

                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:

                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:

                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:

                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }
        }
        #endregion
    }
}

