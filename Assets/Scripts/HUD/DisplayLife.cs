using TMPro;
using UnityEngine;

namespace LoanGenot
{
    public class DisplayLife : MonoBehaviour
    {
        public TextMeshProUGUI m_lifeText;
        [SerializeField] private GameObject m_player;
        // Update is called once per frame
        void Update()
        {
            PlayerLife playerLife = m_player.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                m_lifeText.text = "Life " + playerLife.m_currentLife.ToString();
            }
        }
    }
}
