using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image hp_bar;
    [SerializeField] private Text hp_text;
    public void ChangeHealth(float health,float max_health){
        hp_text.text="HP: "+health.ToString()+"/"+max_health.ToString();
        hp_bar.fillAmount= health/max_health;
    }
}
