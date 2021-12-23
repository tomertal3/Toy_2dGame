using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FinalBoss : MonoBehaviour
{
    private Animator anim;
    public Animator MissionText;

    private AudioSource audio;

    public AudioClip mission;

    public GameObject Blood;
    public GameObject Box;
    public GameObject EndText;
    public GameObject FireWorks;
    public GameObject FinishPopUp;
    
    public SpriteRenderer blackScreen;
    public SpriteRenderer EndScreen;

    private bool spacePressed = false;


    IEnumerator instrction()
    {
        yield return new WaitForSecondsRealtime(1.1f);
        Time.timeScale = 0;
        while (!spacePressed)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        Box.SetActive(false);
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 1;
        MissionText.gameObject.SetActive(true);
        audio.PlayOneShot(mission);
        MissionText.SetTrigger("Blink");
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        StartCoroutine("instrction");
    }

    IEnumerator fadeMusic()
    {
        AudioSource Eaudio = GameObject.Find("EndManager").GetComponent<AudioSource>();
        Eaudio.Play();
        Eaudio.volume = 0.0f;
        float speed = 0.001f;
        for (float i = 0; i < 1; i += speed)
        {
            Eaudio.volume = i;
            yield return null;
        }
    }

    IEnumerator step()
    {
        yield return new WaitForSecondsRealtime(0.35f);
        LoadLevel.instance.count++;
        Blood.SetActive(true);
        audio.Play();
        yield return new WaitForSecondsRealtime(1f);
        Blood.SetActive(false);
        yield return new WaitForSecondsRealtime(2f);
        GameObject.Find("EndManager").GetComponent<AudioSource>().Play();
        Color c = blackScreen.color;
        while (blackScreen.color.a <= 1)
        {
            c.a += 0.05f;
            blackScreen.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.9f);
        c = EndScreen.color;
        while (EndScreen.color.a <= 1)
        {
            c.a += 0.05f;
            EndScreen.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        EndText.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        FireWorks.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        EndText.SetActive(false);
        FinishPopUp.SetActive(true);
        GameObject FTPno = GameObject.Find("GalleryB");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }







    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisonObject = collision.gameObject;
        if (collisonObject.name == "Bipi")
        {
            StartCoroutine("step");
            anim.SetTrigger("Steping");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
    }



}
