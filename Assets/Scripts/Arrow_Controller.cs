using UnityEngine;

public class Arrow_Controller : TrampolineController
{

    [Header("Additional Info")]
    [SerializeField] private float respawnTime;
    [SerializeField] private bool rotacionRight;
    [SerializeField] private float rotacionSpeed = 120;
    
    private int direction = -1;
    [Space]
    [SerializeField] private float scaleUpSpeed = 10f;
    [SerializeField] private Vector3 targetScale;

    private void Start()
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    private void Update()
    {
        HandleScaleUp();
        HandleRotacion();
    }

    private void HandleScaleUp()
    {
        if (transform.localScale.x < targetScale.x)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleUpSpeed * Time.deltaTime);
        }
    }

    private void HandleRotacion()
    {
        direction = rotacionRight ? -1 : 1;
        transform.Rotate(0, 0, (rotacionSpeed * direction) * Time.deltaTime);
    }

    private void DestroyMe()
    {
        GameObject arrowPrefab = GameManager.Instance.arrowPrefab;
        Vector3 respawnPosition = transform.position;
        GameManager.Instance.CreateObject( arrowPrefab, respawnPosition, respawnTime );
        Destroy(gameObject);
    }
}
