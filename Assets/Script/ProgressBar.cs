using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public Image fill;

    private bool eating = false;

    private AudioSource audio;

    public AudioClip lose;

    private bool couht = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    IEnumerator slowDown()
    {
        while(slider.value != 0)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            slider.value -= 1;
        }
    }

    IEnumerator eatingSound()
    {
        eating = true;
        audio.PlayOneShot(audio.clip);
        yield return new WaitForSecondsRealtime(1.3f);
        if (slider.value != 1500)
        {
            eating = false;
        }
        
    }

    IEnumerator Cought()
    {
        couht = true;
        audio.PlayOneShot(lose);
        yield return new WaitForSecondsRealtime(0.7f);
        couht = false;

    }
    public void UpProgress()
    {
        if (!eating)
        {
            StartCoroutine("eatingSound");
        }
        slider.value += 2f;
        fill.color = Color.white;
    }

    public void DownProgress()
    {
        if (!couht)
        {
            StartCoroutine("Cought");
            audio.PlayOneShot(lose);
        }
        fill.color = Color.red;
        StartCoroutine("slowDown");
        //slider.value -= 1;
    }

}
