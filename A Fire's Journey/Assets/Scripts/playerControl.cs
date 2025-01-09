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
    public AudioController audioController;
    public int currentScriptIndex = 0;
    private bool cont=false;
    public bool voiceOverEnd = false;


    void Start(){
        string[] t = gameScript.getNextScript(currentScriptIndex);
        processScript(t);
        Debug.Log(""+cont);
        audioController.OnAudioFinished += OnAudioFinishedHandler;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.E)&&animator.GetBool("burning")){ //always checking for fire animation
            animator.SetTrigger("extinguish");
            animator.SetBool("burning", false);
        }

        if (dialogueManager.dialoguePanel.activeInHierarchy){ //always checking for X input
            if(Input.GetKeyDown(KeyCode.X)&&cont){
                dialogueManager.ShowNextLine();

                cont=false;
            }
        }

        if (dialogueManager.dialoguePanel.activeInHierarchy && dialogueManager.altTrigF){ //checking for F input in special case
            if(Input.GetKeyDown(KeyCode.F)){
                animator.SetTrigger("light");
                animator.SetBool("burning", true);
                dialogueManager.altTrigF = false;
                dialogueManager.EndDialogue();
                continueStory();
            }
        }

    }


    public void changeAnim(){

    }

    public void processScript(string[] s){
        if(s[0] == "altTrig"){
            string trig = s[1];
            string[] dialogueArr = new string[s.Length-2];
            Array.Copy(s, 2, dialogueArr, 0, dialogueArr.Length);
            dialogueManager.StartDialogue(dialogueArr, trig, false, null, currentScriptIndex);
        }
        else if(s[0] == "choice"){
            string[] c = new string[]{s[1],s[2]};
            string[] dialogueArr = new string[s.Length-3];
            Array.Copy(s, 3, dialogueArr, 0, dialogueArr.Length);
            dialogueManager.StartDialogue(dialogueArr, "", true, c, currentScriptIndex);
        }
        else{
            string[] dialogueArr = new string[s.Length-1];
            Array.Copy(s, 1, dialogueArr, 0, dialogueArr.Length);
            dialogueManager.StartDialogue(dialogueArr, "", false, null, currentScriptIndex);
        }
    }


    public void continueStory(){ // This is where the story is specifically scriptedout (if index=0, go to 1, etc)
        if(currentScriptIndex == 0||currentScriptIndex == 5){
            currentScriptIndex++;
            StartCoroutine(WaitAndShowScript(3f));
        }
        

    }


    IEnumerator WaitAndShowScript(float delay){ //how long to wait for
        yield return new WaitForSeconds(delay);
        processScript(gameScript.getNextScript(currentScriptIndex));
    }


    private void OnAudioFinishedHandler(){
        cont=true;
    }
}
