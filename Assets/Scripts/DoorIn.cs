using UnityEngine;

public class DoorIn : MonoBehaviour
{
    private Animator animator;
    private readonly int idInDoor = Animator.StringToHash("OpenDoor");
    private void Start()
    {
       animator = GetComponent<Animator>(); 
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))return;
        animator.SetTrigger(idInDoor);
        collision.GetComponent<PlayerController>().DoorIn();
        collision.transform.position = new Vector3(transform.position.x,collision.transform.position.y,collision.transform.position.z);
    }
}
