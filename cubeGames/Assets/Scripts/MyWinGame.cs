using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWinGame : MonoBehaviour
{
    //public Component[] circleColiders;

    //void Start()
    //{

    //}
    //public void ColiderDisaibler()
    //{
    //    circleColiders = GetComponentsInChildren<CircleCollider2D>();

    //    foreach (CircleCollider2D coliderToDisable in circleColiders)
    //        coliderToDisable.enabled = false;
    //}

    //public void ColiderEnaibler()
    //{
    //    circleColiders = GetComponentsInChildren<CircleCollider2D>();

    //    foreach (CircleCollider2D coliderToDisable in circleColiders)
    //        coliderToDisable.enabled = true;
    //}
    public myCube[] cubesArray;

    public GameObject winUI;
    public void winGame()
    {
        cubesArray = GetComponentsInChildren<myCube>();

        
        for (int i = 0; i < cubesArray.Length; i++)
        {
           if( cubesArray[i].CubeNumber == 4096)
            {
                Time.timeScale = 0f;
                winUI.SetActive(true);
            }

        }
    }

    private void Update()
    {
        winGame();
    }

}
