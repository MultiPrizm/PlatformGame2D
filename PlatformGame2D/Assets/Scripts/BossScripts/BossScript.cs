using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    [SerializeField] private int health = 1;
    [SerializeField] private int speed = 1;
    [SerializeField] private BossTeleportScript[] TeleportList;
    [SerializeField] private Animator anim;

    void Start()
    {
        ControlRollAttack();
    }


    void Update()
    {
        
    }

    void ControlRollAttack()
    {
        anim.SetBool("rollingStart", true);
    }

    public void RunRollAttack()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void StopRollAttack()
    {

    }
}
