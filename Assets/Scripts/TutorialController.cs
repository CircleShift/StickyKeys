using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            //Text text_comp = transform.GetChild(i).gameObject.GetComponent<Text>();
            //text_comp.color = new Color(text_comp.color.r, text_comp.color.g, text_comp.color.b, 0);
        }
        toggleOn(0);
        //transform.GetChild(0).gameObject.SetActive(true);
    }

    public void toggleOff(int index) {
        transform.GetChild(index).gameObject.SetActive(false);
        //StartCoroutine(FadeOut(1f, transform.GetChild(index).gameObject.GetComponent<Text>()));
    }

    public void toggleOn(int index) {
        transform.GetChild(index).gameObject.SetActive(true);
        //StartCoroutine(FadeIn(1f, transform.GetChild(index).gameObject.GetComponent<Text>()));
    }

    public void showTemporary(int index, float time) {
        StartCoroutine(FadeInAndOut(1f, 2f, transform.GetChild(index).gameObject.GetComponent<Text>()));
    }

    public IEnumerator FadeInAndOut(float t1, float t2, Text i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t1));
            yield return null;
        }
        yield return new WaitForSeconds(t2);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t1));
            yield return null;
        }
    }

    public IEnumerator FadeIn(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
 
    public IEnumerator FadeOut(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
