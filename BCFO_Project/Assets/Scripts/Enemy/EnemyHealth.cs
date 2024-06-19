using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float currenthealth;

    [SerializeField] public static bool isBoss;
    public bool boss;

    void Start()
    {
        currenthealth = health;
        isBoss = boss;
    }

    void Update()
    {
        if (currenthealth < health)
        {
            currenthealth = health;
        }
        if (health <= 0 && boss == true)
        {
            PlayerMovement.isBossFight = false;
            Debug.Log("Boss Down");
            SceneManager.LoadScene("Final");
        }
        if (health <= 0 && isBoss == false)
        {
            Debug.Log("Enemy Down");
        }
       
    }

}
