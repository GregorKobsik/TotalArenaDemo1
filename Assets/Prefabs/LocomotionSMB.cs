using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionSMB : StateMachineBehaviour {

    public float m_Damping = 0.15f;

    private readonly int m_HashHorizontalPara = Animator.StringToHash("Horizontal");
    private readonly int m_HashVerticalPara = Animator.StringToHash("Vertical");

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        animator.SetFloat(m_HashHorizontalPara, horizontal, m_Damping, Time.deltaTime);
        animator.SetFloat(m_HashVerticalPara, vertical, m_Damping, Time.deltaTime);



    }
}
