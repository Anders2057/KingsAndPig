using System;
using UnityEngine;

public class EnemyPig : Enemy
{
    private static readonly int Xvelocity = Animator.StringToHash("xVelocity");

    protected override void Update()
    {
        base.Update();
         
        if (isDead) return;

        HandleCollision();
        HandleMovement();

        float xVelocity = Mathf.Abs(rigidbody2D.linearVelocity.x);
        if (!isGrounded)
            xVelocity = 0f;

        animator.SetFloat(Xvelocity, xVelocity);
        if (isGrounded)
            HandleTurnAround();
    }

    private void HandleTurnAround()
    {
        if (!isGroundInFrontDetected || isWallDetected)
        {
            if(!isGrounded) return;
            Flip();
            idleTimer = idleDuration;
            rigidbody2D.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if(idleTimer > 0) return;
        if (!isGrounded) return;

        rigidbody2D.linearVelocity = new Vector2(moveSpeed * facingDirection,rigidbody2D.linearVelocityY);
    }

   
}
