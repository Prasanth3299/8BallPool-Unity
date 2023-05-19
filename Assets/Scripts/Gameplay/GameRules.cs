using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolutionGames.Game
{
    public class GameRules : MonoBehaviour
    {
        public GameManager gameManager;
        private void Start()
        {
            
        }
        public int ValidateCurrentStrike()
        {
            return 1; //Legal pocketing. Player gets another chance
            //return 0; //Game over - pocketed 8 ball legally or illegally. Set winner details
            //return 2; //Illegal pocketing. Opponent gets cue ball in hand
        }
    }
}

