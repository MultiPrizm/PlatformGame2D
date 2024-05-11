using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBossScript : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float speed = 1;
    private Rigidbody2D rb;

    private bool speed_up_level1 = false;
    private bool speed_up_level2 = false;
    private bool speed_up_level3 = false;

    private bool move = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SpeedUp());
    }

    void Update()
    {
        if (move)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        SetDanger();
    }

    private void SetDanger()
    {
        Debug.Log(Vector3.Distance(transform.position, player.position));

        if (Vector3.Distance(transform.position, player.position) > 10f && !speed_up_level1)
        {
            speed += 1;
            speed_up_level1 = true;
        }
        else if (Vector3.Distance(transform.position, player.position) < 10f && speed_up_level1)
        {
            speed -= 1;
            speed_up_level1 = false;
        }

        if (Vector3.Distance(transform.position, player.position) > 15f && !speed_up_level2)
        {
            speed += 2;
            speed_up_level2 = true;
        }
        else if (Vector3.Distance(transform.position, player.position) < 15f && speed_up_level2)
        {
            speed -= 2;
            speed_up_level2 = false;
        }

        if (Vector3.Distance(transform.position, player.position) > 24f && !speed_up_level3)
        {
            speed += 2;
            speed_up_level3 = true;
        }
        else if (Vector3.Distance(transform.position, player.position) < 24f && speed_up_level3)
        {
            speed -= 2;
            speed_up_level3 = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().Damage(3);
            move = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().Damage(10);
        }
    }

    private IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(10f);
        speed += 3;
    }
}
