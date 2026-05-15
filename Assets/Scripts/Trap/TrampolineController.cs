using UnityEngine;

public class TrampolineController : MonoBehaviour
{
   private Animator m_Animator;
    [SerializeField] private float pushPower;
    [SerializeField] private float duration = 0.5f;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Push(transform.up * pushPower,duration);
            m_Animator.SetTrigger("active");
        }

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy!= null)
        {
            enemy.Push(transform.up * pushPower);
            m_Animator.SetTrigger("active");
        }
    }
}
