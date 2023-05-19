using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RevolutionGames.UI
{
    public class ProfileSummaryScreen : MonoBehaviour
    {
        
        public GameObject ProfileDetailsPanel;

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
            ProfileDetailsPanel.SetActive(true);
        }

        public void OnAvatarButtonClicked()
        {
            //Redirect to SHop screen
        }

        public void OnCloseButtonClicked()
        {
            this.gameObject.SetActive(false);
        }
    }
}
