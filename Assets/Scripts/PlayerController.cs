using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigigbody2d;
    private GatherInput m_gatherInput;
    private Transform m_transformPlayer;
    [SerializeField]private float speed;

    private int direction = 1;

    void Start()
    {
        m_gatherInput = GetComponent<GatherInput>();
        m_transformPlayer = GetComponent<Transform>();
        m_rigigbody2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Flip();
        m_rigigbody2d.linearVelocity = new Vector2(speed * m_gatherInput.ValueX, m_rigigbody2d.linearVelocityY);
    }

    private void Flip()
    {
        if(m_gatherInput.ValueX * direction < 0)
        {
            m_transformPlayer.localScale = new Vector3(-m_transformPlayer.localScale.x,1,1);
            direction *= -1;
        }
    }
}
