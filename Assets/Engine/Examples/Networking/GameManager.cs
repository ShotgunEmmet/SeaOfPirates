using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    private const string PLAYER_ID_PREFIX = "Pleyer ";

    private static Dictionary<string, WayfarerHealth> players = new Dictionary<string, WayfarerHealth>();

    internal static void RegisterPlayer(string netID, WayfarerHealth player)
    {
        string playerID = PLAYER_ID_PREFIX + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    internal static void UnRegisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    internal static WayfarerHealth GetPlayer(string playerID)
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
            GUILayout.Label("Health: " + GetPlayer(playerID).Health());
            GUILayout.Label("---------------");
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
