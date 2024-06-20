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
    public int level = 0;

    private void Start()
    {
        level = PlayerPrefs.GetInt("CurrentLevel", 0);
        Debug.Log("Loaded level from PlayerPrefs: " + level);
        InitializeLevel(level);
    }

    private void InitializeLevel(int level)
    {
        Debug.Log("Initializing level: " + level);

        if (level1 != null) level1.SetActive(false);
        if (level2 != null) level2.SetActive(false);
        if (level3 != null) level3.SetActive(false);
        if (bossLevel != null) bossLevel.SetActive(false);

        switch (level)
        {
            case 0:
                if (level1 != null) level1.SetActive(true);
                Debug.Log("Level 1 activated");
                break;
            case 1:
                if (level2 != null) level2.SetActive(true);
                Debug.Log("Level 2 activated");
                break;
            case 2:
                if (level3 != null) level3.SetActive(true);
                Debug.Log("Level 3 activated");
                break;
            case 3:
                if (bossLevel != null) bossLevel.SetActive(true);
                Debug.Log("Boss Level activated");
                break;
            default:
                Debug.LogError("Invalid level index: " + level);
                break;
        }
    }

    public void OnClicked(Button button)
    {
        Debug.Log("Button clicked: " + button.name);

        switch (button.name)
        {
            case "Level 1 Button":
                level = 0;
                SaveLevel();
                SceneManager.LoadScene("Level 1");
                break;
            case "Level 2 Button":
                level = 1;
                SaveLevel();
                SceneManager.LoadScene("Level 2");
                break;
            case "Level 3 Button":
                level = 2;
                SaveLevel();
                SceneManager.LoadScene("Level 3");
                break;
            case "Boss Level Button":
                level = 3;
                SaveLevel();
                SceneManager.LoadScene("Boss Level");
                break;
            default:
                Debug.LogWarning("Unknown button name: " + button.name);
                break;
        }
    }

    private void SaveLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        PlayerPrefs.Save();
        Debug.Log("Level saved to PlayerPrefs: " + level);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        level = PlayerPrefs.GetInt("CurrentLevel", 0); // Reload the level from PlayerPrefs
        InitializeLevel(level);
        Debug.Log("Scene loaded: " + scene.name + ", initializing level: " + level);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
