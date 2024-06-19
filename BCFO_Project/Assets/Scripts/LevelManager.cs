using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public EnemyHealth enemyHp;
    public PlayerHealth playerHp;

    public bool levelComplete = false;

    public string sceneName;



    void Update()
    {
       
       if (enemyHp.health == 0 && sceneName != "Boss Level")
       {
          levelComplete = true;
          LevelChecker(sceneName, levelComplete);
       }
       else if (playerHp.health == 0)
       {
          SceneManager.LoadScene("GameOver");
       }
       else if (enemyHp.health == 0 && sceneName == "Boss Level")
       {
           levelComplete = true;
           SceneManager.LoadScene("Final");
       }


       if (sceneName == "GameOver") 
       {
            SceneManager.LoadScene("Map");
        }
       
        
    }

    public void LevelChecker(string sceneName,bool levelCompete)
    {
        if (sceneName != "Boss Level" && levelComplete)
        {
            SceneManager.LoadScene("Map");
        }
        else if (sceneName == "Boss Level" && levelComplete)
        {
            SceneManager.LoadScene("Final");
        }
    }




}
