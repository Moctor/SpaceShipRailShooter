using UnityEngine;
using UnityEngine.VFX;

namespace LoanGenot
{
    public class Life : MonoBehaviour
    {
        [SerializeField] private int m_baseLife = 3;
        [SerializeField] private int m_baseShield;
        public int m_currentLife;
        public int m_currentShield;
        public int m_surplusDamage;

        [SerializeField] private VisualEffect m_death;

        public virtual void Start()
        {
            m_currentShield = m_baseShield;
            m_currentLife = m_baseLife;
        }

        public virtual void GetDamage(int damage)
        {
            // check si il a du shield si oui les degats sont mit dans le bouclier sinon les degats sont infligé aux points de vies
            if (m_currentShield > 0)
            {
                if(damage > m_currentShield)
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
                }
                if(m_currentLife <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                //sinon va appliquer des dégats aux points de vie et si ils sont a 0 ou moins détruit l'objet
                m_currentLife -= damage;

                if (m_currentLife <= 0)
                {
                    DeathVFX();
                    Destroy(gameObject);
                }
            }

        }

        public void DeathVFX()
        {
            VisualEffect vfx = Instantiate(m_death, transform.position, transform.rotation);

            vfx.Play();
        }

    }
}

