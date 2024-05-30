using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float PlayerSpeed;
    [SerializeField] private float PlayerJump;
    [SerializeField] private float PowerGravityScale;
    [SerializeField] private float DefalteGravityScale;
    private bool jump = true;
    private bool double_jump = true;
    private bool lock_jump = false;

    private Rigidbody2D rb;
    private RaycastHit2D hitground;
    private RaycastHit2D hitattack;
    private RaycastHit2D platform_lgnore;
    [SerializeField] private Animator Anim;

    private int health = 3;
    private bool is_live = true;
    private bool is_sheald = false;

    private int stars = 0;

    private bool move = true;

    private bool attack_cooldown = false;

    private bool ignore_collision_cooldown = false;

    private bool Grounded = true;
    [Header("audio")]
    [SerializeField] private AudioClip runClip;
    private AudioSource _audioSourse;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _audioSourse = GetComponent<AudioSource>();
        _audioSourse.clip = runClip;
        _audioSourse.Stop();
    }

    void Update()
    {
        if (move) {
            Jump();
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * PlayerSpeed, rb.velocity.y);
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && Grounded == true)
                _audioSourse.Play();
            if((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)))
                _audioSourse.Stop();
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
                _audioSourse.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(PlatformIgnore());
            }
        }
        Attack();
    }

    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jump || double_jump) {

                if (jump)
                {
                    //Anim.SetBool("Jump", true);
                    jump = false;
                }
                else if (double_jump)
                {
                    double_jump = false;
                }

                Vector2 jumpVelocity = new Vector2(rb.velocity.x, Mathf.Sqrt(2f * PlayerJump * Mathf.Abs(Physics2D.gravity.y)));

                rb.velocity = jumpVelocity;
                StartCoroutine(LockJump(0.5f));
            }
        }

        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = DefalteGravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = PowerGravityScale;
        }

        //рейкаст який чекає землю(по приколу)
        hitground = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);

        if (hitground.collider != null)
        {
            if (hitground.collider.tag != "Player")
            {
                Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.red);
            }
        }
        if (hitground.collider == null)
        {
            Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.green);
        }

    }

    void Attack()
    {
        if (transform.rotation == new Quaternion(0, 180, 0, 0))
        {
            Debug.DrawRay(transform.position + new Vector3(0.7f, 1, 0), Vector2.right * 1f, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(-0.7f, 1, 0), Vector2.left * 1f, Color.blue);
        }

        if (Input.GetMouseButtonDown(0) && !attack_cooldown)
        {

            if (transform.rotation == new Quaternion(0, 180, 0, 0))
            {
                hitattack = Physics2D.Raycast(transform.position + new Vector3(0.7f, 1, 0), Vector2.right, 1f);

                if (hitattack.collider != null)
                {
                    if (hitattack.collider.gameObject.tag == "Enemy")
                    {
                        hitattack.collider.gameObject.GetComponent<EnemyScript>().Damage(1);
                    }
                    else if (hitattack.collider.gameObject.tag == "boss")
                    {
                        hitattack.collider.gameObject.GetComponent<BossScript>().Damage(1);
                    }
                }
            }
            else
            {
                hitattack = Physics2D.Raycast(transform.position + new Vector3(-0.7f, 1, 0), Vector2.left, 1f);

                if (hitattack.collider != null)
                {
                    if (hitattack.collider.gameObject.tag == "Enemy")
                    {
                        hitattack.collider.gameObject.GetComponent<EnemyScript>().Damage(1);
                    }
                    else if (hitattack.collider.gameObject.tag == "boss")
                    {
                        hitattack.collider.gameObject.GetComponent<BossScript>().Damage(1);
                    }
                }
            }
            Anim.SetBool("Attack", true);

            StartCoroutine(AttackColldown());
        }
    }

    IEnumerator PlatformIgnore()
    {
        if (!ignore_collision_cooldown) {
            ignore_collision_cooldown = true;

            platform_lgnore = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
            bool ignore = false;

            if (platform_lgnore.collider != null)
            {
                if (platform_lgnore.collider.gameObject.tag == "Platform")
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), platform_lgnore.collider.GetComponent<Collider2D>(), true);
                    ignore = true;
                }
            }

            yield return new WaitForSeconds(0.5f);

            if (ignore)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), platform_lgnore.collider.GetComponent<Collider2D>(), false);
            }

            ignore_collision_cooldown = false;
        }
    }

    IEnumerator AttackColldown()
    {
        attack_cooldown = true;
        yield return new WaitForSeconds(1);
        attack_cooldown = false;
    }

    IEnumerator LockJump(float sec)
    {
        yield return new WaitForSeconds(sec);
        lock_jump = false;
    }

    public void Move(bool arg)
    {
        if (arg)
        {
            move = true;
            rb.isKinematic = false;
        }
        else
        {
            move = false;
            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void Damage(int arg)
    {
        if (!is_sheald) {
            Debug.Log("AH");

            StartCoroutine(SetSheald(1f));
            health -= arg;

            if (health <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }

    public bool IsLivePlayer()
    {
        return is_live;
    }

    public int GetStars()
    {
        return stars;
    }

    public int GetHealth()
    {
        return health;
    }

    IEnumerator SetSheald(float sec)
    {
        is_sheald = true;
        yield return new WaitForSeconds(sec);
        is_sheald = false;
    }

    IEnumerator Death()
    {
        Anim.SetBool("Die", true);
        Move(false);

        yield return new WaitForSeconds(1.0f);

        is_live = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Input.GetAxis("Horizontal") != 0) _audioSourse.Play();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //рейкаст який чекає землю
        hitground = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);

        if (hitground.collider != null && !lock_jump)
        {
            if (hitground.collider.tag != "Player")
            {
                Grounded = true;
                lock_jump = true;
                jump = true;
                double_jump = true;
                //Anim.SetBool("Grounded", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "star")
        {
            Debug.Log("Player: Star!!!");
            collision.gameObject.GetComponent<StarScript>().GetStar();
            stars += 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Grounded = false;
        _audioSourse.Stop();
        //isgrounded = false;
    }

}
