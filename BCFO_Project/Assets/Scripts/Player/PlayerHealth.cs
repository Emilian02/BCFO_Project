using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public Slider healthSlider;
    public Gradient gradient;
    public Image sliderFill;
    private float timer = 4;

    void Start()
    {
        currentHealth = health;
        healthSlider.maxValue = health;
        sliderFill.color = gradient.Evaluate(1f);
    }


    void Update()
    {
        if (currentHealth < health)
        {
            currentHealth = health;
            

        }
        if (health <= 0)
        {
            PlayerMovement.KO = true;
            timer -= Time.deltaTime;
            if (timer <= 0) {
                SceneManager.LoadScene("GameOver L1");
            }

            Debug.Log("L bozo");
        }
        SetHealth(currentHealth);
    }

    public void SetHealth(float health)
    {
        
        healthSlider.value = health;

        sliderFill.color = gradient.Evaluate(healthSlider.normalizedValue);

    }
}
