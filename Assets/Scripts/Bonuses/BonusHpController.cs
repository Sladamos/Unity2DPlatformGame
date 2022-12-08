using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHpController : MonoBehaviour
{
    [SerializeField]
    private int numberOfLives;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("IncreaseLives", numberOfLives);
            this.gameObject.SetActive(false);
        }
    }
}
