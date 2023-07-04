using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _spawners;
    private Vector3 offset = new Vector3(0, 0.43f, 0);

    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        GameObject spawner;

        if (PhotonNetwork.IsMasterClient)
        {
             spawner = _spawners[0];
        }
        else
        {
             spawner = _spawners[1];
        }
        

        GameObject player = PhotonNetwork.Instantiate(_player.name, spawner.transform.position + offset, Quaternion.identity);
        player.GetComponent<Player>()._standsTile = spawner;

 
    }

 
}
