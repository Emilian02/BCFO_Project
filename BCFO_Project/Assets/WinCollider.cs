using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCollider : MonoBehaviour
{
    
    public void OnCollission(Collider2D collider)
    {
        if (collider.gameObject.tag == "player")
        {
            SceneManager.LoadScene("Map");
        }
    }

}
