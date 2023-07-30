using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField] Text energyText;
    [SerializeField] int energy = 2000;
    [SerializeField] int energytoAdd = 1000;
    [SerializeField] int energyApply = 5;
    [SerializeField] float rotSpeed = 100f;
    [SerializeField] float flySpeed = 100f;
    [SerializeField] AudioClip flySnd;
    [SerializeField] AudioClip finishSnd;
    [SerializeField] AudioClip deathSnd;
    [SerializeField] ParticleSystem finishPr;
    [SerializeField] ParticleSystem flyPr;
    [SerializeField] ParticleSystem deathPr;
    [SerializeField] int lvlSave;
    int HardMode;
    bool God = false;
    Rigidbody rigidBody; // ������ ��������� ��� �������������� RigidBody
    AudioSource audiosource; // ������ ��� audiosource

    enum State { Play, Dead, Next };
    State state = State.Play;


    // Start is called before the first frame update
    void Start()
    {
        HardMode = PlayerPrefs.GetInt("GameMode");
        energyText.text = energy.ToString();
        state = State.Play;
        audiosource = GetComponent<AudioSource>(); // �������� �������� �� ������� � ��������� � ���������
        rigidBody = GetComponent<Rigidbody>(); // ��� �����
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Play)
        {
            Launch();
            Rotation();
            KeyExit();
        }
        if (Debug.isDebugBuild)
        {
            DebugKeys();
        }
    }
    void Launch()
    {
        if (Input.GetKey(KeyCode.Space) && energy > 0)
        {
            energy -= Mathf.RoundToInt(energyApply*Time.deltaTime);
            energyText.text = energy.ToString();
            rigidBody.AddRelativeForce(Vector3.up * flySpeed * Time.deltaTime);
            if (audiosource.isPlaying == false)
                audiosource.PlayOneShot(flySnd);
            flyPr.Play();
        }
        else
        {
            audiosource.Pause();
            flyPr.Stop();
        }
    }
    void Rotation()
    {
        float roationSpeed = rotSpeed * Time.deltaTime;

        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * roationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * roationSpeed);
        }
        rigidBody.freezeRotation = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Play || God)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Ok");
                break;
            case "Bonus":
                GetEnergy(energytoAdd, collision.gameObject);
                break;
            case "Finish":
                Finish();
                break;
            default:
                Death();
                break;

        }
    }

    void GetEnergy(int energyToAdd, GameObject batteryObj)
    {
        batteryObj.GetComponent<BoxCollider>().enabled = false;
        energy += energyToAdd;
        energyText.text = energy.ToString();
        Destroy(batteryObj);
    }

    void Death()
    {
        print("Death");
        state = State.Dead;
        audiosource.Stop();
        audiosource.PlayOneShot(deathSnd);
        deathPr.Play();
        Invoke("LoadFirstLvl", 2f);
    }

    void Finish()
    {
        print("Win");
        state = State.Next;
        audiosource.Stop();
        audiosource.PlayOneShot(finishSnd);
        finishPr.Play();
        Invoke("LoadNextLvl", 2f);
    }


    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLvl();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            God = !God;
        }
    }

    void KeyExit()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void LoadNextLvl()
    {
        int currentLvlIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLvlIndex = currentLvlIndex + 1;
        lvlSave = nextLvlIndex;
        SaveProggres();

        if (nextLvlIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLvlIndex = 1;
        }

        SceneManager.LoadScene(nextLvlIndex);
    }

    void LoadFirstLvl()
    {
        int lvlDeath;

        lvlDeath = SceneManager.GetActiveScene().buildIndex;

        if(HardMode == 1)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(lvlDeath);
        }
    }

    void SaveProggres()
    {
        PlayerPrefs.SetInt("SaveLvl", lvlSave);
        PlayerPrefs.SetInt("GameMode", HardMode);
    }
}


