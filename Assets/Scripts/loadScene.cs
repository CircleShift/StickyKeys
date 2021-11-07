using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public void OnStartClicked()
    {
        //loads scene #1 in our build index, the title is scene #0
        //you can change or add scenese in the build settings of our game
        SceneManager.LoadScene(1);

    }
}
