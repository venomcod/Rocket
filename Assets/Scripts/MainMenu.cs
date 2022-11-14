using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    int LoadLvl;
    [SerializeField] Text loadtext;


    // Start is called before the first frame update
    void Start()
    {
        HideLoadText();
        LoadLvl = PlayerPrefs.GetInt("SaveLvl");
    }

    // Update is called once per frame
    void Update()
    {
        ControlMenu();
        LoadProggres();
    }

    void ControlMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void HideLoadText()
    {
        if(PlayerPrefs.HasKey("SaveLvl") == false)
        {
            loadtext.text = "";
        }
    }

    void LoadProggres()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SceneManager.LoadScene(LoadLvl);
        }
    }


}
