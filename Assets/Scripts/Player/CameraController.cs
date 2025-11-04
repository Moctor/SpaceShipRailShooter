using UnityEngine;

namespace LoanGenot
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private Transform m_target;
        [SerializeField] private float m_smoothTime = 1.0f;
        [SerializeField] private Vector3 m_velocity;
        [SerializeField] private Vector3 m_offsetPosition;

        private Vector3 m_smoothPosition;

        void Update()
        {
            Vector3 targetPosition = m_target.position + m_offsetPosition;


            m_smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref m_velocity, m_smoothTime);
            // crée un nouveau vecteur avec la transform.position de la camera en x
            // afin de ne pas utiliser de smoothdamp lorsque le player se déplace horizontalement
            transform.position = new Vector3(transform.position.x, m_smoothPosition.y, m_smoothPosition.z);


        }
    }
}
