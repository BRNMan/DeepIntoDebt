using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MallCopState {
    wandering,
    walking,
    targeting,
    alerted,
    circling
}

public class MallCopController : MonoBehaviour
{
    private Vector2 velocity;
    public float speed = 3.0f;
    public GameObject player;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private MallCopState mallCopState;
    private Vector2 chargeDirection;
    static int numberOfEnemies;
    private float elapsedTime;
    private float initialSpeed;
    public float chargeSpeed = 6.0f;
    public float sightDistance = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mallCopState = MallCopState.wandering;
        numberOfEnemies++;
        this.initialSpeed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AI1();
    }

    //Dumb AI, goes straight towards the player.
    private void AI1() {
        Vector3 towardsPlayer3 = Vector3.MoveTowards(this.transform.position, player.transform.position, 1.0f) - this.transform.position;
        Vector2 towardsPlayer = new Vector3(towardsPlayer3.x, towardsPlayer3.y);
        float distance = Vector3.Distance(this.transform.position, player.transform.position);

       if(this.mallCopState == MallCopState.wandering) {
            Vector2 velocity = new Vector2(2.0f*Mathf.PerlinNoise(Time.time/4.0f + numberOfEnemies, 1)-1.0f,
            2.0f*Mathf.PerlinNoise(1, Time.time/4.0f + numberOfEnemies)-1.0f).normalized;
            MoveCop(velocity);
            if(distance < sightDistance) {
                mallCopState = MallCopState.targeting;
                elapsedTime = 0.0f;
            }
        } else if(mallCopState == MallCopState.targeting) {
            MoveCop(towardsPlayer);
        }
    }

    //wandering -> circling -> targeting -> wandering.
    private void AI2() {
        Vector3 towardsPlayer3 = Vector3.MoveTowards(this.transform.position, player.transform.position, 1.0f) - this.transform.position;
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        //Normal Wander
        if(this.mallCopState == MallCopState.wandering) {
            Vector2 velocity = new Vector2(2.0f*Mathf.PerlinNoise(Time.time/4.0f + numberOfEnemies, 1)-1.0f,
            2.0f*Mathf.PerlinNoise(1, Time.time/4.0f + numberOfEnemies)-1.0f).normalized;
            MoveCop(velocity);
            if(distance < sightDistance) {
                mallCopState = MallCopState.circling;
                elapsedTime = 0.0f;
            }
        } else if(this.mallCopState == MallCopState.circling) {
            elapsedTime += Time.deltaTime;
            Vector2 towardsPlayer = new Vector3(towardsPlayer3.x, towardsPlayer3.y);
            Vector2 perpendicular = Vector2.Perpendicular(towardsPlayer);
            MoveCop(perpendicular);
            if(elapsedTime >= 2.0f) {
                this.mallCopState = MallCopState.targeting;
                this.speed = this.chargeSpeed;
                this.chargeDirection = towardsPlayer;
                elapsedTime = 0.0f;
            }
        } else if(this.mallCopState == MallCopState.targeting) {
            elapsedTime += Time.deltaTime;
            MoveCop(this.chargeDirection);
            if(elapsedTime > 3.0f) {
                this.speed = this.initialSpeed;
                this.mallCopState = MallCopState.wandering;
            }
        }
    }

    private void MoveCop(Vector2 velocity) {
        animator.SetFloat("MoveX", velocity.x);
        animator.SetFloat("MoveY", velocity.y);
        animator.SetBool("isWalking", true);
        rigidBody.MovePosition(this.rigidBody.position + velocity*speed*Time.deltaTime); 
    }

}
