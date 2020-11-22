using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeamManager : MonoBehaviour
{
	public float TeamHealth = 100;
	private float maxTeamHealth;
	private GameObject EnemyHealthBar;
	private List<GameObject> CharecterPoints;
	private List<GameObject> Characters;

	public float ChordDamageChance = 0;
	public float MatchDamageChance = 0;
	public float MissChance = 0;

	private bool takingTurn = false;
	public bool isburned = false;
	private GameObject[] attackTurns;

	private ScaleAttack Scale;

	private FightSceneManager FightSceneScript;
	private SpriteRenderer EnemySpriteRender;

	// Start is called before the first frame update
	void Start()
	{

		maxTeamHealth = TeamHealth; //replace with max health from const file when avilable
		EnemyHealthBar = this.transform.Find("EnemyHealthBar").gameObject;

		CharecterPoints = new List<GameObject>();
		CharecterPoints.Add(this.transform.Find("EnemyPoint1").gameObject);

		Characters = new List<GameObject>();
		//this is loaded in form const party file

		if (Constants.C.isBurning) {
			isburned = true;
		}

		UpdateHealth(0);

		attackTurns = new GameObject[3];
		for (int i = 0; i < attackTurns.Length; i++)
		{
			attackTurns[i] = null;
		}
		Scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<ScaleAttack>();

		FightSceneScript = GameObject.FindGameObjectWithTag("FightSceneManager").GetComponent<FightSceneManager>();

		
	}

	// Update is called once per frame

	private void Update()
	{

	}

	public void UpdateHealth(float change)
	{
		TeamHealth += change;

		EnemyHealthBar.GetComponent<HealthBar>().staticHealthUpdate(TeamHealth / maxTeamHealth);
	}

	public void startTeamAttack()
	{
		takingTurn = true;
		if (isburned) {
			FightSceneScript.burnDamage();
		}
		//call scale
		Scale.startCheatAttack(constructAttack());
	}

	private int[] constructAttack()
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

		float normalDamageChance = 1 - (ChordDamageChance + MatchDamageChance + MissChance);
		float randomMoveSelect = UnityEngine.Random.Range(0, 1f);

		int note1 = 8; 
		int note2 = 8;
		int note3 = 8;
		if (randomMoveSelect >= 0 && randomMoveSelect <= MissChance)
		{
			//miss
			//first move always misses

			note2 = UnityEngine.Random.Range(0, 7);
			note3 = UnityEngine.Random.Range(0, 7);
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
			note1 = UnityEngine.Random.Range(0, 5);
			note2 = note1 + 1;
			note3 = note2 + 1;
		}

		int[] returner = new int[3];
		returner[0] = note1;
		returner[1] = note2;
		returner[2] = note3;
		return returner;
	}

	public void checkForfinishedAttack(GameObject[] attacks)
	{
		attackTurns[0] = attacks[0];
		attackTurns[1] = attacks[1];
		attackTurns[2] = attacks[2];

		calcAndApplyAttack();
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
		if (
		(attacks[0] == 0 && attacks[1] == 1 && attacks[2] == 2) ||
		(attacks[0] == 1 && attacks[1] == 2 && attacks[2] == 3) ||
		(attacks[0] == 2 && attacks[1] == 3 && attacks[2] == 4) ||
		(attacks[0] == 3 && attacks[1] == 4 && attacks[2] == 5) ||
		(attacks[0] == 4 && attacks[1] == 5 && attacks[2] == 6) ||
		(attacks[0] == 5 && attacks[1] == 6 && attacks[2] == 7)
		)
		{
			damageToSend *= 2;
			Debug.Log("chord damage");
		}

		//send damage
		FightSceneScript.EndEnemyAttack(damageToSend * 10);
	}

	public void addCharecter(GameObject character)
	{
		Characters.Add(character);

		if (Constants.C.isBurning) {
			Characters[0].GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
}
