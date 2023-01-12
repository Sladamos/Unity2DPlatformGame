using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt
{
    public class SpikesCollider : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                collider.SendMessage("CollidedWithSpike");
            }
        }
    }

}
