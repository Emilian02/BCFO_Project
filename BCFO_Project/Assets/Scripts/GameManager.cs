using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    private int level = 0;
    private Dictionary<string, GameObject> levels;

    private void Start()
    {
        InitializeLevels();
        InitializeLevel();
    }

    private void InitializeLevels()
    {
        levels = new Dictionary<string, GameObject>
        {
            { "Level 1", GameObject.Find("Level 1") },
            { "Level 2", GameObject.Find("Level 2") },
            { "Level 3", GameObject.Find("Level 3") },
            { "Boss Level", GameObject.Find("Boss Level") }
        };
    }

    private void InitializeLevel()
    {
        foreach (var level in levels.Values)
        {
            level.SetActive(false);
        }

        switch (this.level)
        {
            case 0:
                levels["Level 1"].SetActive(true);
                break;
            case 1:
                    this.level++;
                    levels["Level 2"].SetActive(true);
                
                break;
            case 2:
                
                    this.level++;
                    levels["Level 3"].SetActive(true);
                
                break;
            case 3:
                    this.level++;
                    levels["Boss Level"].SetActive(true);
                
                break;
            default:
                Debug.LogError("Invalid level index");
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
            default:
                Debug.LogWarning("Unknown button name: " + button.name);
                break;
        }
    }
}
