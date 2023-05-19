using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RevolutionGames.Dummy
{
    public class ForceUpdater : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CueStick cueStick;
        float updatedYPosition = 0;
        public Image forceFillImage;
        public void OnBeginDrag(PointerEventData eventData)
        {
            updatedYPosition = eventData.position.y;
        }

        public void OnDrag(PointerEventData eventData)
        {
            float diff = eventData.position.y - updatedYPosition;
            if (diff > 0)
            {
                if (transform.GetComponent<RectTransform>().anchoredPosition.y + diff < 420)
                {
                    forceFillImage.fillAmount -= (diff * 0.0025f);
                    transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                    transform.GetComponent<RectTransform>().anchoredPosition.y + diff);
                }
                else
                {
                    transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                    420);
                }
            }
            else
            {
                if (transform.GetComponent<RectTransform>().anchoredPosition.y + diff > 0)
                {
                    forceFillImage.fillAmount += (-diff * 0.0025f);
                    transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                    transform.GetComponent<RectTransform>().anchoredPosition.y + diff);
                }
                else
                {
                    transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
                    0);
                }
            }
            updatedYPosition = eventData.position.y;
            cueStick.UpdateForceValue(forceFillImage.fillAmount);

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            updatedYPosition = 0;
            if (forceFillImage.fillAmount < 0.05f)
            {
                ResetTheForceSliderValues();
            }
            cueStick.StrikeTheBall();
        }

        public void ResetTheForceSliderValues()
        {
            forceFillImage.fillAmount = 0;
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x,
               420);
            cueStick.UpdateForceValue(forceFillImage.fillAmount);
        }
    }
}
