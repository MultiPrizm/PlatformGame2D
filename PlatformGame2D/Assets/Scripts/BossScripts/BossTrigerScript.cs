using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigerScript : MonoBehaviour
{
    [SerializeField] private GameObject trig;
    [SerializeField] private GameObject main_camera;
    [SerializeField] private GameObject camera_focus;
    private Camera camera_;
    private bool IsScale = false;

    private void Start()
    {
        camera_ = main_camera.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trig.SetActive(true);

            main_camera.GetComponent<MoveCameraScript>().target = camera_focus.GetComponent<Transform>();

            main_camera.GetComponent<MoveCameraScript>().smoothSpeed = 0.02f;

            //main_camera.GetComponent<Camera>().orthographicSize = 13.18898f;
            IsScale = true;
        }
    }

    private void Update()
    {
        if (camera_.orthographicSize <= 13.18898f && IsScale)
        {
            camera_.orthographicSize += 1.5f * Time.fixedDeltaTime;
        }
    }
}
