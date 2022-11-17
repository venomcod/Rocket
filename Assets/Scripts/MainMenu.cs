using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    int LoadLvl;
    [SerializeField] Text loadtext;
    [SerializeField] GameObject ModeText;
    [SerializeField] GameObject ModeText1;
    [SerializeField] GameObject MainText1;
    [SerializeField] GameObject MainText2;
    [SerializeField] GameObject MainText3;
    bool ModeTextOn = false;


    // Start is called before the first frame update
    void Start()
    {
        ModeText.SetActive(false);
        ModeText1.SetActive(false);
        HideLoadText();
        LoadLvl = PlayerPrefs.GetInt("SaveLvl");
    }

    // Update is called once per frame
    void Update()
    {
        ControlMenu();
        LoadProggres();
        SetGameMode();
    }

    void ControlMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartNewGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(ModeTextOn == true)
            {
                CloseGameMode();
            }
            else
            {
                Application.Quit();
            }
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

    void StartNewGame()
    {
        ModeTextOn = true;
        ModeText.SetActive(true);
        ModeText1.SetActive(true);
        MainText1.SetActive(false);
        MainText2.SetActive(false);
        MainText3.SetActive(false);
    }
    void SetGameMode()
    {
        if(ModeTextOn == true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                PlayerPrefs.SetInt("GameMode", 1);
                PlayerPrefs.DeleteKey("SaveLvl");
                SceneManager.LoadScene(3);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                PlayerPrefs.SetInt("GameMode", 0);
                SceneManager.LoadScene(3);
            }
        }
    }

    void CloseGameMode()
    {
        ModeTextOn = false;
        ModeText.SetActive(false);
        ModeText1.SetActive(false);
        MainText1.SetActive(true);
        MainText2.SetActive(true);
        MainText3.SetActive(true);
    }
}
