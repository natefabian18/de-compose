using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreakinParticles : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject particleStuff;

    private ParticleSystem particles;
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Stop();
        particles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
