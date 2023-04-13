using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour
{
    private NetworkManager networkmanager;

    void Start()
    {
        networkmanager = NetworkManager.singleton;
        if(networkmanager.matchMaker==null)
        {
            networkmanager.StartMatchMaker();
        }
    }
}
