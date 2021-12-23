using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{
    public Button loginButton;
    public Button YesButton;
    public Button NoButton;
    public Button StartButton;

    public Graphic Name;

    private bool started = false;

    public SpriteRenderer CreditScreen;

    public GameObject BlackBackground;
    public GameObject inputField;
    public GameObject loginField;
    public GameObject yes;
    public GameObject no;
    public GameObject firstBack;
    public GameObject secondBack;

    public Text headerText;
    public Text inputText;

    private AudioSource audio;

    int counter = 0;

    IEnumerator credit()
    {
        Color c = CreditScreen.color;
        yield return new WaitForSecondsRealtime(0.8f);
        while (CreditScreen.color.a <= 1)
        {
            c.a += 0.05f;
            CreditScreen.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1.5f);

        while (CreditScreen.color.a >= 0)
        {
            c.a -= 0.05f;
            CreditScreen.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        BlackBackground.SetActive(false);
        GameObject FTPno = GameObject.Find("StartButton");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }
        started = true;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audio = GetComponent<AudioSource>();
        yes.SetActive(false);
        no.SetActive(false);
        StartCoroutine("credit");


    }


    [System.Obsolete]
    void Update()
    {
        if (started)
        {
            if (Input.anyKeyDown)
            {
                if (StartButton.IsActive())
                {
                    StartButton.onClick.Invoke();
                }
            }
            if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "UserName")
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (inputText.text.Length > 0)
                    {
                        loginButton.onClick.Invoke();
                    }
                    else
                    {
                        inputField.GetComponent<InputField>().ActivateInputField();
                    }

                }
            }

            //for color white
            if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "LoginButton")
            {
                loginButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
            else
            {
                loginButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            }

            if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "No")
            {
                NoButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                NoButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }


            if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "Yes")
            {
                YesButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                YesButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }

            if (EventSystem.current.currentSelectedGameObject == null)
            {
                Debug.Log("hello");
                if(StartButton.IsActive()){
                EventSystem.current.SetSelectedGameObject(StartButton.gameObject);                  
                }
                else if (inputField.activeSelf)
                {
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("UserName"));
                }
                else if (NoButton.IsActive())
                {
                    EventSystem.current.SetSelectedGameObject(NoButton.gameObject);
                }
            }
        }

    }
    
    public void startGame()
    {
        StartCoroutine("MoveNext");
    }
    IEnumerator MoveNext()
    {
        yield return new WaitForSeconds(0.3f);
        YesButton.gameObject.SetActive(false);
        NoButton.gameObject.SetActive(false);
        headerText.transform.position = new Vector2(headerText.transform.position.x, -0.3f);
        headerText.fontSize = 60;
        headerText.text = "אבה ךורב ,קירב םולש";
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(sceneName: "SampleScene");
    }

    public void FirstStart()
    {
        StartCoroutine("whatImDoing");
    }
    IEnumerator whatImDoing()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        if (!StartButton.gameObject.activeSelf) {
            yield break;
        }
        StartButton.gameObject.SetActive(false);
        loginButton.gameObject.SetActive(true);
        inputField.gameObject.SetActive(true);
        headerText.gameObject.SetActive(true);
        firstBack.SetActive(false);
        secondBack.SetActive(true);
        GameObject FTPno = GameObject.Find("UserName");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }

    }
    public void secondStart()
    {
        StartCoroutine("whatImDoing2");
    }

    IEnumerator whatImDoing2()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        headerText.text = "התאש חוטב התא\n ?קירב היהי םשהש הצור";
        headerText.transform.position = new Vector2(headerText.transform.position.x, 0.2f);
        headerText.fontSize = 50;
        YesButton.gameObject.SetActive(true);
        NoButton.gameObject.SetActive(true);
        inputField.SetActive(false);
        loginButton.gameObject.SetActive(false);
        GameObject FTPno = GameObject.Find("No");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }
    }

}
