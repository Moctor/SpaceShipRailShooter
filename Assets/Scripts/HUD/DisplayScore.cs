using TMPro;
using UnityEngine;

namespace LoanGenot
{
    public class DisplayScore : MonoBehaviour
    {
        public TextMeshProUGUI m_scoreText;
        [SerializeField] private GameObject m_player;

        // Update is called once per frame
        void Update()
        {
            PlayerScore playerScore = m_player.GetComponent<PlayerScore>();
            if (playerScore != null)
            {
                m_scoreText.text = "Score " + playerScore.m_score.ToString();
            }
        }
    }
}
