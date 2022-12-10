using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;

    void Start()
    {
        GameManager.instance.SendMessage("SetScore", score);    
    }

    private void IncreaseScore(int value)
    {
        score += value;
        GameManager.instance.SendMessage("SetScore", score);
    }
}
