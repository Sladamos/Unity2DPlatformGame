using System.Collections.Generic;
using NLog;
using UnityEngine;

namespace MIIProjekt.Collectables
{
    public class CollectableChain : MonoBehaviour, ICollector
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        [Header("Settings")]
        [SerializeField]
        private float minRange = 2.0f;

        [SerializeField]
        private float maxRange = 10.0f;

        [SerializeField]
        private float maxSpeed = 5.0f;

        [SerializeField]
        private float swayMagnitude = 3.0f;

        [SerializeField]
        private float swaySpeed = 3.0f;

        [SerializeField]
        private float swayDelay = 0.5f;

        public List<ICollectable> Collectables { get; }

        private float RangeDifference { get => maxRange - minRange; }

        public CollectableChain()
        {
            Collectables = new();
        }

        public void AddCollectable(ICollectable collectable)
        {
            if (Collectables.Contains(collectable)) {
                Logger.Warn("Tried to add collectable that already exists in the list. Collectable: {}", collectable);
                return;
            }

            Collectables.Add(collectable);
        }

        public void RemoveCollectable(ICollectable collectable)
        {
            Collectables.Remove(collectable);
        }

        private void Update()
        {
            Vector2 positionToFollow = transform.position;
            foreach (ICollectable collectable in Collectables)
            {
                Vector2 difference = positionToFollow - collectable.Position;
                Vector2 direction = difference.normalized;
                float distance = difference.magnitude;

                // Get current velocity
                float percent = (distance - minRange) / RangeDifference;
                float velocityScalar = Mathf.Max(0.0f, percent) * maxSpeed;
                Vector2 velocity = direction * velocityScalar;

                collectable.Position += (velocity * Time.deltaTime);

                // Next collectable in the list should follow the current transform
                positionToFollow = collectable.Position;
            }

            for (int i = 0; i < Collectables.Count; i++)
            {
                ICollectable collectable = Collectables[i];
                float timePassed = Time.time - (i * swayDelay);
                float swayAmount = Mathf.Cos(timePassed * swaySpeed) * swayMagnitude;
                collectable.DisplayOffset = new Vector2(0, swayAmount);
            }
        }
    }
}
