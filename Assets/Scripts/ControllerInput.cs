using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

	// Use this for initialization
	public GameObject controllerPivot;

	public GameObject SwordRay;
	public ParticleSystem SwordParticle1;


	private static ControllerInput _instance;
	public static ControllerInput GetInstance()
	{
		return _instance;
	}
	void Awake()
	{
		_instance = this;
	}
	// Update is called once per frame


	void Update () {
		if (GvrController.State != GvrConnectionState.Connected) {
			controllerPivot.SetActive(false);
		}
		controllerPivot.SetActive(true);
		controllerPivot.transform.rotation = GvrController.Orientation;

		int Layermask = 1 << 8;
		Ray ray = new Ray (SwordRay.transform.position, SwordRay.transform.parent.transform.forward);
		Debug.DrawRay (SwordRay.transform.position, SwordRay.transform.parent.transform.forward*10f);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100f, Layermask)) 
		{
			if (hit.collider != null) 
			{
				Debug.Log ("hit "+hit.collider.gameObject.name);
				for (int i=0;i<3;i++)
					for (int j=0;j<3;j++)
						if (TeleportScript.GetInstance().ControlPanels[i].Prizms[j]==hit.collider.gameObject)
							TeleportScript.GetInstance().ControlPanels[i].PrizmSelected (hit.collider.gameObject.name, GvrController.IsTouching);
			}
		}
		else 
			for (int i=0;i<3;i++)
				TeleportScript.GetInstance().ControlPanels[i].PrizmSelected ("none",false);

		if (GvrController.IsTouching) 
		{
			SwordParticle1.Play ();
		}
		else 
			SwordParticle1.Stop ();
		
	}
}
