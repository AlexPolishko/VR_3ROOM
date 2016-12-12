using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

	// Use this for initialization
	public GameObject controllerPivot;
	public GameObject TeleportSystem;
	public GameObject SwordRay;

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

		int Layermask = 1 << 8;
		Ray ray = new Ray (SwordRay.transform.position, SwordRay.transform.forward);
		Debug.DrawRay (SwordRay.transform.position, SwordRay.transform.forward*100f);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100f, Layermask))
		if (hit.collider != null)
			Prizm_Manager.GetInstance ().PrizmSelected (hit.collider.gameObject.name,GvrController.IsTouching);
		else 
			Prizm_Manager.GetInstance ().PrizmSelected ("none",false);

		if (Input.GetMouseButtonDown (0)) 
		{
			Debug.Log ("Teleport");
			StartCoroutine (TeleportCourutine());
		}
	}
}
