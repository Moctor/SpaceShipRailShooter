using UnityEngine;

namespace LoanGenot
{
    public class PauseMenu : UI
    {

        [SerializeField] private GameObject m_pauseMenu;
        [SerializeField] private GameObject m_player;
        [SerializeField] private GameObject m_checkpointNextLevel;

        public static bool s_godMode;
        public static bool s_nextLevel;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            m_pauseMenu.SetActive(false);
            s_isPaused = false;
            s_godMode = false;
            s_nextLevel = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {
                PauseGame();
            }
        }

        public override void PauseGame()
        {
            if (s_isPaused)
            {
                m_pauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
                s_isPaused=false;
            }
            else
            {
                m_pauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
                s_isPaused=true;
            }
        }
        public void NextLevel()
        {
            m_player.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y, m_checkpointNextLevel.transform.position.z);
            s_nextLevel = true;
            s_isPaused = true;
            PauseGame();

        }

        public void GodMode()
        {
            s_godMode = true;
            s_isPaused = true;
            PauseGame();

        }
    }
}
