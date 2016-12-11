using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

	// Use this for initialization
	public GameObject controllerPivot;
	
	// Update is called once per frame
	void Update () {
		if (GvrController.State != GvrConnectionState.Connected) {
			controllerPivot.SetActive(false);
		}
		controllerPivot.SetActive(true);
		controllerPivot.transform.rotation = GvrController.Orientation;
	}
}
