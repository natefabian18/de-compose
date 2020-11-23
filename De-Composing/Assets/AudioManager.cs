using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public AudioClip[] audioTracks;

	private AudioSource sound;
	// Start is called before the first frame update
	void Start()
	{
		sound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void loadSoundAndPlay(AudioClip playMe)
	{
		sound.clip = playMe;
		sound.Play();
	}

	public void pickSoundToPlay(string instrument, int note)
	{
		int inst = 0;
		switch (instrument)
		{
			case "drum":
				inst = 0;
				break;
			case "guitar(Clone)":
				inst = 1;
				break;
			case "harp":
				inst = 2;
				break;
			case "maracas":
				inst = 3;
				break;
			case "saxophone(Clone)":
				inst = 4;
				break;
			case "synth(Clone)":
				inst = 5;
				break;
			case "trumpet":
				inst = 6;
				break;
			case "xylophone":
				inst = 7;
				break;
			default:
				inst = 0;
				Debug.Log("no inst selected default drum");
				break;
		}
		//audioTracks[(inst * 8) + note]
		int calcedNote = (inst * 8) + note;
		Debug.Log(calcedNote);
		loadSoundAndPlay(audioTracks[calcedNote]);
	}
}
