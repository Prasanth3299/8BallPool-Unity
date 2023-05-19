using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RevolutionGames.UI
{
    public class DirectCueStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CueBall cueBall;
        public Transform cueStrikeParent;
        public MeshRenderer cueBallMesh;
        Vector2 startPosition;
        Vector3 mousePos;

        bool cueBallPressed;

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            cueBallPressed = false;
            mousePos = Camera.main.ScreenToWorldPoint(pointerEventData.position);
            mousePos = new Vector3(mousePos.x, cueBallMesh.bounds.min.y, mousePos.z);
            if(cueBall.isCueBallInHand && cueBallMesh.bounds.Contains(mousePos))
            {
                cueBallPressed = true;
                print("Here");
                cueBall.OnPointerDownFunction(pointerEventData);
                //this.gameObject.SetActive(false);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = eventData.position;
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
            cueStrikeParent.eulerAngles = new Vector3(0, eventData.position.magnitude, 0);
            if(cueBallPressed)
            {
                cueBall.OnDragFunction(eventData);
            }
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            print("Now here");
            if (cueBallPressed)
            {
                cueBall.OnPointerUpFunction(pointerEventData);
                cueBallPressed = false;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }
    }
}
