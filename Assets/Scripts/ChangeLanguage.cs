using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] GameObject ChangeText1;
    [SerializeField] GameObject ChangeText2;
    int SetEng = 1;
    string SetLanguage;

    // Start is called before the first frame update
    void Start()
    {
        CheckLanguage();
        ChangeText1.SetActive(false);
        ChangeText2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SetLaun();
        PressEnter();
    }

    void SetLaun()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(SetEng == 1)
            {
                SetEng = 0;
                ChangeText1.SetActive(true);
                ChangeText2.SetActive(false);
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
            if(SetEng == 0)
            {
                SetEng = 1;
                ChangeText1.SetActive(false);
                ChangeText2.SetActive(true);
            }
        }
    }

    void PressEnter()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            if(SetEng == 1)
            {
                PlayerPrefs.SetString("Language", "En");
                SceneManager.LoadScene(1);
            }
            else if(SetEng == 0)
            {
                PlayerPrefs.SetString("Language", "Ru");
                SceneManager.LoadScene(2);
            }
        }
    }

    void CheckLanguage()
    {
        if(PlayerPrefs.HasKey("Language") == true)
        {
            SetLanguage = PlayerPrefs.GetString("Language");
            if(SetLanguage == "En")
            {
                SceneManager.LoadScene(1);
            }
            else if(SetLanguage == "Ru")
            {
                SceneManager.LoadScene(2);
            }
        }
    }

}
