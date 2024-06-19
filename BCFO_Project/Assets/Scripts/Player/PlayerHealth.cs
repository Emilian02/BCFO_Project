using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float currenthealth;
    public Slider healthSlider;
    public Gradient gradient;
    public Image sliderFill;


    void Start()
    {
        currenthealth = health;
        healthSlider.maxValue = health;
        sliderFill.color = gradient.Evaluate(1f);
    }


    void Update()
    {
        if (currenthealth < health)
        {
            currenthealth = health;
            

        }
        if (health <= 0)
        {
            PlayerMovement.KO = true;
            Debug.Log("L bozo");
        }
        SetHealth(currenthealth);
    }

    public void SetHealth(float health)
    {
        
        healthSlider.value = health;

        sliderFill.color = gradient.Evaluate(healthSlider.normalizedValue);

    }
}
