using UnityEngine;
using UnityEngine.VFX;

namespace LoanGenot
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] private float m_speed = 1f;
        [SerializeField] private string m_ownerTag;
        [SerializeField] private int m_damage = 1;
        [SerializeField] private VisualEffect m_projectileImpact;
        [SerializeField] private float m_impact = 3.0f;

        private Rigidbody m_rigidbodyRef;


        void Start()
        {

            m_rigidbodyRef = GetComponent<Rigidbody>();

            // va donner une vitesse dans une direction de 1 en z ( tout droit) au projectile
            m_rigidbodyRef.linearVelocity = transform.forward * m_speed;

        }

        private void OnTriggerEnter(Collider other)
        {
            // check si le tag avec l'objet qui l'entre en collision est different que son owner si oui va chercher une ref au script
            // Life si l'objet est équiper du script Life
            // ensuite va check si la variable life est different de null si c est le cas lui inflige des degats sinon detruit l objet
            if(other != null)
            {
                if (!other.tag.Equals(m_ownerTag))
                {
                    Life life = other.gameObject.GetComponent<Life>();
                    if (life != null)
                    {
                        life.GetDamage(m_damage);
                        Vector3 impactPosition = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + m_impact);
                        ProjectileImpact(impactPosition);
                    }
                    Destroy(gameObject);
                }
            }
        }

        private void ProjectileImpact(Vector3 hitPos)
        {
            Instantiate(m_projectileImpact, hitPos, transform.rotation);
            m_projectileImpact.Play();
        }
    }
}

