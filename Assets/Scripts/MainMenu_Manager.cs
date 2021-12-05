using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayPressed()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void onExitPressed()
    {
        Application.Quit();

        //just to quit while in editor
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
