using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class ScaleAttack : MonoBehaviour
{
	private List<GameObject> notes;
	public GameObject Bar;
	public GameObject start;
	public GameObject end;
	public float speed;
	public float speedModifier = 1;
	private float deltaSpeedModifier = 0;
	public bool cheatmode;
	public int cheatnote;

	private bool barIsMoving = false;

	private GameObject miss = null;
	private GameObject note;
	private float ClosestNoteDistance;
	private GameObject[] notesArray;

	// Start is called before the first frame update
	void Start()
	{
		notes = new List<GameObject>();
		notesArray = GameObject.FindGameObjectsWithTag("Note");

		foreach (GameObject element in notesArray) {
			notes.Add(element);
		}

		deltaSpeedModifier = speedModifier;

		StartAttack();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L)) {
			StartAttack();
		}

		//refactor this to show animations
		if (cheatmode) {
			EndAttack(notes[cheatnote]);
		}

		if (barIsMoving) {
			Bar.transform.position += new Vector3(speed * deltaSpeedModifier, 0, 0);
			deltaSpeedModifier += speedModifier;
			if (Bar.transform.position.x > end.transform.position.x) {
				EndAttack(miss); //no note selected / miss
				deltaSpeedModifier = 0;
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				ClosestNoteDistance = float.MaxValue;
				foreach (GameObject element in notes) {
					float dist = (Bar.transform.position - element.transform.position).magnitude;
					if (dist < ClosestNoteDistance) {
						ClosestNoteDistance = dist;
						note = element;
					} 
				}
				deltaSpeedModifier = 0;
				EndAttack(note);
			}
		}
	}

	public void StartAttack() {
		Bar.transform.position = start.transform.position;
		barIsMoving = true;
	}

	private void EndAttack(GameObject note) {
		//something in manager to tell it the note
		if (note == null) {
			Debug.Log("miss");
		} else {
			Debug.Log(note.name.ToString());
		}

		barIsMoving = false;
	}
}
