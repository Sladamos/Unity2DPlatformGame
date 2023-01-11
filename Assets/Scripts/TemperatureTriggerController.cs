using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MIIProjekt
{
    public class TemperatureTriggerController : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent triggerFunction;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                triggerFunction?.Invoke();
                this.gameObject.SetActive(false);
            }
        }
    }
}
