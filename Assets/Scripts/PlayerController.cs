using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigigbody2D;
    private GatherInput m_gatherInput;
    private Transform m_transformPlayer;
    private Animator m_animator;


    [SerializeField]private float speed;
    private int idSpeed;

    private int direction = 1;

    void Start()
    {
        m_gatherInput = GetComponent<GatherInput>();
        m_transformPlayer = GetComponent<Transform>();
        m_rigigbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        idSpeed = Animator.StringToHash("Speed");
    }
    private void Update()
    {
        SetAnimatorValue();
    }

    void FixedUpdate()
    {
        Move();
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
    private void SetAnimatorValue()
    {
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigigbody2D.linearVelocityX));
    }
}
