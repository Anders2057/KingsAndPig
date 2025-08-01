using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Setting")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerRespawnPoint;
    [SerializeField] float respawnPlayerDelay;

    [SerializeField] private PlayerController _playerController;
    public PlayerController PlayerController { get => _playerController; }

    [Header("Respawn Settings")]
    public bool hasCheckPointActive;
    public Vector3 checkPointRespawnPosition;

    [Header("Diamond Manager")]
    [SerializeField]private int _diamondCollected;
    [SerializeField] private bool _diamondHaveRandomLook;

    public int DiamondCollected { get => _diamondCollected; }

    private void Awake()
    {
        if(Instance == null ) Instance = this;
        else Destroy(gameObject);
    }
    public void RespawnPlayer()
    {
        if (hasCheckPointActive)
        {
            playerRespawnPoint.position = checkPointRespawnPosition;
        }
        StartCoroutine(RespawnPlayerCoroutine());
    } 
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
