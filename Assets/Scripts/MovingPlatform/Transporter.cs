using UnityEngine;

namespace MIIProjekt.MovingPlatform
{
    public class Transporter : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.transform.SetParent(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
