using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneManager : MonoBehaviour
{
	public int playersSelected = 0;

	public GameObject[] playerOptions;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void ButtonSelect(string selection) {
		playersSelected++;

		if (playersSelected <= 3) {
			switch (selection) {
				case "Trumpet":
					Debug.Log("Trumpet");
					Constants.C.AssignPlayer(playerOptions[0]);
					break;
				case "Drum":
					Debug.Log("Drum");
					Constants.C.AssignPlayer(playerOptions[3]);
					break;
				case "Maracas":
					Debug.Log("Maracas");
					Constants.C.AssignPlayer(playerOptions[4]);
					break;
				case "Xylophone":
					Debug.Log("Xylophone");
					Constants.C.AssignPlayer(playerOptions[2]);
					break;
				case "Harp":
					Debug.Log("Harp");
					Constants.C.AssignPlayer(playerOptions[1]);
					break;
				default:
					Debug.LogError("button tossed invalid peram");
					break;
			}
		}

		if (playersSelected == 3) {
			Debug.Log("load next scene");
			UnityEngine.SceneManagement.SceneManager.LoadScene("Overworld");
		}
	}
}
