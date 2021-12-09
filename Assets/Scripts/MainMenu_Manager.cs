using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    public Animator SceneTransition;

    public void onPlayPressed()
    {
        StartCoroutine("LoadLevel");
    }

    public void onExitPressed()
    {
        Application.Quit();

        //just to quit while in editor
        UnityEditor.EditorApplication.isPlaying = false;
    }

    IEnumerator LoadLevel()
    {
        SceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Overworld");

    }
}
