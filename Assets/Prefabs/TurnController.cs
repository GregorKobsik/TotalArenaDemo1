using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour {

    public float HorizontalAxisSensitivity = 10f;
    public float VerticalAxisSensitivity = 10f;

    private float m_TurnSpeedHorizontal = 0;
    private float m_TurnSpeedVertical = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float horizontal = Input.GetAxis("HorizontalRight");
        float vertical = Input.GetAxis("VerticalRight");

        Debug.Log(vertical);

        m_TurnSpeedHorizontal = (m_TurnSpeedHorizontal + horizontal * HorizontalAxisSensitivity) * 0.1f ;
        m_TurnSpeedVertical = (m_TurnSpeedVertical + vertical * VerticalAxisSensitivity) * 0.1f;



        transform.Rotate(Vector3.right, m_TurnSpeedVertical);
        transform.RotateAround(transform.position, Vector3.up, m_TurnSpeedHorizontal);
        


    }
}
