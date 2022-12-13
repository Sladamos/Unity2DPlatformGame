using MIIProjekt.Player;
using TMPro;
using UnityEngine;

namespace MIIProjekt.GameManagers
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HealthDisplayer : MonoBehaviour
    {
        [SerializeField]
        private PlayerLife playerLife;

        private TextMeshProUGUI text;

        private void OnPlayerLifeChanged(int value)
        {
            if (text != null)
            {
                text.SetText($"{value}");
            }
        }

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();

            if (playerLife != null)
            {
                playerLife.PlayerLifeChanged += OnPlayerLifeChanged;
            }
        }
    }
}
