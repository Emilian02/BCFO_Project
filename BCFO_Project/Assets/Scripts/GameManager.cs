using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject bossLevel;
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
                level1.SetActive(true);
                level2.SetActive(false);
                level3.SetActive(false);
                bossLevel.SetActive(false);
                break;
            case 1:
                {
                    level++;
                    level1.SetActive(false);
                    level2.SetActive(true);
                    level3.SetActive(false);
                    bossLevel.SetActive(false);
                }
                break;
            case 2:
                    level++;
                level1.SetActive(false);
                level2.SetActive(false);
                level3.SetActive(true);
                bossLevel.SetActive(false);

                break;
            case 3:
                    level++;
                level1.SetActive(false);
                level2.SetActive(false);
                level3.SetActive(false);
                bossLevel.SetActive(true);

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