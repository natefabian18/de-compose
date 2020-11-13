using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapScene : MonoBehaviour
{
	public float timeout = 2;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		timeout -= Time.deltaTime;

		if (timeout < 0) {
			UnityEngine.SceneManagement.SceneManager.LoadScene("PlayerSelect");
		}
	}
}
