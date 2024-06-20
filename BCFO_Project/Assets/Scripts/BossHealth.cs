using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider healthSlider;
    public Gradient gradient;
    public Image sliderFill;
    private float timer = 4;

    void Start()
    {
        maxHealth = health;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        sliderFill.color = gradient.Evaluate(1f);
    }

    void Update()
    {
        SetHealth(health);

        if (health <= 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene("Final");
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        sliderFill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}
