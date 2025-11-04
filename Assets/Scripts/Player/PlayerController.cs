using UnityEngine;

namespace LoanGenot
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float m_currentSpeed;
        [SerializeField] private float m_baseSpeed = 5.0f;
        [SerializeField] private float m_acceleration = 10.0f;
        [SerializeField] private float m_deceleration = 2.0f;
        [SerializeField] private float m_screenLimitationX = 7.5f;

        private void Start()
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
        }
        // Update is called once per frame
        void Update()
        {
            Movement();
            SpeedManager();

        }

        private void Movement()
        {

            float horizontalAxis = Input.GetAxis("Horizontal");

            //la valeur 1.0f dans l'axe z permet a donner la direction du player afin qu'il avance toujours dans celle-ci
            Vector3 mouvement = new Vector3(horizontalAxis, 0.0f, 1.0f);
            mouvement = mouvement.normalized;


            transform.position += mouvement * Time.deltaTime * m_currentSpeed;

            // Permet de clamp les position sur l'axe X afin que le player ne puisse pas sortir du champ de vision (l'écran) 
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -m_screenLimitationX, m_screenLimitationX), transform.position.y, transform.position.z);
        }

        private void SpeedManager()
        {
            //permet d'acceler en appuyant sur les touches mapper à l'acceleration
            if (Input.GetAxis("Acceleration") > 0)
            {
                m_currentSpeed = m_acceleration;
            }
            //permet de décelerer en appuyant sur les touches mapper à la déceleration
            else if (Input.GetAxis("Deceleration") > 0)
            {
                m_currentSpeed = m_deceleration;
            }
            // si aucune de ses touches n'envoit de donnée donc ne sont appuyer sa vitesse actuelle devient sa vitesse de base
            else
            {
                m_currentSpeed = m_baseSpeed;
            }
        }
    }
}
