using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneC : MonoBehaviour
{
    int SetEng;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Language") == "Ru")
        {
            SetEng = 0;
        }
        else if(PlayerPrefs.GetString("Language") == "En")
        {
            SetEng = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(SetEng == 1)
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(1);
            }
            else if(SetEng == 0)
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(2);
            }
        }
    }
}

