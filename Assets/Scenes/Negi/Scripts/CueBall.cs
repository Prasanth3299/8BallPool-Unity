using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace RevolutionGames.UI
{
    public class CueBall : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public GameObject CueBallInHand, CueStickParent, HeadStringArea, FloorArea;
        public GameObject[] Balls;
        public bool isCueBallInHand = true, isBreakShot = false;

        private bool isMouseButtonPressed = false, noObstaclesFlag = true;// isCueBallInHand = false;
        private float cueBallYPos;
        private SpriteRenderer headStringAreaSprite, floorAreaSprite;
        private Vector3 mousePos, tempPos;

        private Vector3[] ballPositions;

        private void Awake()
        {

        }
        private void Start()
        {
            ballPositions = new Vector3[15];
            headStringAreaSprite = HeadStringArea.GetComponent<SpriteRenderer>();
            floorAreaSprite = FloorArea.GetComponent<SpriteRenderer>();
            CueBallInHand.SetActive(false);
            HeadStringArea.SetActive(false);
            isCueBallInHand = true;
            cueBallYPos = this.transform.position.y;
            tempPos = this.transform.position;

        }

        private void Update()
        {

            //Store positions of each object ball
            for (int i = 0; i < Balls.Length; i++)
            {
                ballPositions[i] = Balls[i].transform.position;
            }

            CueBallInHand.transform.position = new Vector3(this.transform.position.x, CueBallInHand.transform.position.y, this.transform.position.z);

        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            Debug.Log("OnPointerDown called.");
            if (isCueBallInHand)
            {
                Ray ray = Camera.main.ScreenPointToRay(pointerEventData.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    // whatever tag you are looking for on your game object
                    if (hit.collider.tag == "CueBall")
                    {
                        isMouseButtonPressed = true;
                        CueBallInHand.transform.position = new Vector3(this.transform.position.x, CueBallInHand.transform.position.y, this.transform.position.z);
                        CueBallInHand.SetActive(true);
                        CueStickParent.SetActive(false);
                        if (isBreakShot)
                            HeadStringArea.SetActive(true);
                        for (int i = 0; i < Balls.Length; i++)
                        {
                            Balls[i].GetComponent<Rigidbody>().isKinematic = true;

                        }
                        this.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {

            Debug.Log(name + " Game Object up!");
            if (isMouseButtonPressed)
            {
                isMouseButtonPressed = false;
                CueBallInHand.SetActive(false);
                HeadStringArea.SetActive(false);
                CueStickParent.SetActive(true);
                this.transform.position = new Vector3(this.transform.position.x, 0.3f, this.transform.position.z);
                CueStickParent.transform.position = new Vector3(this.GetComponent<Transform>().position.x, CueStickParent.transform.position.y,
                        this.GetComponent<Transform>().position.z);
                this.GetComponent<Rigidbody>().useGravity = true;
                StartCoroutine(TurnOffKinematic());
            }
        }

        public void OnDrag(PointerEventData data)
        {
            Debug.Log("OnDrag called.");
            if (isMouseButtonPressed)
            {
                noObstaclesFlag = true;
                mousePos = Camera.main.ScreenToWorldPoint(data.position);
                //print(Input.mousePosition);
                mousePos = new Vector3(mousePos.x, HeadStringArea.transform.position.y, mousePos.z);
                //print(mousePos);

                if (isBreakShot)
                {
                    if (headStringAreaSprite.bounds.Contains(mousePos))
                    {
                        print("Yes");
                        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        this.transform.position = new Vector3(mousePos.x, cueBallYPos + 1f, mousePos.z);
                        CueBallInHand.transform.position = this.transform.position;
                        this.GetComponent<Rigidbody>().useGravity = false;
                    }
                    else
                    {
                        if ((mousePos.x < headStringAreaSprite.bounds.min.x)) //&& (mousePos.z > headStringAreaSprite.bounds.min.z) && (mousePos.z < headStringAreaSprite.bounds.max.z))
                        {
                            print("Left");
                            if(mousePos.z >= 0)
                                this.transform.position = new Vector3(headStringAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Min(headStringAreaSprite.bounds.max.z, mousePos.z));
                            else
                                this.transform.position = new Vector3(headStringAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Max(headStringAreaSprite.bounds.min.z, mousePos.z));

                        }
                        else if ((mousePos.x > headStringAreaSprite.bounds.max.x)) //&& (mousePos.z > headStringAreaSprite.bounds.min.z) && (mousePos.z < headStringAreaSprite.bounds.max.z))
                        {
                            print("Right");
                            if (mousePos.z >= 0)
                                this.transform.position = new Vector3(headStringAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Min(headStringAreaSprite.bounds.max.z, mousePos.z));
                            else
                                this.transform.position = new Vector3(headStringAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Max(headStringAreaSprite.bounds.min.z, mousePos.z));
                            //this.transform.position = new Vector3(headStringAreaSprite.bounds.max.x, cueBallYPos + 1f, mousePos.z);
                        }
                        else if ((mousePos.z < headStringAreaSprite.bounds.min.z) && (mousePos.x > headStringAreaSprite.bounds.min.x) && (mousePos.x < headStringAreaSprite.bounds.max.x))
                        {
                            print("Down");
                            this.transform.position = new Vector3(mousePos.x, cueBallYPos + 1f, headStringAreaSprite.bounds.min.z);
                        }
                        else if ((mousePos.z > headStringAreaSprite.bounds.max.z) && (mousePos.x > headStringAreaSprite.bounds.min.x) && (mousePos.x < headStringAreaSprite.bounds.max.x))
                        {
                            print("Up");
                            this.transform.position = new Vector3(mousePos.x, cueBallYPos + 1f, headStringAreaSprite.bounds.max.z);
                        }
                        CueBallInHand.transform.position = this.transform.position;
                        this.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
                else
                {
                    if (floorAreaSprite.bounds.Contains(mousePos))
                    {
                        print("FloorYes");
                        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        tempPos = new Vector3(mousePos.x, cueBallYPos + 1f, mousePos.z);
                    }
                    else
                    {
                        if ((mousePos.x < floorAreaSprite.bounds.min.x)) //&& (mousePos.z > floorAreaSprite.bounds.min.z) && (mousePos.z < floorAreaSprite.bounds.max.z))
                        {
                            print("FloorLeft");
                            if (mousePos.z >= 0)
                                tempPos = new Vector3(floorAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Min(floorAreaSprite.bounds.max.z, mousePos.z));
                            else
                                tempPos = new Vector3(floorAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Max(floorAreaSprite.bounds.min.z, mousePos.z));
                        }
                        else if ((mousePos.x > floorAreaSprite.bounds.max.x)) //&& (mousePos.z > floorAreaSprite.bounds.min.z) && (mousePos.z < floorAreaSprite.bounds.max.z))
                        {
                            print("FloorRight");
                            if (mousePos.z >= 0)
                                tempPos = new Vector3(floorAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Min(floorAreaSprite.bounds.max.z, mousePos.z));
                            else
                                tempPos = new Vector3(floorAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Max(floorAreaSprite.bounds.min.z, mousePos.z));
                            //tempPos = new Vector3(floorAreaSprite.bounds.max.x, cueBallYPos + 1f, mousePos.z);
                        }
                        else if ((mousePos.z < floorAreaSprite.bounds.min.z) && (mousePos.x > floorAreaSprite.bounds.min.x) && (mousePos.x < floorAreaSprite.bounds.max.x))
                        {
                            print("FloorDown");
                            tempPos = new Vector3(mousePos.x, cueBallYPos + 1f, floorAreaSprite.bounds.min.z);
                        }
                        else if ((mousePos.z > floorAreaSprite.bounds.max.z) && (mousePos.x > floorAreaSprite.bounds.min.x) && (mousePos.x < floorAreaSprite.bounds.max.x))
                        {
                            print("FloorUp");
                            tempPos = new Vector3(mousePos.x, cueBallYPos + 1f, floorAreaSprite.bounds.max.z);
                        }
                    }

                    //check for obstacles before placing the moved cue ball
                    tempPos = new Vector3(tempPos.x, cueBallYPos, tempPos.z);

                    for (int i = 0; i < Balls.Length; i++)
                    {
                        if (Vector3.Distance(tempPos, ballPositions[i]) <= this.GetComponent<SphereCollider>().bounds.extents.x)
                            noObstaclesFlag = false;
                    }
                    if (noObstaclesFlag)
                    {
                        tempPos = new Vector3(tempPos.x, cueBallYPos + 1f, tempPos.z);
                        this.transform.position = tempPos;
                        CueBallInHand.transform.position = tempPos;
                        this.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            }
        }


        //Functions to be called from DirectCueStick.cs
        public void OnPointerDownFunction(PointerEventData pointerEventData)
        {
            Debug.Log("OnPointerDownFunction called.");
            if (isCueBallInHand)
            {
                Ray ray = Camera.main.ScreenPointToRay(pointerEventData.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    // whatever tag you are looking for on your game object
                    if (hit.collider.tag == "CueBall")
                    {
                        isMouseButtonPressed = true;
                        CueBallInHand.SetActive(true);
                        CueStickParent.SetActive(false);
                        if (isBreakShot)
                            HeadStringArea.SetActive(true);
                        for (int i = 0; i < Balls.Length; i++)
                        {
                            Balls[i].GetComponent<Rigidbody>().isKinematic = true;

                        }
                        this.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }
        }

        public void OnPointerUpFunction(PointerEventData pointerEventData)
        {

            Debug.Log(name + " Game Object upFunction!");
            if (isCueBallInHand)
            {
                isMouseButtonPressed = false;
                CueBallInHand.SetActive(false);
                HeadStringArea.SetActive(false);
                CueStickParent.SetActive(true);
                this.transform.position = new Vector3(this.transform.position.x, 0.3f, this.transform.position.z);
                CueStickParent.transform.position = new Vector3(this.GetComponent<Transform>().position.x, CueStickParent.transform.position.y,
                        this.GetComponent<Transform>().position.z);
                this.GetComponent<Rigidbody>().useGravity = true;
                StartCoroutine(TurnOffKinematic());
            }
        }

        public void OnDragFunction(PointerEventData data)
        {
            Debug.Log("OnDragFunction called.");
            if (isMouseButtonPressed)
            {
                noObstaclesFlag = true;
                mousePos = Camera.main.ScreenToWorldPoint(data.position);
                //print(Input.mousePosition);
                mousePos = new Vector3(mousePos.x, HeadStringArea.transform.position.y, mousePos.z);
                //print(mousePos);

                if (isBreakShot)
                {
                    if (headStringAreaSprite.bounds.Contains(mousePos))
                    {
                        print("Yes");
                        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        this.transform.position = new Vector3(mousePos.x, cueBallYPos + 1f, mousePos.z);
                        CueBallInHand.transform.position = this.transform.position;
                        this.GetComponent<Rigidbody>().useGravity = false;
                    }
                    else
                    {
                        if ((mousePos.x < headStringAreaSprite.bounds.min.x)) //&& (mousePos.z > headStringAreaSprite.bounds.min.z) && (mousePos.z < headStringAreaSprite.bounds.max.z))
                        {
                            print("Left");
                            if (mousePos.z >= 0)
                                this.transform.position = new Vector3(headStringAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Min(headStringAreaSprite.bounds.max.z, mousePos.z));
                            else
                                this.transform.position = new Vector3(headStringAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Max(headStringAreaSprite.bounds.min.z, mousePos.z));

                        }
                        else if ((mousePos.x > headStringAreaSprite.bounds.max.x)) //&& (mousePos.z > headStringAreaSprite.bounds.min.z) && (mousePos.z < headStringAreaSprite.bounds.max.z))
                        {
                            print("Right");
                            if (mousePos.z >= 0)
                                this.transform.position = new Vector3(headStringAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Min(headStringAreaSprite.bounds.max.z, mousePos.z));
                            else
                                this.transform.position = new Vector3(headStringAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Max(headStringAreaSprite.bounds.min.z, mousePos.z));
                            //this.transform.position = new Vector3(headStringAreaSprite.bounds.max.x, cueBallYPos + 1f, mousePos.z);
                        }
                        else if ((mousePos.z < headStringAreaSprite.bounds.min.z) && (mousePos.x > headStringAreaSprite.bounds.min.x) && (mousePos.x < headStringAreaSprite.bounds.max.x))
                        {
                            print("Down");
                            this.transform.position = new Vector3(mousePos.x, cueBallYPos + 1f, headStringAreaSprite.bounds.min.z);
                        }
                        else if ((mousePos.z > headStringAreaSprite.bounds.max.z) && (mousePos.x > headStringAreaSprite.bounds.min.x) && (mousePos.x < headStringAreaSprite.bounds.max.x))
                        {
                            print("Up");
                            this.transform.position = new Vector3(mousePos.x, cueBallYPos + 1f, headStringAreaSprite.bounds.max.z);
                        }
                        CueBallInHand.transform.position = this.transform.position;
                        this.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
                else
                {
                    if (floorAreaSprite.bounds.Contains(mousePos))
                    {
                        print("FloorYes");
                        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        tempPos = new Vector3(mousePos.x, cueBallYPos + 1f, mousePos.z);
                    }
                    else
                    {
                        if ((mousePos.x < floorAreaSprite.bounds.min.x)) //&& (mousePos.z > floorAreaSprite.bounds.min.z) && (mousePos.z < floorAreaSprite.bounds.max.z))
                        {
                            print("FloorLeft");
                            if (mousePos.z >= 0)
                                tempPos = new Vector3(floorAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Min(floorAreaSprite.bounds.max.z, mousePos.z));
                            else
                                tempPos = new Vector3(floorAreaSprite.bounds.min.x, cueBallYPos + 1f, Mathf.Max(floorAreaSprite.bounds.min.z, mousePos.z));
                        }
                        else if ((mousePos.x > floorAreaSprite.bounds.max.x)) //&& (mousePos.z > floorAreaSprite.bounds.min.z) && (mousePos.z < floorAreaSprite.bounds.max.z))
                        {
                            print("FloorRight");
                            if (mousePos.z >= 0)
                                tempPos = new Vector3(floorAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Min(floorAreaSprite.bounds.max.z, mousePos.z));
                            else
                                tempPos = new Vector3(floorAreaSprite.bounds.max.x, cueBallYPos + 1f, Mathf.Max(floorAreaSprite.bounds.min.z, mousePos.z));
                            //tempPos = new Vector3(floorAreaSprite.bounds.max.x, cueBallYPos + 1f, mousePos.z);
                        }
                        else if ((mousePos.z < floorAreaSprite.bounds.min.z) && (mousePos.x > floorAreaSprite.bounds.min.x) && (mousePos.x < floorAreaSprite.bounds.max.x))
                        {
                            print("FloorDown");
                            tempPos = new Vector3(mousePos.x, cueBallYPos + 1f, floorAreaSprite.bounds.min.z);
                        }
                        else if ((mousePos.z > floorAreaSprite.bounds.max.z) && (mousePos.x > floorAreaSprite.bounds.min.x) && (mousePos.x < floorAreaSprite.bounds.max.x))
                        {
                            print("FloorUp");
                            tempPos = new Vector3(mousePos.x, cueBallYPos + 1f, floorAreaSprite.bounds.max.z);
                        }
                    }

                    //check for obstacles before placing the moved cue ball
                    tempPos = new Vector3(tempPos.x, 0.3f, tempPos.z);

                    for (int i = 0; i < Balls.Length; i++)
                    {

                        print(Vector3.Distance(tempPos, ballPositions[i]));
                        if (Vector3.Distance(tempPos, ballPositions[i]) <= (this.GetComponent<SphereCollider>().bounds.extents.x * 2))
                            noObstaclesFlag = false;
                    }
                    if (noObstaclesFlag)
                    {
                        tempPos = new Vector3(tempPos.x, cueBallYPos + 1f, tempPos.z);
                        this.transform.position = tempPos;
                        CueBallInHand.transform.position = tempPos;
                        this.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //print("Collision object : " + collision.collider.name);
        }
        private void OnTriggerEnter(Collider other)
        {
            //print("Trigger object : " + other.name);
        }

        IEnumerator TurnOffKinematic()
        {
            yield return new WaitForSeconds(1f);
            this.GetComponent<Rigidbody>().isKinematic = false;
            for (int i = 0; i < Balls.Length; i++)
            {
                Balls[i].GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
