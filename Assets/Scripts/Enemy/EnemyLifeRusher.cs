using System.Collections;
using UnityEngine;

namespace LoanGenot
{
    public class EnemyLifeRusher : EnemyLife
    {
        [SerializeField] private GameObject m_shieldIdle;
        [SerializeField] private GameObject m_shieldSpawn;
        [SerializeField] private GameObject m_shieldDestroy;
        [SerializeField] private float m_shieldAnimation;
        private bool m_isShieldSpawn = false;

        // Update is called once per frame
        public override void Update()
        {
            ShieldVFX();
            base.Update();
        }

        private void ShieldIdle()
        {
            m_shieldIdle.SetActive(true);
        }   
        
        IEnumerator IEshieldSpawn()
        {
            m_shieldSpawn.SetActive(true);
            m_isShieldSpawn = true;
            yield return new WaitForSeconds(m_shieldAnimation);
            ShieldIdle();
        }        

        private void ShieldDestroy()
        {
            m_shieldIdle.SetActive(false);
            m_shieldDestroy.SetActive(true);
            m_isShieldSpawn = false;
        }
        private void ShieldVFX()
        {
            if (m_currentShield > 0 && !m_isShieldSpawn)
            {
                StartCoroutine(IEshieldSpawn());
            }
            if (m_currentShield <= 0 && m_isShieldSpawn)
            {
                ShieldDestroy();
            }
        }
    }
}
