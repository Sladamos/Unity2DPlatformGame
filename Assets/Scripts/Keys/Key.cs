using System;
using UnityEngine;
using MIIProjekt.Extensions;

namespace MIIProjekt
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class Key : MonoBehaviour
    {
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

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = value;
                colliderComponent.enabled = value;
            }
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

            if (isCollectorValidate(collector) && collector.AcceptedKey(keyIdentifier))
            {
                SetActive(false);
            }
        }

        private bool isCollectorValidate(KeyCollector collector)
        {
            return collector != null;
        }
    }
}