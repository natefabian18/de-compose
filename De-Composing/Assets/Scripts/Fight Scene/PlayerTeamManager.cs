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

	private bool takingTurn = false;
	private GameObject[] attackTurns;

	private ScaleAttack Scale;

	private FightSceneManager FightSceneScript;

	// Start is called before the first frame update
	void Start()
	{

		maxTeamHealth = TeamHealth; //replace with max health from const file when avilable
		PlayerHealthBar = this.transform.Find("PlayerHealthBar").gameObject;

		CharecterPoints = new List<GameObject>();
		CharecterPoints.Add(this.transform.Find("PlayerPoint1").gameObject);

		Charecters = new List<GameObject>();
		//this is loaded in form const party file
		for (int i = 0; i < Constants.C.selectedPlayers.Length; i++)
		{
			Charecters.Add(Constants.C.selectedPlayers[i]);
		}

		initilizeTeamPosition();


		UpdateHealth(0);

		attackTurns = new GameObject[3];
		for (int i = 0; i < attackTurns.Length; i++)
		{
			attackTurns[i] = null;
		}
		Scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<ScaleAttack>();

		FightSceneScript = GameObject.FindGameObjectWithTag("FightSceneManager").GetComponent<FightSceneManager>();

	}

	private void Update()
	{
		if (takingTurn)
		{
			checkForfinishedAttack();
		}
	}

	public void UpdateHealth(float change)
	{
		TeamHealth += change;

		PlayerHealthBar.GetComponent<HealthBar>().staticHealthUpdate(TeamHealth / maxTeamHealth);
	}

	public void startTeamAttack()
	{
		takingTurn = true;
		//call scale
		Scale.StartAttack(true);
	}

	public void AttackRegister(GameObject note)
	{
		//scale calls back home
		for (int i = 0; i < attackTurns.Length; i++)
		{
			if (attackTurns[i] == null)
			{
				attackTurns[i] = note;
				if (i != 2)
				{
					//call scale attack again
					Scale.StartAttack(true);
				}
				return;
			}
		}
	}

	private void checkForfinishedAttack()
	{
		if (attackTurns[0] != null && attackTurns[1] != null && attackTurns[2] != null)
		{
			takingTurn = false;
			Debug.Log("SUCCESSFUL ATTACK");
			//call team attack for calc
			calcAndApplyAttack();
		}
	}

	//base attack power per note is 10
	private void calcAndApplyAttack()
	{
		//TODO: check for attack modifiers: chord
		int[] attacks = new int[3];
		float damageToSend;
		bool isHealing = Constants.C.isHealing;
		//is harp in play?

		for (int i = 0; i < attackTurns.Length; i++)
		{
			int.TryParse((attackTurns[i].name.Substring(attackTurns[0].name.Length - 1)), out attacks[i]);
		}

		damageToSend = attacks[0] + attacks[1] + attacks[2]; //general damage

		//all same note bonus
		if (attacks[0] == attacks[1] && attacks[1] == attacks[2])
		{
			damageToSend = attacks[0] * 4;
		}

		//chord this is gross but were outta time
		if (
		(attacks[0] == 0 && attacks[1] == 1 && attacks[2] == 2) ||
		(attacks[0] == 1 && attacks[1] == 2 && attacks[2] == 3) ||
		(attacks[0] == 2 && attacks[1] == 3 && attacks[2] == 4) ||
		(attacks[0] == 3 && attacks[1] == 4 && attacks[2] == 5) ||
		(attacks[0] == 4 && attacks[1] == 5 && attacks[2] == 6) ||
		(attacks[0] == 5 && attacks[1] == 6 && attacks[2] == 7) 
		) {
			damageToSend *= 2;
			Debug.Log("chord damage");
		}

		Debug.Log(attacks[0]);

		if (Constants.C.isBonusDamage) {
			damageToSend *= 1.15f;
		}

		if (Constants.C.isHealing)
		{
			damageToSend *= 0.9f;
		}

		if (Constants.C.isPrecision)
		{
			damageToSend *= 0.9f;
		}

		//send damage
		FightSceneScript.PlayerAttack(damageToSend * 10, isHealing);
		attackTurns[0] = null;
		attackTurns[1] = null;
		attackTurns[2] = null;
	}

	public void initilizeTeamPosition()
	{
		Charecters[0] = Instantiate(Charecters[0]);
		Charecters[1] = Instantiate(Charecters[1]);
		Charecters[2] = Instantiate(Charecters[2]);

		Charecters[0].transform.position = GameObject.FindGameObjectWithTag("PlayerPoint1").transform.position;
		Charecters[1].transform.position = GameObject.FindGameObjectWithTag("PlayerPoint2").transform.position;
		Charecters[2].transform.position = GameObject.FindGameObjectWithTag("PlayerPoint3").transform.position;
	}
}
