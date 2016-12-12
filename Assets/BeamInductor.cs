using UnityEngine;
using System.Collections;

public class BeamInductor : MonoBehaviour {

	// Use this for initialization
	public LineRenderer linerenderer;
	public Vector3 StartV3, FinishV3;
	public GameObject sword;


	void Start()
	{
		linerenderer.SetWidth (0.05f, 0.02f);


	}

	// Update is called once per frame
	void Update () {
		linerenderer.SetPosition (0, sword.transform.position);
		linerenderer.SetPosition (1,  sword.transform.position+ transform.forward* 2f);
	}
}
