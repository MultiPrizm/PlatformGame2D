using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiableTerrain : MonoBehaviour
{
    [SerializeField] private GameObject hidenTerrain;
    private void Start()
    {
        hidenTerrain.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("cc");
            hidenTerrain.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
