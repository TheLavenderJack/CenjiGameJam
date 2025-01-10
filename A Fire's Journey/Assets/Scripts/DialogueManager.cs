using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public GameObject choicesPanel;  // Panel for choices
    public Button[] choiceButtons;  // Buttons for the choices
    public AudioController audioController;
    private string[] dialogueLines;
    private int currentSIndex;
    private int currentLine = 0;
    private bool proceed;
    private bool choicedQ;
    private string[] playerChoices;
    string alternateTrigger;
    public string choice;
    public bool choiceStop;
    public bool putOutFlame=false;
    public event Action OnDialogueEnd;

    //Alt trigger button bools
    public bool altTrigF;
    
    void Start()
    {

    }

    void Update() {
        

        
    }

    public void StartDialogue(string[] newDialogueLines, string altTrig, bool choiceAtEnd, string[] choices, int curIndex)
    {
        dialogueLines = newDialogueLines; //the arr of strings that make up the text
        currentSIndex = curIndex; //which portion of the script the game is on
        currentLine = 0; //which line of text the text box is on
        if(altTrig==""){proceed=true;} // indicates normal txt box treatment
        else{proceed = false; alternateTrigger=altTrig;} // indicates alternate trigger for txt box
        choicedQ = choiceAtEnd; // indicates if the line of dialogue contains a choice
        playerChoices = choices; 
        dialoguePanel.SetActive(true);
        choicesPanel.SetActive(false);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
            //AudioManager call to play clips based on the current index
            if(currentSIndex==1){
                audioController.PlayVoiceOverAudio(currentSIndex+currentLine);
            }
            else if(currentSIndex==2){} //no audio
            else if(currentSIndex==3){
                audioController.PlayVoiceOverAudio((2+currentSIndex)+currentLine); //start audio at clip 5
            }
            else if(currentSIndex==4){} //no audio
            else if(currentSIndex==5){
                audioController.PlayVoiceOverAudio((4+currentSIndex) +currentLine); //start audio at clip 9
                if(9+currentLine == 12){
                    choiceStop = true;
                }
            }
            else if(currentSIndex==6){ //Burned
                audioController.PlayVoiceOverAudio((7+currentSIndex) +currentLine); //start at audio clip 13 
            }
            else if(currentSIndex==7){ //Light
                audioController.PlayVoiceOverAudio((12+currentSIndex) +currentLine); //start at audio clip 19
            }
            else if (currentSIndex==8){
                audioController.PlayVoiceOverAudio((15+currentSIndex) +currentLine); //start at audio clip 23
                if(23+currentLine == 26){
                    putOutFlame=true;
                }
            }
            
            currentLine++;
        }
        else{
            Debug.Log("Ended Dialogue");
            EndDialogue();
        }
        
        if(currentLine == dialogueLines.Length && choicedQ){ //Choices
            ShowChoices(playerChoices);
        }
        
        if(currentLine == dialogueLines.Length && !proceed) //Alt trig
        {
            altTrigger();
        }
        

    }

    void ShowChoices(string[] choices)
    {
        dialoguePanel.SetActive(true);
        choicesPanel.SetActive(true);

        // Assign choices to buttons
        for (int i = 0; i < choices.Length; i++)
        {
            int index = i;  // Local copy for closure
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => SelectChoice(index));
        }
    }
    void SelectChoice(int choiceIndex)
    {
        if(choiceIndex == 0){
            Debug.Log("Burned Choice");
            choice = "Burned";
        }
        else if(choiceIndex == 1){
            Debug.Log("Light Choice");
            choice = "Light";
        }
        choiceStop = false;
        EndDialogue();  // End dialogue after making a choice
    }

    void altTrigger()
    {
        if(alternateTrigger == "F"){
            altTrigF = true;
        }
        else if (alternateTrigger == "E"){
            altTrigF = false;
        }
        //IMPLEMNT E TRIGGER
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
        OnDialogueEnd?.Invoke();
        Debug.Log("Invoked OnDialogueEnd");
    }


}