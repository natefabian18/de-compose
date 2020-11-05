using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeamManager : MonoBehaviour
{
	public float TeamHealth = 100;
	private float maxTeamHealth;
	private GameObject PlayerHealthBar;
	private List<GameObject> CharecterPoints;
	private List<GameObject> Characters;

	public float ChordDamageChance = 0;
	public float MatchDamageChance = 0;
	public float MissChance = 0;

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

			Characters = new List<GameObject>();
			//this is loaded in form const party file


			UpdateHealth(0);

			attackTurns = new GameObject[3];
			for (int i = 0; i < attackTurns.Length; i++)
			{
				attackTurns[i] = null;
			}
			Scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<ScaleAttack>();

			FightSceneScript = GameObject.FindGameObjectWithTag("FightSceneManager").GetComponent<FightSceneManager>();
		}
		catch (NullReferenceException e)
		{
			Debug.LogError($"missing: {e} Did you set all your wires or rename something?");
		}
	}

	// Update is called once per frame

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
		Scale.startCheatAttack(constructAttack());
	}

	private object constructAttack()
	{
		// 0 - 1
		/*
		 * chord chance is 0.1
		 * match chance is 0.2
		 * miss chance is 0.1
		 * add of these together subtract from one to get normal damage chance
		 * generate a random number between 0 and 1
		 * miss if random number is between 0 and miss chance
		 * normal hit if random number is between miss chance and miss chance + normal damage chance store this as normal offset
		 * match chance if random number is between normal offset and normal offset + match chance store this as match offset
		 * chord chance if random number is between match offset and 1
		 */

		float normalDamageChance = (ChordDamageChance + MatchDamageChance + MissChance) - 1;
		float randomMoveSelect = UnityEngine.Random.Range(0, 1f);

		int note1 = -1;
		int note2 = -1;
		int note3 = -1;
		if (randomMoveSelect >= 0 && randomMoveSelect <= MissChance)
		{
			//miss
			//first move always misses

			note2 = UnityEngine.Random.Range(-1, 7);
			note3 = UnityEngine.Random.Range(-1, 7);
		}
		else if (randomMoveSelect > MissChance && randomMoveSelect <= MissChance + normalDamageChance)
		{
			//normal damage
			note1 = UnityEngine.Random.Range(0, 2);
			note2 = UnityEngine.Random.Range(3, 5);
			note3 = UnityEngine.Random.Range(6, 7);
		}
		else if (randomMoveSelect > MissChance + normalDamageChance && randomMoveSelect <= MissChance + normalDamageChance + MatchDamageChance)
		{
			//match damage
			note1 = UnityEngine.Random.Range(6, 7);
			note2 = note1;
			note3 = note2;
		}
		else if (randomMoveSelect > MissChance + normalDamageChance + MatchDamageChance && randomMoveSelect <= 1)
		{
			//chord damage
			//make later
		}

		return new { note1, note2, note3 };
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
		bool isHealing = false;
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

		//chord test

		Debug.Log(attacks[0]);
		//send damage
		FightSceneScript.PlayerAttack(damageToSend * 10, isHealing);
	}

	public void addCharecter(GameObject character)
	{
		Characters.Add(character);
	}
}
