using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
	public float offsetDistance;
	public int buttonCount;

	private int selected = 0;

	private FightSceneManager fightSceneScript;

	//debug only

	private void Start()
	{
		fightSceneScript = GameObject.FindGameObjectWithTag("FightSceneManager").GetComponent<FightSceneManager>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) {
			MoveLeft();
		}
		
		if (Input.GetKeyDown(KeyCode.D)) {
			MoveRight();
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			tossSelection();
		}
	}

	//debug only

	public void MoveLeft() {
		if (selected == 0) {
			return;
		}

		this.transform.position -= new Vector3(offsetDistance, 0, 0);
		selected--;
	}

	public void MoveRight() {
		if (selected == buttonCount - 1) {
			return;
		}
		this.transform.position += new Vector3(offsetDistance, 0, 0);
		selected++;
	}

	private void tossSelection() {
		switch (selected) {
			case 0:
				GameObject.FindGameObjectWithTag("FightSceneManager");
				fightSceneScript.ButtonSelect("Fight");
				break;
			case 1:
				GameObject.FindGameObjectWithTag("ChordMenu").GetComponent<SpriteRenderer>().enabled ^= true;
				GameObject.FindGameObjectWithTag("TutText").GetComponent<Text>().enabled ^= true;
				break;
			case 2:
				break;
			default:
				Debug.LogError("no button selected");
				break;
		}
	}
}
