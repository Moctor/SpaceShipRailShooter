using UnityEngine;

namespace LoanGenot
{
    public class LosingUI : UI
    {
        [SerializeField] private GameObject m_losingScreen;

        private void OnEnable()
        {
            PauseGame();
        }
    }
}
