using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using UnityEngine.SceneManagement;

namespace RevolutionGames.UI
{
    public class PlayLocalScreen : MonoBehaviour
    {
        public UIManager uiManager;
        public Text PassNPlayEntryText, QuickFireEntryText;
        public GameObject popupScreen;
        private string modeSelection = "";
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }

        public void OnPassnPlayButtonClicked()
        {
            SceneManager.LoadScene("GameplayScene");
            if (modeSelection == "offline mode") 
            {
                modeSelection = "";
                GameData.Instance().ToScreenName = "login";
            }
            else
            {
                GameData.Instance().ToScreenName = "home";
            }

        }
        public void OfflineModeSlection()
        {
            modeSelection = "offline mode";
        }
        public void OnPlayQuickFireButtonClicked()
        {
            SceneManager.LoadScene("QuickFireScene");
            GameData.Instance().ToScreenName = "home";
           // popupScreen.SetActive(true);
        }
        public void PopupCloseButton()
        {
             popupScreen.SetActive(false);
           
        }
        public void OnBackButtonClicked()
        {
            if(modeSelection== "offline mode")
            {
                modeSelection = "";
                uiManager.playLocalScreenParent.SetActive(false);
                uiManager.loginScreenParent.SetActive(true);

            }
            else
            {
                uiManager.playLocalScreenParent.SetActive(false);
                uiManager.homeScreenParent.SetActive(true);
            }
            
        }

    }
}
