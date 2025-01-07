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
    private bool proceed = true;

    void Start()
    {
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
    }

    void Update() {
        if (dialoguePanel.activeInHierarchy){
            if(Input.GetKeyDown(KeyCode.X)){
                ShowNextLine();
            }
        }
    }

    public void StartDialogue(string[] newDialogueLines)
    {
        dialogueLines = newDialogueLines;
        currentLine = 0;
        dialoguePanel.SetActive(true);
        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (currentLine < dialogueLines.Length && proceed)
        {
            dialogueText.text = dialogueLines[currentLine];
            currentLine++;
        }
        else
        {
            EndDialogue();
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
        // Handle the player's choice here, possibly starting another dialogue or affecting gameplay
        Debug.Log("Player chose option: " + choiceIndex);
        EndDialogue();  // End dialogue after making a choice
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
    }
}