using UnityEngine;
using UnityEngine.VFX;

namespace LoanGenot
{
    public class PlayerFire : MonoBehaviour
    {
        public float m_heatLimit = 100.0f; 
        public float m_heat = 0.0f;

        [SerializeField] private float m_currentHeatGainProjectile ;
        [SerializeField] private float m_baseHeatGainProjectile = 10.0f;
        [SerializeField] private float m_currentHeatGainRaycast;
        [SerializeField] private float m_baseHeatGainRaycast = 20.0f;
        [SerializeField] private float m_heatLost = 30;

        [SerializeField] private float m_fireRateProjectile = 1.0f;
        [SerializeField] private float m_fireRateRaycast = 1.0f;

        [SerializeField] private float m_raycastDistance = 12;
        [SerializeField] private int m_raycastDamage = 1;
        [SerializeField] private LayerMask m_layerMask;

        [SerializeField] private Transform m_cannonOutletRight;
        [SerializeField] private Transform m_cannonOutletLeft;
        [SerializeField] private Transform m_LaserOutlet;
        [SerializeField] private GameObject m_projectile;
        [SerializeField] private GameObject m_projectileMuzzleLeft;
        [SerializeField] private GameObject m_projectileMuzzleRight;
        [SerializeField] private GameObject m_raycastLaser;
        [SerializeField] private GameObject m_raycastMuzzle;
        [SerializeField] private VisualEffect m_raycastImpact;

        [SerializeField] private SkinnedMeshRenderer m_overHeatTurretShaderRight;
        [SerializeField] private SkinnedMeshRenderer m_overHeatTurretShaderLeft;
        [SerializeField] private SkinnedMeshRenderer m_overHeatLaserShader;
        [SerializeField] private MeshRenderer m_smokeTurretLeft;
        [SerializeField] private MeshRenderer m_smokeTurretRight;
        [SerializeField] private MeshRenderer m_smokeLaser;
        [SerializeField] private  float m_impact;
        private float m_lastFireProjectile;
        private float m_lastFireRaycast;

        private bool m_isOverheating;

        private Vector3 m_raycastDirection = new Vector3(0,0,1f);

        private void Start()
        {
            m_currentHeatGainRaycast = m_baseHeatGainRaycast;
            m_currentHeatGainProjectile = m_baseHeatGainProjectile;
        }
        // Update is called once per frame
        void Update()
        {
            if(PauseMenu.s_godMode)
            {
                m_currentHeatGainProjectile = 0;
                m_currentHeatGainRaycast = 0;
            }
            else
            {
                m_currentHeatGainRaycast = m_baseHeatGainRaycast;
                m_currentHeatGainProjectile = m_baseHeatGainProjectile;
            }
            // le if !UI.m_isPaused permet de check si le jeu est en pause car sinon je pouvais créer un projectiel ou raycast une fois
            // sans cette verification
            if (!UI.s_isPaused)
            {
                if (Input.GetButton("Fire1"))
                {
                    ProjectileShoot();
                }
                if (Input.GetButton("Fire2"))
                {
                    RaycastShoot();
                }

                CheckOverheating();
                if (m_heat >= 0)
                {
                    m_heat -= m_heatLost * Time.deltaTime;
                }

                ShadderOverHeat();
            }
        }

        private void ProjectileShoot()
        {
            // si il est pas en surchauffe tire un projectile et gagne de la chaleur
            if (!m_isOverheating)
            {
                if(Time.time > m_fireRateProjectile + m_lastFireProjectile)
                {
                    m_projectileMuzzleLeft.SetActive(true);
                    m_projectileMuzzleRight.SetActive(true);
                    Instantiate(m_projectile, m_cannonOutletRight.position, m_cannonOutletRight.rotation);
                    Instantiate(m_projectile, m_cannonOutletLeft.position, m_cannonOutletLeft.rotation);
                    Heating(m_currentHeatGainProjectile);
                    m_lastFireProjectile = Time.time;
                }
            }

        }

        private void RaycastShoot()
        {
            if (!m_isOverheating)
            {
                if(Time.time > m_fireRateRaycast + m_lastFireRaycast)
                {
                    m_raycastLaser.SetActive(true);
                    m_raycastMuzzle.SetActive(true);
                    // effectue un raycast et si on touche un enemy on lui inflige des points de dégats
                    RaycastHit hit;
                    if (Physics.Raycast(m_LaserOutlet.position, m_raycastDirection, out hit, m_raycastDistance, m_layerMask))
                    {

                        Life life = hit.transform.GetComponent<Life>();

                        if (life != null)
                        {
                            life.GetDamage(m_raycastDamage);
                            Vector3 impactPosition = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z + m_impact);
                            RaycastImpact(impactPosition);
                        }     
                    }
                    Heating(m_currentHeatGainRaycast);
                    m_lastFireRaycast = Time.time;
                }

            }
        }

        private void Heating(float heatGain)
        {
            //ajoute la chaleur a notre variable m_heat
            m_heat += heatGain;

        }

        private void CheckOverheating()
        {
            //si on est en surchauffe on dit qu'on surchauffe
            //sinon si on a surchauffé check quand on descend en dessous de 50 pour pouvoir ne plus etre ne surchauffe
            if (m_heat >= m_heatLimit)
            {
                Debug.Log("ça chauffe");
                m_isOverheating = true;
            }
            if (m_heat <= 50)
            {
                m_isOverheating = false;
            }
        }

        private void RaycastImpact(Vector3 hitPos)
        {

            Instantiate(m_raycastImpact,hitPos, transform.rotation);
            m_raycastImpact.Play();
        }

        private void ShadderOverHeat()
        {
            float percentage;
            percentage = m_heat/m_heatLimit;
            //je fais divise la chaleur par la chaleur limite pour me donner un pourcentage et le mettre dans mon shader
            m_overHeatTurretShaderRight.material.SetFloat("_Lerp", percentage);
            m_overHeatTurretShaderLeft.material.SetFloat("_Lerp", percentage);
            m_overHeatLaserShader.material.SetFloat("_Lerp", percentage);

            m_smokeLaser.material.SetFloat("_GlobalOpacity", percentage);
            m_smokeTurretLeft.material.SetFloat("_GlobalOpacity", percentage);
            m_smokeTurretRight.material.SetFloat("_GlobalOpacity", percentage);

            m_smokeLaser.material.SetFloat("_Speed", percentage);
            m_smokeTurretLeft.material.SetFloat("_Speed", percentage);
            m_smokeTurretRight.material.SetFloat("_Speed", percentage);
        }
    }
}
