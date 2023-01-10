using System.Collections.Generic;
using UnityEngine;

namespace MIIProjekt.Collectables
{
    public class CollectableChain : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private float minRange = 2.0f;

        [SerializeField]
        private float maxRange = 10.0f;

        [SerializeField]
        private float maxSpeed = 5.0f;

        [Header("Debug display")]
        [SerializeField]
        private List<ICollectable> collectables = new();

        private float RangeDifference { get => maxRange - minRange; }

        private void Update()
        {
            Transform transformToFollow = transform;
            foreach (ICollectable collectable in collectables)
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
