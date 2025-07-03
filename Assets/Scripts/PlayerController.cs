using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigigbody2D;
    private GatherInput m_gatherInput;
    private Transform m_transformPlayer;
    private Animator m_animator;

    [Header("Move and Jump Settings")]
    [SerializeField]private float speed;
    private int direction = 1;
    
    [SerializeField] private bool isJumping;

    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJumps;
    [SerializeField] private int CounterExtraJumps;
    private int idSpeed;

    [Header("Ground Settings")]
    [SerializeField] private Transform rFoot;
    [SerializeField] private Transform lFoot;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayLegnth;
    [SerializeField] private LayerMask groundLayer;
    private int idIsGrounded;

    void Start()
    {
        m_gatherInput = GetComponent<GatherInput>();
        m_transformPlayer = GetComponent<Transform>();
        m_rigigbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
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
        Move();
        jump();
        CheckGound();
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
            }
            if(CounterExtraJumps > 0)
            {
                m_rigigbody2D.linearVelocity = new Vector2(speed * m_gatherInput.ValueX, jumpForce);
                CounterExtraJumps--;
            }
        }
        m_gatherInput.IsJumping = false;
    }
    private void CheckGound()
    {
        RaycastHit2D lFootRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLegnth, groundLayer);
        RaycastHit2D rFootRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLegnth, groundLayer);

        if (lFootRay || rFootRay)
        {
            isGrounded = true;
            CounterExtraJumps = extraJumps;
        }
        else 
        {
            isGrounded = false;
        }
    }
}
