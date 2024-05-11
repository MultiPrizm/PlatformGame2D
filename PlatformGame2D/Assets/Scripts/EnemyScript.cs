using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private Animator anim;

    [SerializeField] private int Health = 1;
    [SerializeField] private int Speed = 1;

    //лучи для детекта обрива(налаштування)
    [SerializeField] private float Down_R_DetectionRay = 0;
    [SerializeField] private float Down_L_DetectionRay = 0;
    [SerializeField] private float Down_R_DetectionRayPos = 1;
    [SerializeField] private float Down_L_DetectionRayPos = 1;
    private RaycastHit2D Down_r_Hit;
    private RaycastHit2D Down_l_Hit;

    //лули для детекта стін(налаштування)
    [SerializeField] private float R_DetectionRayPos = 0;
    [SerializeField] private float R_DetectionRayDistance = 1;
    [SerializeField] private float L_DetectionRayPos = 0;
    [SerializeField] private float L_DetectionRayDistance = 1;

    [SerializeField] private float R_Attack_RayDistance = 1;
    [SerializeField] private float R_Attack_RayDistancePos = 0;
    [SerializeField] private float L_Attack_RayDistance = 1;
    [SerializeField] private float L_Attack_RayDistancePos = 0;

    private RaycastHit2D RightHit;
    private RaycastHit2D LeftHit;

    [SerializeField] private bool Move = true;
    [SerializeField] private bool Inveres = false;

    private int walkVector;
    private Rigidbody2D rb;

    private RaycastHit2D AttackHit;
    private RaycastHit2D RadarHit;
    private bool attack_cooldown = false;
    void Start()
    {
        int randomNumber = Random.Range(0, 2);

        walkVector = (randomNumber == 0) ? -1 : 1;

        rb = GetComponent<Rigidbody2D>();

        gameObject.tag = "Enemy";
    }

    void Update()
    {
        Radar();
        if (Move)
        {
            anim.SetBool("HasTarget", true);
            
            WalkControl();
            rb.velocity = new Vector2(walkVector * Speed, rb.velocity.y);
        }
        else
        {
            anim.SetBool("HasTarget", false);
        }
    }

    void WalkControl()
    {
        Down_r_Hit = Physics2D.Raycast(transform.position + new Vector3(Down_R_DetectionRayPos, 0, 0), Vector2.down, Down_R_DetectionRay, 3);
        Down_l_Hit = Physics2D.Raycast(transform.position + new Vector3(Down_L_DetectionRayPos, 0, 0), Vector2.down, Down_L_DetectionRay, 3);

        if (Down_r_Hit.collider != null)
        {
            Debug.DrawRay(transform.position + new Vector3(Down_R_DetectionRayPos, 0, 0), Vector2.down * Down_R_DetectionRay, Color.red);   
        }
        else
        {
            if (Inveres)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }

            walkVector = -1;
            Debug.DrawRay(transform.position + new Vector3(Down_R_DetectionRayPos, 0, 0), Vector2.down * Down_R_DetectionRay, Color.green);
        }

        if (Down_l_Hit.collider != null)
        {
            Debug.DrawRay(transform.position + new Vector3(Down_L_DetectionRayPos, 0, 0), Vector2.down * Down_L_DetectionRay, Color.red);
        }
        else
        {
            if (Inveres)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            walkVector = 1;
            Debug.DrawRay(transform.position + new Vector3(Down_L_DetectionRayPos, 0, 0), Vector2.down * Down_L_DetectionRay, Color.green);
        }

        RightHit = Physics2D.Raycast(transform.position + new Vector3(R_DetectionRayPos, 0, 0), Vector2.right, R_DetectionRayDistance, 3);
        LeftHit = Physics2D.Raycast(transform.position + new Vector3(L_DetectionRayPos, 0, 0), Vector2.left, L_DetectionRayDistance, 3);

        if (RightHit.collider != null)
        {
            walkVector = -1;
            Debug.DrawRay(transform.position + new Vector3(R_DetectionRayPos, 0, 0), Vector2.right * R_DetectionRayDistance, Color.red);

            if (Inveres)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
        else if (RightHit.collider == null)
        {
            Debug.DrawRay(transform.position + new Vector3(R_DetectionRayPos, 0, 0), Vector2.right * R_DetectionRayDistance, Color.green);
        }

        if (LeftHit.collider != null)
        {
            walkVector = 1;
            Debug.DrawRay(transform.position + new Vector3(L_DetectionRayPos, 0, 0), Vector2.left * L_DetectionRayDistance, Color.red);

            if (Inveres)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
        else if (LeftHit.collider == null)
        {
            Debug.DrawRay(transform.position + new Vector3(L_DetectionRayPos, 0, 0), Vector2.left * L_DetectionRayDistance, Color.green);
        }
    }

    public void Damage(int arg)
    {
        Health -= arg;
        anim.SetBool("hit", true);

        if(Health <= 0)
        {
            Move = true;
            anim.SetBool("isDead", true);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void Radar()
    {
        RadarHit = Physics2D.Raycast(transform.position + new Vector3(R_Attack_RayDistancePos, -0.2f, 0), Vector2.right, R_Attack_RayDistance);

        if (RadarHit.collider != null)
        {
            if (RadarHit.collider.gameObject.tag == "Player")
            {
                Debug.DrawRay(transform.position + new Vector3(R_Attack_RayDistancePos, -0.2f, 0), Vector2.right * R_Attack_RayDistance, Color.red);
                StartCoroutine(Attack("r"));
            }
        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(R_Attack_RayDistancePos, -0.2f, 0), Vector2.right * R_Attack_RayDistance, Color.blue);
        }

        RadarHit = Physics2D.Raycast(transform.position + new Vector3(L_Attack_RayDistancePos, 0, 0), Vector2.left, L_Attack_RayDistance);

        if (RadarHit.collider != null)
        {
            if (RadarHit.collider.gameObject.tag == "Player")
            {
                Debug.DrawRay(transform.position + new Vector3(L_Attack_RayDistancePos, -0.2f, 0), Vector2.left * L_Attack_RayDistance, Color.red);
                StartCoroutine(Attack("l"));
            }
            else
            {
                Debug.DrawRay(transform.position + new Vector3(L_Attack_RayDistancePos, -0.2f, 0), Vector2.left * L_Attack_RayDistance, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(L_Attack_RayDistancePos, -0.2f, 0), Vector2.left * L_Attack_RayDistance, Color.blue);
        }
    }

    IEnumerator Attack(string radar)
    {
        if (!attack_cooldown)
        {


            Move = false;
            if (radar == "r")
            {
                if (Inveres)
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);

                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }


            }
            else if (radar == "l")
            {
                if (Inveres)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                }

            }

            attack_cooldown = true;

            yield return new WaitForSeconds(0.5f);

            anim.SetBool("attack", true);
        }

    }

    public void HitDamage()
    {
        Debug.Log("Enemy: Bam");
        AttackHit = Physics2D.Raycast(transform.position + new Vector3(R_Attack_RayDistancePos, -0.2f, 0), Vector2.right, R_Attack_RayDistance);

        if (AttackHit.collider != null)
        {
            if (AttackHit.collider.gameObject.tag == "Player")
            {
                AttackHit.collider.gameObject.GetComponent<PlayerScript>().Damage(1);
            }
        }
   
        AttackHit = Physics2D.Raycast(transform.position + new Vector3(L_Attack_RayDistancePos, -0.2f, 0), Vector2.left, L_Attack_RayDistance);

        if (AttackHit.collider != null)
        {
            if (AttackHit.collider.gameObject.tag == "Player")
            {
                AttackHit.collider.gameObject.GetComponent<PlayerScript>().Damage(1);
            }
        }


        if (walkVector == 1)
        {
            if (Inveres)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
        else if (walkVector == -1)
        {
            if (Inveres)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }

        attack_cooldown = false;
        Move = true;
    }

    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(3, 3, false);
    }
}
