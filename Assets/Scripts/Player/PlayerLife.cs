using UnityEngine;
using System.Collections;


namespace LoanGenot
{
    public class PlayerLife : Life
    {
        [SerializeField] private float m_respawnDelay = 2.0f;
        [SerializeField] private GameObject m_losingScreen;
        [SerializeField] private GameObject m_shieldIdle;
        [SerializeField] private GameObject m_shieldSpawn;
        [SerializeField] private GameObject m_shieldDestroy;
        [SerializeField] private float m_shieldAnimation;
        [SerializeField] private SkinnedMeshRenderer[] m_blinkShader;
        [SerializeField] private float m_blinkTime;
        [SerializeField] private int m_blinkNumber;

        private bool m_canBeDamage = true;
        private bool m_isShieldSpawn = false;

        private Vector3 m_deathPosition;

        private void Update()
        {
            ShieldVFX();
        }
        public override void GetDamage(int damage)
        {
            if (!PauseMenu.s_godMode)
            {
                // on override la fonction getdamage du script life, on check si on peut recevoir des degats, si oui on retire les degats
                // a la notre variable de vie et on desactive le space ship et pui on lance la methode respawn
                if (m_canBeDamage)
                {
                    // check si il a du shield si oui les degats sont mit dans le bouclier sinon les degats sont infligé aux points de vies
                    if (m_currentShield > 0)
                    {

                        if (damage > m_currentShield)
                        {
                            m_surplusDamage = damage - m_currentShield;
                        }
                        else
                        {
                            m_currentShield -= damage;
                        }
                        if (m_surplusDamage > 0)
                        {
                            m_currentLife -= m_surplusDamage;
                            Damage();
                        }
                    }
                    else
                    {
                        m_currentLife -= damage;
                        Damage();
                    }
                    if (m_currentLife <= 0)
                    {
                        Death();
                    }
                }
            }
        }
        private void Damage()
        {
            DeathVFX();
            m_deathPosition = transform.position;
            gameObject.SetActive(false);
            Debug.Log(m_currentLife);

            // on utilise un invoke pour donner un delay de réaparition au vaiseau
            Invoke("RespawnPlayer", m_respawnDelay);
        }
        private void RespawnPlayer()
        {
            // si notre variable de vie est au dessus de 1 on réactive le vaiseau et on le met a la position de sa mort
            // puis on lance la coroutine IEinvulnerable
            if (m_currentLife >= 1)
            {
                gameObject.SetActive(true);
                transform.position = m_deathPosition;
                StartCoroutine(IEinvulnerable());
            }

        }

        IEnumerator IEinvulnerable()
        {
            // on va faire 3 que le joueur ne peut pas prendre de degats avec un delay de mit dans la variable m_invulnerableTime
            // ce qui nous permet de jouer un clignottement une fois faire on lui remet la possibilité de prendre des dégats
            for (int i = 0; i < m_blinkNumber; i++)
            {
                m_canBeDamage = false;
                foreach (SkinnedMeshRenderer blink in m_blinkShader)
                {
                    blink.material.SetFloat("_Blink", 0);
                }
                yield return new WaitForSeconds(m_blinkTime);
                foreach (SkinnedMeshRenderer blink in m_blinkShader)
                {
                    blink.material.SetFloat("_Blink", 1);
                }
                yield return new WaitForSeconds(m_blinkTime);
            }
            m_canBeDamage = true;
        }

        private void Death()
        {

            m_losingScreen.SetActive(true);
        }

        IEnumerator IEshieldSpawn()
        {
            m_shieldSpawn.SetActive(true);
            m_isShieldSpawn = true;
            yield return new WaitForSeconds(m_shieldAnimation);
            ShieldIdle();
        }
        private void ShieldIdle()
        {
            m_shieldIdle.SetActive(true);
        }
        
        private void ShieldDestroy()
        {
            m_shieldIdle.SetActive(false );
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
