using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldenemyAI : MonoBehaviour
{
    public GameObject startPosition;
    public GameObject endPosition;

	private Vector2 directionLeft;
	private Vector2 directionRight;

	private bool idleDirection = false; //true is left    false is right

    public float speed = 1f;


    // Start is called before the first frame update
    void Start()
    {
		directionLeft = startPosition.transform.position - endPosition.transform.position;
		directionLeft.Normalize();
		directionRight = endPosition.transform.position - startPosition.transform.position;
		directionRight.Normalize();
	}

    // Update is called once per frame
    void Update()
    {
		Vector2 directionToStart = startPosition.transform.position - transform.position;
		Vector2 directionToEnd = endPosition.transform.position - transform.position;
		float distanceToStart = directionToStart.magnitude;
		float distanceToEnd = directionToEnd.magnitude;
		if (!idleDirection && distanceToEnd > 2)
		{
			transform.position += new Vector3(directionRight.x, directionRight.y, 0) * speed * Time.deltaTime;
		}
		else if (!idleDirection && distanceToEnd < 2)
		{
			idleDirection = true;
		}
		else if (idleDirection && distanceToStart > 2)
		{
			transform.position += new Vector3(directionLeft.x, directionLeft.y, 0) * speed * Time.deltaTime;
		}
		else
		{
			idleDirection = false;
		}

	}

}
