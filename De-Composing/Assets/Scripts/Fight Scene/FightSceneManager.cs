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
	private EnemyTeamManager EnemyTeamScript;

	private HealthBar PlayerHealth;
	private HealthBar EnemyHealth;

	// Start is called before the first frame update
	void Start()
	{
		PlayerHealth = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<HealthBar>();
		EnemyHealth = GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<HealthBar>();

		PlayerTeamScript = GameObject.FindGameObjectWithTag("PlayerTeam").GetComponent<PlayerTeamManager>();
		EnemyTeamScript = GameObject.FindGameObjectWithTag("EnemyTeam").GetComponent<EnemyTeamManager>();
		instaniateTeams();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void burnDamage() {
		EnemyHealth.HealthUpdate(0.05f);
		checkHealthAndDeclareWinner();
	}

	private void instaniateTeams() {
		switch (currentEncounterEnemy) {
			case EnemyType.Encounter1:
				Enemy = Instantiate(Guitar);
				Enemy.transform.position = GameObject.FindGameObjectWithTag("EnemyPoint").transform.position;
				EnemyTeamScript.addCharecter(Enemy);
				break;
			case EnemyType.Encounter2:
				Enemy = Instantiate(Trombone);
				Enemy.transform.position = GameObject.FindGameObjectWithTag("EnemyPoint").transform.position;
				EnemyTeamScript.addCharecter(Enemy);
				break;
			case EnemyType.Encounter3:
				Enemy = Instantiate(Synth);
				Enemy.transform.position = GameObject.FindGameObjectWithTag("EnemyPoint").transform.position;
				EnemyTeamScript.addCharecter(Enemy);
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

	public void PlayerAttack(float damage, bool healPlayer) {
		Debug.Log($"damage is {damage} enemyHealth is {EnemyHealth.TotalHealth} damage percent is {damage / EnemyHealth.TotalHealth}");
		EnemyHealth.HealthUpdate(damage / EnemyHealth.TotalHealth);
		if (healPlayer) {
			PlayerHealth.HealthUpdate((damage / PlayerHealth.TotalHealth) * -0.2f);
		}
		//Enemy do some stuff
		checkHealthAndDeclareWinner();

		EnemyAttack();
	}

	public void EnemyAttack() {
		EnemyTeamScript.startTeamAttack();
	}

	public void EndEnemyAttack(float damage) {
		PlayerHealth.HealthUpdate(damage / PlayerHealth.TotalHealth);

		checkHealthAndDeclareWinner();

		ButtonSelectable = true;
	}

	private void checkHealthAndDeclareWinner() {
		if (EnemyHealth.HealthPercent == 0) {
			//player wins
			Debug.Log("Enemy dead");
			//check if final boss was killed
			string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
			Debug.Log("This is the current scene" + currentScene);
			if(currentScene == "Battle 1")
            {
				
				Constants.C.enemy1dead = true;
            }
			else if(currentScene == "Battle 2"){
				Debug.Log("Ended battle 2");
				Constants.C.enemy2dead = true;
            }

			else if(currentScene == "Battle")
            {
				Debug.Log("boss is dead");
				Constants.C.bossdead = true;
				//UnityEngine.SceneManagement.SceneManager.LoadScene("Wrapup_End");
            }
			//load scene and kill enemy
			UnityEngine.SceneManagement.SceneManager.LoadScene("Overworld");
		}

		if (PlayerHealth.HealthPercent == 0) {
			//enemy wins boot back to overworld
			Debug.Log("Player dead");
			//load scene back at town
			UnityEngine.SceneManagement.SceneManager.LoadScene("Overworld");
		}

		// no winner do nothing
	}
}
