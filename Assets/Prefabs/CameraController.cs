using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform myCamera;
    public Transform boundingBox;

    private bool startupCheck = false;
    private float m_moveVelocity;

	// Use this for initialization
	void Start () {
		if (myCamera != null && boundingBox != null)
        {
            startupCheck = true;
        } else
        {
            startupCheck = false;
            Debug.Log("Camera Controller: Error at start, camera or bounding box not assigned");
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (startupCheck)
        {
            float currDistance = myCamera.GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip>().closestDistance;
            float targetDistance = boundingBox.GetComponent<SetBoundsScript>().GetCameraDistance();
            myCamera.GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip>().closestDistance = Mathf.SmoothDamp(currDistance, targetDistance, ref m_moveVelocity, 0.5f);
            Vector3 bbPos = boundingBox.GetComponent<Transform>().position;
            myCamera.GetComponent<Transform>().position.Set(bbPos.x, 0, bbPos.z);
        }
	}
}
