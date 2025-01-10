using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using TMPro;

public class playerControl : MonoBehaviour
{
    public Animator animator;
    public DialogueManager dialogueManager;
    public GameScript gameScript;
    public AudioController audioController;
    public int currentScriptIndex = 0;
    private bool cont=false;
    public bool voiceOverEnd = false;
    private int fTrigCount = 0;
    public TextMeshProUGUI credits;


    void Start(){
        string[] t = gameScript.getNextScript(currentScriptIndex);
        processScript(t);

        credits.enabled = false;
        
        audioController.OnAudioFinished += OnAudioFinishedHandler;
        dialogueManager.OnDialogueEnd += OnDialogueEndHandler;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.E)&&animator.GetBool("burning")&&dialogueManager.putOutFlame){ //always checking for fire animation
            animator.SetTrigger("extinguish");
            animator.SetBool("burning", false);
            audioController.fireSound.Stop();
            dialogueManager.EndDialogue();
            StartCoroutine(ShowCredits(4f));

        }

        if (dialogueManager.dialoguePanel.activeInHierarchy){ //always checking for X input
            if(Input.GetKeyDown(KeyCode.X)&&cont&&!dialogueManager.choiceStop){
                dialogueManager.ShowNextLine();
                cont=false;
            }
        }

        if (dialogueManager.dialoguePanel.activeInHierarchy && dialogueManager.altTrigF){ //checking for F input in special case
            if(Input.GetKeyDown(KeyCode.F)){
                if(fTrigCount<1){
                    animator.SetTrigger("light");
                    animator.SetBool("burning", true);
                    audioController.fireSound.Play();
                    audioController.music.Play();
                }
                else if(fTrigCount==1){
                    animator.SetTrigger("goMedium");
                    audioController.fireWhoosh.Play();
                    
                }
                else if(fTrigCount==2){
                    animator.SetTrigger("goLarge");
                    audioController.fireWhoosh.Play();
                }
                fTrigCount++;
                dialogueManager.altTrigF = false;
                dialogueManager.EndDialogue();
            }
        }

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
        if(currentScriptIndex == 0){
            currentScriptIndex++; //Set to small flame
            Debug.Log("Before CoRoutine, the currentScriptIndex is "+currentScriptIndex);
            StartCoroutine(WaitAndShowScript(3f)); //pause after the first F press 
        }
        else if(currentScriptIndex == 1){
            currentScriptIndex++; //sets to Grow Flame (F)
            processScript(gameScript.getNextScript(currentScriptIndex));
        }
        else if(currentScriptIndex == 2){
            currentScriptIndex++; //Set to different flames
            StartCoroutine(WaitAndShowScript(3f));
        }
        else if(currentScriptIndex == 3){
            currentScriptIndex++; //Set to fan Flames (F)
            processScript(gameScript.getNextScript(currentScriptIndex));
        }
        else if(currentScriptIndex == 4){
            currentScriptIndex++; //Set to different flames CHOICE
            StartCoroutine(WaitAndShowScript(3f));
        }
        else if(currentScriptIndex == 5){
            if(dialogueManager.choice == "Burned"){
                currentScriptIndex=6;
            }
            else if (dialogueManager.choice=="Light"){
                currentScriptIndex=7;
            }
            processScript(gameScript.getNextScript(currentScriptIndex));
        }
        else if(currentScriptIndex == 6 || currentScriptIndex == 7){
            currentScriptIndex=8;
            processScript(gameScript.getNextScript(currentScriptIndex));
        }
        
        

    }


    IEnumerator WaitAndShowScript(float delay){ //how long to wait for
        yield return new WaitForSeconds(delay);
        processScript(gameScript.getNextScript(currentScriptIndex));
    }

    IEnumerator ShowCredits(float delay){
        yield return new WaitForSeconds(delay);
        credits.enabled = true;
    }

    private void OnAudioFinishedHandler(){
        cont=true;
    }

    private void OnDialogueEndHandler(){
        continueStory();
    }
}
