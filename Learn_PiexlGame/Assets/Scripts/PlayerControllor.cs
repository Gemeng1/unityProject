using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllor : MonoBehaviour
{
    [SerializeField] private float speed;
    private float xVelocity;
    private Rigidbody2D rig;
    private Animator animator;
    private bool isGround;
    private bool PlayerDead;

    public float randius;
    public GameObject checkPoint;
    public LayerMask platform;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(checkPoint.transform.position, randius, platform);
        MoveMent();
    }

    private void MoveMent()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        

        rig.velocity = new Vector2(xVelocity * speed, rig.velocity.y);
        animator.SetFloat("speed", rig.velocity.x);
        animator.SetBool("isOnGround", isGround);
        if (xVelocity != 0) {
            gameObject.transform.localScale = new Vector3(xVelocity, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fan"))
        {
            rig.velocity = new Vector2(rig.velocity.x, 10f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            animator.SetTrigger("dead");
        }
    }

    private void onPlayerDead()
    {
        PlayerDead = true;
        GameManager.GameOver(PlayerDead);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(checkPoint.transform.position, randius);
    }
}
