using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCubeCollision : MonoBehaviour
{
    myCube cube;
    AudioSource audioSource;

    private void Awake()
    {
        cube = GetComponent<myCube>();
        audioSource = FindObjectOfType<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        myCube otheCube = collision.gameObject.GetComponent<myCube>();
        
        // check if contacted with other cube
        if(otheCube != null && cube.CubeID> otheCube.CubeID)
        {
            // check if both cubes have same number
            if(cube.CubeNumber == otheCube.CubeNumber)
            {
                Debug.Log("HIT :" + cube.CubeNumber);
                Vector3 contactPoint = collision.contacts[0].point;

                // check if cubes number less than max number in CubeSpawner
                if(otheCube.CubeNumber < MyCubeSpawner.Instance.maxCubeNumber)
                {
                    // spawn a new ube as a reslt
                    myCube newCube = MyCubeSpawner.Instance.Spawn(cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f);
                    audioSource.Play();
                    // push the new cube up and forward: 
                    float pushForce = 2.5f;
                    newCube.CubeRgidbody.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);

                    // add some torque
                    float randomValue = Random.Range(-20f, 20f);
                    Vector3 randomDirection = Vector3.one * randomValue;
                    newCube.CubeRgidbody.AddTorque(randomDirection);
                }
                // the explosion should affect surrounded cubes too:
                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 400f;
                float explosionRadius = 1.5f;
                foreach (Collider coll in surroundedCubes)
                {
                    if (coll.attachedRigidbody != null)
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                }
                MyFX.Inctance.PlayCubeExplosionFX(contactPoint, cube.CubeColor);

                //Destroy the two cubes:
                MyCubeSpawner.Instance.DestroyCube(cube);
                MyCubeSpawner.Instance.DestroyCube(otheCube);
            }
        }
    }
}
