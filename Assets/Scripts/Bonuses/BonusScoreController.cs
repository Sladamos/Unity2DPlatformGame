using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScoreController : MonoBehaviour
{
    [SerializeField]
    private int scoreValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("IncreaseScore", scoreValue);
            this.gameObject.SetActive(false);
        }
    }
}
