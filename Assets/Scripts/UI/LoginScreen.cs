using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;

namespace RevolutionGames.UI
{
    public class LoginScreen : MonoBehaviour
    {
        public UIManager uiManager;
        public GameObject loginScreen;
        public GameObject loginSelectionScreen;
        public GameObject loginEmailScreen;
        public GameObject newLoginEmailScreen;
        public GameObject oldLoginEmailScreen;
        public GameObject forgotEmailPasswordScreen;
        public GameObject thaksForGotEmailPasswordScreen;
        public GameObject guestLoginScreen;
        public GameObject selectAvatarScreen;
        public Text emailLoginErrorText;
        public Text emailNewUserNameText;
        public Text emailOldUserNmaeText;
        public Text emailPasswordEnterErrorText;
        public Text emailPasswordLoginErrorText;
        public Text emailForgotPasswordErrorText;
        public Text guestErrorText;
        public GameObject femaleAvatarSelect;
        public GameObject maleAvatarSelect;
        public GameObject loginGoogleButton;
        public GameObject loginAppleButton;
        private string loginName = "";
        const string matchEmailPattern = @"^([a-zA-Z0-9_\-\+\.]+)@([a-zA-Z0-9_\-\+\.]+)\.([a-zA-Z]{2,5})$";


        public void Start()
        {
            
#if UNITY_IOS
            Debug.Log("Ios platform");
            loginAppleButton.SetActive(true);
            loginGoogleButton.SetActive(false);

#else
            loginAppleButton.SetActive(false);
            loginGoogleButton.SetActive(true);
            //Debug.Log("Any other platform");

#endif
        }
        private void OnEnable()
        {
            loginSelectionScreen.SetActive(true);
            loginEmailScreen.SetActive(false);
            newLoginEmailScreen.SetActive(false);
            oldLoginEmailScreen.SetActive(false);
            forgotEmailPasswordScreen.SetActive(false);
            thaksForGotEmailPasswordScreen.SetActive(false);
            guestLoginScreen.SetActive(false);
            selectAvatarScreen.SetActive(false);
        }
        //private string playerName;
        public void EmailLoginButton()
        {
            loginSelectionScreen.SetActive(false);
            loginEmailScreen.SetActive(true);
        }
        public void PlayGuestButton()
        {
            loginSelectionScreen.SetActive(false);
            guestLoginScreen.SetActive(true);

        }       
        public void EmailScreenNameEntered(string name)
        {
            GameData.Instance().PlayerName = name.Trim();
        }
        public void EmailScreenEmailIdEntered(string email)
        {
            
            GameData.Instance().PlayerEmailId = email.Trim();
        }
        public void LoginEmailContinueButtonClicked()
        {
            string playreName = GameData.Instance().PlayerName;
            string playerEmailId = GameData.Instance().PlayerEmailId;
            if (playreName.Length == 0)
            {
                emailLoginErrorText.text = "Please enter your name.";
                print("emailLoginErrorText" + emailLoginErrorText);
            }
            else if (playreName.Length <= 2)
            {
                emailLoginErrorText.text = "Name should be at least 3 characters,";
            }
            else if (playerEmailId.Length == 0)
            {
                emailLoginErrorText.text = "Please enter your Email Id.";
            }
            else if (Regex.IsMatch(playerEmailId, matchEmailPattern) == false)
            {
                emailLoginErrorText.text = "Please enter valid Email address.";
            }
            else
            {
                //uiManager.apiManager.APIValidateEmailPlayer();
                LoginEmailContinueButtonCallBack();
            }
        }
        
        public void LoginEmailContinueButtonCallBack(bool oldUser = false)
        {

            if (!oldUser)
            {
                loginEmailScreen.SetActive(false);
                newLoginEmailScreen.SetActive(true);
                emailNewUserNameText.text = "Hello " + GameData.Instance().PlayerName + "!";

            }
            else
            {
                loginEmailScreen.SetActive(false);
                oldLoginEmailScreen.SetActive(true);
                emailOldUserNmaeText.text= GameData.Instance().PlayerName+"!";
            }
        }
        public void EmailNewUserPasswordEntered(string password)
        {
            GameData.Instance().PlayerPassword = password.Trim();
        }
        public void EmailNewUserPasswordContinueButtonClicked()
        {
            string playerPassword = GameData.Instance().PlayerPassword;
            if (playerPassword.Length == 0)
            {
                emailPasswordEnterErrorText.text = "Please enter password";
            }
            else if (playerPassword.Length < 4 || playerPassword.Length > 12)
            {
                emailPasswordEnterErrorText.text = "Please enter valid password";
            }
            else
            {
                //uiManager.apiManager.APIRegisterEmailPlayer();
                EmailNewUserPasswordContinueButtonCallBack();
            }
        }
        public void EmailNewUserPasswordContinueButtonCallBack()
        {
            PlayerPrefs.SetInt("email login", 1);
            newLoginEmailScreen.SetActive(false);
            selectAvatarScreen.SetActive(true);
        }
        public void EmailOldUserPasswordEntered(string password)
        {
            GameData.Instance().PlayerPassword = password.Trim();
        }
        public void EmailOldUserPasswordContinueButtonClicked()
        {
            string playerPassword = GameData.Instance().PlayerPassword;
            if (playerPassword.Length == 0)
            {
                emailPasswordLoginErrorText.text = "Please enter your password";
            }
            else if (playerPassword.Length < 4 || playerPassword.Length > 12)
            {
                emailPasswordLoginErrorText.text = "Please enter valid password";
            }
            else
            {
                //uiManager.apiManager.APILoginEmailPlayer();//
                EmailOldUserPasswordContinueButtonCallBack();
            }
        }
        public void EmailOldUserPasswordContinueButtonCallBack(string errorMessage="")
        {
            print("errorMeassage:" + errorMessage);
            if (errorMessage == "")
            {
                oldLoginEmailScreen.SetActive(false);
                selectAvatarScreen.SetActive(true);
            }
            else
            {
                oldLoginEmailScreen.SetActive(true);
                emailPasswordLoginErrorText.text = errorMessage;
            }
            emailPasswordLoginErrorText.text = errorMessage.ToString();
        }
        public void ForgotEmailPasswordClickButton()
        {
            oldLoginEmailScreen.SetActive(false);
            forgotEmailPasswordScreen.SetActive(true);
        }
        public void ForgotEmailIdEntered(string email)
        {
            GameData.Instance().PlayerEmailId = email;
        }
        public void EmailForgotPasswordButtonClicked()
        {
            string playerEmailId = GameData.Instance().PlayerEmailId;
            if (playerEmailId.Length == 0)
            {
                emailForgotPasswordErrorText.text = "Please enter your Email ID";
            }
            else if (Regex.IsMatch(playerEmailId, matchEmailPattern) == false)
            {
                emailForgotPasswordErrorText.text = "Please enter valid Email ID";
            }
            else
            {
                //uiManager.apiManager.APIForgotPasswordEmailPlayer();
                EmailForgotPasswordButtonCallBack();
            }
        }
        
