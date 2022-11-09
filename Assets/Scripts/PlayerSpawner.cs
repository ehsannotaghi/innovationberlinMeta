using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;


public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Vector3 StartPosition;

    // Starts when Player connects to matchmaking server
    void Start()
    {
        // Spawns a player from resources folder in network 
        var spawnedPlayer = PhotonNetwork.Instantiate(PlayerPrefab.name, StartPosition, Quaternion.identity);

    }


}
