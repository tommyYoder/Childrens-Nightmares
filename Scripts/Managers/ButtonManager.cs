using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioSource ClickSound;
    public AudioSource MainSound;

    public void NewGameBtn(string newGameLevel)
    {
        ClickSound.Play();
        SceneManager.LoadScene(newGameLevel);
        MainSound.Stop();
    }
    public void ExitGameBtn()
    {
        ClickSound.Play();
        MainSound.Stop();
        Application.Quit();
    }
}
