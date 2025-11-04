using UnityEngine;

namespace LoanGenot
{
    public class KillingWall : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
