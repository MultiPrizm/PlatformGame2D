using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [Header("audio")]
    [SerializeField] private AudioClip TakeClip;
    private AudioSource _audioSourse;
    private void Start()
    {
        _audioSourse = GetComponent<AudioSource>();
    }

    public void GetStar()
    {
        _audioSourse.clip = TakeClip;
        _audioSourse.Play();
        anim.SetBool("picked", true);
    }

    public void _Destroy()
    {
        Destroy(gameObject);
    }
}
