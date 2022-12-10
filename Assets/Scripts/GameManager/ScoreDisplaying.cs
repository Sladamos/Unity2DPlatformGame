using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreDisplaying : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    private void SetScore(int score)
    {
        scoreText.text = score.ToString("000");
    }
}
