using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private int Health = 1;
    [SerializeField] private int Speed = 1;

    //лучи для детекта обрива(налаштування)
    [SerializeField] private float DownRightDetectionRay = 0;
    [SerializeField] private float DownLeftDetectionRay = 0;
    [SerializeField] private float DownRightDetectionRayPos = 1;
    [SerializeField] private float DownLeftDetectionRayPos = 1;
    private RaycastHit2D DownrightHit;
    private RaycastHit2D DownleftHit;

    //лули для детекта стін(налаштування)
    [SerializeField] private float RightDetectionRayPos = 0;
    [SerializeField] private float RightDetectionRayDistance = 1;
    [SerializeField] private float LeftDetectionRayPos = 0;
    [SerializeField] private float LeftDetectionRayDistance = 1;
    private RaycastHit2D RightHit;
    private RaycastHit2D LeftHit;

    [SerializeField] private bool Move = true;
    [SerializeField] private bool Inveres = false;

    private int walkVector;
    private Rigidbody2D rb;
    void Start()
    {
        int randomNumber = Random.Range(0, 2);

        walkVector = (randomNumber == 0) ? -1 : 1;

        rb = GetComponent<Rigidbody2D>();

        gameObject.tag = "Enemy";
    }

    void Update()
    {
        WalkControl();
        if (Move)
        {
            rb.velocity = new Vector2(walkVector * Speed, rb.velocity.y);
        }
    }

    void WalkControl()
    {
        DownrightHit = Physics2D.Raycast(transform.position + new Vector3(DownRightDetectionRay, 0, 0), Vector2.down, DownRightDetectionRayPos);
        DownleftHit = Physics2D.Raycast(transform.position + new Vector3(DownLeftDetectionRay, 0, 0), Vector2.down, DownLeftDetectionRayPos);

        if (DownrightHit.collider != null)
        {
            Debug.DrawRay(transform.position + new Vector3(DownRightDetectionRay, 0, 0), Vector2.down * DownRightDetectionRayPos, Color.red);   
        }
        else if (DownrightHit.collider == null)
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
            Debug.DrawRay(transform.position + new Vector3(DownRightDetectionRay, 0, 0), Vector2.down * DownRightDetectionRayPos, Color.green);
        }

        if (DownleftHit.collider != null)
        {
            Debug.DrawRay(transform.position + new Vector3(DownLeftDetectionRay, 0, 0), Vector2.down * DownLeftDetectionRayPos, Color.red);
        }
        else if (DownleftHit.collider == null)
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
            Debug.DrawRay(transform.position + new Vector3(DownLeftDetectionRay, 0, 0), Vector2.down * DownLeftDetectionRayPos, Color.green);
        }

        RightHit = Physics2D.Raycast(transform.position + new Vector3(RightDetectionRayPos, 0, 0), Vector2.right, RightDetectionRayDistance);
        LeftHit = Physics2D.Raycast(transform.position + new Vector3(LeftDetectionRayPos, 0, 0), Vector2.left, LeftDetectionRayDistance);

        if (RightHit.collider != null)
        {
            walkVector = -1;
            Debug.DrawRay(transform.position + new Vector3(RightDetectionRayPos, 0, 0), Vector2.right * RightDetectionRayDistance, Color.red);
        }
        else if (RightHit.collider == null)
        {
            Debug.DrawRay(transform.position + new Vector3(RightDetectionRayPos, 0, 0), Vector2.right * RightDetectionRayDistance, Color.green);
        }

        if (LeftHit.collider != null)
        {
            walkVector = 1;
            Debug.DrawRay(transform.position + new Vector3(LeftDetectionRayPos, 0, 0), Vector2.left * LeftDetectionRayDistance, Color.red);
        }
        else if (LeftHit.collider == null)
        {
            Debug.DrawRay(transform.position + new Vector3(LeftDetectionRayPos, 0, 0), Vector2.left * LeftDetectionRayDistance, Color.green);
        }
    }

    public void Damage(int arg)
    {
        Health -= arg;

        if(Health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(6, 6, false);
    }
}
