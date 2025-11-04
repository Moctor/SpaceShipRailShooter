using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoanGenot
{
    public class UI : MonoBehaviour
    {
        public static bool s_isPaused;
        public virtual void PauseGame()
        {

             if (s_isPaused)
             {
                Time.timeScale = 1.0f;
                s_isPaused = false;
             }
             else
             {
                Time.timeScale = 0.0f;
                s_isPaused = true;
             }
        }

        public void Restart()
        {
            
            s_isPaused = true;
            PauseGame();
            SceneManager.LoadScene(1);
        }

        public void BackToMainMenu()
        {
            
            s_isPaused = true;
            PauseGame();
            SceneManager.LoadScene(0);
        }
    }
}
