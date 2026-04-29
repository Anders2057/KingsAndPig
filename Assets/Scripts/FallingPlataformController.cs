using Unity.VisualScripting;
using UnityEngine;

public class FallingPlataformController : MonoBehaviour
{
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody2D;
    private BoxCollider2D [] boxCollider2D;

    [Header("Plataform Setting")]
    [SerializeField] private float speed = 0.75f;
    [SerializeField] private float distance;
    private Vector3[] wayPoints;
    private int waypointIndex;
    private bool canMove = false;

    [Header("Plataform Fall Settings")]
    [SerializeField] private bool canFall;
    [SerializeField] private float fallDelay = 0.5f;
    [Space]
    [SerializeField] private float impactSpeed = 3f;
    [SerializeField] private float impactDuration = 0.1f;
    private float impactTimer;
    private bool impacHappened;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponents<BoxCollider2D>();
    }

    public void Start()
    {
        SetupWayPoint();
        float randomDelay = Random.Range(0,0.6f);
        Invoke(nameof(ActivatePlataform),randomDelay);
    }
    private void Update()
    {
        HandleImpact();
        HandleMovement();
    }

    private void ActivatePlataform() => canMove = true;
    private void SetupWayPoint()
    {
        wayPoints = new Vector3[2];
        float YOffset = distance / 2;
        wayPoints[0] = transform.position + new Vector3(0, YOffset, 0);
        wayPoints[1] = transform.position + new Vector3(0, -YOffset, 0);
    }

    private void HandleMovement()
    {
        if (!canMove) return;

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[waypointIndex], speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoints[waypointIndex]) < 0.1f)
        {
            waypointIndex++;
            if (waypointIndex >= wayPoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }
    private void HandleImpact()
    {
        if (impactTimer < 0)
            return;
        impactTimer -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3.down * 10),impactSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;
        if(!canFall) return;
        if (impacHappened) return;
            
        Invoke(nameof(SwitchOffPlataform), fallDelay);
        impactTimer = impactDuration;
        impacHappened = true;
        
    }
    private void SwitchOffPlataform()
    {
        m_Animator.SetTrigger("desactive");
        canMove = false;

        m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        m_Rigidbody2D.gravityScale = 3.5f;
        m_Rigidbody2D.linearDamping = 0.5f;

        foreach (BoxCollider2D collider in boxCollider2D)
        {
            collider.enabled = false;
        }
    }
}
