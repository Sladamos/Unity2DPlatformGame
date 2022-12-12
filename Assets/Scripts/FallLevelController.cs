using UnityEngine;

namespace MIIProjekt
{
    public class FallLevelController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.SendMessage("GetHit");
            }
        }
    }
}
