using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Player Setting")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerRespawnPoint;
    [SerializeField] float respawnPlayerDelay;

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
    public void RespawnPlayer() => StartCoroutine(RespawnPlayerCoroutine());
    
    IEnumerator RespawnPlayerCoroutine()
    {
        yield return new WaitForSeconds(respawnPlayerDelay);
        GameObject newPlayer = Instantiate(playerPrefab, playerRespawnPoint.position, Quaternion.identity);
        newPlayer.name = "Player";
        _playerController = newPlayer.GetComponent<PlayerController>();
    }
    public void AddScore()
    {
        Debug.Log("Added Score");
    }
    public void AddDiamond() =>  _diamondCollected++;
    public bool DiamondHaveRandomLook() => _diamondHaveRandomLook;
    
}
