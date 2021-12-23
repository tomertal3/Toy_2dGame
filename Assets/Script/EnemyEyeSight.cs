using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyEyeSight : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] positions;

    private int index;

    private Animator anim;

    public Slider slider;

    public SpriteRenderer vision1;
    public SpriteRenderer vision2;
    public SpriteRenderer vision3;

    public GameObject Box;
    public GameObject DoctorBox;
    public GameObject BurgerAnim;
    public GameObject VisionAnim;

    public Animator MissionText;


    private bool spacePressed = false;
    private bool see = false;
  
    IEnumerator finishLevel()
    {
        anim.SetBool("isRunning", false);
        yield return new WaitForSecondsRealtime(3);
        LoadLevel.instance.count = 3;
        SceneManager.LoadScene("MainRoom");
    }

    IEnumerator interduction()
    {
        spacePressed = false;
        yield return new WaitForSecondsRealtime(1.1f);
        spacePressed = false;
        Time.timeScale = 0;
        DoctorBox.SetActive(true);
        while (!spacePressed)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        spacePressed = false;
        DoctorBox.SetActive(false);
        Box.SetActive(true);
        BurgerAnim.SetActive(true);
        while (!spacePressed)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        BurgerAnim.SetActive(false);
        spacePressed = false;
        Box.GetComponentInChildren<TextMeshProUGUI>().text = "ןשעל קיספת הייארה חווטב אצמנ אבא םא\nדרי דמה תרחא  ";
        VisionAnim.SetActive(true);
        //round2
        while (!spacePressed)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Box.SetActive(false);
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 1;
        MissionText.gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        MissionText.SetTrigger("Blink");


    }



    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
        StartCoroutine("interduction");

    }



    void Update()
    {
      
        if (slider.value != 1500)
        {

            if (index % 2 == 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);


            if (transform.position == positions[index])
            {

                if (index == positions.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
        }
        else
        {
            StartCoroutine("finishLevel");

        }
        if (Input.GetKey(KeyCode.F))
        {
            if (see)
            {
                vision1.color = Color.red;
                vision2.color = Color.red;
                vision3.color = Color.red;

            }
            else
            {
                vision1.color = Color.white;
                vision2.color = Color.white;
                vision3.color = Color.white;

            }
        }
        if (!see && !Input.GetKey(KeyCode.F))
        {
            vision1.color = Color.white;
            vision2.color = Color.white;
            vision3.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            see = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            see = false;

        }
    }

    



}
