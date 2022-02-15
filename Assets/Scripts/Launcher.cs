
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Tooltip("Game version for game version checks between players")]
    public string gameVersion = "1";
    [Tooltip("Status text object")]
    public Text status;

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    public void StartNmsAttempt()
    {
        status.text = "Initializing Connection";
        Connect();
    }
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            status.text = "Already connected to photon, joining room";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    public override void OnConnectedToMaster()
    {
        status.text = "Connection to photon succeeded press btn again to join a random room, or create one.";
        
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        status.text = "Disconnected from photon due to " + cause.ToString();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        status.text = "Wasn't able to find a room. Creating one. (" + returnCode + message + ")";
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersPerRoom });

    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            status.text = "Joined a room as " + PhotonNetwork.NickName + ". There is no-one else here yet. ";
        }
        else
        {
            status.text = "Joined a room as " + PhotonNetwork.NickName + ". Other players in the room include " + Arrange(PhotonNetwork.PlayerListOthers);
        }
        
        SceneManager.LoadScene("Loading");
    }
    public string Arrange(Player[] players)
    {
        string returnval = string.Empty;
        foreach (Player p in players)
        {
            returnval += p.NickName.ToString() + ", ";
        }
        return returnval;
    }
}
