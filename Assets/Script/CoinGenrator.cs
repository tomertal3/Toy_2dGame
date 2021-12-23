using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinGenrator : MonoBehaviour
{
    public static CoinGenrator instance;

    public ArrayList collectedCoins = new ArrayList();

    AudioSource audio;

    public GameObject FallingItemImage;
    public GameObject portal1;
    public GameObject portal2;
    public GameObject portal3;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;

    private GameObject bipi;

    [SerializeField] private GameObject[] FallingItems;

    // counter 
    public TextMeshProUGUI text;

    public Animator MissionText;

    private SpriteRenderer FBttuon1;
    private SpriteRenderer FBttuon2;
    private SpriteRenderer FBttuon3;

    private int score;
    private int level;

    private bool firstLog = true;
    private bool firstText = false;
    private bool secondText = false;
    //########################################################################
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        audio = GetComponent<AudioSource>();
        bipi = GameObject.Find("Bipi");
        FBttuon1 = GameObject.Find("FButon1").GetComponent<SpriteRenderer>();
        FBttuon2 = GameObject.Find("FButon2").GetComponent<SpriteRenderer>();
        FBttuon3 = GameObject.Find("FButon3").GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(FBttuon1);
        DontDestroyOnLoad(FBttuon2);
        DontDestroyOnLoad(FBttuon3);
        if (instance == null)
        {
            instance = this;
        }
        if (!instance.firstLog)
        {
            Destroy(text);
            Destroy(FallingItemImage);
            MissionText.GetComponent<AudioSource>().Play();
            MissionText.gameObject.SetActive(true);
        }
        else
        {
            LoadLevel.instance.count = 0;
            StartCoroutine("dropItems");
        }
        instance.firstLog = false;
        if (LoadLevel.instance.count == 2)
        {
            //MissionText.text = "תוששואתה רדחל סנכהל";
            if (!firstText)
            {
                MissionText.GetComponent<AudioSource>().Play();
                MissionText.SetTrigger("Recovery");
                firstText = true;
            }

        }
        if (LoadLevel.instance.count == 3 && SceneManager.GetActiveScene().name == "MainRoom")
        {
            //MissionText.text = "תאצל";
            if (!secondText)
            {
                MissionText.GetComponent<AudioSource>().Play();
                MissionText.SetTrigger("Exit");
                secondText = true;
            }

        }
    }
    //###################################################################################
    private void Update()
    {
        if (level == 2)
        { 
            if(LoadLevel.instance.count == 1 && portal3 != null)
            {
                portal1.SetActive(true);
                door1.SetActive(false);
            }
            else if (LoadLevel.instance.count == 2 && portal3 != null)
            {
                
                door1.SetActive(true);
                portal1.SetActive(false);
                door2.SetActive(false);
                portal2.SetActive(true);
            }
            else if (LoadLevel.instance.count == 3 && portal3 != null)
            {
                door2.SetActive(true);
                portal2.SetActive(false);
                door3.SetActive(false);
                portal3.SetActive(true);
            }
            if (bipi.transform.position.x >= 0.48f && bipi.transform.position.x <= 3.10f && LoadLevel.instance.count == 1)
        {
            Color c = FBttuon1.color;
            c.a = 1f;
            FBttuon1.color = c;
            c.a = 0.0f;
            FBttuon2.color = c;
            FBttuon3.color = c;


        }
        else if (bipi.transform.position.x >= 6.28f && bipi.transform.position.x <= 8.7f && LoadLevel.instance.count == 2)
        {
            Color c = FBttuon2.color;
            c.a = 1f;
            FBttuon2.color = c;
            c.a = 0.0f;
            FBttuon1.color = c;
            FBttuon3.color = c;
        }
        else if (bipi.transform.position.x >= 14f && LoadLevel.instance.count == 3)
        {
            Color c = FBttuon3.color;
            c.a = 1f;
            FBttuon3.color = c;
            c.a = 0.0f;
            FBttuon1.color = c;
            FBttuon2.color = c;
        }
        else
        {
            Color c = FBttuon3.color;
            c.a = 0.0f;
            FBttuon3.color = c;
            FBttuon1.color = c;
            FBttuon2.color = c;
        }
    }
        else
        {
            Color c = FBttuon3.color;
            c.a = 0.0f;
            FBttuon3.color = c;
            FBttuon1.color = c;
            FBttuon2.color = c;
        }
    }
    //########################################################################

    private void OnLevelWasLoaded(int level)
    {
        this.level = level;
    }

    //########################################################################

    IEnumerator dropItems()
    {
        while (bipi.transform.position.x < -8.4)
        {
            yield return new WaitForSecondsRealtime(0.05f);
        }
        while (score != 10)
        {
            float spawnY = Random.Range(8.0f, 10.0f);
            float spawnX = Random.Range(-6.0f, 11.8f);
            int random = Random.Range(0, 2);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            Instantiate(FallingItems[random], spawnPosition, Quaternion.identity);
            if (score < 7)
            {
                yield return new WaitForSecondsRealtime(2.5f);
            }
            else
            {
                yield return new WaitForSecondsRealtime(4f);

            }
        }
        LoadLevel.instance.count = 1;
        Destroy(text);
        Destroy(FallingItemImage);
        //MissionText.text = " חותינ רדחל סנכהל ";
        MissionText.GetComponent<AudioSource>().Play();
        MissionText.SetTrigger("Oprartion");

    }
    //########################################################################
    public void Decrease()
    {
        instance.audio.Play();
        score += 1;
        instance.text.text = "X" + score.ToString();
    }

}
