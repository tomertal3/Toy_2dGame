using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void LowerHelath(int health)
    {
        slider.value -=  health;
    }

    public void Rest()
    {
        slider.value = 100;
    }

    public float getValue()
    {
        return slider.value;
    }
}

