using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Taking player attacks to be dealt
 * Tracking Health for the player Team
 * Player Positions in the team
 * Chord/Bonus attack checking
 * Applying and receaving damage //done
 */

public class PlayerTeamManager : MonoBehaviour
{
	public float TeamHealth = 100;
	private float maxTeamHealth;
	private GameObject PlayerHealthBar;
	private List<GameObject> CharecterPoints;
	private List<GameObject> Charecters;

	//refactor me please
	public GameObject Trumpet;

	private bool takingTurn = false;
	private GameObject[] attackTurns;

	private ScaleAttack Scale;

	// Start is called before the first frame update
	void Start()
	{
		try
		{
			maxTeamHealth = TeamHealth; //replace with max health from const file when avilable
			PlayerHealthBar = this.transform.Find("PlayerHealthBar").gameObject;

			CharecterPoints = new List<GameObject>();
			CharecterPoints.Add(this.transform.Find("PlayerPoint1").gameObject);

			Charecters = new List<GameObject>();
			//this is loaded in form const party file
			Charecters.Add(Trumpet);

			UpdateHealth(0);

			attackTurns = new GameObject[3];
			for (int i = 0; i < attackTurns.Length; i++) {
				attackTurns[i] = null;
			}
			Scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<ScaleAttack>();
		}
		catch (NullReferenceException e) {
			Debug.LogError($"missing: {e} Did you set all your wires or rename something?");
		}
	}

	private void Update()
	{
		if (takingTurn)
		{
			checkForfinishedAttack();
		}
	}

	public void UpdateHealth(float change) {
		TeamHealth += change;

		PlayerHealthBar.GetComponent<HealthBar>().HealthUpdate(TeamHealth/maxTeamHealth);
	}

	public void startTeamAttack() {
		takingTurn = true;
		//call scale
		Scale.StartAttack(true);
	}

	public void AttackRegister(GameObject note) {
		//scale calls back home
		for (int i = 0; i < attackTurns.Length; i++) {
			if (attackTurns[i] == null) {
				attackTurns[i] = note;
				if (i != 2) {
					//call scale attack again
					Scale.StartAttack(true);
				}
				return;
			}
		}
	}

	private void checkForfinishedAttack() { 
		if (attackTurns[0] != null && attackTurns[1] != null && attackTurns[2] != null) {
			takingTurn = false;
			Debug.Log("SUCCESSFUL ATTACK");
			//call team attack for calc
		}
	}
}
