using System;
using Unity.Cinemachine;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   [SerializeField] private Animator animator;
   [SerializeField] private bool isActive;

    private int IdFlagUp = Animator.StringToHash("FlagUp");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive) return;

        if (collision.CompareTag("Player"))
        {
            ActiveCheckPoint();
        }
        GameManager.Instance.hasCheckPointActive = true;
        GameManager.Instance.checkPointRespawnPosition = transform.position;
    }

    private void ActiveCheckPoint()
    {
        isActive = true;
        animator.SetTrigger(IdFlagUp);
    }
}
