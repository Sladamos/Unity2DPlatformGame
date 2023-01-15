using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MIIProjekt.Triggerers
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent triggerFunction;

        private Collider2D selfCollider;

        private void Awake()
        {
            selfCollider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                triggerFunction?.Invoke();
                selfCollider.enabled = false;
            }
        }
    }
}
