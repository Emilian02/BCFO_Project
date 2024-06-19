using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public EnemyHealth enemyHp;
    [SerializeField] private PlayerHealth playerHp;

    private bool levelCompleted;




    void Update()
    {
        if (enemyHp.health == 0)
        {
            levelCompleted = true;
            SceneManager.LoadScene("Map");
        }
        else if (playerHp.health == 0)
        {
            SceneManager.LoadScene("Lose 1");
        }

    }

}
