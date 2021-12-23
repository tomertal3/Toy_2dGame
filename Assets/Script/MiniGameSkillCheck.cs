using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSkillCheck : MonoBehaviour
{

    public GameObject Fbutton;
    public void play()
    {
        if (gameObject.activeSelf)
        {
            SkillCheck.instance.onPoint = true;
            Fbutton.SetActive(true);
        }
    }
    public void stop()
    {
        if (gameObject.activeSelf)
        {
            SkillCheck.instance.onPoint = false;
            Fbutton.SetActive(false);

        }

    }

}
