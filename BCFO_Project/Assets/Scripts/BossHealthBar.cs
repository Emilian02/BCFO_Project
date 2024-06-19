using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient gradient;
    public Image sliderFill;
    float bossHealth;
    float bossCurrentHealth;



    void Start()
    {
        bossHealth = GetComponent<EnemyHealth>().health;
        bossCurrentHealth = GetComponent<EnemyHealth>().currenthealth;
        bossCurrentHealth = bossHealth;
        healthSlider.maxValue = bossHealth;
        sliderFill.color = gradient.Evaluate(1f);
    }

    void Update()
    {
        
        SetHealth(bossCurrentHealth);

    }

    public void SetHealth(float health)
    {

        healthSlider.value = health;

        sliderFill.color = gradient.Evaluate(healthSlider.normalizedValue);

    }
}
