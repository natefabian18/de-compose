using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
	public float offsetDistance;
	public int buttonCount;

	private int selected = 0;

	//debug only

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) {
			MoveLeft();
		}
		
		if (Input.GetKeyDown(KeyCode.D)) {
			MoveRight();
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
}
