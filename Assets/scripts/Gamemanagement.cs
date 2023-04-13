using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Gamemanagement : MonoBehaviour
{
    public static Gamemanagement instance;
    public MatchSettings matchSettings;
    void Awake()
    {
    if(instance!=null)
        {
            Debug.LogError("More Than One Manager In Scene");
        }
    }
    #region PlayerTracking
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();
    public static  void RegisterPlayer(string netId,Player player)
    {
        string playerId = "Player " + netId;
        players.Add(playerId, player);
        player.transform.name = playerId;
    }
    public static void unRegister(string playerID)
    {
        players.Remove(playerID);
    }
    public static Player GetPlayerId(string playerID)
    {
        return players[playerID];
    }
    //  void OnGUI()
    //  {
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //      GUILayout.BeginVertical();
    //      foreach(string playerid in players.Keys)
    //     {
    //        GUILayout.Label(playerid + "    -    " + players[playerid].transform.name);
    //    }
    //     GUILayout.EndArea();
    // }
    #endregion
}
