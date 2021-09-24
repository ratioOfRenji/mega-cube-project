using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;

    ParticleSystem.MainModule cubeExplosionFXMainModule;
    //singleton class
    public static MyFX Inctance;

    private void Awake()
    {
        Inctance = this;
    }
    private void Start()
    {
        cubeExplosionFXMainModule = cubeExplosionFX.main;
    }
    public void PlayCubeExplosionFX(Vector3 positon, Color color)
    {
        cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        cubeExplosionFX.transform.position = positon;
        cubeExplosionFX.Play();
    }
}
