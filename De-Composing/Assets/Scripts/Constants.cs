﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
	public static Constants C;
	public GameObject[] selectedPlayers;

	private void Awake()
	{
		C = this;
	}
	// Start is called before the first frame update
	void Start()
	{
		selectedPlayers = new GameObject[3];
		/*
		if (PlayerPrefs.HasKey(playerPrefHighScore))
		{
			highScore = PlayerPrefs.GetInt(playerPrefHighScore);
		}
		else
		{
			PlayerPrefs.SetInt(playerPrefHighScore, playerScore);
		}
		*/

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void AssignPlayer(GameObject playerSelection) {
		for (int i = 0; i < selectedPlayers.Length; i++) {
			if (selectedPlayers[i] == null) {
				selectedPlayers[i] = playerSelection;
				break;
			}
		}
	}
}