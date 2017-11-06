using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;


        Text text;


        void Awake()
        {
            text = GetComponent<Text>();                // Looks for text component and sets the score to 0.
            score = 0;
        }


        void Update()
        {
            text.text = "Score: " + score;             // Updates score text UI.
        }
    }
}
