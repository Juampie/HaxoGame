using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField _createInput;
    [SerializeField] private InputField _joinInput;
    [SerializeField] private InputField _playerName;

    public void OnCreateRoomButtonClick()
    {
        RoomOptions roomOptions = new();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(_createInput.text, roomOptions);
    }

    public void OnJoinRoomButtonClick()
    {
        PhotonNetwork.NickName = _playerName.text;
        PhotonNetwork.JoinRoom(_joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = _playerName.text;
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Ошибка при создании комнаты: " + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Ошибка при присоединении к комнате: " + message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Отключение от сервера: " + cause.ToString());
    }
}
