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
            Transform transformToFollow = transform;
            foreach (ICollectable collectable in Collectables)
            {
                Transform currentTransform = collectable.Transform;
                Vector2 difference = transformToFollow.position - currentTransform.position;
                Vector2 direction = difference.normalized;
                float distance = difference.magnitude;

                // Get current velocity
                float percent = (distance - minRange) / RangeDifference;
                float velocityScalar = Mathf.Max(0.0f, percent) * maxSpeed;
                Vector2 velocity = direction * velocityScalar;

                currentTransform.position += (Vector3)(velocity * Time.deltaTime);

                // Next collectable in the list should follow the current transform
                transformToFollow = collectable.Transform;
            }
        }
    }
}
