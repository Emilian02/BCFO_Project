using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startCutscene : MonoBehaviour
{
    public static bool isCutsceneOn;
    public Animator camAnim;
    public Animator fbAnim;
    public Animator bossAnim;
    public Animator leftAnim;
    public Animator rightAnim;
    public bool finish;

    void  OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCutsceneOn = true;
            PlayerMovement.isBossFight = true;
            camAnim.SetBool("cutScene1", true);
            fbAnim.SetBool("squish", true);
            bossAnim.SetBool("raise", true);
            leftAnim.SetBool("raiseL", true);
            rightAnim.SetBool("raiseR", true);
            Invoke(nameof(StopCutscene), 5.0f);
        }
    }

    void StopCutscene()
    {
        isCutsceneOn = false;
        camAnim.SetBool("cutScene1", false);
        finish = true;
    }
}
