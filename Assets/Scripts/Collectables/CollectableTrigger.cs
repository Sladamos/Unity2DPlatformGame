using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using UnityEngine;

namespace MIIProjekt.Collectables
{
    [RequireComponent(typeof(Collider2D))]
    public class CollectableTrigger : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private ICollectableTriggerTarget collectableTriggerTarget;
        private bool isActive = true;

        [SerializeField]
        private Transform collectableTriggerTargetTransform;

        [SerializeField]
        private List<string> requiredCollectableNames;

        private bool doesCollectorContainAllRequiredCollectables(ICollector collector)
        {
            foreach (string requiredCollectableName in requiredCollectableNames)
            {
                if (!collector.Contains(requiredCollectableName))
                {
                    return false;
                }
            }
            
            return true;
        }

        private void Awake()
        {
            if (collectableTriggerTargetTransform == null)
            {
                Logger.Error("CollectableTriggerTargetTransform is null!");
                return;
            }

            collectableTriggerTarget = collectableTriggerTargetTransform.GetComponent<ICollectableTriggerTarget>();

            if (collectableTriggerTarget == null)
            {
                Logger.Error("CollectableTriggerTarget is null!");
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!isActive)
            {
                return;
            }

            ICollector collector = collider.GetComponent<ICollector>();
            if (collector == null)
            {
                Logger.Trace("Something that is not a collector has entered the DoorTrigger");
                return;
            }

            if (!doesCollectorContainAllRequiredCollectables(collector))
            {
                Logger.Debug("Not all collectables are collected");
                return;
            }

            List<ICollectable> collectables = collector.Collectables
                .Where(x => requiredCollectableNames.Contains(x.Name))
                .ToList();

            foreach (ICollectable collectable in collectables)
            {
                collector.RemoveCollectable(collectable);
            }

            Logger.Info("All collectables collected {}", String.Join(", ",  collectables));
            isActive = false;
            
            collectableTriggerTarget.InvokeCollectableTarget(collectables);
        }
    }
}
