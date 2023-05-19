
using RevolutionGames.Game;
using RevolutionGames.Hud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RevolutionGames.Game
{
    public class CueBallCollisionHandlerQuickFireMode : MonoBehaviour
    {
        public HudManagerQuickFireMode hudManager;

        private BoardManagerQuickFireMode boardManager;
        private CueStickManagerQuickFireMode cueStickManager;
        private GameObject[] balls;
        private RaycastHit hit;
        private Rigidbody rb;
        private Vector3 velocityBeforePhysicsUpdate = Vector3.zero;
        private float spinValue = 450f;
        private float initialSpinValue = 450f;

        public Vector3 VelocityBeforePhysicsUpdate { get => velocityBeforePhysicsUpdate; set => velocityBeforePhysicsUpdate = value; }


        // Start is called before the first frame update
        void Start()
        {
            boardManager = hudManager.gameManager.boardManager;
            cueStickManager = hudManager.gameManager.cueStickManager;
            balls = hudManager.gameManager.balls;
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (this.rb.velocity.magnitude >= 0.05f)
            {
                transform.RotateAround(transform.position,
                    Vector3.Cross(this.transform.position, this.rb.velocity.normalized),
                    this.rb.velocity.magnitude * spinValue * Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            velocityBeforePhysicsUpdate = rb.velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "BoardSides")
            {
                /*Vector3 firstVel = this.GetComponent<Rigidbody>().velocity;
                print("Entered" + firstVel);
                //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                this.GetComponent<Rigidbody>().velocity = Vector3.Reflect(firstVel, collision.contacts[0].normal);
                this.GetComponent<Rigidbody>().velocity += collision.contacts[0].normal * 8f;*/
                /*this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                this.GetComponent<Rigidbody>().AddForce(
                    (direction.normalized * speed), ForceMode.Acceleration);

                //this.GetComponent<Rigidbody>().velocity = direction * Mathf.Max(speed, 10f);*/

                if (cueStickManager.FirstHitDoneCueBall)
                {
                    //print("Entered");
                    cueStickManager.FirstHitDoneCueBall = false;
                    //print(cueStickManager.TorqueZValue + ":" + cueStickManager.TorqueXValue);

                    if ((cueStickManager.TorqueZValue == -1 && cueStickManager.TorqueXValue == 1) ||
                        (cueStickManager.TorqueZValue == -1 && cueStickManager.TorqueXValue == -1))//left
                    {
                        this.rb.velocity = Quaternion.AngleAxis(cueStickManager.CueSpinAngle / 6, cueStickManager.cueStick.up) * this.rb.velocity;

                    }
                    else if ((cueStickManager.TorqueZValue == 1 && cueStickManager.TorqueXValue == 1) ||
                        (cueStickManager.TorqueZValue == 1 && cueStickManager.TorqueXValue == -1))//right
                    {
                        this.rb.velocity = Quaternion.AngleAxis(cueStickManager.CueSpinAngle / 6, cueStickManager.cueStick.up) * this.rb.velocity;
                    }

                    if ((cueStickManager.TorqueZValue == -1 && cueStickManager.TorqueXValue == 1) ||
                            (cueStickManager.TorqueZValue == 1 && cueStickManager.TorqueXValue == 1))//Top
                    {
                        //print("Top");
                        this.rb.velocity = this.rb.velocity * 1.15f;
                    }
                    else if ((cueStickManager.TorqueZValue == -1 && cueStickManager.TorqueXValue == -1) ||
                            (cueStickManager.TorqueZValue == 1 && cueStickManager.TorqueXValue == -1))//Back
                    {
                        //print("Back");
                        this.rb.velocity = this.rb.velocity * 0.5f;

                    }
                }

            }
            else if (collision.collider.tag == "Ball")
            {
                if ((cueStickManager.TorqueZValue == -1 && cueStickManager.TorqueXValue == 1) ||
                        (cueStickManager.TorqueZValue == -1 && cueStickManager.TorqueXValue == -1))
                {
                    this.rb.velocity = Quaternion.AngleAxis(cueStickManager.CueSpinAngle / 6, cueStickManager.cueStick.up) * this.rb.velocity;

                }
                else if ((cueStickManager.TorqueZValue == 1 && cueStickManager.TorqueXValue == 1) ||
                    (cueStickManager.TorqueZValue == 1 && cueStickManager.TorqueXValue == -1))
                {
                    this.rb.velocity = Quaternion.AngleAxis(cueStickManager.CueSpinAngle / 6, cueStickManager.cueStick.up) * this.rb.velocity;
                }
                if ((cueStickManager.TorqueZValue == -1 && cueStickManager.TorqueXValue == 1) ||
                            (cueStickManager.TorqueZValue == 1 && cueStickManager.TorqueXValue == 1))//Top
                {
                    //print("Top");
                    this.rb.velocity = this.rb.velocity * 1.2f;
                }
                else if ((cueStickManager.TorqueZValue == -1 && cueStickManager.TorqueXValue == -1) ||
                        (cueStickManager.TorqueZValue == 1 && cueStickManager.TorqueXValue == -1))//Back
                {
                    //print("Back");
                    this.rb.velocity = this.rb.velocity * 0.5f;

                }
            }
            //else
            //{
            //    this.GetComponent<Rigidbody>().isKinematic = false;
            //    this.GetComponent<Rigidbody>().AddForce((cueStickManager.CueBallFinalPos - cueStickManager.CueBallHitPosition).normalized * (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 75f), ForceMode.Acceleration);

            //}

            if (hudManager.IsCueBallPressed && (collision.collider.tag == "Ball" || collision.collider.tag == "BoardSides"))
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                if (collision.collider.tag == "Ball")
                {
                    collision.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    collision.collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
            }
            if (collision.collider.tag == "Ball")
            {
                boardManager.AddHitBallsCount(collision.collider.gameObject);
            }
        }

        private void OnCollisionStay(Collision collision)
        {

            //print(hudManager.IsCueBallPressed);
            if (hudManager.IsCueBallPressed && (collision.collider.tag == "Ball" || collision.collider.tag == "BoardSides"))
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                if (collision.collider.tag == "Ball")
                {
                    collision.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    collision.collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }

            }
            if (collision.collider.tag == "Ball")
            {
                boardManager.AddHitBallsCount(collision.collider.gameObject);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            //this.GetComponent<Rigidbody>().isKinematic = true;
            /*this.GetComponent<SphereCollider>().isTrigger = false;
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i].GetComponent<SphereCollider>().isTrigger = false;
            }*/
            //print("Exit" + hudManager.IsCueBallPressed);
            if (hudManager.IsCueBallPressed && (collision.collider.tag == "Ball" || collision.collider.tag == "BoardSides"))
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                if (collision.collider.tag == "Ball")
                {
                    collision.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    collision.collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
            }
        }

        //Triggers
        public void OnTriggerEnter(Collider collider)
        {
            //print("CueBall" + collider.name);

            if (collider.gameObject.tag == "BoardSides")
            {
                //Vector3 reflectPos = Vector3.Reflect(cueStickManager.hit.point, cueStickManager.hit.normal);
                //print(reflectPos);
                //this.GetComponent<SphereCollider>().isTrigger = false;
                //this.GetComponent<Rigidbody>().AddForce(reflectPos, ForceMode.Acceleration);
                /*RaycastHit hit;
                Physics.Raycast(transform.position, transform.forward, out hit);
                float speed = this.GetComponent<Rigidbody>().velocity.magnitude;
                Vector3 direction = Vector3.Reflect(this.GetComponent<Rigidbody>().velocity.normalized, hit.normal);

                Debug.Log("Reflect: " + direction);
                this.GetComponent<Rigidbody>().velocity = direction * Mathf.Max(speed, 10f);*/
            }
        }

        public void UpdateSpinValue(int spin)
        {
            spinValue = initialSpinValue + (10 * spin);
            //print(spinValue);
        }
    }
}

