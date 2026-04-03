using UnityEngine;

public class PetAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }

    public void IsWalking()
    {
        animator.SetBool("IsWalking", true);
    }
    
    public void IsIDLEing()
    {
        animator.SetBool("IsWalking", false);
    }
}
