using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] GameObject ChangeText1;
    [SerializeField] GameObject ChangeText2;
    int SetEng = 1;

    // Start is called before the first frame update
    void Start()
    {
        ChangeText1.SetActive(false);
        ChangeText2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SetLaun();
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
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
            if(SetEng == 0)
            {
                SetEng = 1;
                ChangeText1.SetActive(false);
                ChangeText2.SetActive(true);
            }
        }
    }

}
