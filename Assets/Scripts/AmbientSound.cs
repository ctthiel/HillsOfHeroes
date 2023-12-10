using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    public AudioSource windSource;
    public AudioSource cicadasSource;
    void Start()
    {
        windSource.Play();
        cicadasSource.Play();
    }
}
