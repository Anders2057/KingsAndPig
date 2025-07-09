using System;
using System.Collections;
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
    private int idIsWallDetected;

    [Header("Move Settings")]
    [SerializeField]private float speed;
    private int direction = 1;
    
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJumps;
    [SerializeField] private int CounterExtraJumps;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private Vector2 wallJumpForce;
    [SerializeField] private bool isWallJumping;
    [SerializeField] private float wallJumpDuration;

    

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
    [SerializeField] private bool canWallSlide;
    [SerializeField] private float slideSpeed;


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
        idIsWallDetected = Animator.StringToHash("IsWallDetected");
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
        m_animator.SetBool(idIsWallDetected, isWallDetected);
    }
    void FixedUpdate()
    {
        CheckCollicion();
        Move();
        jump();
    }
    private void HandleWall()
    {
        isWallDetected = Physics2D.Raycast(m_transformPlayer.transform.position, Vector2.right * direction, checkWallDistance, groundLayer);
    }
    private void CheckCollicion()
    {
        HandleGound();
        HandleWall();
        HandleWallSlider();
    }

    private void HandleWallSlider()
    {
        canWallSlide = isWallDetected;
        if(!canWallSlide) return;
        canDoubleJump = false;
        slideSpeed = m_gatherInput.Value.y < 0 ? 1 : 0.5f;
        m_rigigbody2D.linearVelocity = new Vector2(m_rigigbody2D.linearVelocityX, m_rigigbody2D.linearVelocity.y * slideSpeed);
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
        if (isWallDetected && !isGrounded) return;
        if(isWallJumping) return;
            Flip();
            m_rigigbody2D.linearVelocity = new Vector2(speed * m_gatherInput.Value.x, m_rigigbody2D.linearVelocityY);
        
    }
   
    private void Flip()
    {
        if(m_gatherInput.Value.x * direction < 0)
        {
            HandleDirection();
        }
    }

    private void HandleDirection()
    {
        m_transformPlayer.localScale = new Vector3(-m_transformPlayer.localScale.x, 1, 1);
        direction *= -1;
    }

    private void jump()
    {
        if (m_gatherInput.IsJumping) 
        {
            if (isGrounded)
            {
                m_rigigbody2D.linearVelocity = new Vector2(speed * m_gatherInput.Value.x, jumpForce);
                canDoubleJump = true;
            }
            else if (isWallDetected)
            {
                WallJump();
            }
            else if(CounterExtraJumps > 0 && canDoubleJump)
            {
                DoubleJump();
            }
        }
        m_gatherInput.IsJumping = false;
    }

    private void WallJump()
    {
        m_rigigbody2D.linearVelocity = new Vector2(wallJumpForce.x * -direction, wallJumpForce.y);
        HandleDirection();
        StartCoroutine(WallJumpRoutine());
    }
    IEnumerator WallJumpRoutine()
    {
        isWallJumping = true;
        yield return  new WaitForSeconds(wallJumpDuration);
        isWallJumping = false;
    }

    private void DoubleJump()
    {
        m_rigigbody2D.linearVelocity = new Vector2(speed * m_gatherInput.Value.x, jumpForce);
        CounterExtraJumps--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(m_transformPlayer.transform.position, new Vector2(m_transformPlayer.position.x + (checkWallDistance * direction), m_transformPlayer.position.y));
    }
}
