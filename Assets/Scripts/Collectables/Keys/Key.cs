using MIIProjekt.Extensions;
using UnityEngine;

namespace MIIProjekt.Collectables.Keys
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class Key : MonoBehaviour, ICollectable
    {
        private SpriteRenderer spriteRenderer;
        private Collider2D colliderComponent;
        private KeyAttributes keyAttributes;

        private bool collidable = true;
        private bool active = true;

        private Vector2? position;
        private Vector2 displayOffset;

        [SerializeField]
        private string keyIdentifier;

        public Vector2 Position
        {
            get
            {
                return (Vector2)(position != null ? position : transform.position);
            }
            set
            {
                position = value;
                UpdatePosition();
            }
        }

        public string Name => keyAttributes.Identifier;

        public bool Collidable
        {
            set
            {
                collidable = value;
                UpdateComponents();
            }
        }

        public bool Active
        {
            set
            {
                active = value;
                UpdateComponents();
            }
        }

        public Vector2 DisplayOffset
        {
            get
            {
                return displayOffset;
            }
            set
            {
                displayOffset = value;
                UpdatePosition();
            }
        }

        public void UpdateComponents()
        {
            spriteRenderer.enabled = active;
            colliderComponent.enabled = active ? collidable : false;
        }

        public void UpdatePosition()
        {
            if (position != null)
            {
                transform.position = Position + displayOffset;
            }
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>().VerifyNotNull("SpriteRenderer is required.");
            colliderComponent = GetComponent<Collider2D>().VerifyNotNull("Collider2D is required.");
            Color keyColor = spriteRenderer.color;
            keyAttributes = new(keyIdentifier, keyColor);
            UpdateComponents();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            ICollector collector = collider.GetComponent<ICollector>();

            if (collector == null)
            {
                return;
            }

            collector.AddCollectable(this);
            Collidable = false;
        }
    }
}
