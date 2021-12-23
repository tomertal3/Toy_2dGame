using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class SimpleEnemyManager : MonoBehaviour
{
    public static SimpleEnemyManager instance;

    HealthBar healthBar;

    public GameObject PopUp;

    public TextMeshProUGUI instructionsText;

    public Animator MissionText;

    public AudioClip mission;

    private AudioSource audio;

    public int counter = 0;

    private bool breakTime = true;
    private bool spacePressed = false;
    private bool firstTime = true;

    private int health = 100;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
        }
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        if (LoadLevel.instance.mark)
        {
            StartCoroutine("instrction");
            LoadLevel.instance.mark = false;
        }
        else
        {
            GameObject.Find("Bipi").GetComponent<Animator>().SetBool("isRunning", false);
            MissionText.gameObject.SetActive(true);
        }
    }
    //#########################################################################

    void Update()
    {
        if (healthBar.getValue() <= 0)
        {
            healthBar.Rest();
            SceneManager.LoadScene("Door4");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
        if (counter == 6)
        {
            StartCoroutine("finishLevel");
        }
    }
    //#########################################################################

    public void EnemyHit()
    {
        StartCoroutine("Hit");
    }
  
     //#########################################################################

    public void KillEnemy()
    {
        counter++;
    }
    //#########################################################################

    IEnumerator Hit()
    {
        if (breakTime)
        {
            breakTime = false;
            healthBar.LowerHelath(25);
            yield return new WaitForSecondsRealtime(1f);
            breakTime = true;
        }

    }
    //#########################################################################
    IEnumerator finishLevel()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        LoadLevel.instance.count = 2;
        SceneManager.LoadScene("MainRoom");
    }
    //#########################################################################
    IEnumerator instrction()
    {
        PopUp.SetActive(true);
        yield return new WaitForSecondsRealtime(1.1f);
        Time.timeScale = 0;
        while (!spacePressed)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        PopUp.SetActive(false);
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 1;
        MissionText.gameObject.SetActive(true);
        audio.PlayOneShot(mission);
        MissionText.SetTrigger("Blink");
    }

}
