using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
	public static Constants C;
	public GameObject[] selectedPlayers;
	public Vector2 mapPositionGlobal;
	public bool enemy1dead = false;
	public bool enemy2dead = false;
	public bool bossdead= false;

	public bool isHealing = false;

	private void Awake()
	{
		C = this;
	}
	// Start is called before the first frame update
	void Start()
	{
		selectedPlayers = new GameObject[3];
		mapPositionGlobal = new Vector2(-6.74f, 4.24f);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void AssignPlayer(GameObject playerSelection) {
		for (int i = 0; i < selectedPlayers.Length; i++) {
			if (selectedPlayers[i] == null) {
				selectedPlayers[i] = playerSelection;
				break;
			}
		}
	}
}
