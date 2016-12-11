using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

	// Use this for initialization
	public GameObject controllerPivot;
	public GameObject TeleportSystem;

	// Update is called once per frame
	IEnumerator TeleportCourutine()
	{
		Debug.Log ("Teleport Start");
		TeleportSystem.SetActive (true);
		float TeleportHeight = -2f;
		while (TeleportHeight<3f)
		{
			TeleportHeight += Time.deltaTime;
			TeleportSystem.transform.localPosition = new Vector3 (0f, TeleportHeight, 0f);
			Debug.Log ("Teleport Finish "+TeleportHeight.ToString());
			yield return null;
		}
		TeleportSystem.SetActive (false);
		Debug.Log ("Teleport Finish");
	}

	void Update () {
		if (GvrController.State != GvrConnectionState.Connected) {
			controllerPivot.SetActive(false);
		}
		controllerPivot.SetActive(true);
		controllerPivot.transform.rotation = GvrController.Orientation;


		if (Input.GetMouseButtonDown (0)) 
		{
			Debug.Log ("Teleport");
			StartCoroutine (TeleportCourutine());
		}
	}
}
