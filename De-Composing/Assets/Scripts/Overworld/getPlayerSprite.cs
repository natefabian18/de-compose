using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPlayerSprite : MonoBehaviour
{
    //this script is going to set the player sprite to the first party member the player chooses from the constant script
    //going to wait until merge with Nate's branch sinc he is going to use a constants file as well
    public ParticleSystem particles;
   
    public SpriteRenderer spriteRenderer;
    public Sprite tempSprite;

    private Sprite playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        particles.Stop();
        particles.Play();
        playerSprite = Constants.C.selectedPlayers[0].GetComponent<SpriteRenderer>().sprite;
        //playerSprite = tempSprite;
        spriteRenderer.sprite = playerSprite;
        this.transform.localScale = new Vector3(.2f, .2f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
