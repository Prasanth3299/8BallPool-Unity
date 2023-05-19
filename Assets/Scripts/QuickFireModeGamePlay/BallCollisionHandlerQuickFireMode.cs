
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolutionGames.Game
{
    public class BallCollisionHandlerQuickFireMode : MonoBehaviour
    {
        public GameManagerQuickFire gameManager;

        private float torqueValue = 500;
        private Vector3 target;
        private bool isTargetSet = false;
        private float pottedRotationValue = 400;
        private Vector3 collisionTarget;
        private RaycastHit hit;
        private Vector3 normal;
        private CueStickManagerQuickFireMode cueStickManager;
        private GameObject[] balls;
        private Rigidbody rb;
        private Vector3 velocityBeforePhysicsUpdate = Vector3.zero;
        private int[] ballInPos;
        private bool firstHitDone = false;
        //private Vector3 velocityForSpin = Vector3.zero;


        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Rigidbody>().ResetCenterOfMass();
            cueStickManager = gameManager.cueStickManager;
            balls = gameManager.balls;
            rb = GetComponent<Rigidbody>();
        }
        bool isMoving = false;

        public Vector3 VelocityBeforePhysicsUpdate { get => velocityBeforePhysicsUpdate; set => velocityBeforePhysicsUpdate = value; }

        // Update is called once per frame
        void Update()
        {
            if (isTargetSet)
            {
                if (Vector3.Distance(transform.position, target) > 0.1f)
                {
                    transform.position = Vector3.Lerp(transform.position, target, 0.9f * Time.deltaTime);
                    //transform.RotateAround(transform.position, -Vector3.right, pottedRotationValue * Time.deltaTime);
                    transform.RotateAround(
                    transform.position,
                    -Vector3.right,
                    Vector3.Distance(transform.position, target) * pottedRotationValue * Time.deltaTime);
                }
                else
                {
                    isTargetSet = false;
                }
            }

            if (this.rb.velocity.magnitude >= 0.05f)
            {
                //Debug.DrawRay(transform.position, this.rb.velocity, Color.red);
                transform.RotateAround(
                    transform.position,
                    Vector3.Cross(this.transform.position, this.rb.velocity.normalized),
                    this.rb.velocity.magnitude * 450f * Time.deltaTime);

                //TRY
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(this.rb.velocity, transform.up), Time.deltaTime * 400f);
            }
        }

        private void FixedUpdate()
        {
            //print(gameManager.CurrentGameState);
            velocityBeforePhysicsUpdate = rb.velocity;
        }

        public void OnCollisionEnter(Collision collision)
        {
            ballInPos = gameManager.BallInPos;
            if (collision.collider.tag == "BoardSides")
            {

                firstHitDone = true;
                //this.rb.angularVelocity = Vector3.zero;
                /*Vector3 firstVel = this.GetComponent<Rigidbody>().velocity;
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;*/
                /*Physics.Raycast(transform.position, transform.forward, out hit);
                float speed = firstVel.magnitude;
                Vector3 direction = Vector3.Reflect(firstVel.normalized, hit.normal);*/
                //this.GetComponent<Rigidbody>().velocity = direction * Mathf.Max(speed, 10f);
                // calculate with Vector3.Reflect
                ////this.GetComponent<Rigidbody>().velocity = Vector3.Reflect(firstVel, collision.contacts[0].normal);

                // bumper effect to speed up ball
                ////this.GetComponent<Rigidbody>().velocity += collision.contacts[0].normal * 6.5f;
            }
            else if (collision.collider.tag == "Floor")
            {
                //this.rb.angularVelocity = Vector3.zero;

            }
            else if (collision.collider.tag == "Pocket" || collision.collider.tag == "PocketSides")
            {

            }
            else if ((gameManager.CurrentGameState == GameManagerQuickFire.GameState.isItCueballBreak || gameManager.CurrentGameState == GameManagerQuickFire.GameState.isItCueballBreakDone))
            {
                /*if(cueStickManager.ForceFromCueStick <= 10000f)
                {
                    this.rb.velocity = new Vector3(0.008f, 0.008f, 0.008f);
                    collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.008f, 0.008f, 0.008f);
                    this.rb.angularVelocity = Vector3.zero;
                    collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    print(ballInPos[0]);
                    balls[ballInPos[0]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, -45, 0) * Vector3.right * 50f,
                        ForceMode.Force);
                    balls[ballInPos[4]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, 45, 0) * Vector3.right * 50f,
                        ForceMode.Force);
                    cueStickManager.FirstHitDoneCueBall = false;
                }
                else if(cueStickManager.ForceFromCueStick > 10000f)
                {
                    rb.AddForce(Random.Range(500, 750) * transform.forward, ForceMode.Force);
                    cueStickManager.FirstHitDoneCueBall = false;
                }*/

                Vector3 firstVel = velocityBeforePhysicsUpdate;
                Vector3 secondVel;
                if (collision.collider.tag == "CueBall")
                {
                    secondVel = collision.gameObject.GetComponent<CueBallCollisionHandlerQuickFireMode>().VelocityBeforePhysicsUpdate;
                }
                else
                {
                    secondVel = collision.gameObject.GetComponent<BallCollisionHandlerQuickFireMode>().VelocityBeforePhysicsUpdate;
                }
                if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > secondVel.magnitude)
                {
                    secondVel = collision.gameObject.GetComponent<Rigidbody>().velocity;
                }
                Vector3 colNormal = collision.contacts[0].normal;

                if (cueStickManager.FirstHitDoneCueBall)
                {
                    if (collision.gameObject.tag == "CueBall")
                    {
                        cueStickManager.FirstHitDoneCueBall = false;
                        cueStickManager.FirstHitDoneBall = false;
                        this.rb.velocity = new Vector3(0.008f, 0.008f, 0.008f);
                        collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.008f, 0.008f, 0.008f);
                        //this.rb.angularVelocity = Vector3.zero;
                        //collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                        //print(secondVel.magnitude);
                        float vel1 = secondVel.magnitude * 120f; //300f //600 //500 //350f
                        float vel = secondVel.magnitude * 20f; //100
                        vel1 = Mathf.Max(vel1, 0.5f);
                        vel = Mathf.Max(vel, 0.5f);

                        collision.gameObject.GetComponent<Rigidbody>().AddForce(
                            (cueStickManager.CueBallFinalPos - cueStickManager.CueBallHitPosition).normalized * vel,
                            ForceMode.Force);
                        this.rb.AddForce((cueStickManager.BallFinalPos - this.transform.position).normalized * vel1, ForceMode.Force);


                        balls[ballInPos[0]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(-40, -60), 0) * Vector3.right * secondVel.magnitude * Random.Range(50, 80f),
                                ForceMode.Force);
                        balls[ballInPos[1]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(-30, -50), 0) * Vector3.right * secondVel.magnitude * Random.Range(5, 40f),
                            ForceMode.Force);
                        balls[ballInPos[2]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(20, 30), 0) * Vector3.right * secondVel.magnitude * Random.Range(5, 40f),
                            ForceMode.Force);
                        balls[ballInPos[3]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(50, 60), 0) * Vector3.right * secondVel.magnitude * Random.Range(5, 40f),
                            ForceMode.Force);
                        balls[ballInPos[4]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(40, 60), 0) * Vector3.right * secondVel.magnitude * Random.Range(45, 62f),
                            ForceMode.Force);
                        balls[ballInPos[5]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(90, 120), 0) * Vector3.right * secondVel.magnitude * Random.Range(5, 45f),
                            ForceMode.Force);
                        balls[ballInPos[9]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(-70, -100), 0) * Vector3.right * secondVel.magnitude * Random.Range(3, 40f),
                            ForceMode.Force);
                        balls[ballInPos[11]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(-80, -85), 0) * Vector3.right * secondVel.magnitude * Random.Range(5, 25f),
                            ForceMode.Force);

                        if (cueStickManager.ForceFromCueStick >= 8000f)
                        {
                            balls[ballInPos[8]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(-150, -160), 0) * Vector3.right * secondVel.magnitude * Random.Range(200f, 300f),
                                ForceMode.Force);
                            balls[ballInPos[12]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(-150, -160), 0) * Vector3.right * secondVel.magnitude * Random.Range(250f, 300f),
                                ForceMode.Force);
                            balls[ballInPos[13]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(-180, -190), 0) * Vector3.right * secondVel.magnitude * Random.Range(220f, 280f),
                                ForceMode.Force);
                            balls[ballInPos[14]].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, Random.Range(100, 135), 0) * Vector3.right * secondVel.magnitude * Random.Range(270f, 280f),
                                ForceMode.Force);
                        }
                    }
                }
                else
                {
                    if (collision.collider.tag == "Ball" || collision.collider.tag == "CueBall")
                    {
                        if (!firstHitDone)
                        {
                            if (firstVel.magnitude < secondVel.magnitude)
                            {
                                firstHitDone = true;

                                this.rb.velocity = new Vector3(0.008f, 0.008f, 0.008f);
                                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.008f, 0.008f, 0.008f);
                                //this.rb.angularVelocity = Vector3.zero;
                                //collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                                Vector3 dir = secondVel.normalized;
                                float vel1 = secondVel.magnitude * 180f; //75 200 500 //200f
                                float vel = secondVel.magnitude * 25f; //25f
                                if (cueStickManager.ForceFromCueStick >= 8000f)
                                {
                                    this.rb.AddForce(colNormal.normalized * vel1, ForceMode.Force);
                                    collision.gameObject.GetComponent<Rigidbody>().AddForce(
                                        (Quaternion.AngleAxis(-90, Vector3.up) * (-1 * colNormal)).normalized * (vel),
                                        ForceMode.Force);
                                }
                            }
                        }
                        else
                        {
                            if (firstVel.magnitude < secondVel.magnitude)
                            {

                                this.rb.velocity = new Vector3(0.008f, 0.008f, 0.008f);
                                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.008f, 0.008f, 0.008f);
                                //this.rb.angularVelocity = Vector3.zero;
                                //collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                                Vector3 dir = secondVel.normalized;
                                float vel1 = secondVel.magnitude * 180f; //75 200 500 //200f
                                float vel = secondVel.magnitude * 50f; //25f
                                //print("ElseaEn1" + vel1 + ":" + vel);
                                //this.GetComponent<Rigidbody>().isKinematic = false;
                                this.rb.AddForce(colNormal.normalized * vel1, ForceMode.Force);
                                collision.gameObject.GetComponent<Rigidbody>().AddForce(
                                    (Quaternion.AngleAxis(-90, Vector3.up) * (-1 * colNormal)).normalized * (vel),
                                    ForceMode.Force);
                            }
                        }
                    }
                    else
                    {
                        firstHitDone = true;
                        //this.rb.angularVelocity = Vector3.zero;
                        //collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    }
                }
            }
            /*else if ((gameManager.CurrentGameState == GameManager.GameState.isItCueballBreak ||
                gameManager.CurrentGameState == GameManager.GameState.isItCueballBreakDone) && cueStickManager.ForceFromCueStick >= 15000f)
            {
                rb.AddForce(Random.Range(500, 750) * transform.forward, ForceMode.Force);
                cueStickManager.FirstHitDoneCueBall = false;
            }
            else if((gameManager.CurrentGameState == GameManager.GameState.isItCueballBreak || 
                gameManager.CurrentGameState == GameManager.GameState.isItCueballBreakDone) && cueStickManager.ForceFromCueStick >= 10000f)
            {
                rb.AddForce(Random.Range(300, 400) * transform.forward, ForceMode.Force);
                cueStickManager.FirstHitDoneCueBall = false;
            }
            else if ((gameManager.CurrentGameState == GameManager.GameState.isItCueballBreak ||
                gameManager.CurrentGameState == GameManager.GameState.isItCueballBreakDone) && cueStickManager.ForceFromCueStick >= 6000f)
            {
                rb.AddForce(Random.Range(100, 300) * transform.forward, ForceMode.Force);
                cueStickManager.FirstHitDoneCueBall = false;
            }*/
            else
            {
                Vector3 firstVel = velocityBeforePhysicsUpdate;
                Vector3 secondVel;
                if (collision.collider.tag == "CueBall")
                {
                    secondVel = collision.gameObject.GetComponent<CueBallCollisionHandlerQuickFireMode>().VelocityBeforePhysicsUpdate;
                }
                else
                {
                    secondVel = collision.gameObject.GetComponent<BallCollisionHandlerQuickFireMode>().VelocityBeforePhysicsUpdate;
                }
                //print(this.gameObject.GetComponent<Rigidbody>().velocity.magnitude + ":update " + firstVel.magnitude);
                //print(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude + ":update " + secondVel.magnitude);
                if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > secondVel.magnitude)
                {
                    secondVel = collision.gameObject.GetComponent<Rigidbody>().velocity;
                }
                Vector3 colNormal = collision.contacts[0].normal;

                if (cueStickManager.FirstHitDoneCueBall)
                {
                    cueStickManager.FirstHitDoneBall = false;
                    this.rb.velocity = new Vector3(0.008f, 0.008f, 0.008f);
                    collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.008f, 0.008f, 0.008f);
                    //this.rb.angularVelocity = Vector3.zero;
                    //collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    //print(secondVel.magnitude);
                    float vel1 = secondVel.magnitude * 300f; //300f //600 //500 //350f
                    float vel = secondVel.magnitude * 100f; //100
                    vel1 = Mathf.Max(vel1, 0.5f);
                    vel = Mathf.Max(vel, 0.5f);
                   // print("FirstHit" + this.gameObject.name + "Collider" + collision.collider.name);
                    //print(vel1 + ":" + vel);
                    cueStickManager.FirstHitDoneCueBall = false;
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(
                        (cueStickManager.CueBallFinalPos - cueStickManager.CueBallHitPosition).normalized * vel,
                        ForceMode.Force);
                    this.rb.AddForce((cueStickManager.BallFinalPos - this.transform.position).normalized * vel1, ForceMode.Force);

                    //this.GetComponent<Rigidbody>().angularVelocity = Vector3.Cross(this.transform.position, (cueStickManager.BallFinalPos - this.transform.position).normalized * vel1) * (cueStickManager.BallFinalPos - this.transform.position).magnitude * Mathf.Deg2Rad * 500f;
                    //this.GetComponent<Rigidbody>().angularVelocity = Vector3.Cross(this.transform.position, (cueStickManager.BallFinalPos - this.transform.position).normalized) * (cueStickManager.BallFinalPos - this.transform.position).magnitude * Mathf.Deg2Rad * 500f;
                    //this.transform.Rotate((new Vector3(0.0f, 0.0f, -1000.0f) * Time.deltaTime), Space.Self);
                    //this.GetComponent<Rigidbody>().AddTorque((cueStickManager.BallFinalPos - this.transform.position).normalized * vel1, ForceMode.Acceleration);
                }
                else
                {
                    if (collision.collider.tag == "Ball" || collision.collider.tag == "CueBall")
                    {
                       // print("En0" + this.name + collision.gameObject.name + " " + firstVel.magnitude + " : " + secondVel.magnitude);
                        if (firstVel.magnitude < secondVel.magnitude)
                        {

                            //this.rb.velocity = new Vector3(0.008f, 0.008f, 0.008f);
                            //collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.008f, 0.008f, 0.008f);
                            //this.rb.angularVelocity = Vector3.zero;
                            //collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                            float vel1 = secondVel.magnitude * 250f; //75 200 500 //200f
                            float vel = secondVel.magnitude * 100f; //25f

                            //if(vel <= 20f)
                            //{
                            //    //collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.008f, 0.008f, 0.008f);
                            //    vel = 1f;
                            //}
                            vel = 2.5f;
                            vel1 = 2.5f;
                           // print("En1" + vel1 + ":" + vel);
                            vel1 = Mathf.Min(vel1, 3500f);
                            vel = Mathf.Min(vel, 3500f);
                            //this.GetComponent<Rigidbody>().isKinematic = false;
                            this.rb.AddForce(colNormal.normalized * vel1, ForceMode.Force);
                            collision.gameObject.GetComponent<Rigidbody>().AddForce(
                                (Quaternion.AngleAxis(-90, Vector3.up) * (-1 * colNormal)).normalized * (vel),
                                ForceMode.Force);
                        }
                    }
                }
            }
            /*if (collision.collider.tag == "Ball")
            {
                float f = Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity) * collision.gameObject.GetComponent<Rigidbody>().mass;
                //this.GetComponent<Rigidbody>().AddTorque(0, collision.gameObject.GetComponent<Rigidbody>().velocity.x * 5f, 0, ForceMode.Force);
                //collision.collider.GetComponent<Rigidbody>().AddTorque(5000, 0, 5000, ForceMode.Acceleration);
            }*/

            //if (gameManager.CurrentGameState != GameManager.GameState.isGameOn &&
            //    collision.relativeVelocity.magnitude > 0.2f)
            //{
            //    //print(collision.gameObject.name);
            //    if (collision.rigidbody != null)
            //    {
            //        //GetComponent<Rigidbody>().AddTorque(collision.rigidbody.velocity * torqueValue, ForceMode.Acceleration);
            //    }
            //}
        }

        public void OnCollisionStay(Collision collision)
        {
        }

        public void StartTrayMovementForPottedBall(Vector3 targetPoint)
        {
            target = targetPoint;
            isTargetSet = true;
        }

        //Triggers
        public void OnTriggerEnter(Collider collision)
        {
            /*print("Ball" + collision.tag);
            Vector3 col = collision.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
            Vector3 normal = transform.position - col;
            //var col = (transform.position - collision.transform.position);
            print(col + " " + normal);
            //Debug.DrawRay(transform.position, transform.position + (Vector3.right * 15f), Color.white);
            guideLine1.SetPosition(0, transform.position);
            guideLine1.SetPosition(1, normal);
            //print(collision.GetComponent<Collider>().transform.position);
            //var normal = transform.position - collision.GetComponent<Collider>().transform.position;
            //print(transform.position - collision.transform.position);
            //Vector3 normal = col + ((transform.position - collision.transform.position) * 5f);
            //collisionTarget = new Vector3(normal.x, transform.position.y, normal.z) ;
            //collisionTarget = new Vector3(collisionTarget.x, transform.position.y, collisionTarget.z);
            if (collision.gameObject.tag == "Ball")
            {
                print(collision.gameObject.GetComponent<Rigidbody>().velocity);
                transform.GetComponent<Rigidbody>().AddForce(normal * 500, ForceMode.Acceleration);
            }
            else if(collision.gameObject.tag == "BoardSides")
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.forward, out hit);
                var speed = this.GetComponent<Rigidbody>().velocity.magnitude;
                var direction = Vector3.Reflect(this.GetComponent<Rigidbody>().velocity.normalized, hit.normal);

                Debug.Log("Reflect " + direction);
                this.GetComponent<Rigidbody>().velocity = direction * Mathf.Max(speed, 1f);
            }*/
            //print(collisionTarget);
            //collidedFlag = true;
            /*Physics.Raycast(collision.gameObject.transform.position, this.transform.position - collision.gameObject.transform.position, out hit, Screen.width);
            Debug.DrawRay(collision.gameObject.transform.position, this.transform.position + (this.transform.right * 15f) - collision.gameObject.transform.position);
            Vector3 ballFinalPos = hit.point + ((this.transform.position - hit.point).normalized * 15f);
            ballFinalPos = new Vector3(ballFinalPos.x, this.transform.position.y, ballFinalPos.z);
            Vector3 cueBallFinalPos = hit.point + (Quaternion.AngleAxis(-90, Vector3.up) * (-1 * ballFinalPos) * this.GetComponent<SphereCollider>().bounds.extents.x);
            guideLine1.SetPosition(0, this.transform.position);
            guideLine1.SetPosition(1, ballFinalPos);*/

        }

        public void OnCollisionExit(Collision collision)
        {

            //this.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
}


