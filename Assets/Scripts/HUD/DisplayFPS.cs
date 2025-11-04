using TMPro;
using UnityEngine;

namespace LoanGenot
{
    public class DisplayFPS : MonoBehaviour
    {
        public TextMeshProUGUI m_fpsText;

        private float m_time;
        private float m_frames;
        private float m_seconds = 1f;
        void Update()
        {
            m_time += Time.deltaTime;
            m_frames++;
            if(m_time >= m_seconds)
            {
                int fps = Mathf.RoundToInt(m_frames / m_time);
                m_fpsText.text = "FPS " + fps.ToString();

                m_time = 0;
                m_frames = 0;
            }
        }
    }
}
