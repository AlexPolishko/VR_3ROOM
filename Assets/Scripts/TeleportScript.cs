using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour {

	public GameObject[] Rooms;
	public Prizm_Manager[] ControlPanels;

	public GameObject Player;
	private static TeleportScript _instance;
	Vector3 PlayerLocalPosition;
	Quaternion PlayerLocalRotation;
	private static bool isTeleportation;
	public GameObject TeleportSystem;
	public float TeleportJumpMoment =0.1f;
	public static TeleportScript GetInstance()
	{
		return _instance;
	}
	void Awake()
	{
		_instance = this;
	}
	public static bool IsTeleportation()
	{
		return isTeleportation;
	}

	public void JumptoRoom(int RoomNo)
	{
		PlayerLocalPosition = Player.transform.localPosition;
		PlayerLocalRotation = Player.transform.localRotation;

		Player.transform.parent = Rooms [RoomNo].transform;

		Player.transform.localPosition = PlayerLocalPosition;
		Player.transform.localRotation = PlayerLocalRotation;

		for (int i = 0; i < 3; i++)
			if (i == RoomNo)
				Rooms [i].SetActive (true);
			else
				Rooms [i].SetActive (false);
	}

	IEnumerator TeleportCourutine(int RoomNo)
	{
		bool jump = false;
		isTeleportation = true;
		Debug.Log ("Teleport Start");
		TeleportSystem.SetActive (true);
		float TeleportHeight = -2f;
		while (TeleportHeight<2f)
		{
			TeleportHeight += Time.deltaTime;
			TeleportSystem.transform.localPosition = new Vector3 (0f, TeleportHeight, 0f);
			if ((TeleportHeight > TeleportJumpMoment) && (!jump))
				TeleportScript.GetInstance ().JumptoRoom (RoomNo);
			yield return null;
		}
		TeleportSystem.SetActive (false);
		isTeleportation = false;
		Debug.Log ("Teleport Finish");
	}
	public void StartTeleport(int RoomNo)
	{
		if (!IsTeleportation()) 
		{
			StopAllCoroutines ();
			TeleportSystem.transform.localPosition = new Vector3 (0f, -1f, 0f);
			//TeleportSystem.GetComponent<AudioSource> ().Play ();
			Debug.Log ("Play");
			StartCoroutine (TeleportCourutine (RoomNo));
		}
	}

}
