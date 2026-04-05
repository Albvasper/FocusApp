using UnityEngine;

public class PetAnimator : MonoBehaviour
{
    private Animator animator;
    private bool focusingTaskAssigned = false;

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }

    public void IsWalking()
    {
        focusingTaskAssigned = false;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsReading", false);
        animator.SetBool("IsDrawing", false);
        animator.SetBool("IsSleeping", false);
    }
    
    public void IsIDLEing()
    {
        focusingTaskAssigned = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsReading", false);
        animator.SetBool("IsDrawing", false);
        animator.SetBool("IsSleeping", false);

    }

    public void IsFocusing()
    {
        if (focusingTaskAssigned)
            return;
        
        switch (Random.Range(0, 3))
        {
            case 0: animator.SetBool("IsReading", true); break;
            case 1: animator.SetBool("IsDrawing", true); break;
            case 2: animator.SetBool("IsSleeping", true); break;
        }
        focusingTaskAssigned = true;
    }
}
