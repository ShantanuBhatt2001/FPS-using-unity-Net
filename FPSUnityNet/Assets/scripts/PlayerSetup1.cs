using UnityEngine.Networking;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSetup1 : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    Camera sceneCamera;
    public string RemoteLayerName = "RemotePlayerLayer";
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
                gameObject.layer = LayerMask.NameToLayer(RemoteLayerName);
            }
            
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
        GetComponent<Player>().Setup();
        
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        Gamemanagement.RegisterPlayer(netID,player);
    }

    private void OnDisable()
    {
        if(sceneCamera!=null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        Gamemanagement.unRegister(transform.name);

    }

}

