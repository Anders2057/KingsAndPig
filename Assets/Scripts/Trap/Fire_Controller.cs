using System;
using System.Collections;
using UnityEngine;

public class Fire_Controller : MonoBehaviour
{
    [SerializeField] private float offDuration;
    [SerializeField] private FireButton_Controller fireButton;

    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;
    private bool isActive;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        if (fireButton == null) 
        {
            Debug.LogWarning("no fire button assigned" + gameObject.name);
        }
        SetFire(true);
    }
    public void SwitchOffFire()
    {
        if (isActive == false) return;
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        SetFire(false);
        yield return new WaitForSeconds(offDuration);
        SetFire(true);
    }

    private void SetFire(bool active)
    {
        animator.SetBool("Active",active);
        capsuleCollider2D.enabled = active;
        isActive = active;
    }
}
