using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPlayerSprite : MonoBehaviour
{
    //this script is going to set the player sprite to the first party member the player chooses from the constant script
    //going to wait until merge with Nate's branch sinc he is going to use a constants file as well

   
    public SpriteRenderer spriteRenderer;
    public Sprite tempSprite;

    private Sprite playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        //playerSprite = Constants.C.partymember1;
        playerSprite = tempSprite;
        spriteRenderer.sprite = playerSprite;
        this.transform.localScale = new Vector3(.04f, .04f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
