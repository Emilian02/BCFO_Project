using System.Collections;
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
        else
        {
            Debug.Log("Scene name: " + sceneName); 
        }


        if (playerHp == null)
        {
            Debug.LogWarning("PlayerHealth component is not assigned.");
        }
        else if (playerHp != null)
        {
            playerHp.GetComponent<PlayerHealth>().health = 100;
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
                ReturnToGame(sceneName);
            }
            else if (enemyHp.health <= 0 && sceneName == "Boss Level")
            {
                SceneManager.LoadScene("Final");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            ReturnToGame(sceneName);
        }
    }

    private void ReturnToGame(string sceneName)
    {
        
        switch (sceneName)
        {
            case "GameOver L1":
                SceneManager.LoadScene("Level 1");
                break;
            case "GameOver L2":
                SceneManager.LoadScene("Level 2");
                break;
            case "GameOver L3":
                SceneManager.LoadScene("Level 3");
                break;
            case "GameOver B":
                SceneManager.LoadScene("Boss Level");
                break;
            default:
                break;
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
