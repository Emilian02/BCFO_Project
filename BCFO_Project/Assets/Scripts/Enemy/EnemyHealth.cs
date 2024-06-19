using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float currenthealth;
    public Slider healthSlider;
    public Gradient gradient;
    public Image sliderFill;

    [SerializeField] public static bool isBoss;

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
        if (health <= 0 && isBoss == true)
        {
            PlayerMovement.isBossFight = false;
            Debug.Log("Boss Down");
        }
        if (health <= 0 && isBoss == false)
        {
            Debug.Log("Enemy Down");
        }
        SetHealth(currenthealth);
    }

    public void SetHealth(float health)
    {

        healthSlider.value = health;

        sliderFill.color = gradient.Evaluate(healthSlider.normalizedValue);

    }
}
