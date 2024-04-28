using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float PlayerSpeed;
    [SerializeField] private float PlayerJump;
    [SerializeField] private float PowerGravityScale;
    [SerializeField] private float DefalteGravityScale;
    private bool isgrounded = true;

    private Rigidbody2D rb;
    private RaycastHit2D hitground;
    [SerializeField] private Animator Anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * PlayerSpeed, rb.velocity.y);

        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            Anim.SetFloat("RunState", 0.5f);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            Anim.SetFloat("RunState", 0.5f);
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Anim.SetFloat("RunState", 0);
        }
    }

    void Jump()
    {
        //рейкаст який чекає землю
        hitground = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 3);

        if (hitground.collider != null)
        {
            if (hitground.collider.tag != "Player")
            {
                Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.red);
                isgrounded = true;
            }
        }
        if (hitground.collider == null)
        {
            Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.green);
        }

        float input = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
        {
            isgrounded = false;

            Vector2 jumpVelocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2f * PlayerJump * Mathf.Abs(Physics2D.gravity.y)));

            rb.velocity = jumpVelocity;
        }

        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = DefalteGravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = PowerGravityScale;
        }

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //isgrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //isgrounded = false;
    }

    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(3, 3, false);
    }

}
