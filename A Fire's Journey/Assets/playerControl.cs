using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public Animator animator;

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

}
