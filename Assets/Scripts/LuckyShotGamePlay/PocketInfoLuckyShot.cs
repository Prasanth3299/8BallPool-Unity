using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RevolutionGames.Game
{
    public class PocketInfoLuckyShot : MonoBehaviour
    {
        public BoardManagerLuckyShot boardManager;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            boardManager.PocketedBallDetails(collision);
        }
    }

}