using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRedZone : MonoBehaviour
{
    public GameObject gameOver;
    private void OnTriggerStay(Collider other)
    {
        myCube cube = other.GetComponent<myCube>();
        if(cube != null)
        {
            if(!cube.IsMainCube && cube.CubeRgidbody.velocity.magnitude < .1f)
            {
                gameOver.SetActive(true);
                Time.timeScale = 0f;

            }
        }
    }
}
