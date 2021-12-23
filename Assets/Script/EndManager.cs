using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndManager : MonoBehaviour
{
    public static EndManager instance;

    public GameObject Menu;
    public GameObject Gallery;
    public GameObject EndText;
    public GameObject FireWorks;
    public GameObject FinishPopUp;
    public GameObject spcae;



    public SpriteRenderer blackScreen;
    public SpriteRenderer EndScreen;


    public Button galleryButton;
    public Button exitButton;

    private bool pressedSpace = false;
    //############################################################
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        }
    public void End()
    {
        StartCoroutine("endGame");
    }
    public void Quit()
    {
        Application.Quit();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressedSpace = true;
        }
        if (Menu.activeSelf)
        {
            if (!Gallery.activeSelf)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    GameObject FTPno = GameObject.Find("GalleryB");
                    if (FTPno)
                    {
                        EventSystem.current.SetSelectedGameObject(FTPno);
                    }
                }
                //for white
                if (EventSystem.current.currentSelectedGameObject.name == "GalleryB")
                {
                    galleryButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                }
                else
                {
                    galleryButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }

                if (EventSystem.current.currentSelectedGameObject.name == "QuitButtonB")
                {
                    exitButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                }
                else
                {
                    exitButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
            }
            else
            {

                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    GameObject FTPno = GameObject.Find("IconButton1B");
                    if (FTPno)
                    {
                        EventSystem.current.SetSelectedGameObject(FTPno);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameObject.Find("ExitGalleryB").GetComponent<Button>().onClick.Invoke();
                    GameObject Sb = GameObject.Find("IconButton1B");
                    if (Sb)
                    {
                        EventSystem.current.SetSelectedGameObject(Sb);
                    }
                    Gallery.SetActive(false);
                    GameObject FTPno = GameObject.Find("GalleryB");
                    if (FTPno)
                    {
                        EventSystem.current.SetSelectedGameObject(FTPno);
                    }
                }

            }
        }
    }
    IEnumerator endGame()
    {
        LoadLevel.instance.count = 7;
        GetComponent<AudioSource>().Play();
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
        FireWorks.SetActive(true);
        yield return new WaitForSecondsRealtime(6f);
        spcae.SetActive(true);
        pressedSpace = false;
        while (!pressedSpace)
        {
            yield return new WaitForSecondsRealtime(0.001f);

        }
        spcae.SetActive(false);
        EndText.SetActive(false);
        yield return new WaitForSecondsRealtime(2.5f);
        FinishPopUp.SetActive(true);
        GameObject FTPno = GameObject.Find("GalleryB");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }
    }
}