using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Instructions : MonoBehaviour
{
    public static Instructions instance;

    public TextMeshProUGUI instructionsText;

    public Animator MissionText;

    public GameObject Box;

    private int count = 0;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        if (instance.count > 0)
        {
            Box.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (count == 0)
            {
                //6
                instructionsText.text = " םשג דרוי ובש דחוימ םוקיל תעגה\n זנוימו לצינש לש   ";
                count++;
            }
            else if(count == 1)
            {
                instructionsText.text = "לכותש המכ ףוסאת";
                count++;
            }
            else if(count == 2)
            {
                instructionsText.text = "or or or or or or";
                count++;
            }
        }
        if (count == 3)
        {
            Box.SetActive(false);
            MissionText.gameObject.SetActive(true);
            MissionText.GetComponent<AudioSource>().Play();
            MissionText.SetTrigger("Blink");
            count++;
        }
    }
}
