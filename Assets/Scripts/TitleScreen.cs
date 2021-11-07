using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Button PlayButton;
    public Button QuitButton;
    public Button CreditsButton;
    public Button BackButton;

    private bool menuActive = true;

    private Animator anim;

    void Start()
    {
        anim = PlayButton.GetComponent<Animator>();
        //anim["Selected"].layer = 1;
    }
        public void OnStartClicked() {
        //loads scene #1 in our build index, the title is scene #0
        //you can change or add scenese in the build settings of our game
        SceneManager.LoadScene(1);

        }
  public void QuitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKeyDown("return") && menuActive)
        {
            anim.Play("Selected");
        }
        if(Input.GetKey(KeyCode.Escape) &&menuActive)
        {
            QuitButton.onClick.Invoke();
        }
        if ((Input.GetKey(KeyCode.Slash) || Input.GetKey(KeyCode.Question)) && menuActive)
        {
            CreditsButton.onClick.Invoke();
            menuActive = false;
        }
        if (Input.GetKey(KeyCode.Escape) && menuActive == false)
        {
            BackButton.onClick.Invoke();
            menuActive = true;
        }
    }
    

}
