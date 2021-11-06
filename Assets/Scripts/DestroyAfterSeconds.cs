using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public int time = 2;

    private void Start()
    {
        Destroy(gameObject, time);
    }
}
