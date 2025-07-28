using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Dead_Area : MonoBehaviour
{
    [SerializeField] private PlayerController player;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            player.Die();
            GameManager.instance.RespawnPlayer();
        }
    }
}
