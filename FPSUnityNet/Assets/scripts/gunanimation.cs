using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class gunanimation : NetworkBehaviour
{
    public Animator thegun;
    public int bullets = 6;
    public float fireRate = 4f;
    public float lastShot = 0.0f;
    public bool check2 = true;


    

    void Update()
    {
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(1, true);
        if (thegun.GetBool("firetrigger"))
        {
            thegun.SetBool("firetrigger", false);
        }
        if (thegun.GetBool("GunReload"))
        {
            thegun.SetBool("GunReload", false);
        }
       // if (Time.time > fireRate + lastShot)
      //  {
            if (Input.GetButtonDown("Fire1") && bullets > 0 && !thegun.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GunReload") && !thegun.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Fire"))
            {
                thegun.SetBool("firetrigger",true);
                
                lastShot = Time.time;
                bullets--;
                
            }
            
            if ((Input.GetButtonDown("Reload") && bullets != 6 && !thegun.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Fire") && !thegun.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GunReload")) )
            {
                thegun.SetBool("GunReload",true);

                bullets = 6;
            }
            

        //}
    }
}