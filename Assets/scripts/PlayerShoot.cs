using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;
    public Camera Cam;
    public LayerMask mask;
    public int bullets = 6;
    public float fireRate = 4f;
    public ParticleSystem muzzleflash;
    public GameObject hiteffect;
    public Animator thegun;
    
    public GameObject firePoint;
    public float i;
    RaycastHit hit;
    Vector3 rot;
    
    [Command]
    void CmdOnShoot()
    {
        RpcMuzzleFlash();
    }
    [Command]
    void CmdOnHit(Vector4 pos,Vector3 normal)
    {
        RpcHit(pos,normal);
    }

    [ClientRpc]
    void RpcMuzzleFlash()
    {
        muzzleflash.Play();
    }
    [ClientRpc]
    void RpcHit(Vector3 pos,Vector3 normal)
    {
        GameObject HitEffect=(GameObject)Instantiate(hiteffect,pos,Quaternion.LookRotation(normal));
        Destroy(HitEffect,2f); 
    }
    [Client]
    void Shoot()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        CmdOnShoot();
        RaycastHit hit;
        if(Physics.Raycast(Cam.transform.position,Cam.transform.forward,out hit,mask))
        {
            if(hit.collider.tag=="Player")
            {
               
                CmdPlayerShot(hit.collider.name, weapon.damage);
            }
            CmdOnHit(hit.point, hit.normal);
        }
    }
    [Command]
    void CmdPlayerShot(string ID, int damage)
    {
        Debug.Log(ID + "has been shot move1proj");
        Player player = Gamemanagement.GetPlayerId(ID);
        player.RpcTakeDamage(damage);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
   
    void Update()
    {
        
          
        
        
        if (thegun.GetBool("firetrigger"))
        {
            thegun.SetBool("firetrigger", false);
            
            


        }
        if (thegun.GetBool("GunReload"))
        {
            thegun.SetBool("GunReload", false);
        }
        
        if (Input.GetButton("Fire1") && bullets > 0 && !thegun.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GunReload") && !thegun.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            thegun.SetBool("firetrigger", true);
            Shoot();
            
            bullets--;

        }

        if ((Input.GetButtonDown("Reload") && bullets != 6 && !thegun.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Fire") && !thegun.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GunReload")))
        {
            thegun.SetBool("GunReload", true);

            bullets = 6;
        }
    }
 
   

}
