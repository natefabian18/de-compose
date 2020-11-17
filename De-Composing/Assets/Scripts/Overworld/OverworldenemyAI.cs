using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldenemyAI : MonoBehaviour
{
    public Vector2 startPosition;
    public Vector2 endPosition;

    public float speed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(transform.position.x - endPosition.x, transform.position.y - endPosition.y);
        
    }

}