        public void EmailForgotPasswordButtonCallBack()
        {
            forgotEmailPasswordScreen.SetActive(false);
            thaksForGotEmailPasswordScreen.SetActive(true);
            
        }
        public void GuestNameEntered(string name)
        {
            GameData.Instance().PlayerName = name.Trim();
        }
        public void GuestLoginContinueButtoinClicked()
        {
            string playerName = GameData.Instance().PlayerName;
            if (playerName.Length == 0)
            {
                guestErrorText.text = "Please enter your name";
            }
            else if (playerName.Length < 4 || playerName.Length > 12)
            {
                guestErrorText.text = "Please enter valid name";
            }
            else
            {
                //uiManager.apiManager.APILoginGuestPlayer();
                guestLoginContinueButtonCallBack();
            }
        }
        public void guestLoginContinueButtonCallBack()
        {
            guestLoginScreen.SetActive(false);
            selectAvatarScreen.SetActive(true);
        }
        public void CloseThanksScreen()
        {
            thaksForGotEmailPasswordScreen.SetActive(false);
            loginSelectionScreen.SetActive(true);
        }
        public void CloseEmailForgetPasswordButtonClicked()
        {
            forgotEmailPasswordScreen.SetActive(false);
            oldLoginEmailScreen.SetActive(true);
            
        }
        public void ColseEmailLoginScreenButtonClicked()
        {
            loginEmailScreen.SetActive(false);
            loginSelectionScreen.SetActive(true);
        }
        public void CloseNewLoginEmailScreenButtonClicked()
        {
            loginEmailScreen.SetActive(true);
            newLoginEmailScreen.SetActive(false);
        }
        public void CloseOldLoginEmailScreenButtonClicked()
        {
            loginEmailScreen.SetActive(true);
            oldLoginEmailScreen.SetActive(false);
        }
        public void GuestLoginScreenCloseButtonClicked()
        {
            guestLoginScreen.SetActive(false);
            loginSelectionScreen.SetActive(true);   
        }
        public void FemeleAvatarSelectButton()
        {
            femaleAvatarSelect.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            maleAvatarSelect.transform.GetComponent<Image>().color=new Color32 (77,77,77,255);
        }
        public void MaleAvatarSelectButton()
        {
            
            femaleAvatarSelect.transform.GetComponent<Image>().color = new Color32(77, 77, 77, 255);

            maleAvatarSelect.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        public void SelectAvatarScreenContinueButtonClicked()
        {
            if (loginName == "facebook")
            {
                PlayerPrefs.SetInt("facebook login", 1);
            }
            if (loginName == "google")
            {
                PlayerPrefs.SetInt("google login", 1);
            }
            if (loginName == "apple")
            {
                PlayerPrefs.SetInt("apple login",1);
            }
           
            uiManager.loginScreenParent.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }

        public void OnLoginFacebookButtonClicked()
        {
            loginName = "facebook";
            
            loginSelectionScreen.SetActive(false);
            selectAvatarScreen.SetActive(true);
        }

        public void OnLoginAppleButtonClicked()
        {
            loginName = "apple";
            loginSelectionScreen.SetActive(false);
            selectAvatarScreen.SetActive(true);
        }
        public void OnLoginGoogleButtonClicked()
        {
            loginName = "google";
            loginSelectionScreen.SetActive(false);
            selectAvatarScreen.SetActive(true);
        }

        public void OnPracticeOfflineButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.playLocalScreenParent.SetActive(true);
            uiManager.playLocalScreenParent.transform.GetComponent<PlayLocalScreen>().OfflineModeSlection();
        }
        public void OnAvatarSelectBackButtonClicked()
        {
            loginName = "";
            selectAvatarScreen.SetActive(false);
            loginSelectionScreen.SetActive(true);
        }
        
    }
}
