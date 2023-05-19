using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RevolutionGames.Dummy
{
    public class CueStick : MonoBehaviour
    {
        public LineRenderer line;
        public Rigidbody cueBall;
        public Slider forceSlider;
        private float forceFromCueStick = 1500;
        private float constantForceFromCueStick = 1500;
        Ray ray;
        RaycastHit hit;
        bool isMouseDown = false;
        Vector2 mouseStartPosition = Vector2.zero;
        float tempY = 0;
        Transform colliderTransform = null;
        bool isStrikeCompleted = true;
        float prevSliderValue = 0;
        Vector3 startCueStickPosition;
        public float torqueXValue;
        public float torqueZValue;
        public GameObject cueStickSpotScreen;
        public ForceUpdater forceUpdater;
        private void OnCollisionEnter(Collision collision)
        {
            //print("Collision object : " + collision.collider.name);
            if (collision.transform.tag == "CueBall")
            {
                Vector3 direction = collision.contacts[0].point - transform.position;
                direction.Normalize();
                collision.rigidbody.AddForce(direction * forceFromCueStick);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            //print("Trigger object : " + other.name);
            if (other.tag == "CueBall")
            {
                other.transform.GetComponent<Rigidbody>().AddRelativeForce(-transform.up * forceFromCueStick);
            }
        }

        private void Start()
        {
            //prevSliderValue = forceSlider.value;
            prevSliderValue = 0;
            startCueStickPosition = transform.localPosition;
            //line.SetPosition(0, cueBall.GetComponent<Transform>().localPosition);
            //line.SetPosition(1, cueBall.GetComponent<Transform>().localPosition * -2);

        }
        void Update()
        {

            /*if(Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name == "CueStick")
                    {
                        isMouseDown = true;
                        tempY = hit.collider.transform.parent.localEulerAngles.y;
                        colliderTransform = hit.collider.transform.parent;
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isMouseDown = false;
            }
            if(isMouseDown)
            {
                if (colliderTransform != null)
                {
                    colliderTransform.localEulerAngles = new Vector3(0, tempY, 0);
                    tempY+=0.2f;
                }
            }*/

            if (isStrikeCompleted)
            {
                ResetCueStick();
            }
        }

        public void UpdateForceValue(float forceValue)
        {
            /*forceFromCueStick = constantForceFromCueStick*forceSlider.value;

            if (prevSliderValue <= forceSlider.value)
            {
                transform.localPosition += Vector3.right * -5 *(forceSlider.value - prevSliderValue);
                prevSliderValue = forceSlider.value;
            }
            else
            {
                transform.localPosition += Vector3.right * 5 * (prevSliderValue - forceSlider.value);
                prevSliderValue = forceSlider.value;
            }*/

            forceFromCueStick = constantForceFromCueStick * forceValue;

            if (prevSliderValue <= forceValue)
            {
                transform.localPosition += Vector3.right * -5 * (forceValue - prevSliderValue);
                prevSliderValue = forceValue;
            }
            else
            {
                transform.localPosition += Vector3.right * 5 * (prevSliderValue - forceValue);
                prevSliderValue = forceValue;
            }
        }

        public void StrikeTheBall()
        {
            Vector3 newDirection = transform.forward;
            Vector3 newPosition = cueBall.GetComponent<Transform>().position;
            newPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z + 0.5f);
            //newDirection = new Vector3(newDirection.x+0.5f, newDirection.y, newDirection.z);

            cueBall.AddForce(newDirection * forceFromCueStick);
            //cueBall.velocity = newDirection * 100;
            cueBall.AddTorque(new Vector3(torqueXValue, 0, torqueZValue), ForceMode.Force);
            //cueBall.AddTorque(new Vector3(0, 0, -2000), ForceMode.Force);

            //cueBall.AddForceAtPosition(newDirection * forceFromCueStick, newPosition);
            transform.localPosition = startCueStickPosition;
            StartCoroutine(ResetGameFlag());
        }

        IEnumerator ResetGameFlag()
        {
            yield return new WaitForSeconds(0.2f);
            isStrikeCompleted = true;
        }
        public void ResetCueStick()
        {
            if (cueBall.velocity.magnitude <= 0.01f)
            {
                transform.parent.position = new Vector3(cueBall.GetComponent<Transform>().position.x, transform.parent.position.y,
                    cueBall.GetComponent<Transform>().position.z);
                isStrikeCompleted = false;
                forceSlider.value = 0;
                forceUpdater.ResetTheForceSliderValues();
                forceFromCueStick = constantForceFromCueStick;
                transform.localPosition = startCueStickPosition;
            }
        }

        public void CloseCueStickSpot()
        {
            cueStickSpotScreen.SetActive(false);
        }
        public void OpenCloseCueStickSpot()
        {
            cueStickSpotScreen.SetActive(true);
        }
    }
}
