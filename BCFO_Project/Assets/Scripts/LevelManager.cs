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
        if (enemyHp == null)
        {
            Debug.LogError("EnemyHealth reference not set in LevelManager.");
        }

        if (playerHp == null)
        {
            Debug.LogError("PlayerHealth reference not set in LevelManager.");
        }

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


        if (sceneName == "GameOver L1" && Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    public void LevelChecker(string sceneName)
    {
        if (sceneName != "Boss Level")
        {
            SceneManager.LoadScene("Map");
        }
        else if (sceneName == "Boss Level" )
        {
            SceneManager.LoadScene("Final");
        }
    }
}
