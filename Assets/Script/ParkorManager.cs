using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParkorManager : MonoBehaviour
{
    public static ParkorManager instance;

    private AudioSource audio;

    private GameObject Bipi;

    public Animator MissionText;

    public AudioClip mission;

    public TextMeshProUGUI text;

    public GameObject PopUp;
    public GameObject spikes1;
    public GameObject spikes2;
    private bool spacePressed = false;
    private bool did;
    //########################################################################
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        audio = GetComponent<AudioSource>();
        Bipi = GameObject.Find("Bipi");
        StartCoroutine("instrction");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
        if (!did)
        {
            did = true;
            SpriteRenderer shad = GameObject.Find("Shadow").GetComponent<SpriteRenderer>();
            Color c = shad.color;
            c.a = 0;
            shad.color = c;
        }
    }
    //########################################################################
    public void CheckPoint()
    {
        if (Bipi.transform.position.x < 1.68f)
        {
            Bipi.transform.position = GameObject.Find("CheckPoint1").transform.position;
        }
        else if (Bipi.transform.position.x < 17)
        {
            Bipi.transform.position = GameObject.Find("CheckPoint2").transform.position;
        }
        else if (Bipi.transform.position.x < 28)

        {
            Bipi.transform.position = GameObject.Find("CheckPoint3").transform.position;
        }
        else
        {
            Bipi.transform.position = GameObject.Find("CheckPoint4").transform.position;

        }
    }
    //#######################################################################################
    IEnumerator instrction()
    {
        PopUp.SetActive(true);
        yield return new WaitForSecondsRealtime(1.1f);
        Time.timeScale = 0;
        while (!spacePressed)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        spacePressed = false;
        spikes1.SetActive(false);
        spikes2.SetActive(true);

        text.text = " םילושכמה דחאב תעגפ םא\n הרוחא רוזחל ךרטצת";

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
