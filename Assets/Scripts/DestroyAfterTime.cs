using System.Collections;
using UnityEngine;

public class DestroyAfterTime: MonoBehaviour
{

    [SerializeField] private float m_destroyTime = 1f;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_destroyTime);
        Destroy(gameObject);
    }

}
