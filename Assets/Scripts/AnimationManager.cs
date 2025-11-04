using UnityEngine;

namespace LoanGenot
{
    public class AnimationManager : MonoBehaviour
    {
        Animator animator;
        void Start()
        {
            animator = GetComponent<Animator>();

        }


        void Update()
        {
            MouvementAnimation();
            ShootingAnimation();
        }

        private void MouvementAnimation()
        {
            if (Input.GetAxis("Acceleration") > 0)
            {
                animator.SetBool("IsAccelerating", true);
                animator.SetBool("IsDeccelerating", false);
                animator.SetBool("IsStanding", false);
            }
            else
            {
                animator.SetBool("IsAccelerating", false);
            }
            if (Input.GetAxis("Deceleration") > 0)
            {
                animator.SetBool("IsDeccelerating", true);
                animator.SetBool("IsAccelerating", false);
                animator.SetBool("IsStanding", false);
            }
            else
            {
                animator.SetBool("IsDeccelerating", false);

            }
            if (Input.GetAxis("Acceleration")! > 0 && Input.GetAxis("Deceleration")! > 0)
            {
                animator.SetBool("IsStanding", true);
                animator.SetBool("IsAccelerating", false);
                animator.SetBool("IsDeccelerating", false);
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                animator.SetBool("IsTurningR", true);
                animator.SetBool("IsTurningL", false);
            }
            else
            {
                animator.SetBool("IsTurningR", false);


            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                animator.SetBool("IsTurningL", true);
                animator.SetBool("IsTurningR", false);
            }
            else
            {
                animator.SetBool("IsTurningL", false);

            }
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Deceleration") == 0 && Input.GetAxis("Acceleration") == 0)
            {
                animator.SetBool("IsStanding", true);
                animator.SetBool("IsTurningL", false);
                animator.SetBool("IsTurningR", false);
                animator.SetBool("IsDeccelerating", false);
                animator.SetBool("IsAccelerating", false);
                animator.SetBool("IsStanding", false);
            }
        }

        private void ShootingAnimation()
        {
            if (Input.GetButton("Fire1"))
            {
                animator.SetBool("IsShooting", true);
            }
            else
            {
                animator.SetBool("IsShooting", false);
            }
        }
    }
}
