using UnityEngine;

namespace LoanGenot
{
    public class CollectableLife : Collectable
    {



        [SerializeField] private int m_healCollectable = 1;
        public override void OnTriggerEnter(Collider other)
        {
            if(other != null)
            {
                PlayerLife playerLife = other.gameObject.GetComponent<PlayerLife>();

                if (playerLife.m_currentLife < 3)
                {
                    playerLife.m_currentLife += m_healCollectable;
                }
                
                base.OnTriggerEnter(other);
            }
        }
    }
}
