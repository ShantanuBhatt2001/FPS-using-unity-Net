using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.Networking;

public class move1projectile : NetworkBehaviour
{
    public float speed;

    public PlayerWeapon weapon;

    public float power = 10.0f, radius = 10f, upforce = 1.0f;
    public float damage = 10f;
    Rigidbody rb;

    // Start is called before the first frame update
    private void Awake()
    {
         rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if(speed!=0)
        {
            rb.velocity= transform.forward * speed * Time.deltaTime;
        }
        
        
    }
    [Client]
    void OnCollisionEnter(Collision co)
    {
        if (co.collider.gameObject.layer != LayerMask.NameToLayer("LocalPlayerLayer"))
        {


            speed = 0;
            ContactPoint contact = co.contacts[0];
            Debug.Log(co.collider.name);


            if (co.collider.tag == "Player")
            {
                Debug.Log(co.collider.name);
                CmdPlayerShot(co.collider.name, weapon.damage);

            }


            Destroy(gameObject);
        }
    }
    [Command]
    void CmdPlayerShot(string ID, int damage)
    {
        Debug.Log(ID + "has been shot move1proj");
        Player player = Gamemanagement.GetPlayerId(ID);
       player.RpcTakeDamage(damage);
    }
   
}
