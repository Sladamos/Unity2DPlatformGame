using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHpController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("IncreaseLives", 1);
            this.gameObject.SetActive(false);
        }
    }
}
