using UnityEngine;

namespace LoanGenot
{
    public class PlayerScore : MonoBehaviour
    {
        public int m_score = 0;

        private int m_previousScore;

        void Update()
        {
            ScoreUpdate();
        }

        private void ScoreUpdate()
        {
            // check si notre score est plus grand que notre previous score
            //si oui le previous score devient notre score actuelle
            if (m_score > m_previousScore)
            {
                m_previousScore = m_score;
            }
        }
    }
}
