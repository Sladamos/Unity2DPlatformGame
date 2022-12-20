using System.Collections.Generic;
using MIIProjekt.Logging;
using NLog;
using UnityEngine;

namespace MIIProjekt.UI.Level
{
    public class PlayerLifeDisplay : MonoBehaviour
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private List<RectTransform> Hearts { get; } = new List<RectTransform>();

        public void DisplayHearts(int amount)
        {
            int heartsToDisplay = amount;
            if (amount < 0)
            {
                Logger.Warn("Tried to display less than 0 hearts. {} < 0", amount);
                heartsToDisplay = 0;
            }
            else if (amount > Hearts.Count)
            {
                Logger.Warn("Tried to display more hearts than this object is managing. {} > {}", amount, Hearts.Count);
                heartsToDisplay = Hearts.Count;
            }

            for (int i = 0; i < Hearts.Count; i++)
            {
                Hearts[i].gameObject.SetActive(i < heartsToDisplay);
            }
        }

        private void Awake()
        {
            LoggingManager.InitializeLogging();

            foreach (RectTransform child in transform)
            {
                Hearts.Add(child);
            }

            // Initialize hearts to some state
            DisplayHearts(0);
        }
    }
}
