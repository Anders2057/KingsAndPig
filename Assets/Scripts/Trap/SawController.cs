using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SawController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform [] wayPoins;
    [SerializeField] private Vector3 [] wayPoinsPosition;
    [SerializeField] private int indexWayPoint = 1;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float waitForMove = 1;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _moveDir = 1;

    private Animator animator;
    private int IdSawActive = Animator.StringToHash("SawActive");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        UpdateWaypoints();
        transform.position = wayPoinsPosition[0];
    }

    private void UpdateWaypoints()
    {
        List<SawWayPoint> wayPointsList = new List<SawWayPoint>(GetComponentsInChildren<SawWayPoint>());
        if (wayPointsList.Count != wayPoins.Length)
        {
            wayPoins = new Transform[wayPointsList.Count];
            for (int i = 0; i < wayPointsList.Count; i++)
            {
                wayPoins[i] = wayPointsList[i].transform;
            }
        }

        wayPoinsPosition = new Vector3[wayPoins.Length];
        for (int i = 0; i < wayPoins.Length; i++)
        {
            wayPoinsPosition[i] = wayPoins[i].position;
        }
    }

    private void Update()
    {
        animator.SetBool(IdSawActive,canMove);
        if (!canMove) return;
        transform.position = Vector2.MoveTowards(transform.position, wayPoinsPosition[indexWayPoint], speed * Time.deltaTime);
        if (Vector2.Distance(transform.position,wayPoinsPosition[indexWayPoint]) < 0.1f)
        {
            if (indexWayPoint == wayPoinsPosition.Length -1 || indexWayPoint == 0 )
            {
                _moveDir = _moveDir * -1;
                StartCoroutine(StopMove(waitForMove));
            }
            indexWayPoint = indexWayPoint + _moveDir;
        }
    }

    IEnumerator StopMove(float delayTime)
    {
        canMove = false;
        yield return new WaitForSeconds(delayTime);
        canMove = true;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}


