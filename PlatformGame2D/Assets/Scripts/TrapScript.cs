using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private PlayerScript sc;
    [SerializeField] private int Damage = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            sc = collision.gameObject.GetComponent<PlayerScript>();
            sc.Damage(Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            sc = collision.gameObject.GetComponent<PlayerScript>();
            sc.Damage(Damage);
        }
    }
}
