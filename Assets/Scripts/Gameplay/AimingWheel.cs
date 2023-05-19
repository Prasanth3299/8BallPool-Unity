using RevolutionGames.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RevolutionGames.Hud
{
    public class AimingWheel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public HudManager hudManager;
        public Transform scale, scale2, scale3;

        private CueStickManager cueStickManager;
        private Vector2 dragVectorDirection = Vector2.zero;
        private float dragVectorMag = 0;
        private float positiveX = 0;
        private float positiveY = 0;
        private Vector3 scaleInitialPosition, scale2InitialPosition, scale3InitialPosition;


        // Start is called before the first frame update
        void Start()
        {
            cueStickManager = hudManager.gameManager.cueStickManager;
            GetInitialPosition();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragVectorDirection = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            dragVectorMag = (eventData.position - dragVectorDirection).magnitude;
            dragVectorDirection = (eventData.position - dragVectorDirection).normalized;
            cueStickManager.RotateCueStick(dragVectorDirection, dragVectorMag, Camera.main.ScreenToWorldPoint(eventData.position));
            positiveX = Mathf.Abs(dragVectorDirection.x);
            positiveY = Mathf.Abs(dragVectorDirection.y);
            if (positiveX <= positiveY)
            {
                if (dragVectorDirection.y > 0)
                {
                    scale.position += Vector3.up;
                    scale2.position += Vector3.up;
                    scale3.position += Vector3.up;
                }
                else
                {
                    scale.position -= Vector3.up;
                    scale2.position -= Vector3.up;
                    scale3.position -= Vector3.up;
                }
            }
            dragVectorDirection = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }

        public void GetInitialPosition()
        {
            scaleInitialPosition = scale.position;
            scale2InitialPosition = scale2.position;
            scale3InitialPosition = scale3.position;
        }

        public void ResetAimingWheel()
        {
            this.gameObject.SetActive(true);
            scale.position = scaleInitialPosition;
            scale2.position = scale2InitialPosition;
            scale3.position = scale3InitialPosition;
        }
    }
}
