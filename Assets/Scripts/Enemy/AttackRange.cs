using UnityEngine;

namespace LoanGenot
{
    public class AttackRange : MonoBehaviour
    {
        public Transform m_playerTarget = null;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                m_playerTarget = other.transform;
            }
        }
    }
}
