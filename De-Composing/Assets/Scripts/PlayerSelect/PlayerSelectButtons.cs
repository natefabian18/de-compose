using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectButtons : MonoBehaviour
{
	public float offsetDistance;
	public int buttonCount;

	private int selected = 0;

	private PlayerSceneManager playerSceneScript;

	private bool[] taken;

	//debug only

	private void Start()
	{
		playerSceneScript = GameObject.FindGameObjectWithTag("PlayerSceneManager").GetComponent<PlayerSceneManager>();
		taken = new bool[buttonCount];
		for (int i = 0; i < taken.Length; i++) {
			taken[i] = false;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			MoveLeft();
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			MoveRight();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			tossSelection();
		}
	}

	//debug only

	public void MoveLeft()
	{
		if (selected == 0)
		{
			return;
		}

		this.transform.position -= new Vector3(offsetDistance, 0, 0);
		selected--;
	}

	public void MoveRight()
	{
		if (selected == buttonCount - 1)
		{
			return;
		}
		this.transform.position += new Vector3(offsetDistance, 0, 0);
		selected++;
	}

	private void tossSelection()
	{
		if (taken[selected] == false)
		{
			switch (selected)
			{
				case 0:
					GameObject.FindGameObjectWithTag("PlayerSceneManager");
					playerSceneScript.ButtonSelect("Trumpet");
					taken[selected] = true;
					break;
				case 1:
					GameObject.FindGameObjectWithTag("PlayerSceneManager");
					playerSceneScript.ButtonSelect("Harp");
					taken[selected] = true;
					break;
				case 2:
					GameObject.FindGameObjectWithTag("PlayerSceneManager");
					playerSceneScript.ButtonSelect("Xylophone");
					taken[selected] = true;
					break;
				case 3:
					GameObject.FindGameObjectWithTag("PlayerSceneManager");
					playerSceneScript.ButtonSelect("Drum");
					taken[selected] = true;
					break;
				case 4:
					GameObject.FindGameObjectWithTag("PlayerSceneManager");
					playerSceneScript.ButtonSelect("Maracas");
					taken[selected] = true;
					break;
				default:
					Debug.LogError("no button selected");
					break;
			}
		}
	}
}
