using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Ins.MainCam)
        {
            transform.position = new Vector3(
                GameManager.Ins.MainCam.transform.position.x,
                GameManager.Ins.MainCam.transform.position.y,
                0f
                );
            
        }    
    }
}
