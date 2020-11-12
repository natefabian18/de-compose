using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneManager : MonoBehaviour
{
	public int playersSelected = 0;

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
					break;
				case "Drum":
					Debug.Log("Drum");
					break;
				case "Maracas":
					Debug.Log("Maracas");
					break;
				case "Xylophone":
					Debug.Log("Xylophone");
					break;
				case "Harp":
					Debug.Log("Harp");
					break;
				default:
					Debug.LogError("button tossed invalid peram");
					break;
			}
		}

		if (playersSelected == 3) {
			Debug.Log("load next scene");
		}
	}
}
