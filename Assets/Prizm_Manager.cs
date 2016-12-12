using UnityEngine;
using System.Collections;

public class Prizm_Manager : MonoBehaviour {



	public int CurrentRoomNo;
	public static float LEVEL_OF_BEAM = 0.4f;

	// Use this for initialization
	public GameObject[] Prizms;
	public Material[] PrizmsMaterials;
	private float[] Levels = new float[3];
	private bool[] CorutinaBusy = new bool [3];

	void ChangeColor(int number, float f)
	{
		Color c = PrizmsMaterials [number].GetColor ("_EmissionColor");
		switch (number) 
		{
		case 0:
			c.g = f;
			c.b = f;
			break;
		case 1:
			c.g = f;
			c.r = f;
			break;
		case 2:
			c.b = f;
			c.r = f;
			break;
		}
		Levels [number] = f;
		PrizmsMaterials [number].SetColor ("_EmissionColor", c); 
	}

	void Pressed(int number)	// Push prizm
	{
		if (Prizms [number].transform.localPosition.z<0.6f)
		Prizms [number].transform.Translate (new Vector3 (0f, 0f,Time.deltaTime));

		if (Prizms [number].transform.localPosition.z >LEVEL_OF_BEAM)
			TeleportScript.GetInstance().StartTeleport (number);
			
	}
	void UnPressed(int number)
	{
		if (Prizms [number].transform.localPosition.z>0f)
		Prizms [number].transform.Translate (new Vector3 (0f, 0f,-Time.deltaTime));
	}

	void FadeOut(int number) {	// fade out prizm form white to color
		if (Levels[number] > 0f) 
		{
			Levels[number] -= Time.deltaTime;
			ChangeColor (number, Levels[number] );
		}

	}
	void  FadeIn(int number) {
		if (Levels[number] < 1f) 
		{
			Levels[number] += Time.deltaTime;
			ChangeColor (number, Levels[number] );
		}
		}
	void Start () {

		PrizmsMaterials [0].SetColor ("_EmissionColor", Color.red);
		PrizmsMaterials [1].SetColor ("_EmissionColor",Color.blue);
		PrizmsMaterials [2].SetColor ("_EmissionColor", Color.green);
		Prizms [CurrentRoomNo].transform.localPosition = new Vector3 (Prizms [CurrentRoomNo].transform.localPosition.x,Prizms [CurrentRoomNo].transform.localPosition.y,0.7f);

	}
	
	// Update is called once per frame
	public void PrizmSelected(string nameselected, bool isTouch)
	{
		if (!TeleportScript.IsTeleportation())
		for (int i = 0; i < 3; i++) 
		{
			if (i != CurrentRoomNo) 
			{
				if (Prizms [i].name == nameselected) {
					FadeIn (i);
					if (isTouch)
						Pressed (i);
					else
						UnPressed (i);
				} else {
					FadeOut (i);
					UnPressed (i);
				}
			}
		}
	}



}
