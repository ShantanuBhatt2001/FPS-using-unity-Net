using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{   [SerializeField]
    public int maxHealth = 200;
    [SyncVar]
    private int CurrentHealth;
    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

   [SyncVar]
    private bool IsDead = false;
    public bool isDead
    {
        get{ return IsDead; }
        protected set { IsDead = value; }

    }

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        SetDefault();
    }
   /* void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(999);
        }
    }*/
    public void SetDefault()
    {
        CurrentHealth = maxHealth;
        isDead = false;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }
        Collider col = GetComponent<Collider>();
        if(col!=null)
        {
            col.enabled = true;
        }
    }
    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        if (isDead)
            return;
        CurrentHealth -= amount;
        Debug.Log(transform.name + "    " + CurrentHealth);
        if(CurrentHealth<=0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
        Debug.Log(transform.name + "is dead");
        StartCoroutine(Respawn());

    }
    private IEnumerator Respawn ()
    {
        yield return new WaitForSeconds(3f);
        SetDefault();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
    }
}
