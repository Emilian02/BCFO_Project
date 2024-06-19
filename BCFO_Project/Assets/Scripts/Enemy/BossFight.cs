using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class BossFight : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] startCutscene cutscene;
    [Header("Player")]
    [SerializeField] Transform player;
    [Header("Enemies")]
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [Header("Spawner")]
    [SerializeField] Transform spawner1;
    [SerializeField] Transform spawner2;

    private float timer;
    private int random;

    void Start()
    {

    }

    void Update()
    {
        if (cutscene.finish)
        {
            timer += Time.deltaTime;

            if (timer >= 5)
            {
                random = UnityEngine.Random.Range(1, 3);
                if (random == 1)
                {
                    Enemy1();
                }
                else if (random == 2)
                {
                    Enemy2();
                }
            }
        }
    }

    void Enemy1()
    {
        GameObject enemy = Instantiate(enemy1, spawner1.position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().target = player;
    }
    void Enemy2()
    {
        GameObject enemy = Instantiate(enemy2, spawner2.position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().target = player;
    }

}
