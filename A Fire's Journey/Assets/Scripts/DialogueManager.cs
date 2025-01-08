using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public GameObject choicesPanel;  // Panel for choices
    public Button[] choiceButtons;  // Buttons for the choices
    private string[] dialogueLines;
    private int currentLine = 0;
    private bool proceed;
    private bool choicedQ;
    private string[] playerChoices;
    string alternateTrigger;

    //Alt trigger button bools
    private bool altTrigF;
    void Start()
    {

    }

    void Update() {
        if (dialoguePanel.activeInHierarchy){ //checking for X input
            if(Input.GetKeyDown(KeyCode.X)){
                ShowNextLine();
            }
        }

        if (dialoguePanel.activeInHierarchy && altTrigF){ //checking for F input in special case
            if(Input.GetKeyDown(KeyCode.F)){
                Debug.Log("Now LIGHT that fuckin fire");
                altTrigF = false;
                EndDialogue();
            }
        }
    }

    public void StartDialogue(string[] newDialogueLines, string altTrig, bool choiceAtEnd, string[] choices)
    {
        dialogueLines = newDialogueLines; //the arr of strings that make up the text
        currentLine = 0; //which line of text the text box is on
        if(altTrig==""){proceed=true;} // indicates normal txt box treatment
        else{proceed = false; alternateTrigger=altTrig;} // indicates alternate trigger for txt box
        choicedQ = choiceAtEnd; // indicates if the line of dialogue contains a choice
        playerChoices = choices; 
        dialoguePanel.SetActive(true);
        choicesPanel.SetActive(false);
        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            Debug.Log("In ShowNextLine with currentLine<dialogueLines.Length");
            dialogueText.text = dialogueLines[currentLine];
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
        EndDialogue();  // End dialogue after making a choice
    }

    void altTrigger()
    {
        Debug.Log("made it into AltTrigger method");
        if(alternateTrigger == "F"){
            altTrigF = true;
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
    }


}