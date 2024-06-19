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
        level = 0;
        InitializeLevel();
    }
    private void Update()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        if (level == 0)
        {
            
            level1.SetActive(true);
            level2.SetActive(false);
            level3.SetActive(false);
            bossLevel.SetActive(false);
        }
        else if (level == 1)
        {
            
            level1.SetActive(false);
            level2.SetActive(true);
            level3.SetActive(false);
            bossLevel.SetActive(false);
        }
        else if (level == 2)
        {
            
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(true);
            bossLevel.SetActive(false);
        }
        else if (level == 3)
        {

           
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            bossLevel.SetActive(true);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }

    public void OnClicked(Button button)
    {
        switch (button.name)
        {
            case "Level 1 Button":
                SceneManager.LoadScene("Level 1");
                level++;
                break;
            case "Level 2 Button":
                SceneManager.LoadScene("Level 2");
                level++;
                break;
            case "Level 3 Button":
                SceneManager.LoadScene("Level 3");
                level++;
                break;
            case "Boss Level Button":
                SceneManager.LoadScene("Boss Level");
                break;
        }
    }
}