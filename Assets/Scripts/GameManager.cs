using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Button StartGameBtn;
    public Text statusText;
    public override void OnLeftRoom()
    {
        
        SceneManager.LoadScene(0);

        base.OnLeftRoom();
    }
    
    private void Start()
    {
        checkup();
    }
    public void checkup()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            StartGameBtn.interactable = false;
        }
        else
        {
            StartGameBtn.interactable = true;
        }
    }
    public void LoadLevelOfTicTacFuckedYourMother()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Cant load scene when not master client");
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Game");
        }
        else
        {
            statusText.text = ("Not enough players to start a match");
        }
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.PlayerListOthers.Length > 0)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerListOthers[0]);
        }
        PhotonNetwork.LeaveRoom();
    }

    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        statusText.text = newPlayer.NickName + $" has joined the room! ({PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers})";
        checkup();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        statusText.text = otherPlayer.NickName + $" has left the room! ({PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers})";
        checkup();
    }
}
