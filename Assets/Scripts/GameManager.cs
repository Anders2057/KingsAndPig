using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private PlayerController _playerController;
    public PlayerController PlayerController { get => _playerController; }

    [SerializeField]private int _diamondCollected;
    public int DiamondCollected { get => _diamondCollected; }

    [SerializeField] private bool _diamondHaveRandomLook;
    

    private void Awake()
    {
        if(instance == null ) instance = this;
        else Destroy(gameObject);
    }
    public void AddScore()
    {
        Debug.Log("Added Score");
    }
    public void AddDiamond() =>  _diamondCollected++;
    public bool DiamondHaveRandomLook() => _diamondHaveRandomLook;
    
}
