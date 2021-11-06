using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    // Start is called before the first frame update
    Transform tutorial1;
    Transform tutorial2;
    Transform tutorial3;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void toggleOff(int index) {
        transform.GetChild(index).gameObject.SetActive(false);
    }

    public void toggleOn(int index) {
        transform.GetChild(index).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
