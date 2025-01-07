using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public Animator animator;
    public DialogueManager dialogueManager;

    string[] exText = {"First Dialogue", "second", "Press F to light your Flame"};
    void Update() {
        if(Input.GetKeyDown(KeyCode.F)&&!animator.GetBool("burning")){
            animator.SetTrigger("light");
            animator.SetBool("burning", true);
        }

        if(Input.GetKeyDown(KeyCode.E)&&animator.GetBool("burning")){
            animator.SetTrigger("extinguish");
            animator.SetBool("burning", false);
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            dialogueManager.StartDialogue(exText);
        }


    }

}
