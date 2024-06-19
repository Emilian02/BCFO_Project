using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    private int level = 0;

    private void Start()
    {
        
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        switch (level)
        {
            case 0:
                level++;
                GameObject.Find("Level 1").SetActive(true);
                GameObject.Find("Level 2").SetActive(false);
                GameObject.Find("Level 3").SetActive(false);
                GameObject.Find("Boss Level").SetActive(false);
                break;
            case 1:
                {
                    level++;
                    GameObject.Find("Level 1").SetActive(false);
                    GameObject.Find("Level 2").SetActive(true);
                    GameObject.Find("Level 3").SetActive(false);
                    GameObject.Find("Boss Level").SetActive(false);
                }
                break;
            case 2:
                    level++;
                    GameObject.Find("Level 1").SetActive(false);
                    GameObject.Find("Level 2").SetActive(false);
                    GameObject.Find("Level 3").SetActive(true);
                    GameObject.Find("Boss Level").SetActive(false);
                
                break;
            case 3:
                    level++;
                    GameObject.Find("Level 1").SetActive(false);
                    GameObject.Find("Level 2").SetActive(false);
                    GameObject.Find("Level 3").SetActive(false);
                    GameObject.Find("Boss Level").SetActive(true);
                
                break;
            default:
                Debug.Log("ERROR");
                break;
        }
    }

    public void OnClicked(Button button)
    {
        switch (button.name)
        {
            case "Level 1 Button":
                SceneManager.LoadScene("Level 1");
                break;
            case "Level 2 Button":
                SceneManager.LoadScene("Level 2");
                break;
            case "Level 3 Button":
                SceneManager.LoadScene("Level 3");
                break;
            case "Boss Level Button":
                SceneManager.LoadScene("Boss Level");
                break;
        }
    }
}