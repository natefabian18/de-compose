using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworldplayermovement : MonoBehaviour
{
	public Vector2 mapPosition;  //keeps track of player location on map when they transition back to overworld after combat
	public float speed = 1f;
	public float recoveryFactor = 2f;
	public float movementFactor = 2f;
	public Vector2 startPosition;
	public float distance = .1f;
	public Color color;
	public int layerMask;
	public static int Raycast;
	public GameObject enemy2;

	private RaycastHit2D[] results;
	private ContactFilter2D contactFilter;
	private Vector3 oldPosition;
	private Vector2 deltaMovement;
	private Vector2 direction;
	private float playerX;
	private float playerY;
	// Start is called before the first frame update
	void Start()
	{
		oldPosition = new Vector3(0, 0, 0);
		transform.position = Constants.C.mapPositionGlobal;
		startPosition = transform.position;
		contactFilter.SetDepth(-2f, -12f);
		results = new RaycastHit2D[100];
	}

	// Update is called once per frame
	void Update()
	{
		if (Constants.C.bossdead == true)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Wrapup_End");
		}
		if (Constants.C.enemy2dead == true)
		{
			Destroy(enemy2);
		}
		oldPosition = transform.position;

		direction = getInput();
		transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

		startPosition = transform.position;
		int stuffHit = Physics2D.Raycast(startPosition, direction, contactFilter, results, distance);
		Debug.DrawRay(startPosition, direction, color, distance);
		if (stuffHit != 0)
		{
			if (results[0].collider.gameObject.tag == "OOB")
			{
				Debug.Log(results[0].collider.gameObject.name);
                Vector2 reverseDir = new Vector2(direction.x * -1, direction.y * -1);
				reverseDir.Normalize();
                Vector3 fix = new Vector3(reverseDir.x, reverseDir.y, 0) * speed * Time.deltaTime;
                transform.position += fix;
            }
		}

		mapPosition = transform.position;
		Constants.C.mapPositionGlobal = mapPosition;
		//transform.position = new Vector3(playerX, playerY, 0);
		//transform.position += new Vector3(deltaMovement.x, deltaMovement.y, 0);

	}

	private Vector2 getInput()
	{
		Vector2 inputVector = new Vector2(0,0);
		if (Input.GetKey(KeyCode.W))
		{
			inputVector.y = 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			inputVector.y = -1;
		}

		if (Input.GetKey(KeyCode.A))
		{
			inputVector.x = -1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			inputVector.x = 1;
		}
		inputVector.Normalize();
		return inputVector;

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//please store player position and dead enemys in the constants file before loading any scenes

		if (collision.tag == "Enemy1")
		{
			if(Constants.C.enemy1dead == true)
            {
				return;
            }
			UnityEngine.SceneManagement.SceneManager.LoadScene("Battle 1");
		}
		else if (collision.tag == "Enemy2")
		{
			
			UnityEngine.SceneManagement.SceneManager.LoadScene("Battle 2");
		}
		else if (collision.tag == "Boss_transition")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
			//move to fight scene with enemy #3
		}
		//Debug.Log("OOB");
		//Vector3 recoveryDirection = oldPosition - transform.position;
		//recoveryDirection.Normalize();
		//recoveryDirection *= recoveryFactor;
		//Vector3 fix = new Vector3(deltaMovement.x, deltaMovement.y, 0);
		//fix.Scale(recoveryDirection);
		//transform.position += fix;
	}
}
