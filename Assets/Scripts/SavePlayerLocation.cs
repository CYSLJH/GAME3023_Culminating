using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadLocation();
        //GameSaver.OnSaveEvent.AddListener(SaveLocation);
        //GameSaver.OnLoadEvent.AddListener(LoadLocation);
    }

    public void SaveLocation()
    {
        PlayerPrefs.SetFloat("X", transform.position.x);
        PlayerPrefs.SetFloat("Y", transform.position.y);
        PlayerPrefs.SetFloat("Z", transform.position.z);

        Debug.Log("Location Saved");
    }

    public void LoadLocation()
    {
        if (!PlayerPrefs.HasKey("X"))
        {
            Debug.Log("No save data?");
            return;
        }
        if (!PlayerPrefs.HasKey("Y"))
        {
            Debug.Log("No save data?");
            return;
        }
        if (!PlayerPrefs.HasKey("Z"))
        {
            Debug.Log("No save data?");
            return;
        }

        transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));

        Debug.Log("Location Loaded");
    }
}
