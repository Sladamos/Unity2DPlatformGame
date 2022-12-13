using MIIProjekt.Player;
using TMPro;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    public class HealthDisplayer : MonoBehaviour
    {
        [SerializeField]
        private PlayerLife playerLife;

        [SerializeField]
        private TMP_Text hitpointsText;

        private void OnPlayerLifeChanged(int value)
        {
            if (hitpointsText != null)
            {
                hitpointsText.SetText($"{value}");
            }
        }

        private void Awake()
        {
            if (playerLife != null)
            {
                playerLife.PlayerLifeChanged += OnPlayerLifeChanged;
            }
        }
    }
}
