using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState==CursorLockMode.Locked)
        {
            Cursor.lockState=CursorLockMode.None;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState==CursorLockMode.None)
        {
            Cursor.lockState=CursorLockMode.Locked;
        }
    }

   
}
