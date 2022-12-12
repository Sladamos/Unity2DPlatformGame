using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;

    private void IncreaseScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }
}
