using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem musicParticles;

    private bool timeToPlay = false;
    void Start()
    {
        musicParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSomeParticles()
    {
        Debug.Log("time to particle");
        timeToPlay = true;
        musicParticles.Play();
    }

    public void STAHPParticle()
    {
        musicParticles.Stop();
    }
}
