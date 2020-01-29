using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	// Stores the Target Position
	public Vector3 TargetPosition;

	// Lerp Speed/Smoothness
	public float Smoothness = 1f;

    // Start is called before the first frame update
    void Start()
    {
		// Assign Current Camera Script 
		GameController.instance.CurrentCameraFollowScript = this;

		// Set Current Target Position
		TargetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = Vector3.Lerp(transform.position, TargetPosition, Smoothness * Time.deltaTime);
    }
}
