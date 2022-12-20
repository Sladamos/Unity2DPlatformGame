using MIIProjekt.Extensions;
using UnityEngine;

namespace MIIProjekt.Keys
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class Key : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Collider2D colliderComponent;
        private KeyAttributes keyAttributes;

        [SerializeField]
        private string keyIdentifier;

        [SerializeField]
        private bool active;

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
            Color keyColor = spriteRenderer.color;
            keyAttributes = new(keyIdentifier, keyColor);
            SetActive(active);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            KeyCollectorComponent collector = other.GetComponent<KeyCollectorComponent>();

            if (isCollectorValidate(collector) && collector.AcceptedKey(keyAttributes))
            {
                SetActive(false);
            }
        }

        private bool isCollectorValidate(KeyCollectorComponent collector)
        {
            return collector != null;
        }
    }
}
