using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public static LoadLevel instance;
    private GameObject[] Levels;

    public AudioClip DoorSound;

    //to know where
    public int iLevelToLoad;
    public string sLevelToLoad;

    public int count = -1;

    public bool intToLoadLvl = false;
    private bool inTheDoor = false;

    //bad code for simple enemy level
    public bool mark = true;


    public SpriteRenderer fadeAway;

    //########################################################################
    void Start()
    {
        if (name == "NextLevel")
        {
            DontDestroyOnLoad(gameObject);
        }
         if (instance == null)
        {
            instance = this;
        }
        fadeAway = GameObject.Find("FadeOut").GetComponent<SpriteRenderer>();

    }
    //########################################################################
    private void OnLevelWasLoaded(int level)
    {
        Levels = GameObject.FindGameObjectsWithTag("Levels");
        if (Levels.Length > 1)
        {
            Destroy(Levels[1]);
        }
    }
    //########################################################################
    void Update()
    {
        if (inTheDoor && Input.GetKeyDown(KeyCode.F))
        {
            if (gameObject.name == "NextLevel")
            {
                if (instance.count == -1)
                {
                    LoadScene();
                }
                else
                {
                    return;
                }
            }
            if (gameObject.name == "Door1")
            {
                if (instance.count == 1)
                {
                    LoadScene();
                }
                else 
                { 
                    return; 
                }
            }
            if (gameObject.name == "Door2")
            {
                if (instance.count == 2)
                {
                    LoadScene();
                }
                else
                {
                    return;
                }
            }
            if (gameObject.name == "Door3")
            {
                if (instance.count == 3)
                {
                    LoadScene();
                }
                else
                {
                    return;
                }
            }         
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisonObject = collision.gameObject;
        if (collisonObject.name == "Bipi")
        {
            inTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collisonObject = collision.gameObject;
        if (collisonObject.name == "Bipi")
        {
            inTheDoor = false;
        }
    }

    IEnumerator intFade()
    {
        GameObject.Find("Bipi").GetComponent<AudioSource>().PlayOneShot(instance.DoorSound);
        Color c = fadeAway.color;
        while (fadeAway.color.a <= 1)
        {
            c.a += 0.05f;
            fadeAway.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(iLevelToLoad);
    }
    IEnumerator stringFade()
    {
        GameObject.Find("Bipi").GetComponent<AudioSource>().PlayOneShot(instance.DoorSound);
        Color c = fadeAway.color;
        while (fadeAway.color.a <= 1)
        {
            c.a += 0.05f;
            fadeAway.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(sLevelToLoad);
    }
    void LoadScene()
    {
        if (intToLoadLvl)
        {
            StartCoroutine("intFade");
        }
        else
        {
            StartCoroutine("stringFade");
        }
    }
}
