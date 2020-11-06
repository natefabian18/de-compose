using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworldplayermovement : MonoBehaviour
{
	public Vector2 mapPosition;  //keeps track of player location on map when they transition back to overworld after combat
	public float speed = 1f;
	public float recoveryFactor = 2f;
	public float movementFactor = 2f;

	private Vector3 oldPosition;
	private Vector2 deltaMovement;
	private Vector2 direction;
	private float playerX;
	private float playerY;
	// Start is called before the first frame update
	void Start()
	{
		oldPosition = new Vector3(0, 0, 0);

	}

	// Update is called once per frame
	void Update()
	{
		oldPosition = transform.position;

		direction = getInput();
		direction.Normalize();
		deltaMovement = speed * direction * Time.deltaTime;

		mapPosition = transform.position;
		transform.position = new Vector3(playerX, playerY, 0);
		//transform.position += new Vector3(deltaMovement.x, deltaMovement.y, 0);

	}

	private Vector2 getInput()
	{
		Vector2 inputVector = direction;
		playerX = this.transform.position.x;
		playerY = this.transform.position.y;
		if (Input.GetKey(KeyCode.W))
		{
			inputVector.y = 1;
			playerY += inputVector.y * movementFactor;
		}
		if (Input.GetKey(KeyCode.S))
		{
			inputVector.y = -1;
			playerY += inputVector.y * movementFactor;
		}

		if (Input.GetKey(KeyCode.A))
		{
			inputVector.x = -1;
			playerX += inputVector.x * movementFactor;
		}
		if (Input.GetKey(KeyCode.D))
		{
			inputVector.x = 1;
			playerX += inputVector.x * movementFactor;
		}
		inputVector.Normalize();
		return inputVector;

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//please store player position and dead enemys in the constants file before loading any scenes

		if (collision.tag == "Enemy1")
		{
			//move to fight scene with enemy #1
		}
		else if (collision.tag == "Enemy2")
		{
			//move to fight scene with enemy #2
		}
		else if (collision.tag == "Enemy3")
		{
			//move to fight scene with enemy #3
		}
		else if (collision.tag == "Boss_transition")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
			//move to fight scene with enemy #3
		}
		Debug.Log("OOB");
		Vector3 recoveryDirection = oldPosition - transform.position;
		recoveryDirection.Normalize();
		recoveryDirection *= recoveryFactor;
		Vector3 fix = new Vector3(deltaMovement.x, deltaMovement.y, 0);
		fix.Scale(recoveryDirection);
		transform.position += fix;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Enemy1")
		{
			//move to fight scene with enemy #1
		}
		else if (collision.tag == "Enemy2")
		{
			//move to fight scene with enemy #2
		}
		else if (collision.tag == "Enemy3")
		{
			//move to fight scene with enemy #3
		}
		Debug.Log("OOB");
		Vector3 recoveryDirection = oldPosition - transform.position;
		recoveryDirection.Normalize();
		recoveryDirection *= recoveryFactor;
		Vector3 fix = new Vector3(deltaMovement.x, deltaMovement.y, 0);
		fix.Scale(recoveryDirection);
		transform.position += fix;

	}
}
