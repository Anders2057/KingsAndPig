using UnityEngine;

public class SpikeBallController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2;
    [SerializeField] private float pushForce;

    private void Start()
    {
        Vector2 pushVector = new Vector2(pushForce, 0);
        rigidbody2.AddForce(pushVector,ForceMode2D.Impulse);
    }
}
