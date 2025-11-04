using UnityEngine;

namespace LoanGenot
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float m_speed = 1.0f;
        [SerializeField] private float m_attackSpeed = 1.0f;
        [SerializeField] private float m_screenLimitation = 7.5f;

        public AttackRange attackRange;
        public bool m_canChase = true;

        private bool m_moveLeft = false;
        private bool m_moveRight = true;
        private void Start()
        {
            
        }
        void Update()
        {
            

            if (attackRange.m_playerTarget == null)
            {
                PatrolBehavior();
            }
            else
            {
                AttackBehavior();
            }

        }

        private void PatrolBehavior()
        {
            //va faire en sorte que notre enemy se déplace de droite a gauche dans la limitation de l'écran qui est de 7.5 différent du joueur car pas la meme taille
            //le joueur
            if(m_moveRight)
            {
                Mouvement(m_speed);

                if (transform.position.x == m_screenLimitation)
                {
                    m_moveRight = false;
                    m_moveLeft = true;
                }
            }
            else if(m_moveLeft)
            {
                Mouvement(-m_speed);

                if (transform.position.x == - m_screenLimitation)
                {
                    m_moveRight = true;
                    m_moveLeft = false;
                }
            }
        }

        private void Mouvement(float speed)
        {
            Vector3 mouvement = transform.right;
            transform.position += mouvement * speed * Time.deltaTime;

            // Permet de clamp les position sur l'axe X afin que le player ne puisse pas sortir du champ de vision (l'écran)
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -m_screenLimitation, m_screenLimitation), transform.position.y, transform.position.z);
        }

        public virtual void AttackBehavior()
        {
            // si il est pas aligné en x avec le player & qu'il peut attaquer, il va bouger jusqu'a etre aligné afin de le chasser 
            // ce qui permet au rusher de chasser sa cible et au shooter de tirer vers la position du player

            if (attackRange.m_playerTarget.position.x != transform.position.x && m_canChase)
            {
                Vector3 followingPlayer = new Vector3(attackRange.m_playerTarget.position.x - transform.position.x, 0.0f, 0.0f);
                transform.position += followingPlayer * m_attackSpeed * Time.deltaTime;
            }
        }
    }
}
