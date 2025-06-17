using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigigbody2d;
    private GatherInput m_gatherInput;
    private Transform m_transformPlayer;
    [SerializeField]private float speed;

    void Start()
    {
        m_gatherInput = GetComponent<GatherInput>();
        m_transformPlayer = GetComponent<Transform>();
        m_rigigbody2d = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        m_rigigbody2d.linearVelocity = new Vector2(speed * m_gatherInput.ValueX, m_rigigbody2d.linearVelocityY);
    }
}
