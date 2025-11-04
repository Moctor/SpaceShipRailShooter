using UnityEngine;

namespace LoanGenot
{
    public class EnemyLife : Life
    {
        [SerializeField] private int m_score;
        [SerializeField] private int m_collectableDropRates = 3;

        [SerializeField] private GameObject m_collectableLife;
        [SerializeField] private GameObject m_collectableShield;


        private GameObject m_player;
        private GameObject m_enemyManager;
        private GameObject m_killingWall;

        public override void Start()
        {
            m_killingWall = GameObject.Find("KillingWall");
            base.Start();
        }
        public virtual void Update()
        {
            if(transform.position.z < m_killingWall.transform.position.z)
            {
                Destroy(gameObject);
            }
        }
        public override void GetDamage(int damage)
        {
            base.GetDamage(damage);
            if (m_currentLife <=0 )
            {
                Death();
            }
        }
        private void Death()
        {
            m_player = GameObject.Find("Player");
            if (m_player != null)
            {
                PlayerScore playerScore = m_player.GetComponent<PlayerScore>();
                if (playerScore != null)
                {
                    playerScore.m_score += m_score;
                }
            }
            Drop();
        }

        private void Drop()
        {
            //on genere un chiffre entre 1 & 10 si celui ci est plus petit ou egal que notre taux de drop
            int randomNumber = Random.Range(1, 11);
            // on genere un deuxieme chiffre mais entre 1 & 2 qui va nous donner notre collectable qui est soit le shield soit le heal
            if (randomNumber <= m_collectableDropRates)
            {
                int randonNumber2 = Random.Range(1,3);
                if(randonNumber2 == 1)
                {
                    //drop le prefab collectable qui redonne 1 hp au player
                    Instantiate(m_collectableLife, transform.position,transform.rotation);
                }
                else if (randonNumber2 == 2)
                {
                    //drop le prefab collectable qui donne un shield au player
                    Instantiate(m_collectableShield, transform.position, transform.rotation);
                }
            }
        }
        private void OnDestroy()
        {
            // a chaque fois qu'un enemy est détruit va décrémenté le nombre d enemy qu'il reste en jeu
            m_enemyManager = GameObject.Find("EnemyManager");
            if (m_enemyManager != null)
            {
                EnemyManager enemyManager = m_enemyManager.GetComponent<EnemyManager>();
                if (enemyManager != null)
                {
                    enemyManager.m_enemyCount--;
                }
            }
        }
    }
}
