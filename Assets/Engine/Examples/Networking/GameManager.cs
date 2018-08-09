using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    private const string PLAYER_ID_PREFIX = "Pleyer ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    internal static void RegisterPlayer(string netID, Player player)
    {
        string playerID = PLAYER_ID_PREFIX + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    internal static void UnRegisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    internal static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20,140,200,500));
        GUILayout.BeginVertical();

        foreach(string playerID in players.Keys)
        {
            GUILayout.Label(playerID); 
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
