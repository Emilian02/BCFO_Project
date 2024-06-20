using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float currenthealth;


    [SerializeField] public static bool isBoss;
    public bool boss;

    public Slider healthSlider;
    public Gradient gradient;
    public Image sliderFill;

    void Start()
    {
        currenthealth = health;
        isBoss = boss;

        if (boss && healthSlider != null)
        {

            SetMaxHealth(health);
        }

    }

    void Update()
    {
        if (currenthealth < health && !boss)
        {
            currenthealth = health;
        }
        else if(currenthealth < health && boss)
        {
            currenthealth = health;
            SetHealth(health);
        }
        if (health <= 0 && isBoss)
        {
            
            PlayerMovement.isBossFight = false;
            Debug.Log("Boss Down");
            SceneManager.LoadScene("Final");
        }
        if (health <= 0 && isBoss == false)
        {
            Debug.Log("Enemy Down");
        }
       
    }

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        sliderFill.color = gradient.Evaluate(1f);
        

    }
    public void SetHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        sliderFill.color = gradient.Evaluate(healthSlider.normalizedValue);

    }

}
