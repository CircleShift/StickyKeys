using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Button MenuButton;
    public Button QuitButton;

	public void GoMenu() {
		SceneManager.LoadScene("TitleScreen");
	}

  	public void QuitGame()
	{
		Application.Quit();
	}

    private void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            MenuButton.onClick.Invoke();
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            QuitButton.onClick.Invoke();
        }
    }
}