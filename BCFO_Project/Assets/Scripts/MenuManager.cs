using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    Scene Map;

    public GameObject mainMenu;
    public Button startButton;
    public GameObject howTo;
    public Button howToButton;
    public GameObject Credits;
    public Button CreditsButton;
    public GameObject Settings;
    public Button SettingsButton;



    void Start()
    {
       
    }

    private void Update()
    {
    }

    private void GetPosition(Button button)
    {
        Vector3 pos = button.transform.position;
     
    }
    public void OnClicked(Button button)
    {
        print(button.name);
    }

}
