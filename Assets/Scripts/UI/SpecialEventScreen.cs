using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using System.Text.RegularExpressions;
using RevolutionGames.APIData;

namespace RevolutionGames.UI
{

    public class SpecialEventScreen : MonoBehaviour
    {
        public UIManager uiManager;

        public GameObject eventsParent;
        public GameObject noGamesPopup;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            eventsParent.SetActive(false);
            noGamesPopup.SetActive(true);    
        }

        public void OnPopupCloseButtonClicked()
        {
            OnBackButtonClicked();
        }

        public void OnBackButtonClicked()
        {
            uiManager.specialEventScreenParent.SetActive(false);
            uiManager.homeScreenParent.SetActive(true);
        }

    }
}
