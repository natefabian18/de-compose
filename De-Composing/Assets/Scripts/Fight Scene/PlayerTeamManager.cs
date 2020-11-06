﻿using System;
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

	private FightSceneManager FightSceneScript;

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

			FightSceneScript = GameObject.FindGameObjectWithTag("FightSceneManager").GetComponent<FightSceneManager>();
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

		PlayerHealthBar.GetComponent<HealthBar>().staticHealthUpdate(TeamHealth/maxTeamHealth);
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
			calcAndApplyAttack();
		}
	}

	//base attack power per note is 10
	private void calcAndApplyAttack() {
		//TODO: check for attack modifiers: chord
		int[] attacks = new int[3];
		float damageToSend;
		bool isHealing = false;
		//is harp in play?

		for (int i = 0; i < attackTurns.Length; i++) {
			int.TryParse((attackTurns[i].name.Substring(attackTurns[0].name.Length - 1)), out attacks[i]);
		}

		damageToSend = attacks[0] + attacks[1] + attacks[2]; //general damage

		//all same note bonus
		if (attacks[0] == attacks[1] && attacks[1] == attacks[2]) {
			damageToSend = attacks[0] * 4;
		}

		//chord test

		Debug.Log(attacks[0]);
		//send damage
		FightSceneScript.PlayerAttack(damageToSend * 10, isHealing);
		attackTurns[0] = null;
		attackTurns[1] = null;
		attackTurns[2] = null;
	}
}
