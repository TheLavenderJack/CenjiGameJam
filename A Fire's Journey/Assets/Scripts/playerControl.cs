using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class playerControl : MonoBehaviour
{
    public Animator animator;
    public DialogueManager dialogueManager;
    public GameScript gameScript;
    public int currentScriptIndex = 0;


    void Start(){
        string[] t = gameScript.getNextScript(currentScriptIndex);
        processScript(t);
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.F)&&!animator.GetBool("burning")){
            animator.SetTrigger("light");
            animator.SetBool("burning", true);
        }

        if(Input.GetKeyDown(KeyCode.E)&&animator.GetBool("burning")){
            animator.SetTrigger("extinguish");
            animator.SetBool("burning", false);
        }

    }


    public void changeAnim(){

    }

    public void processScript(string[] s){
        if(s[0] == "altTrig"){
            string trig = s[1];
            string[] dialogueArr = new string[s.Length-2];
            Array.Copy(s, 2, dialogueArr, 0, dialogueArr.Length);
            dialogueManager.StartDialogue(dialogueArr, trig, false, null);
        }
        else if(s[0] == "choice"){
            string[] c = new string[]{s[1],s[2]};
            string[] dialogueArr = new string[s.Length-3];
            Array.Copy(s, 3, dialogueArr, 0, dialogueArr.Length);
            dialogueManager.StartDialogue(dialogueArr, "", true, c);
        }
        else{
            string[] dialogueArr = new string[s.Length-1];
            Array.Copy(s, 1, dialogueArr, 0, dialogueArr.Length);
            dialogueManager.StartDialogue(dialogueArr, "", false, null);
        }



    }

}
