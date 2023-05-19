using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RevolutionGames.Game
{
    public class PocketInfo : MonoBehaviour
    {
        public BoardManager boardManager;

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