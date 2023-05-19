
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RevolutionGames.Game;

namespace RevolutionGames.Hud
{
    public class ForceSliderQuickFireMode : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public HudManagerQuickFireMode hudManager;
        public Image forceFillImage;

        private CueStickManagerQuickFireMode cueStick;
        private float updatedYPosition = 0;
        private float updatedXPosition = 0;
        private float imageSizeToSlideY;
        private float imageSizeToSlideX;
        private GameObject handImage;
        private int powerBarOrientationFlag = 0;
        private int powerBarLocationFlag = 0;
        private bool dragStarted = false;

        void Start()
        {
            cueStick = hudManager.gameManager.cueStickManager;
            imageSizeToSlideY = forceFillImage.rectTransform.sizeDelta.y;
            imageSizeToSlideX = forceFillImage.rectTransform.sizeDelta.x;
            handImage = hudManager.gameManager.handImage;
        }
        public void OnPointerDown(PointerEventData data)
        {
            dragStarted = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // 0 - vertical
            // 1 - horizontal
            powerBarOrientationFlag = cueStick.GetPowerBarOrientationFlag();
            powerBarLocationFlag = cueStick.GetPowerBarLocationFlag();
            handImage.SetActive(false);
            updatedYPosition = eventData.position.y;
            updatedXPosition = eventData.position.x;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragStarted)
            {
                if (powerBarOrientationFlag == 0) // vertical
                {
                    float diff = eventData.position.y - updatedYPosition;
                    if (diff > 0)
                    {
                        //print(transform.GetComponent<RectTransform>().anchoredPosition.y + diff);
                        if (transform.GetComponent<RectTransform>().anchoredPosition.y + diff < imageSizeToSlideY)
                        {
                            forceFillImage.fillAmount -= (diff * 0.0025f);
                            if (forceFillImage.fillAmount >= 0.05f)
                            {
                                forceFillImage.color = new Color(1, 1 - forceFillImage.fillAmount, 0, 1);
                            }
                            else
                            {
                                forceFillImage.color = new Color(0, 0, 0, 1);
                            }
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            transform.GetComponent<RectTransform>().anchoredPosition.y + diff);
                        }
                        else
                        {
                            forceFillImage.fillAmount = 0;
                            forceFillImage.color = new Color(0, 0, 0, 1);
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            imageSizeToSlideY);
                        }
                    }
                    else
                    {
                        if (transform.GetComponent<RectTransform>().anchoredPosition.y + diff > 0)
                        {
                            forceFillImage.fillAmount += (-diff * 0.0025f);
                            if (forceFillImage.fillAmount >= 0.05f)
                            {
                                forceFillImage.color = new Color(1, 1 - forceFillImage.fillAmount, 0, 1);
                            }
                            else
                            {
                                forceFillImage.color = new Color(0, 0, 0, 1);
                            }
                            float val = Mathf.Max(transform.GetComponent<RectTransform>().anchoredPosition.y + diff, 20f);
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            val);
                        }
                        else
                        {
                            forceFillImage.fillAmount = 1;
                            forceFillImage.color = new Color(1, 0, 0, 1);
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            20f);
                        }
                    }
                    updatedYPosition = eventData.position.y;
                    cueStick.UpdateForceValue(forceFillImage.fillAmount);
                }
                else if (powerBarLocationFlag == 0)//Left horizontal
                {
                    float diff = eventData.position.x - updatedXPosition;
                    if (diff > 0)
                    {

                        if (transform.GetComponent<RectTransform>().anchoredPosition.y + diff < imageSizeToSlideY)
                        {
                            forceFillImage.fillAmount -= (diff * 0.0025f);
                            if (forceFillImage.fillAmount >= 0.05f)
                            {
                                forceFillImage.color = new Color(1, 1 - forceFillImage.fillAmount, 0, 1);
                            }
                            else
                            {
                                forceFillImage.color = new Color(0, 0, 0, 1);
                            }
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            transform.GetComponent<RectTransform>().anchoredPosition.y + diff);
                        }
                        else
                        {
                            forceFillImage.fillAmount = 0;
                            forceFillImage.color = new Color(0, 0, 0, 1);
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            imageSizeToSlideY);
                        }
                    }
                    else
                    {

                        if (transform.GetComponent<RectTransform>().anchoredPosition.y + diff > 0)
                        {
                            forceFillImage.fillAmount += (-diff * 0.0025f);
                            if (forceFillImage.fillAmount >= 0.05f)
                            {
                                forceFillImage.color = new Color(1, 1 - forceFillImage.fillAmount, 0, 1);
                            }
                            else
                            {
                                forceFillImage.color = new Color(0, 0, 0, 1);
                            }
                            float val = Mathf.Max(transform.GetComponent<RectTransform>().anchoredPosition.y + diff, 20f);
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            val);
                        }
                        else
                        {
                            forceFillImage.fillAmount = 1;
                            forceFillImage.color = new Color(1, 0, 0, 1);
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            20f);
                        }
                    }
                    updatedXPosition = eventData.position.x;
                    cueStick.UpdateForceValue(forceFillImage.fillAmount);
                }
                else //right horizontal
                {
                    float diff = updatedXPosition - eventData.position.x;
                    if (diff > 0)
                    {

                        if (transform.GetComponent<RectTransform>().anchoredPosition.y + diff < imageSizeToSlideY)
                        {
                            forceFillImage.fillAmount -= (diff * 0.0025f);
                            if (forceFillImage.fillAmount >= 0.05f)
                            {
                                forceFillImage.color = new Color(1, 1 - forceFillImage.fillAmount, 0, 1);
                            }
                            else
                            {
                                forceFillImage.color = new Color(0, 0, 0, 1);
                            }
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            transform.GetComponent<RectTransform>().anchoredPosition.y + diff);
                        }
                        else
                        {
                            forceFillImage.fillAmount = 0;
                            forceFillImage.color = new Color(0, 0, 0, 1);
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            imageSizeToSlideY);
                        }
                    }
                    else
                    {

                        if (transform.GetComponent<RectTransform>().anchoredPosition.y + diff > 0)
                        {
                            forceFillImage.fillAmount += (-diff * 0.0025f);
                            if (forceFillImage.fillAmount >= 0.05f)
                            {
                                forceFillImage.color = new Color(1, 1 - forceFillImage.fillAmount, 0, 1);
                            }
                            else
                            {
                                forceFillImage.color = new Color(0, 0, 0, 1);
                            }
                            float val = Mathf.Max(transform.GetComponent<RectTransform>().anchoredPosition.y + diff, 20f);
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            val);
                        }
                        else
                        {
                            forceFillImage.fillAmount = 1;
                            forceFillImage.color = new Color(1, 0, 0, 1);
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                            20f);
                        }
                    }
                    updatedXPosition = eventData.position.x;
                    cueStick.UpdateForceValue(forceFillImage.fillAmount);
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            updatedYPosition = 0;
            updatedXPosition = 0;
            if (forceFillImage.fillAmount < 0.05f)
            {
                ResetTheForceSliderValues();
            }
            else
            {
                hudManager.EnableCueBallIndicator(false);
                this.transform.parent.gameObject.SetActive(false);
                cueStick.StrikeTheBall();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }

        public void ResetTheForceSliderValues()
        {
            dragStarted = false;
            hudManager.EnableCueBallIndicator(true);
            this.transform.parent.gameObject.SetActive(true);
            forceFillImage.fillAmount = 0;
            forceFillImage.color = new Color(0, 0, 0, 1);
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
               imageSizeToSlideY);
            cueStick.UpdateForceValue(forceFillImage.fillAmount);
        }
    }
}


