using UnityEngine;
using UnityEngine.VFX;

namespace LoanGenot
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private VisualEffect m_pickUpDestroy;

        public virtual void OnTriggerEnter(Collider other)
        {

            if (other != null)
            {
                SpawnDestroyPickUp();
                Destroy(gameObject);
            }
        }

        private void SpawnDestroyPickUp()
        {
            
            VisualEffect vfx = Instantiate(m_pickUpDestroy,transform.position, transform.rotation);

            vfx.Play();
        }
    }
}
