using UnityEngine;

public class FireButton_Controller : MonoBehaviour
{
    private Animator animator;
    private Fire_Controller fireController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        fireController = GetComponentInParent<Fire_Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null) 
        {
            animator.SetTrigger("Active");
            fireController.SwitchOffFire();
        }
    }
}
