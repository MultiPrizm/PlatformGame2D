using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void GetStar()
    {
        anim.SetBool("picked", true);
    }

    public void _Destroy()
    {
        Destroy(gameObject);
    }
}
