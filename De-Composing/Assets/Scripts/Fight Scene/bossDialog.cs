using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossDialog : MonoBehaviour
{
    public Text bossText;

    private bool changeText = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(stopIntroDialog());
    }

    // Update is called once per frame
    void Update()
    {
        if (changeText)
        {
            bossText.text = "";
        }           
    }

    IEnumerator stopIntroDialog()
    {
        yield return new WaitForSeconds(8f);
        changeText = true;
    }
}
