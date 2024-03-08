using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D platformerCollider;
    public LayerMask isGround;
    public float ascentMax;
    public float ascentTime;
    public float ascentSpeed;
    public float descentMax;
    public float descentTime;
    public AnimationCurve jumpAscentCurve;
    public AnimationCurve jumpDescentCurve;
    bool grounded;
    Vector2 initJumpPos;
    bool jumpInput;
    bool descent;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformerCollider = GetComponent<BoxCollider2D>();
        InputManager.RegisterJumpInputCallback(HandleJumpInput);

    }

    void FixedUpdate()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, platformerCollider.size.y/2f + 0.025f, isGround);
        grounded = groundHit.collider != null;
        Debug.Log($"Y Velocity = {rb.velocity.y}");
        if(jumpInput)
        {
            if (ascentTime == 0)
            {
                initJumpPos = transform.position;
            }
            if(ascentTime < 1 && !descent)
            {
                rb.MovePosition(initJumpPos + Vector2.up * ascentMax * jumpAscentCurve.Evaluate(ascentTime));
                ascentTime += Time.deltaTime * ascentSpeed;
                descent = false;
            }
            if(ascentTime > 1)
            {
                descent = true;
            }
        }
        else
        {
            if(!grounded)
            {
                descent = true;
            }
        }

        if(descent)
        {
            descentTime += Time.deltaTime;
            ascentTime = 0;
            rb.MovePosition((Vector2)transform.position + Vector2.down * descentMax * jumpDescentCurve.Evaluate(descentTime));
            if(grounded)
            {
                descent = false;

            }
        }
    }
    void HandleJumpInput(float heldTime)
    {
        jumpInput = heldTime > 0;
    }
}