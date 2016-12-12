using UnityEngine;
using System.Collections;

public class Prizm_Manager : MonoBehaviour {

	private static Prizm_Manager _instance;
	public static Prizm_Manager GetInstance()
	{
			return _instance;
	}

	void Awake()
	{
		_instance = this;
	}
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

	void FadeOut(int number) {
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
	}
	
	// Update is called once per frame
	public void PrizmHighlighted(string nameselected)
	{
		

		for (int i = 0; i < 3; i++) 
		{
			
			if (Prizms [i].name == nameselected)
					FadeIn (i);
			else
				FadeOut(i);
		}
	}

}
