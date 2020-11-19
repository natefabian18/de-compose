using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour
{
    public Text narration;
    public Text Title;
    public Text Controls;
    public Text Continue;

    private bool scenetransition = false;
    private bool text2 = false;
    // Start is called before the first frame update
    void Start()
    {
        narration.text = "";
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Title.text = "";
            Continue.text = "";
            Controls.text = "";
            narration.text = "Hey you, yeah you. Listen...I need a small favor from you, nothing crazy or anything, just a small errand to run. Remember Tom? Tone-deaf? Lots of moles? Can’t play the towns most prized and magical harmonica? Yeah...well he stole it and barricaded himself in a castle, not quite sure where he got that actually. But anyways, I need you to go and get it back, you can of course take a few instruments of your own to defend yourself.";
            StartCoroutine(newText());
        }

        if (text2)
        {
            narration.text = "What’s that...you want a reward? HA! Your reward is not being evicted, now get your sorry butt moving.";
            StartCoroutine(transitionScene());
        }

        if (scenetransition)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PlayerSelect");
        }
    }

    IEnumerator newText()
    {
        yield return new WaitForSeconds(30f);
        text2 = true;
    }

    IEnumerator transitionScene()
    {
        yield return new WaitForSeconds(10f);
        scenetransition = true;
    }
}
