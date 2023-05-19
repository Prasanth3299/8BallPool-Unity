using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RevolutionGames.Data;
using UnityEngine.SceneManagement;

namespace RevolutionGames.UI
{
    
    public class RematchGameScreen : MonoBehaviour
    {
        public UIManager uiManager;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnRematchButtonClicked()
        {
            SceneManager.LoadScene("GameplayScene");
        }
        public void OnAddCoinButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.shopScreenParent.SetActive(true);
        }
    }
}
