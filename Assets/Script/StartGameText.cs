using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGameText : MonoBehaviour
{
    public GameObject Box;

    public GameObject spaceAnim;
    public GameObject spaceJumpAnim;
    public GameObject arowsAnim;
    public GameObject escapeAnim;
    public GameObject EnterAnim;
    public GameObject GalleryAnim;

    public SpriteRenderer FButton;

    public TextMeshProUGUI FallingText;
    public GameObject MissionText;

    private bool isPressed = false;

    private GameObject bipi;

    private void Start()
    {
        bipi = GameObject.Find("Bipi");
        StartCoroutine("nextPage");
    }

    IEnumerator popMission()
    {
        yield return new WaitForSecondsRealtime(3f);
        MissionText.SetActive(true);
        Animator MissionTextAnim = MissionText.GetComponent<Animator>();
        MissionTextAnim.SetTrigger("Blink");
    }
    IEnumerator nextPage()
    {
        Box.SetActive(false);
        //round 1
        float timeR = Time.fixedTime;
        yield return new WaitForSecondsRealtime(5);
        while(Time.fixedTime - timeR < 5)
        {
            yield return new WaitForSecondsRealtime(0.001f);

        }
        Time.timeScale = 0f;
        Box.SetActive(true);
        spaceAnim.SetActive(true);
        while (!isPressed)
        {
            yield return new WaitForSecondsRealtime(1);

        }
        Time.timeScale = 1f;
        spaceAnim.SetActive(false);
        Box.SetActive(false);
        isPressed = false;
        //round 2
        timeR = Time.fixedTime;
        yield return new WaitForSecondsRealtime(5);
        while (Time.fixedTime - timeR < 5)
        {
            yield return new WaitForSecondsRealtime(0.001f);

        }
        Time.timeScale = 0f;
        Box.SetActive(true);
        spaceJumpAnim.SetActive(true);
        FallingText.text = "םג שמשי חוור שקמה קחשמה ךלהמב\nלעופ אל תועדוהה ךסמ רשאכ הציפקל";
        while (!isPressed)
        {
            yield return new WaitForSecondsRealtime(1);

        }
        Time.timeScale = 1f;
        spaceJumpAnim.SetActive(false);
        Box.SetActive(false);
        isPressed = false;
        //round 3

        timeR = Time.fixedTime;
        yield return new WaitForSecondsRealtime(2.5f);
        while (Time.fixedTime - timeR < 2)
        {
            yield return new WaitForSecondsRealtime(0.001f);

        }

        Time.timeScale = 0f;
        Box.SetActive(true);
        arowsAnim.SetActive(true);
        FallingText.text = "הזוזתל םישמשמ םיצחה";
        while (!isPressed)
        {
            yield return new WaitForSecondsRealtime(1);

        }
        Time.timeScale = 1f;
        arowsAnim.SetActive(false);
        Box.SetActive(false);
        isPressed = false;

        //mission text
        yield return new WaitForSecondsRealtime(0.3f);
        MissionText.SetActive(true);
        Animator MissionTextAnim = MissionText.GetComponent<Animator>();
        MissionTextAnim.SetTrigger("Blink");
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
        //round 4
       /* while (bipi.transform.position.x <= 3.97f)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        while (Time.timeScale == 0)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        Time.timeScale = 0f;
        Box.SetActive(true);
        escapeAnim.SetActive(true);
        FallingText.text = "טירפתה חתפי פייקסא רותפכ לע הציחלב"; 
        while (!isPressed)
        {
            yield return new WaitForSecondsRealtime(1);

        }       
        FallingText.text = "רותפכב ,קחשמה תא ךישמי 'ךשמה' רותפכה\n  .םיבלשה לע םירבסה תוארל ילכות 'ךירדמ'\n קחשמהמ אצת 'האיצי";
        isPressed = false;
        while (!isPressed)
        {
            yield return new WaitForSecondsRealtime(1);

        }
        isPressed = false;
        escapeAnim.SetActive(false);
        GalleryAnim.SetActive(true);
        FallingText.text = " ןכדעתהל ילכות הירלגה רותפכ לע הציחלב\n קחשמה ךלהמב תחוורהש םירקיטסבו תויומדב";
        while (!isPressed)
        {
            yield return new WaitForSecondsRealtime(1);

        }

        Time.timeScale = 1f;
        GalleryAnim.SetActive(false);
        Box.SetActive(false); 
        isPressed = false;*/
        //round 5
        while (bipi.transform.position.x <= 9.5f)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        while (Time.timeScale == 0)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        Time.timeScale = 0f;
        Box.SetActive(true);
        EnterAnim.SetActive(true);
        FallingText.text = "תותלדב סנכהל ליבשב שמשי f שקמה";
        while (!isPressed)
        {
            yield return new WaitForSecondsRealtime(1);

        }
        Time.timeScale = 1f;
        EnterAnim.SetActive(false);
        Box.SetActive(false);
        isPressed = false;

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Box.activeSelf)
        {
            isPressed = true;
            GameObject.Find("PauseButton").GetComponent<AudioSource>().Play();
        }
        if(bipi.transform.position.x <= 13.8f && bipi.transform.position.x >= 11.02f)
        {
            Color c = FButton.color;
            c.a = 1f;
            FButton.color = c;

        }
        else
        {
            Color c = FButton.color;
            c.a = 0f;
            FButton.color = c;
        }
        
    }


}

