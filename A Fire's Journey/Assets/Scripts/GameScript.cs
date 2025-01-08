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
    static string[] test1 = new string[] {"choice", "Choice A", "Choice B", "This is a choice questions with multiple boxes", "Now... choose wisely."};
    static string[] test2 = new string[] {"norm","A normal txt box you can get rid of with X", "some cool txt", "some morecool txt"};
    static string[] path1_Str1 = new string[] {"choice", "Choice A", "Choice B", "This is a choice questions with multiple boxes", "Now... choose wisely."};
    static string[] path1_Str2 = new string[] {"norm","A normal txt box you can get rid of with X", "some cool txt", "some morecool txt"};



    
    string[][] scriptHolder = new string[][] {startStr, test1, test2};


    public string[] getNextScript(int index){
        return scriptHolder[index];
    }

}
