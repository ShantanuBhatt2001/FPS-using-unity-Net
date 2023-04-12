using UnityEngine.Networking;
using UnityEngine;

public class HostGame : MonoBehaviour
{   [SerializeField]
    private uint roomSize = 5;

    private string roomName;
    private NetworkManager networkmanager;

    void Start()
    {
        networkmanager = NetworkManager.singleton;
        if(networkmanager.matchMaker==null)
        {
            networkmanager.StartMatchMaker();
        }
    }
    public void SetRoomName(string name)
    {
        roomName = name;
    }

    public void CreateRoom()
    {
        if(roomName!="" && roomName!=null)
        {
            Debug.Log("CreatingRoom");
            NetworkManager.singleton.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkmanager.OnMatchCreate);

        }
    }
}
