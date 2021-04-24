using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  PlayerState {
    idle,
    walking
}

public class PlayerMovement : MonoBehaviour
{

    public float speed = 4.0f;
    private Vector2 velocity;
    public PlayerState playerState;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        if(Vector2.Distance(velocity, Vector2.zero) > 0.01) {
            playerState = PlayerState.walking;
            myAnimator.SetFloat("MoveX", velocity.x);
            myAnimator.SetFloat("MoveY", velocity.y);
            myAnimator.SetBool("isWalking", true);
            
            velocity.Normalize();
            myRigidBody.MovePosition(myRigidBody.position + velocity*speed*Time.deltaTime);
        } else {
            playerState = PlayerState.idle;
            myAnimator.SetBool("isWalking", false);
        }
    }
}
