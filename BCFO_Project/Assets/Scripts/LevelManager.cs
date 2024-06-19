using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public EnemyHealth enemyHp;
    public PlayerHealth playerHp;

    public string sceneName;

    void Start()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name not set in LevelManager.");
        }
    }

    void Update()
    {
        if (enemyHp != null && playerHp != null)
        {
            if (enemyHp.health <= 0 && sceneName != "Boss Level")
            {
                LevelChecker(sceneName);
            }
            else if (playerHp.health <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else if (enemyHp.health <= 0 && sceneName == "Boss Level")
            {
                SceneManager.LoadScene("Final");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (sceneName == "GameOver L1")
            {
                SceneManager.LoadScene("Level 1");
            }
            else if (sceneName == "GameOver L2")
            {
                SceneManager.LoadScene("Level 2");
            }
            else if (sceneName == "GameOver L3")
            {
                SceneManager.LoadScene("Level 3");
            }
            else if (sceneName == "GameOver B")
            {
                SceneManager.LoadScene("Boss Level");
            }
        }
    }

    public void LevelChecker(string sceneName)
    {
        if (sceneName != "Boss Level")
        {
            SceneManager.LoadScene("Map");
        }
        else if (sceneName == "Boss Level")
        {
            SceneManager.LoadScene("Final");
        }
    }

}
