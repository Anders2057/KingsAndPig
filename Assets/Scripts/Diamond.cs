using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Diamond : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private Animator animator;
    [SerializeField] private DiamondTypes diamondTypes;

    private int idPikedDiamond;
    private int idDiamondIndex;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        idPikedDiamond = Animator.StringToHash("PickedDiamond");
        idDiamondIndex = Animator.StringToHash("DiamondIndex");

    }
    private void Start()
    {
        gameManager = GameManager.instance;
        SetRandomDiamond();
    }

    private void SetRandomDiamond()
    {
        if (!GameManager.instance.DiamondHaveRandomLook())
        {
            UpdateDimondType();
            return;
        }
        var randomDiamondIndex = Random.Range(0, 6);
        animator.SetFloat(idDiamondIndex, randomDiamondIndex);
    }

    private void UpdateDimondType()
    {
        animator.SetFloat(idDiamondIndex, (int)diamondTypes);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
           // spriteRenderer.enabled = false;
            m_rigidbody2D.simulated = false;
            gameManager.AddDiamond();
            animator.SetTrigger(idPikedDiamond);
        }
    }
}
