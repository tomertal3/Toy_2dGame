using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public TextMeshProUGUI text;
    public Animator MissionText;

    public GameObject CoinImage;

    public GameObject bipi;

    private SpriteRenderer FBttuon1;
    private SpriteRenderer FBttuon2;
    private SpriteRenderer FBttuon3;

    public int score;
    private int level;

    public ArrayList collectedCoins = new ArrayList();

    private bool firstLog = true;
    private bool firstText = false;
    private bool secondText = false;
    private bool thiedText = false;

   
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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
            Destroy(CoinImage);

        }
        if (!instance.firstLog)
        {
            MissionText.GetComponent<AudioSource>().Play();
            MissionText.gameObject.SetActive(true);
        }
        instance.firstLog = false;
        if (LoadLevel.instance.count == 4)
        {
            //MissionText.text = "תוששואתה רדחל סנכהל";
            if (!firstText)
            {
                MissionText.GetComponent<AudioSource>().Play();
                MissionText.SetTrigger("Recovery");
                firstText = true;
            }

        }
        

        if (LoadLevel.instance.count == 5 && SceneManager.GetActiveScene().name == "MainRoom")
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
    public void Update()
    {
        if (score == 6)
        {
            Destroy(text);
            Destroy(CoinImage);
            LoadLevel.instance.count++;
            //MissionText.text = " חותינ רדחל סנכהל ";
            MissionText.GetComponent<AudioSource>().Play();
            MissionText.SetTrigger("Oprartion");

             score++;
        }
        if (level == 2)
        {
            if (bipi.transform.position.x >= 0.48f && bipi.transform.position.x <= 3.10f && score == 7)
            {
                Color c = FBttuon1.color;
                c.a = 1f;
                FBttuon1.color = c;
                c.a = 0.0f;
                FBttuon2.color = c;
                FBttuon3.color = c;


            }
            else if (bipi.transform.position.x >= 6.28f && bipi.transform.position.x <= 8.7f && LoadLevel.instance.count == 4)
            {
                Color c = FBttuon2.color;
                c.a = 1f;
                FBttuon2.color = c;
                c.a = 0.0f;
                FBttuon1.color = c;
                FBttuon3.color = c;
            }
            else if (bipi.transform.position.x >= 14f && LoadLevel.instance.count == 5)
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
        }
    private void OnLevelWasLoaded(int level)
    {
        this.level = level;
    }
    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();

    }

}



