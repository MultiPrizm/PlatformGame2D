using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleportScript : MonoBehaviour
{
    [Header("1-left 2-right")]
    [SerializeField, Range(1, 2)] private int vector;

    [SerializeField] private bool random;

    private string resp;

    public string GetVector()
    {
        if (random)
        {
            vector = Random.Range(1, 2);
        }

        if (vector == 1)
        {
            resp = "left";
        }
        else if (vector == 2)
        {
            resp = "right";
        }

        return resp;
    }

    public Vector3 GetTransf()
    {
        return transform.position;
    }
}
