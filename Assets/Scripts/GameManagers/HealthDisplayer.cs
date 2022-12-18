using MIIProjekt.Player;
using UnityEngine;
using UnityEngine.UI;

namespace MIIProjekt.GameManagers
{
    public class HealthDisplayer : MonoBehaviour
    {
        [SerializeField]
        private PlayerLife playerLife;

        [SerializeField]
        private Image[] hitpointsImages;

        private void OnPlayerLifeChanged(int oldValue, int value)
        {
            int numberOfImages = hitpointsImages.Length;
            for (int i = 0; i < numberOfImages; i++)
            {
                if (i < value)
                {
                    hitpointsImages[i].enabled = true;
                }
                else
                {
                    hitpointsImages[i].enabled = false;
                }
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
