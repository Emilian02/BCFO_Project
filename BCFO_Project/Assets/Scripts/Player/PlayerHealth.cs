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
    public LevelManager levelManager;


    void Start()
    {
        currentHealth = health;
        healthSlider.maxValue = currentHealth;
        sliderFill.color = gradient.Evaluate(1f);
    }


    void Update()
    {
        SetHealth(health);
        if (currentHealth < health)
        {
            currentHealth = health;
            SetHealth(currentHealth);

        }
        if (health <= 0)
        {
            PlayerMovement.KO = true;
            timer -= Time.deltaTime;
            if (timer <= 0) {
                GameOverScene(levelManager);
            }
            
            Debug.Log("L bozo");
        }
        
    }

    public void GameOverScene(LevelManager levelManager)
    {
        string sceneName = levelManager.GetComponent<LevelManager>().sceneName;

        switch(sceneName)
        {
            case "Level 1":
                SceneManager.LoadScene("GameOver l1");
                break;
            case "Level 2":
                SceneManager.LoadScene("GameOver l2");
                break;
            case "Level 3":
                SceneManager.LoadScene("GameOver l3");
                break;
            case "Boss Level":
                SceneManager.LoadScene("GameOver B");
                break;
            default:
                Debug.Log("Error in GameOverScene");
                break;

        }
    }
    public void SetHealth(float health)
    {
        
        healthSlider.value = health;

        sliderFill.color = gradient.Evaluate(healthSlider.normalizedValue);

    }
}
