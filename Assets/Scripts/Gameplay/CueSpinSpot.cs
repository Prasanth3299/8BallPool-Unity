using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RevolutionGames.Game;
using UnityEngine.UI;

namespace RevolutionGames.Hud
{
    public class CueSpinSpot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public HudManager hudManager;

        private CueStickManager cueStick;
        private Vector2 spinBgSize;
        private Vector2 previousTouchPosition;
        private float updateSpinXPosition;
        private float updateSpinYPosition;
        private int xValue = 0;
        private int yValue = 0;
        private bool dragStarted = false;
        private Vector2 initialPos = Vector2.zero;
        private float radius = 0;
        private RectTransform redSpot;
        private float angle = 0;

        void Start()
        {
            cueStick = hudManager.gameManager.cueStickManager;
            spinBgSize = transform.parent.GetComponent<RectTransform>().sizeDelta;
            updateSpinXPosition = 0;
            updateSpinYPosition = 0;
            initialPos = this.transform.position;
            radius = hudManager.canvas.lossyScale.x * 190f; //190f
            redSpot = hudManager.redSpot;
        }

        public void OnPointerDown(PointerEventData data)
        {
            dragStarted = true;
            if (Vector2.Distance(initialPos, data.position) <= radius)
            {
                transform.position = data.position;
            }
            else
            {
                data.position = initialPos + ((data.position - initialPos).normalized * radius);
                transform.position = data.position;
            }
        }

        public void OnBeginDrag(PointerEventData data)
        {
            previousTouchPosition = data.position;
        }

        public void OnDrag(PointerEventData data)
        {
            if (dragStarted)
            {
                /*if (data.position.x > previousTouchPosition.x)
                {
                    updateSpinXPosition = data.position.x - previousTouchPosition.x;
                    if ((transform.GetComponent<RectTransform>().anchoredPosition.x + updateSpinXPosition) <
                        (spinBgSize.x / 3))
                    {
                        //transform.position = new Vector3(transform.position.x + updateSpinXPosition, transform.position.y, transform.position.z);
                        //previousTouchPosition = data.position;
                        updateSpinXPosition = transform.position.x + updateSpinXPosition;
                    }
                    else
                    {
                        updateSpinXPosition = 0;
                    }
                }
                else
                {
                    updateSpinXPosition = previousTouchPosition.x - data.position.x;
                    if ((transform.GetComponent<RectTransform>().anchoredPosition.x - updateSpinXPosition) >
                        (-spinBgSize.x / 3))
                    {
                        //transform.position = new Vector3(transform.position.x - updateSpinXPosition, transform.position.y, transform.position.z);
                        //previousTouchPosition = data.position;
                        updateSpinXPosition = transform.position.x - updateSpinXPosition;
                    }
                    else
                    {
                        updateSpinXPosition = 0;
                    }
                }

                if (data.position.y > previousTouchPosition.y)
                {
                    updateSpinYPosition = data.position.y - previousTouchPosition.y;
                    if ((transform.GetComponent<RectTransform>().anchoredPosition.y + updateSpinYPosition) <
                        (spinBgSize.y / 3))
                    {
                        //transform.position = new Vector3(transform.position.x, transform.position.y + updateSpinYPosition, transform.position.z);
                        //previousTouchPosition = data.position;
                        updateSpinYPosition = transform.position.y + updateSpinYPosition;
                    }
                    else
                    {
                        updateSpinYPosition = 0;
                    }
                }
                else
                {
                    updateSpinYPosition = previousTouchPosition.y - data.position.y;
                    if ((transform.GetComponent<RectTransform>().anchoredPosition.y - updateSpinYPosition) >
                        (-spinBgSize.y / 3))
                    {
                        //transform.position = new Vector3(transform.position.x, transform.position.y - updateSpinYPosition, transform.position.z);
                        //previousTouchPosition = data.position;
                        updateSpinYPosition = transform.position.y - updateSpinYPosition;
                    }
                    else
                    {
                        updateSpinYPosition = 0;
                    }
                }
                if ((updateSpinXPosition != 0) && (updateSpinYPosition != 0))
                {
                    transform.position = new Vector3(updateSpinXPosition, updateSpinYPosition, transform.position.z);
                    previousTouchPosition = data.position;
                }*/
                if (Vector2.Distance(initialPos, data.position) <= radius)
                {
                    transform.position = data.position;
                }
                else
                {
                    data.position = initialPos + ((data.position - initialPos).normalized * radius);
                    transform.position = data.position;
                }
            }
        }

        public void OnEndDrag(PointerEventData data)
        {
            //if ((updateSpinXPosition != 0) && (updateSpinYPosition != 0))
            if(transform.GetComponent<RectTransform>().anchoredPosition != Vector2.zero)
            {
                angle = Vector2.SignedAngle(Vector2.up, transform.GetComponent<RectTransform>().anchoredPosition);
                //print(angle);
                cueStick.CueSpinAngle = angle;
                if (angle < -90)
                {
                    cueStick.CueSpinAngle = -(angle + 180);
                }
                else if (angle > 90)
                {
                    cueStick.CueSpinAngle = -(angle - 180);
                }
                //print(cueStick.CueSpinAngle);
                if (transform.GetComponent<RectTransform>().anchoredPosition.x < 0)
                {
                    cueStick.TorqueZValue = -1; //(-1* cueStick.ForceFromCueStick);
                }
                else
                {
                    cueStick.TorqueZValue = 1; //(1 * cueStick.ForceFromCueStick);
                }
                if (transform.GetComponent<RectTransform>().anchoredPosition.y < 0)
                {
                    cueStick.TorqueXValue = -1; //(-1 * cueStick.ForceFromCueStick);
                }
                else
                {
                    cueStick.TorqueXValue = 1; //(1 * cueStick.ForceFromCueStick);
                }
            }
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            OnEndDrag(pointerEventData);
            dragStarted = false;
            redSpot.anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x * 0.23f, 
                transform.GetComponent<RectTransform>().anchoredPosition.y * 0.23f);
            hudManager.OnCueSpinClicked();
        }

        public void ResetCueSpinPosition()
        {
            dragStarted = false;
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            if(redSpot == null)
            {
                redSpot = hudManager.redSpot;
                cueStick = hudManager.gameManager.cueStickManager;
            }
            redSpot.anchoredPosition = Vector2.zero;
            cueStick.TorqueZValue = 0;
            cueStick.TorqueXValue = 0;
            cueStick.CueSpinAngle = 0;
        }
    }
}

