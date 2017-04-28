using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCharControl : MonoBehaviour {

    public Animator animator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") > 0.1f) 
        {
            animator.SetBool("Walk Forward", true);
        }
        else
        {
            animator.SetBool("Walk Forward", false);
        }

        if (Input.GetAxis("Vertical") < -0.1f)
        {
            animator.SetBool("Walk Backward", true);
        }
        else
        {
            animator.SetBool("Walk Backward", false);
        }

        if (Input.GetKeyDown("Fire1"))
        {
            animator.SetTrigger("PunchTrigger");
        }
    }
}
