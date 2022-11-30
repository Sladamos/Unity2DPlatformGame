using System;
using UnityEngine;
using MIIProjekt.Extensions;

namespace MIIProjekt
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class Key : MonoBehaviour
    {
        public event Action KeyCollected;

        [SerializeField]
        private string keyIdentifier;

        [SerializeField]
        private bool active;

        private SpriteRenderer spriteRenderer;
        private Collider2D colliderComponent;

        /// <summary>
        /// Sets the visibility and collision for an object.
        /// </summary>x`
        /// <param name="value"></param>
        public void SetActive(bool value)
        {
            active = value;

            if (spriteRenderer == null) {
                // The component is not initialized yet
                return;
            }

            spriteRenderer.enabled = value;
            colliderComponent.enabled = value;
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>().VerifyNotNull();
            colliderComponent = GetComponent<Collider2D>().VerifyNotNull();
            SetActive(active);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            KeyCollector collector = other.GetComponent<KeyCollector>();
            
            if (collector == null) {
                // Object that entered the key's collider is not a KeyCollector
                return;
            }

            if (!collector.AcceptKey(keyIdentifier)) {
                // Collector did not accept a key
                return;
            }
            
            SetActive(false);
            KeyCollected?.Invoke();
        }
    }
}