using UnityEngine;

namespace LoanGenot
{
    public class WinningCheckpoint : MonoBehaviour
    {
        [SerializeField] private GameObject m_winningScreen;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                m_winningScreen.SetActive(true);
            }
        }
    }
}
