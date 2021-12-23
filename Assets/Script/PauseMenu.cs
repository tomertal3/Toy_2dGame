using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEditor;




public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public GameObject menual;
    public GameObject galery;
    public GameObject quit;
    public GameObject resume;


    [SerializeField]
    private List<GameObject> MenualList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> CharcterPages = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Stickers = new List<GameObject>();


    public Button rightButton;
    public Button leftButton;

    private int menualCounter;



    public void Pause()
    {
        if (Time.timeScale != 0)
        {

            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            GameObject FTPno = GameObject.Find("ResumeButton");
            if (FTPno)
            {
                EventSystem.current.SetSelectedGameObject(FTPno);
            }
        }
    }

  IEnumerator resumeNow()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    IEnumerator waitForGallery()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        GameObject FTPno = GameObject.Find("IconButton1");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }
    }

    public void Resume()
    {
        StartCoroutine("resumeNow");
    }

    public void Quit()
    {
        Application.Quit(); 
    }

    public void Menualleft()
    {
        if (menualCounter > 0)
        {
            MenualList[menualCounter].SetActive(false);
            menualCounter -= 1;
            MenualList[menualCounter].SetActive(true);
        }
    }
    IEnumerator left()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        if (menualCounter < MenualList.Count - 1)
        {
            if (menualCounter == 4)
            {
                if (LoadLevel.instance.count <= 0)
                {
                    yield break;
                }
            }
            if (menualCounter == 5)
            {
                if (LoadLevel.instance.count <= 1)
                {
                    yield break;
                }
            }
            if (menualCounter == 7)
            {
                if (LoadLevel.instance.count <= 2)
                {
                    yield break;
                }
            }
            MenualList[menualCounter].SetActive(false);
            menualCounter++;
            MenualList[menualCounter].SetActive(true);

        }
    }
    public void MenualRight()
    {
        StartCoroutine("left");
    }

    public void Gallery()
    {
        if (LoadLevel.instance.count >= 3)
        {
            for (int i = 4; i <= 9; i++)
            {
                Stickers[i].SetActive(true);
            }
            CharcterPages[1].SetActive(true);
        }
        if (LoadLevel.instance.count >= 2)
        {
            for(int i = 2; i <=3; i++)
            {
                Stickers[i].SetActive(true);
            }
        }
        if(LoadLevel.instance.count >= 0)
        {
            CharcterPages[0].SetActive(true);
            Stickers[0].SetActive(true);
            Stickers[1].SetActive(true);
        }
        StartCoroutine("waitForGallery");
    }
    private void Update()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject);
        if (pauseMenu.activeSelf)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                GameObject FTPno = GameObject.Find("ResumeButton");
                if (FTPno)
                {
                    EventSystem.current.SetSelectedGameObject(FTPno);
                }
            }
            if (EventSystem.current.currentSelectedGameObject == resume)
            {

                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255f, 255f, 255f, 255f);
              


            }
            else
            {
                resume.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0f, 0f, 0f, 255f);

            }

            if (EventSystem.current.currentSelectedGameObject == menual)
            {

                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255f, 255f, 255f, 255f);

            }
            else
            {
                menual.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0f, 0f, 0f, 255f);

            }

            if (EventSystem.current.currentSelectedGameObject == galery)
            {

                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255f, 255f, 255f, 255f);

            }
            else
            {
                galery.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0f, 0f, 0f, 255f);

            }

            if (EventSystem.current.currentSelectedGameObject == quit)
            {

                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255f, 255f, 255f, 255f);

            }
            else
            {
                quit.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0f, 0f, 0f, 255f);

            }
            if (GameObject.Find("MenualTab") != null && GameObject.Find("MenualTab").activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameObject.Find("ExitMenual").GetComponent<Button>().onClick.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Graphic graphic = rightButton.GetComponent<Graphic>();
                    graphic.CrossFadeColor(rightButton.colors.pressedColor, rightButton.colors.fadeDuration, true, true);
                    rightButton.onClick.Invoke();
                }
                else if(Input.GetKeyUp(KeyCode.RightArrow))
                {
                    Graphic graphic = rightButton.GetComponent<Graphic>();
                    graphic.CrossFadeColor(rightButton.colors.normalColor, rightButton.colors.fadeDuration, true, true);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Graphic graphic = leftButton.GetComponent<Graphic>();
                    graphic.CrossFadeColor(leftButton.colors.pressedColor, leftButton.colors.fadeDuration, true, true);
                    leftButton.onClick.Invoke();
                }
                else if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    Graphic graphic = leftButton.GetComponent<Graphic>();
                    graphic.CrossFadeColor(leftButton.colors.normalColor, leftButton.colors.fadeDuration, true, true);
                }

            }
            if (GameObject.Find("GalleryTab") != null && GameObject.Find("GalleryTab").activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameObject.Find("ExitGallery").GetComponent<Button>().onClick.Invoke();
                    GameObject Sb = GameObject.Find("IconButton1");
                    if (Sb)
                    {
                        EventSystem.current.SetSelectedGameObject(Sb);
                    }
                    GameObject.Find("GalleryTab").SetActive(false);
                    GameObject FTPno = GameObject.Find("Gallery");
                    if (FTPno)
                    {
                        EventSystem.current.SetSelectedGameObject(FTPno);
                    }
                }

            }

        }
        if (Input.GetKeyDown(KeyCode.Escape) && LoadLevel.instance.count != 7)
        {

            Pause();
        }
    }




}
