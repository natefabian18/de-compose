using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    private ParticleSystem particles_snow;
    // Start is called before the first frame update
    void Start()
    {
        particles_snow = GetComponent<ParticleSystem>();
        particles_snow.Stop();
        particles_snow.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("PARTICLES");
    }
}
