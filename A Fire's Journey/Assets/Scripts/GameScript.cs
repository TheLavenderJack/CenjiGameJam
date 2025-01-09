using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    //Form of strings for Dialogue Manager: StartDialogue(string[] newDialogueLines, bool altTrig, bool choiceAtEnd, string[] choices)

    //First string dedicated to defining type of dialogue
    /*
    If "choice", then next two strings are the two choices
    If "altTrig", 2nd str (index [1]) is the button to push
    If "norm", proceed regularly
    */



    static string[] startStr = new string[] {"altTrig", "F","Press F to light your Flame"};
    static string[] path1_Str1 = new string[] {"norm", 
    "Every Fire starts off small.",
    "Sometimes all we want is for our Fire to grow, to expand.",
    "But often times we forget to appreciate it.",
    "Such a small Fire is easy to control, whereas later on..."
    };
    static string[] path1_transition = new string[] {"altTrig", "F","Press F to help your Fire grow"};



    
    string[][] scriptHolder = new string[][] {startStr, path1_Str1, path1_transition};


    public string[] getNextScript(int index){
        return scriptHolder[index];
    }

}
