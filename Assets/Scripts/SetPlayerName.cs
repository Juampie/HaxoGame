using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SetPlayerName : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMesh _playerName;

    void Start()
    {
        if (photonView.IsMine)
        {
            _playerName.text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            _playerName.text = photonView.Owner.NickName;
        }
    }
   



    
}
