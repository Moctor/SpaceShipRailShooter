using UnityEngine;

namespace LoanGenot
{
    public class EnemyControllerRusher : EnemyController
    {
        [SerializeField] private float m_rushAttackRange = 3f;
        [SerializeField] private int m_rushAttackDamage = 1;

        [SerializeField] private Transform m_raycastOutlet;
        [SerializeField] private LayerMask m_layerMask;
        public override void AttackBehavior()
        {
            base.AttackBehavior();
            RushAttack();
        }

        private void RushAttack()
        {
            //fait un raycast et si le player rentre dedans lui inlige des dégats et se détruit
            RaycastHit hit;
            Debug.DrawRay(m_raycastOutlet.position, -transform.forward * m_rushAttackRange, Color.blue);
            if (Physics.Raycast(m_raycastOutlet.position,-transform.forward, out hit, m_rushAttackRange,m_layerMask))
            {
                Debug.Log(hit.transform.name);
                Life life = hit.transform.GetComponent<Life>();

                if (life != null)
                {
                    life.GetDamage(m_rushAttackDamage);
                    life.DeathVFX();
                }
                Destroy(gameObject);
            }
        }
    }
}
