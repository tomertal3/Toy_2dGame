using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerManager : MonoBehaviour
{
    public static BurgerManager instance;

    private ProgressBar progressBar;

    private bool onPoint = false;
    private bool inSight = false;
    private bool finshedRoom = false;
    //########################################################################
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    //#########################################################################

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && !finshedRoom)
        {
            if (onPoint)
            {
                if (inSight)
                {
                    progressBar.DownProgress();
                }
                else
                {
                    progressBar.UpProgress();
                }
            }
        }
        // check if progres burger level is finished
        if (progressBar != null && progressBar.slider.value == 1500)
        {
            inSight = false;
            finshedRoom = true;
            if (GameObject.Find("BurgerMain") != null)
            {
                GameObject.Find("BurgerMain").SetActive(false);
            }
        }
    }
    //#########################################################################

    public void OnPoint()
    {
        if (progressBar == null)
        {
            progressBar = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
        }
        onPoint = true;
        ApearItem(GameObject.Find("FButon").GetComponent<SpriteRenderer>());
    }
    //#########################################################################

    public void InSight()
    {
        inSight = true;
    }
    //#########################################################################

    public void UpOrDown()
    {
        if (finshedRoom || inSight)
        {
            DisapearItem(GameObject.Find("FButon").GetComponent<SpriteRenderer>());
        }
        else
        {
            ApearItem(GameObject.Find("FButon").GetComponent<SpriteRenderer>());
        }
    }
    //#########################################################################

    public void OutOfPoint()
    {
        onPoint = false;
        DisapearItem(GameObject.Find("FButon").GetComponent<SpriteRenderer>());
    }
    //#########################################################################

    public void OutOfEnemySight()
    {
        inSight = false;
    }
    //#########################################################################

    private void DisapearItem(SpriteRenderer s)
    {
        Color c = s.color;
        c.a = 0f;
        s.color = c;
    }
    //#########################################################################
    private void ApearItem(SpriteRenderer s)
    {
        Color c = s.color;
        c.a = 1f;
        s.color = c;
    }
}
