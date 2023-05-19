using RevolutionGames.APIData;
using RevolutionGames.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RevolutionGames.UI
{
    public class ProfileScreen : MonoBehaviour
    {

        public UIManager uIManager;

        public GameObject TopPanel, ProfileDetailsPanel, ProgressPanel;
        public GameObject ForwardButton, BackwardButton, ProgressImage, ProgressRoadContent;
        public Sprite[] testSprites;
        public int PlayerProgress;

        private GameObject newImage, pointerImage;
        private int prevMilestoneText;
        private float fillamt, playerProgressPos;
        private RectTransform progressRoadContentPos;
        private Vector3 playerProgressTargetPos;
        private bool scroll;
        public Image profileImage;


        // Start is called before the first frame update
        void Start()
        {
            scroll = false;
            progressRoadContentPos = ProgressRoadContent.GetComponent<RectTransform>();
            PlayerProgress = 400;
            prevMilestoneText = 0;
            OnGetProgressCallBack();
           
        }

        // Update is called once per frame
        void Update()
        {
            float step = 5000f * Time.deltaTime;
            if (scroll)
            {
                progressRoadContentPos.anchoredPosition = Vector3.MoveTowards(progressRoadContentPos.anchoredPosition, playerProgressTargetPos, step);
                if (Vector3.Distance(progressRoadContentPos.anchoredPosition, playerProgressTargetPos) < 0.001f)
                {
                    scroll = false;
                }
            }
        }

        private void OnEnable()
        {
            TopPanel.SetActive(true);
            ForwardButton.SetActive(false);
            BackwardButton.SetActive(false);
            ProfileDetailsPanel.SetActive(true);
            ProgressPanel.SetActive(false);
            print("Start");
            //if (uIManager.SubScreenNavigation == UIManager.SubScreens.ProfileDetailsPanel)
            //OnProfileTabButtonClicked();
            //else if (uIManager.SubScreenNavigation == UIManager.SubScreens.ProgressPanel)
            // OnProgressTabButtonClicked();
        }

        //Prefab instantiation
        public void InstantiateProgressRoadContent(List<ProgressMilestone> progressMilestones)
        {
            for (int i = 0; i < ProgressRoadContent.transform.childCount; i++) // To remove any existing childs
            {
                Destroy(ProgressRoadContent.transform.GetChild(i).gameObject);
            }

            
            for (int i = 0; i < progressMilestones.Count; i++)
            {
                newImage = Instantiate(ProgressImage) as GameObject;
                newImage.transform.SetParent(ProgressRoadContent.transform, true);
                newImage.transform.GetChild(0).GetComponent<Text>().text = progressMilestones[i].milestoneText.ToString();
                if(i == 0)
                    newImage.transform.GetChild(1).gameObject.SetActive(true);

                newImage.transform.GetChild(3).GetComponent<Image>().sprite = progressMilestones[i].prizeImage;
                newImage.transform.GetChild(3).GetComponent<Image>().SetNativeSize();
                newImage.transform.GetChild(3).GetChild(0).transform.localPosition = new Vector3(progressMilestones[i].prizeImage.rect.width / 2, progressMilestones[i].prizeImage.rect.height / 2);
                newImage.transform.GetChild(3).GetChild(1).transform.localPosition = new Vector3(progressMilestones[i].prizeImage.rect.width / 2, progressMilestones[i].prizeImage.rect.height / 2);

                //Determine the fill amount of progress bar to show player's progress
                fillamt = (float)(PlayerProgress - prevMilestoneText) / (float)(progressMilestones[i].milestoneText - prevMilestoneText);
                newImage.transform.GetChild(2).GetComponent<Image>().fillAmount = fillamt;

                if (prevMilestoneText < PlayerProgress && PlayerProgress <= progressMilestones[i].milestoneText)
                {
                    pointerImage = newImage.transform.GetChild(4).gameObject;
                    float pos = fillamt * ProgressRoadContent.GetComponent<GridLayoutGroup>().cellSize.x;
                    pointerImage.transform.localPosition = new Vector3(pos,pointerImage.transform.localPosition.y, 0);
                    pointerImage.SetActive(true);
                    playerProgressPos = ProgressRoadContent.GetComponent<GridLayoutGroup>().cellSize.x * i + pos + progressRoadContentPos.GetComponent<GridLayoutGroup>().padding.left;
                    
                    progressRoadContentPos.anchoredPosition = new Vector3(-(playerProgressPos - pos), progressRoadContentPos.anchoredPosition.y, 0);
                }
                prevMilestoneText = progressMilestones[i].milestoneText;
            }
            
        }

        public void OnGetProgressCallBack()
        {
            List<ProgressMilestone> progressMilestones = new List<ProgressMilestone>();

            ProgressMilestone progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 8;
            progressMilestone.prizeImage = testSprites[0];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 16;
            progressMilestone.prizeImage = testSprites[1];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 50;
            progressMilestone.prizeImage = testSprites[2];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 85;
            progressMilestone.prizeImage = testSprites[0];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 100;
            progressMilestone.prizeImage = testSprites[0];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 250;
            progressMilestone.prizeImage = testSprites[0];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 350;
            progressMilestone.prizeImage = testSprites[0];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 450;
            progressMilestone.prizeImage = testSprites[0];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 550;
            progressMilestone.prizeImage = testSprites[0];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 650;
            progressMilestone.prizeImage = testSprites[0];
            progressMilestones.Add(progressMilestone);

            progressMilestone = new ProgressMilestone();
            progressMilestone.milestoneText = 750;
            progressMilestone.prizeImage = testSprites[0];
            progressMilestones.Add(progressMilestone);

            InstantiateProgressRoadContent(progressMilestones);
        }

        //when scrolling happens
        public void OnProgressRoadValueChanged(Vector2 pos)
        {
            /*if (ProgressRoadContent.transform.localPosition.x < playerProgressIndex * 500)
                BackwardButton.SetActive(true);

            if (ProgressRoadContent.transform.localPosition.x > playerProgressIndex * 500)
                ForwardButton.SetActive(true);*/
            if(progressRoadContentPos.anchoredPosition.x < -playerProgressPos)
                BackwardButton.SetActive(true);
            else
                BackwardButton.SetActive(false);
            if (progressRoadContentPos.anchoredPosition.x > -playerProgressPos + Screen.width)
                ForwardButton.SetActive(true);
            else
                ForwardButton.SetActive(false);
        }

        //TopPanel
        public void OnProfileTabButtonClicked()
        {
            ProfileDetailsPanel.SetActive(true);
            ProgressPanel.SetActive(false);
        }

        public void OnProgressTabButtonClicked()
        {
            ProfileDetailsPanel.SetActive(false);
            ProgressPanel.SetActive(true);
        }

        public void OnBackButtonClicked()
        {
            this.gameObject.SetActive(false);
            uIManager.homeScreenParent.SetActive(true);
            //uIManager.ShowScreen(UIManager.Screens.ProfileScreen, UIManager.Screens.HomeScreen);
        }
        public void ProfileditButtonClicked()
        {
            this.gameObject.SetActive(false);
            uIManager.shopScreenParent.SetActive(true);
            uIManager.shopScreenParent.transform.GetComponent<ShopScreen>().ProfileEditButtonClicked();
        }


        //ProfileDetails Panel
        public void OnAvatarButtonClicked()
        {
            //GameData.Instance().ShopSubScreen = Screens.Avatar;
            //uIManager.ShowScreen(this.gameObject, Screens.ShopScreen);
        }

        //LevelProgress Panel
        public void OnBackwardButtonClicked()
        {
            if (playerProgressPos >= (Screen.width / 2))
                playerProgressTargetPos = new Vector3(-playerProgressPos + (Screen.width / 2), progressRoadContentPos.anchoredPosition.y, 0);
      
            else
                playerProgressTargetPos = new Vector3(0f, progressRoadContentPos.anchoredPosition.y, 0);
            scroll = true;
            BackwardButton.SetActive(false);
        }

        public void OnForwardButtonClicked()
        {
            print(playerProgressPos);
            if (playerProgressPos <= (ProgressRoadContent.transform.childCount * ProgressRoadContent.GetComponent<GridLayoutGroup>().cellSize.x))
                playerProgressTargetPos = new Vector3(-playerProgressPos + (Screen.width / 2), progressRoadContentPos.anchoredPosition.y, 0);
            else
                playerProgressTargetPos = new Vector3(-(ProgressRoadContent.transform.childCount*ProgressRoadContent.GetComponent<GridLayoutGroup>().cellSize.x - Screen.width + (ProgressRoadContent.GetComponent<GridLayoutGroup>().padding.left *2)), progressRoadContentPos.anchoredPosition.y, 0);
            print(playerProgressTargetPos);
            scroll = true;
            ForwardButton.SetActive(false);
        }
        public void ProfilePictureUpdate(Sprite avatar)
        {
            profileImage.sprite = avatar;
        }
    }
}
