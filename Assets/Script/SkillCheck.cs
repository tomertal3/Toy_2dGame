using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class SkillCheck : MonoBehaviour
{
    public static SkillCheck instance;

    private AudioSource audio;

    public AudioClip Hit;
    public AudioClip mission;


    public GameObject Circle;
    public GameObject Blood;
    public GameObject Stav;

    public GameObject popUp;
    public GameObject sucssesAnim;
    public GameObject failAnim;

    public bool finshed = false;
    public bool onPoint = false;
    public bool finshedCycle = false;
    private bool isPressed = false;

    public Animator MissionText;

    bool activeted = false;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        Circle.SetActive(false);
        audio = GetComponent<AudioSource>();
        StartCoroutine("instructions");
    }
    IEnumerator instructions()
        {
        yield return new WaitForSecondsRealtime(1.1f);
        Time.timeScale = 0;
        while (!isPressed)
        {
            yield return new WaitForSecondsRealtime(0.001f);
        }
        sucssesAnim.SetActive(false);
        isPressed = false;
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = "תוסנל יצלאת ןוכנ אלה םוקמב f לע תצחל םא \n יחילצתש דע בוש";
        failAnim.SetActive(true);
        while (!isPressed)
        {
            yield return new WaitForSecondsRealtime(0.001f);
        }
        popUp.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        MissionText.gameObject.SetActive(true);
        audio.PlayOneShot(mission);
        MissionText.SetTrigger("Blink");

    }

    IEnumerator push()
    {
        Stav.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        Circle.SetActive(true);
        while (!instance.finshedCycle)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Circle.SetActive(false);
        //round2
        Circle.transform.position = new Vector2(-2.8f, 2.0f);
        instance.finshedCycle = false;
        yield return new WaitForSecondsRealtime(2.5f);
        Circle.SetActive(true);
        Circle.GetComponent<Animator>().speed = 2f;
        while (!instance.finshedCycle)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Circle.SetActive(false);
        //round3
        Circle.transform.position = new Vector2(-14.0f, 1.0f);
        instance.finshedCycle = false;
        yield return new WaitForSecondsRealtime(2.5f);
        Circle.SetActive(true);
        Circle.GetComponent<Animator>().speed = 2.5f;
        while (!instance.finshedCycle)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Circle.SetActive(false);
        activeted = true;
        Stav.SetActive(false);
        instance.finshed = true;
        yield return new WaitForSecondsRealtime(3);
        LoadLevel.instance.count++;
        SceneManager.LoadScene("MainRoom");

    }

    IEnumerator Bloody()
    {
        Blood.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        Blood.SetActive(false);

    }
    IEnumerator Miss()
    {
        Circle.SetActive(false);
        yield return new WaitForSecondsRealtime(0.6f);
        if (!onPoint)
        Circle.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisonObject = collision.gameObject;
        if (collisonObject.name == "Bipi")
        {
            if (!activeted)
            {
                StartCoroutine("push");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Circle.activeSelf)
            {
                if (instance.onPoint)
                {
                    Debug.Log("hit");
                    audio.PlayOneShot(Hit);
                    finshedCycle = true;
                }
                else
                {
                    StartCoroutine("Miss");
                    StartCoroutine("Bloody");
                    audio.Play();


                }
            }
        }

    }


}
