using System.Collections;
using UnityEngine;

namespace LoanGenot
{
    public class DiseableAfterTime : MonoBehaviour
    {

        [SerializeField] private float m_lifetime = 1f;

        private void OnEnable()
        {
            StartCoroutine(IEdisable());
        }
        IEnumerator IEdisable()
        {
            yield return new WaitForSeconds(m_lifetime);
            gameObject.SetActive(false);
        }
    }
}
