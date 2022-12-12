using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnAllKeysCollected()
    {
        Debug.Log("All keys collected. Opening door...");
        gameObject.SetActive(false);
    }
}
