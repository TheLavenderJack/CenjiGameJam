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
    static string[] path2_Str1 = new string[] {"norm",
        "Everyone's Fire is different, even when they may look the same on the outside.",
        "Each Flame burns with different fuel, casts different shadows, warms different hearts.",
        "Some enjoy the comfort of their heat;",
        "Others wish that their Flame was different somehow."
    };
    public static string[] path2_transition = new string[] {"altTrig", "F", "Press F to fan your Flames"};
    static string[] path3_Str1 = new string[] {"choice",
        "Burned",
        "Light",
        "Some moments, while fleeting, still remain burned into our memories.",
        "They may leave scars...",
        "Or they may light up the darkness.",
        "What kind of Fire have you felt recently?"
    };
    static string[] pathBurned_Str1 = new string[] {"norm",
        "Ah... a raging fire... it's not something easy to overcome.",
        "Our scars can remind us of our pain...",
        "but they also reshape who we are.",
        "Sometimes our own Flames seem out of control, or seem about to peeter out, but...",
        "Every person's Flame, including your own, brings light to other people's worlds.",
        "Take care to remember that.",
    };
    static string[] pathLight_Str1 = new string[] {"norm",
        "Ah... a calm Flame...",
        "a place to rest, a reprieve from the darkness.",
        "May you be a Flame that burns bright for others, a light that people may follow.",
        "However, do remember to tend to your own Flames, not just others.",
    };
    public static string[] pathFinal = new string[] {"altTrig", "E",
        "Each and every Flame is unique,",
        "and each one lights the path forward,",
        "until finally, gracefully...",
        "Press E"
    };



    
    string[][] scriptHolder = new string[][] {
        startStr, path1_Str1, path1_transition, path2_Str1, 
        path2_transition, path3_Str1, pathBurned_Str1,
        pathLight_Str1, pathFinal};


    public string[] getNextScript(int index){
        return scriptHolder[index];
    }

}
