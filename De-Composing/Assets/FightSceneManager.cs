using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
	instaniate the teams
	apply the damage from each team attack
	handle whos turn it is
	determine a winner/handle player death
 */
public class FightSceneManager : MonoBehaviour
{
	//Enemy Setup vars
	public enum EnemyType { Encounter1, Encounter2, Encounter3 };

	public EnemyType currentEncounterEnemy = EnemyType.Encounter1;

	private GameObject Enemy;

	public GameObject Guitar;
	public GameObject Trombone;
	public GameObject Synth;

	private string Encounter1Enemy = "guitar";
	private string Encounter2Enemy = "trombone";
	private string Encounter3Enemy = "synth";
	//Enemy Setup vars

	private bool ButtonSelectable = true; //is true when its players turn false when its the enemys turn
	private PlayerTeamManager PlayerTeamScript;

	// Start is called before the first frame update
	void Start()
	{
		PlayerTeamScript = GameObject.FindGameObjectWithTag("PlayerTeam").GetComponent<PlayerTeamManager>();
		instaniateTeams();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void instaniateTeams() {
		Debug.Log($"Building Teams");
		switch (currentEncounterEnemy) {
			case EnemyType.Encounter1:
				Enemy = Instantiate(Guitar);
				Enemy.transform.position = GameObject.FindGameObjectWithTag("EnemyPoint").transform.position;
				Debug.Log("guitarLoaded");
				break;
			case EnemyType.Encounter2:
				break;
			case EnemyType.Encounter3:
				break;
			default:
				Debug.LogError("No Enemy type found tossing error");
				break;
		}
	}

	public void ButtonSelect(string selection) {
		if (ButtonSelectable) {
			ButtonSelectable = false;
			//call player team attack
			PlayerTeamScript.startTeamAttack();
		}
	}
}
