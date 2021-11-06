using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTriggerEnter2D : MonoBehaviour
{
    public GameObject EnabledObj;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            EnabledObj.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            EnabledObj.SetActive(false);
        }
    }
}
