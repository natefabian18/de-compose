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
	private int[] cheatNotes;
	private GameObject[] cheatednotes;
	private bool[] cheatNoteSelected;

	private PlayerTeamManager Player;
	private EnemyTeamManager Enemy;

	private bool attacking;

	public int playerCharAttacking;
	private int enemyNoteTicker;
	private AudioManager Sound;

	// Start is called before the first frame update
	void Start()
	{
		cheatednotes = new GameObject[3];
		notes = new List<GameObject>();
		notesArray = GameObject.FindGameObjectsWithTag("Note");

		foreach (GameObject element in notesArray) {
			notes.Add(element);
		}

		deltaSpeedModifier = speedModifier;

		Sound = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

		Bar.GetComponent<SpriteRenderer>().enabled = false;
		Player = GameObject.FindGameObjectWithTag("PlayerTeam").GetComponent<PlayerTeamManager>();
		Enemy = GameObject.FindGameObjectWithTag("EnemyTeam").GetComponent<EnemyTeamManager>();
		cheatNoteSelected = new bool[3];
		playerCharAttacking = 0;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L)) {
			StartAttack(true);
		}

		//refactor this to show animations
		if (cheatmode) {
			Bar.transform.position += new Vector3(speed * deltaSpeedModifier, 0, 0);
			deltaSpeedModifier += speedModifier;

			for (int i = 0; i < cheatNoteSelected.Length; i++) {
				if (cheatNoteSelected[i] == false) {
					if ((Bar.transform.position - cheatednotes[i].transform.position).magnitude < 0.1) {
						cheatNoteSelected[i] = true;
						if (cheatednotes[enemyNoteTicker].name != "Miss0")
						{
							Sound.pickSoundToPlay(Enemy.Characters[0].name, int.Parse(cheatednotes[enemyNoteTicker].name[cheatednotes[enemyNoteTicker].name.Length - 1 ].ToString()));
						}
						Bar.transform.position = start.transform.position;
						deltaSpeedModifier = 0;
					}
				}

				if (cheatNoteSelected[cheatNoteSelected.Length - 1] == true) {
					//end attack
					Debug.Log("end enemy attack");
					endCheatAttack();
				}
			}
		}

		

		if (barIsMoving) {
			Bar.transform.position += new Vector3(speed * deltaSpeedModifier, 0, 0);
			if (Constants.C.isPrecision)
			{
				deltaSpeedModifier += speedModifier * 0.5f;
			}
			else
			{
				deltaSpeedModifier += speedModifier;
			}
			if (Bar.transform.position.x > end.transform.position.x) {
				EndAttack(notes[8]); //no note selected / miss
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

	public void StartAttack(bool attacker) {
		playerCharAttacking += 1;
		cheatmode = false;
		Bar.transform.position = start.transform.position;
		barIsMoving = true;
		Bar.GetComponent<SpriteRenderer>().enabled = true;
		attacking = attacker;
	}

	private void EndAttack(GameObject note) {
		Bar.GetComponent<SpriteRenderer>().enabled = false;
		barIsMoving = false;
		if (note.name != "Miss0") {
			Sound.pickSoundToPlay(Constants.C.selectedPlayers[playerCharAttacking - 1].name, int.Parse(note.name[note.name.Length - 1].ToString()) - 1);
		}
		Bar.transform.position = start.transform.position;
		//something in manager to tell it the note
		Player.AttackRegister(note);
	}

	public void startCheatAttack(int[] selectedNote) {
		cheatmode = true;
		Debug.Log($"{selectedNote[0]}\n{selectedNote[1]}\n{selectedNote[2]}");
		cheatNotes = selectedNote;
		Debug.Log($"selectedNote {selectedNote[0]}\ncheatNotes {cheatNotes[0]}\nnotesArray {notesArray[cheatNotes[0]]}");
		cheatednotes[0] = notesArray[cheatNotes[0]];
		cheatednotes[1] = notesArray[cheatNotes[1]];
		cheatednotes[2] = notesArray[cheatNotes[2]];
		cheatNoteSelected[0] = false;
		cheatNoteSelected[1] = false;
		cheatNoteSelected[2] = false;
		enemyNoteTicker = 0;
		Bar.GetComponent<SpriteRenderer>().enabled = true;
	}

	private void endCheatAttack() {
		Bar.GetComponent<SpriteRenderer>().enabled = false;
		barIsMoving = false;
		//#stopthebar

		Enemy.checkForfinishedAttack(cheatednotes);

		cheatmode = false;
	}

	/* 
		selectnote
		startmoveing to bar till dist to selected < 0.1
		move bar back and select next note
		once all notes selected end the attack with the cheat notes
		send attack
		check who dead
		start player turn again
	 */
}
