using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Content;

public class MenuManager : MonoBehaviour
{

    public void OnClicked(Button button)
    {
        if (button.name == "Start")
        {
            SceneManager.LoadScene("Map");
        }
    }



}
