using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Components
    [Header("Player Component")]
    [SerializeField] private Transform m_transformPlayer;
    private Rigidbody2D m_rigigbody2D;
    private GatherInput m_gatherInput;
    private Animator m_animator;

    //Animator ID
    private int idIsGrounded;
    private int idSpeed;

    [Header("Move Settings")]
    [SerializeField]private float speed;
    private int direction = 1;
    
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJumps;
    [SerializeField] private int CounterExtraJumps;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool canDoubleJump;
    

    [Header("Ground Settings")]
    [SerializeField] private Transform rFoot;
    [SerializeField] private Transform lFoot;
    RaycastHit2D lFootRay;
    RaycastHit2D rFootRay;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask groundLayer;

    [Header("Wall Settings")]
    [SerializeField] private float checkWallDistance;
    [SerializeField] private bool isWallDetected;

    private void Awake()
    {
        m_gatherInput = GetComponent<GatherInput>();
        m_transformPlayer = GetComponent<Transform>();
        m_rigigbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }
    void Start()
    {
        idSpeed = Animator.StringToHash("Speed");
        idIsGrounded = Animator.StringToHash("IsGrounded");
        lFoot = GameObject.Find("LFoot").GetComponent<Transform>();
        rFoot = GameObject.Find("RFoot").GetComponent<Transform>();
        CounterExtraJumps = extraJumps;
    }
    private void Update()
    {
        SetAnimatorValue();
    }
    private void SetAnimatorValue()
    {
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigigbody2D.linearVelocityX));
        m_animator.SetBool(idIsGrounded, isGrounded);
       
    }
    void FixedUpdate()
    {
        CheckCollicion();
        Move();
        jump();
    }
    private void CheckCollicion()
    {
        HandleGound();
        HandleWall();
    }

    private void HandleWall()
    {
       isWallDetected = Physics2D.Raycast(m_transformPlayer.transform.position, Vector2.right * direction , checkWallDistance, groundLayer);
    }

    private void HandleGound()
    {
        lFootRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLength, groundLayer);
        rFootRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLength, groundLayer);

        if (lFootRay || rFootRay)
        {
            isGrounded = true;
            CounterExtraJumps = extraJumps;
            canDoubleJump = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Move()
    {
        Flip();
        m_rigigbody2D.linearVelocity = new Vector2(speed * m_gatherInput.ValueX, m_rigigbody2D.linearVelocityY);
    }
   
    private void Flip()
    {
        if(m_gatherInput.ValueX * direction < 0)
        {
            m_transformPlayer.localScale = new Vector3(-m_transformPlayer.localScale.x,1,1);
            direction *= -1;
        }
    }
    private void jump()
    {
        if (m_gatherInput.IsJumping) 
        {
            if (isGrounded)
            {
                m_rigigbody2D.linearVelocity = new Vector2(speed * m_gatherInput.ValueX, jumpForce);
                canDoubleJump = true;
            }
            else if(CounterExtraJumps > 0 && canDoubleJump)
            {
                m_rigigbody2D.linearVelocity = new Vector2(speed * m_gatherInput.ValueX, jumpForce);
                CounterExtraJumps--;
            }
        }
        m_gatherInput.IsJumping = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(m_transformPlayer.transform.position, new Vector2(m_transformPlayer.position.x + (checkWallDistance * direction), m_transformPlayer.position.y));
    }
}
