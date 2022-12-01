using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MIIProjekt.Extensions;

namespace MIIProjekt
{
    public class FinishTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                collider.gameObject
                    .GetComponent<FoxController>()
                    .VerifyNotNull()
                    .Finish();
            }
        }
    }
}
