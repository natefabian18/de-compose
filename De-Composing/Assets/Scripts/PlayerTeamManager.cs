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
	public bool ISTURN = true;

	public float TeamHealth = 100;
	private float maxTeamHealth;
	private GameObject PlayerHealthBar;
	private List<GameObject> CharecterPoints;
	private List<GameObject> Charecters;

	//refactor me please
	public GameObject Trumpet;

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
		}
		catch (NullReferenceException e) {
			Debug.LogError($"missing: {e} Did you set all your wires or rename something?");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (ISTURN) { 
		
		}
	}

	public void UpdateHealth(float change) {
		TeamHealth += change;

		PlayerHealthBar.GetComponent<HealthBar>().HealthUpdate(TeamHealth/maxTeamHealth);
	}
}
