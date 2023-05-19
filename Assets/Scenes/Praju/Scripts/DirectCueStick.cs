using RevolutionGames.Game;
using RevolutionGames.Hud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public class CanvasInputPositionHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public HudManager hudManager;

        private CueBallManager cueBallManager;
        private CueStickManager cueStickManager;
        private MeshRenderer cueBallMesh;
        private bool cueBallPressed = false;
        private Vector3 mousePos = Vector3.zero;

        private void Start()
        {
            cueBallManager = hudManager.gameManager.cueBallManager;
            cueStickManager = hudManager.gameManager.cueStickManager;
            cueBallMesh = hudManager.gameManager.cueBall.GetComponent<MeshRenderer>();
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            cueBallPressed = false;

            mousePos = Camera.main.ScreenToWorldPoint(pointerEventData.position);
            mousePos = new Vector3(mousePos.x, cueBallMesh.bounds.min.y, mousePos.z);
            if (cueBallMesh.bounds.Contains(mousePos))
            {
                cueStickManager.ShowGuidelineFlag = false;
                cueBallPressed = true;
                cueBallManager.OnPointerDownFunction(pointerEventData);
            }
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            if (cueBallPressed)
            {
                cueStickManager.ShowGuidelineFlag = true;
                cueBallManager.OnPointerUpFunction(pointerEventData);
                cueBallPressed = false;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("OnBeginDrag");
        }

        // Drag the selected item.
        public void OnDrag(PointerEventData eventData)
        {
            /*if(startPosition.y < eventData.position.y)
            {
                cueStrikeParent.eulerAngles = new Vector3(0, cueStrikeParent.eulerAngles.y + eventData.delta.y, 0);
            }
            else
            {
                cueStrikeParent.eulerAngles = new Vector3(0, cueStrikeParent.eulerAngles.y - eventData.delta.y, 0);
            }*/

            //cueStickManager.RotateCueStick(eventData);

            if (cueBallPressed)
            {
                cueBallManager.OnDragFunction(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("OnEndDrag");
        }
    }
