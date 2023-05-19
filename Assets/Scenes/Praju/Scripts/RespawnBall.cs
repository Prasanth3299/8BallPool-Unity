using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolutionGames.Dummy
{
    public class RespawnBall : MonoBehaviour
    {
        public Transform pottedTransform;
        public Transform cueBallObject;
        public CueStick cueStick;
        bool isCueBallMoving = false;
        // Start is called before the first frame update
        private void OnCollisionEnter(Collision collision)
        {
            collision.rigidbody.velocity = Vector3.zero;
            collision.transform.position = pottedTransform.position;
            if (collision.transform.tag == "CueBall")
            {
                StartCoroutine(SetFlagForResetting());
            }
        }
        private void Update()
        {
            if (isCueBallMoving)
            {
                if (cueBallObject.GetComponent<Rigidbody>().velocity.magnitude <= 0.01f)
                {
                    StartCoroutine(ResetCueball());
                }
            }
        }

        IEnumerator SetFlagForResetting()
        {
            yield return new WaitForSeconds(2);
            isCueBallMoving = true;
        }
        IEnumerator ResetCueball()
        {
            yield return new WaitForSeconds(2);

            cueBallObject.localPosition = new Vector3(-0.4f, 0.5f, 0);
            cueStick.ResetCueStick();
            isCueBallMoving = false;
        }
    }
}
