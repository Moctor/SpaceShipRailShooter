using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace LoanGenot
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform m_player;
        [SerializeField] private Waves[] m_wave;
        [SerializeField] private VisualEffect m_portalSpawn;
        [SerializeField] private float m_minZSpawnRange;
        [SerializeField] private float m_maxZSpawnRange;

        private Vector3 m_spawnPosition;
        public int m_enemyCount;
        private int m_waveIndexer;
        private GameObject [] m_enemySpawn;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            m_waveIndexer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(m_enemyCount == 0 && m_waveIndexer < m_wave.Length)
            {
                SpawnWave();
            }
            if (PauseMenu.s_nextLevel)
            {
                NextLevel();
            }
        }

        private void SpawnWave()
        {
            foreach (GameObject enemy in m_wave[m_waveIndexer].m_enemy)
            {
                SpawnPoint();
                StartCoroutine(IEportal());
                Instantiate(enemy, m_spawnPosition, transform.rotation);
                m_enemyCount++;
            }
            m_waveIndexer++;
        }

        // me permet de générer un spawnpoint random
        private void SpawnPoint()
        {
            float offset = m_player.transform.position.z;
            float randomX = Random.Range(-5f, 5f); // limitation des mouvements du player
            float randomZ = Random.Range(m_minZSpawnRange, m_maxZSpawnRange);
            m_spawnPosition = new Vector3(randomX,1.0f,randomZ + offset); //1.0f en y pour qu'il spawn a bonne hauteur par rapprot au player
        }

        IEnumerator IEportal()
        {
            Instantiate(m_portalSpawn, m_spawnPosition, transform.rotation);
            m_portalSpawn.Play();
            yield return null;
        }

        private void NextLevel ()
        {
            PauseMenu.s_nextLevel = false;
        }
    }

    //me permet d'avoir "une array qui gere des arrays" dans ce cas mon array wave va gerer mes array enemt ce qui me permet
    // de creer plusieurs wave comportant un certains nombre d enemi a ma guise dans l'inspecteur et c est + pratique
    // surtout pour les games designer et si on veut augmenter notre nombre de wave plus tard
    [System.Serializable]
    public class Waves
    {
        public GameObject[]m_enemy; 
    }
}
