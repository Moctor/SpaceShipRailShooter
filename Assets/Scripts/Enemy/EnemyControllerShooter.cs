using UnityEngine;

namespace LoanGenot
{
    public class EnemyControllerShooter : EnemyController
    {
        [SerializeField] private float m_shootingRange;
        [SerializeField] private float m_fireRate = 1.0f;
        [SerializeField] private float m_checkerRange = 2.0f;

        [SerializeField] private Transform m_cannonOutlet;
        [SerializeField] private GameObject m_projectile;
        [SerializeField] private GameObject m_projectileMuzzle;

        private float m_lastFire;
        public override void AttackBehavior()
        {
            //effectue la fonction de base de l'attackbehavior
            base.AttackBehavior();
            // permet de verifier si il y a un autre shooter proche de nous
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.right, out hit, m_checkerRange) || Physics.Raycast(transform.position, -transform.right, out hit, m_checkerRange))
            {
                m_canChase = false;
            }
            else
            {
                m_canChase = true;
            }
            if (!m_canChase)
            {
                transform.position = transform.position; // freeze les position pour eviter qu'il se supperpose en chasant
                //pas trouvé comment empecher qu'il se surpepose si ils se trouvent au meme positio en x au moment qu'on déclenche le
                //le mode attack
            }
            //permet de faire en sorte que l'enemy  shooter reste toujours a distance de l'ennemi en fonction de sa shootingrange
            Vector3 shootingPosition = new Vector3(transform.position.x,transform.position.y, attackRange.m_playerTarget.position.z + m_shootingRange);
            transform.position = shootingPosition;

            Fire();

        }

        private void Fire()
        {
            // permet d'ajouter un delay entre chaque tire
            if (Time.time > m_fireRate + m_lastFire )
            {
                Instantiate(m_projectile, m_cannonOutlet.position , m_cannonOutlet.rotation);
                m_projectileMuzzle.SetActive(true);
                m_lastFire = Time.time;
            }
        }
    }
}
