using UnityEngine;
using UnityEngine.UI;

namespace LoanGenot
{
    public class DisplayHeat : MonoBehaviour
    {
        [SerializeField] private Gradient m_gradient;
        [SerializeField] private Image m_image;
        [SerializeField] private Slider m_slider;
        [SerializeField] private GameObject m_player;

        private void Start()
        {
            m_image = GetComponent<Image>();
        }
        
        void Update()
        {
            HeatGradientSlider();
        }

        // va actualiser la couleur de l'image avec notre gradient grace à la valeur du slider
        // en fonction de ma chaleur provenant du player et j'ai diviser la chaleur par la chaleur max pour avoir
        // une valeur entre 0 & 1 car m_gradient.Evaluate clamp les valeur entre 0 & 1
        private void HeatGradientSlider()
        {
            m_image.color = m_gradient.Evaluate(m_slider.value);
            PlayerFire playerFire = m_player.GetComponent<PlayerFire>();
            if (playerFire != null)
            {
                float heatPercent = playerFire.m_heat / playerFire.m_heatLimit;
                m_slider.value = heatPercent;
            }
        }
    }
}
