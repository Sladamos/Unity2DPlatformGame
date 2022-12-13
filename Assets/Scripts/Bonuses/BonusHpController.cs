using UnityEngine;
using MIIProjekt.Player;

namespace MIIProjekt.Bonuses
{
    public class BonusHpController : MonoBehaviour
    {
        [SerializeField]
        private int numberOfLives;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player") && collider.GetComponent<PlayerLife>().CanPickupBonusLife())
            {
                collider.SendMessage("IncreaseLives", numberOfLives);
                this.gameObject.SetActive(false);
            }
        }
    }
}
