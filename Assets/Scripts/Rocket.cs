using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotSpeed = 100f;
    [SerializeField] float flySpeed = 100f;
    [SerializeField] AudioClip flySnd;
    [SerializeField] AudioClip finishSnd;
    [SerializeField] AudioClip deathSnd;
    [SerializeField] ParticleSystem finishPr;
    [SerializeField] ParticleSystem flyPr;
    [SerializeField] ParticleSystem deathPr;
    bool God = false;
    Rigidbody rigidBody; // создаём переменую для использовнание RigidBody
    AudioSource audiosource; // создаём для audiosource

    enum State { Play, Dead, Next };
    State state = State.Play;


    // Start is called before the first frame update
    void Start()
    {
        state = State.Play;
        audiosource = GetComponent<AudioSource>(); // получаем компонет из обьекта и добовляем в переменую
        rigidBody = GetComponent<Rigidbody>(); // тож самое
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
        if (Input.GetKey(KeyCode.Space))
        {
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
                print("Скучно");
                break;
            case "Bonus":
                print("Вкинулся");
                break;
            case "Finish":
                Finish();
                break;
            default:
                Death();
                break;

        }
    }

    void Death()
    {
        print("Не везёт");
        state = State.Dead;
        audiosource.Stop();
        audiosource.PlayOneShot(deathSnd);
        deathPr.Play();
        Invoke("LoadFirstLvl", 2f);
    }

    void Finish()
    {
        print("Ура победа!");
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

        if (nextLvlIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLvlIndex = 1;
        }

        SceneManager.LoadScene(nextLvlIndex);
    }

    void LoadFirstLvl()
    {
        SceneManager.LoadScene(1);
    }
}


