using RevolutionGames.Game;
using RevolutionGames.Hud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RevolutionGames.Hud
{
    public class CanvasInputPositionHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public HudManager hudManager;

        private CueBallManager cueBallManager;
        private CueStickManager cueStickManager;
        //private BoardManager boardManager;
        private SpriteRenderer cueBallInHandSprite;
        private bool cueBallPressed = false;
        private Vector3 mousePos = Vector3.zero;
        private Vector2 dragVectorDirection;
        private float dragVectorMag = 0;
        private float cueStickAngle = 0;
        private bool isDrag = false;

        private void Start()
        {
            cueBallManager = hudManager.gameManager.cueBallManager;
            cueStickManager = hudManager.gameManager.cueStickManager;
            cueBallInHandSprite = hudManager.gameManager.cueBallInHandCircle.GetComponent<SpriteRenderer>();
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            cueBallPressed = false;

            mousePos = Camera.main.ScreenToWorldPoint(pointerEventData.position);
            mousePos = new Vector3(mousePos.x, cueBallInHandSprite.bounds.min.y, mousePos.z);
            if ((hudManager.gameManager.CurrentGameState == GameManager.GameState.isItResetCueballOn ||
                hudManager.gameManager.CurrentGameState == GameManager.GameState.isItCueballBreak) && cueBallInHandSprite.bounds.Contains(mousePos))
            {
                cueStickManager.ShowGuidelineFlag = false;
                cueBallPressed = true;
                cueBallManager.OnPointerDownFunction(pointerEventData);
            }
            hudManager.IsCueBallPressed = cueBallPressed;
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            if (cueBallPressed)
            {
                cueStickManager.ShowGuidelineFlag = true;
                cueBallManager.OnPointerUpFunction(pointerEventData);
                cueBallPressed = false;
            }
            else if(cueStickManager.GetTapToAimFlag() == 1 && !isDrag) //tap to aim enabled and user has not dragged to position cue stick
            {
                Vector3 temp = (Camera.main.ScreenToWorldPoint(pointerEventData.position));
                temp = new Vector3(temp.x, cueBallInHandSprite.gameObject.transform.position.y, temp.z);
                cueStickAngle = Vector3.SignedAngle(temp - cueBallInHandSprite.gameObject.transform.position, cueBallInHandSprite.gameObject.transform.right, Vector3.up);
                cueStickManager.TapAndRotateCueStick(cueStickAngle);
            }
            isDrag = false;
            StartCoroutine(Delay());
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("OnBeginDrag");
            cueBallManager.ShowHandImage(false);
            dragVectorDirection = eventData.position;
        }

        // Drag the selected item.
        public void OnDrag(PointerEventData eventData)
        {
            //Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition);
            print("OnDrag");
            dragVectorMag = (eventData.position - dragVectorDirection).magnitude;
            dragVectorDirection = (eventData.position - dragVectorDirection).normalized;//eventData.position - dragVectorDirection;
            if (cueBallPressed)
            {
                cueBallManager.OnDragFunction(eventData);
            }
            else
            {
                if (dragVectorMag >= 0.5f)
                {
                    cueStickManager.RotateCueStick(dragVectorDirection, dragVectorMag, Camera.main.ScreenToWorldPoint(eventData.position)); //* Time.deltaTime);
                }
                else
                {
                    cueStickManager.RotateCueStick(Vector3.zero, 0, Camera.main.ScreenToWorldPoint(eventData.position));
                }
            }
            dragVectorDirection = eventData.position;
            isDrag = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            print("EndDrag");
            cueStickManager.RotateCueStick(Vector3.zero, 0, Camera.main.ScreenToWorldPoint(eventData.position)); //* Time.deltaTime);
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(3f);
            hudManager.IsCueBallPressed = false;

        }
    }
}
