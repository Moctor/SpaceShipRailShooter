using UnityEngine;
using UnityEngine.VFX;

namespace LoanGenot
{
    public class CollectableShield : Collectable
    {
        [SerializeField] private int m_shieldCollectable = 1;
        [SerializeField] private VisualEffect m_shieldPlayerSpawn;
        public override void OnTriggerEnter(Collider other)
        {
            if(other !=null)
            {
                PlayerLife playerLife = other.gameObject.GetComponent<PlayerLife>();

                if (playerLife.m_currentShield == 0)
                {
                    ShieldPlayerSpawn();
                    playerLife.m_currentShield += m_shieldCollectable;
                }
                base.OnTriggerEnter(other);
            }
        }

        private void ShieldPlayerSpawn()
        {
            Instantiate(m_shieldPlayerSpawn,transform.position,transform.rotation);

            m_shieldPlayerSpawn.Play();
        }
    }
}
