using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigerScript : MonoBehaviour
{
    [SerializeField] private GameObject trig;
    [SerializeField] private GameObject main_camera;
    [SerializeField] private GameObject camera_focus;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trig.SetActive(true);

            main_camera.GetComponent<MoveCameraScript>().target = camera_focus.GetComponent<Transform>();

            main_camera.GetComponent<Camera>().orthographicSize = 13.18898f;
        }
    }
}
