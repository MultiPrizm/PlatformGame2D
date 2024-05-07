using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private GameObject FinalTriger;

    [SerializeField] private int health = 1;
    [SerializeField] private int speed = 1;
    private bool sheald = false;

    [SerializeField] private BossTeleportScript[] TeleportList;
    private BossTeleportScript tel;

    [SerializeField] private Animator anim;

    private Rigidbody2D rb;

    private int walk_control = -1;

    private bool rolling = false;
    private bool rolling_cooldown = false;

    private bool teport_cooldown = false;

    private int roll_time = 6;
    private int roll_count = 6;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        ControlRollAttack();
    }

    void ControlRollAttack()
    {
        if (!rolling && !rolling_cooldown) {
            anim.SetBool("rollingStart", true);
        }

        if (rolling)
        {
            rb.velocity = new Vector2(walk_control * speed, rb.velocity.y);
        }

        if (roll_time <= 0)
        {
            StartCoroutine(StopingRolling());
        }
    }

    public void RunRollAttack()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;

        rolling = true;
        sheald = true;
        rb.isKinematic = false;
    }

    public void StopRollAttack()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;

        rolling = false;
        rb.velocity = new Vector2(0, 0);

        roll_time = roll_count;
        sheald = false;
        rb.isKinematic = true;
        rb.velocity = new Vector2(0, 0);
    }

    IEnumerator TCooldown()
    {
        teport_cooldown = true;

        yield return new WaitForSeconds(0.5f);

        teport_cooldown = false;
    }

    IEnumerator StopingRolling()
    {
        StartCoroutine(RollingCooldown());

        yield return new WaitForSeconds(0.88f);

        anim.SetBool("rollingStart", false);
    }

    IEnumerator RollingCooldown()
    {
        rolling_cooldown = true;

        yield return new WaitForSeconds(4f);

        rolling_cooldown = false;
    }

    public void Damage(int arg)
    {
        if (!sheald)
        {
            health -= arg;

            anim.SetBool("hit", true);

            if (health <= 0)
            {
                anim.SetBool("isDead", true);
            }
        }
    }

    public void Destroy_()
    {
        FinalTriger.SetActive(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "boss" && !teport_cooldown)
        {
            Debug.Log("Boss: AHAHAHA");

            tel = TeleportList[Random.Range(0, TeleportList.Length)];

            if (tel.GetVector() == "left")
            {
                transform.position = tel.GetTransf() + new Vector3(-1, 0, 0);
                transform.rotation = new Quaternion(0, 0, 0, 0);

                walk_control = -1;
            }
            else if (tel.GetVector() == "right")
            {
                transform.position = tel.GetTransf() + new Vector3(1, 0, 0);
                transform.rotation = new Quaternion(0, 180, 0, 0);

                walk_control = 1;
            }

            roll_time -= 1;

            StartCoroutine(TCooldown());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && rolling)
        {
            collision.gameObject.GetComponent<PlayerScript>().Damage(3);

            rolling = false;

            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0);
        }
    }
}
