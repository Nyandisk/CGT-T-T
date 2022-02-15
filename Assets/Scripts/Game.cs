using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq;

public class Game : MonoBehaviour
{
    public Color myClr,theirClr;

    public Text versusText;
    public string greenIs;
    public string redIs;
    public bool someonehasWon = false;
    private bool turnMaster = true;
    public SpriteRenderer clr1, clr2;
    public SpriteRenderer[] circleColors;
    bool check = false;


    private void Update()
    {
        if (someonehasWon && PhotonNetwork.IsMasterClient)
        {
            if (!check)
            {
                check = true;
                StartCoroutine(wait_until_lobby());
            }
            
        }
    }
    private void Start()
    {
        versusText.text = PhotonNetwork.LocalPlayer.NickName + " VS " + PhotonNetwork.PlayerListOthers[0].NickName;
        if (PhotonNetwork.IsMasterClient)
        {
        //is host aka is red
            myClr = Color.red;
            theirClr = Color.green;
            clr1.color = myClr;
            clr2.color = theirClr;
            redIs = PhotonNetwork.MasterClient.NickName;
            greenIs = PhotonNetwork.PlayerListOthers[0].NickName;
        }
        else
        {
        //is not masterclient aka host in photon
            myClr = Color.green;
            theirClr = Color.red;
            clr1.color = theirClr;
            clr2.color = myClr;
            redIs = PhotonNetwork.MasterClient.NickName;
            greenIs = PhotonNetwork.LocalPlayer.NickName;
        }
        clr1.color = myClr;
        clr2.color = theirClr;
        
    }

    string[] valid = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" }; //valid squares for usage by maketurn.

    public void makeTurn(string nm)
    {
        if (!someonehasWon)
        {
            if (valid.Contains(nm))
            {
                if (GameObject.Find(nm).GetComponent<SpriteRenderer>().color == Color.white)
                {
                    if (turnMaster && PhotonNetwork.IsMasterClient)
                    {
                        GameObject.Find(nm).GetComponent<SpriteRenderer>().color = myClr;
                        PhotonView photonView = PhotonView.Get(this);
                        turnMaster = false;
                        photonView.RPC("RPCMakeTurnForGuests", RpcTarget.All, nm); //master always red
                    }
                    else if (!turnMaster && !PhotonNetwork.IsMasterClient)
                    {
                        GameObject.Find(nm).GetComponent<SpriteRenderer>().color = myClr;
                        PhotonView photonView = PhotonView.Get(this);
                        turnMaster = true;
                        photonView.RPC("RPCMakeTurnForMaster", RpcTarget.All, nm); //guest always  green
                    }
                }
                
            }
            else
            {
                return;
            }
            winCheck();
        }
        
    }
    public void winCheck()
    {
        char[] boardSet = new char[9] { GetLetterRepresentation(circleColors[0].color), GetLetterRepresentation(circleColors[1].color), GetLetterRepresentation(circleColors[2].color),
        GetLetterRepresentation(circleColors[3].color),GetLetterRepresentation(circleColors[4].color),GetLetterRepresentation(circleColors[5].color),
        GetLetterRepresentation(circleColors[6].color),GetLetterRepresentation(circleColors[7].color),GetLetterRepresentation(circleColors[8].color)};
       
        
        if (boardSet[0] == 'R' && boardSet[1] == 'R' && boardSet[2] == 'R')
        {
            someonehasWon = true;
            versusText.text = redIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, redIs);
        }
        if (boardSet[0] == 'G' && boardSet[1] == 'G' && boardSet[2] == 'G')
        {
            someonehasWon = true;
            versusText.text = greenIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, greenIs);
        }
        if (boardSet[3] == 'R' && boardSet[4] == 'R' && boardSet[5] == 'R')
        {
            someonehasWon = true;
            versusText.text = redIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, redIs);
        }
        if (boardSet[3] == 'G' && boardSet[4] == 'G' && boardSet[5] == 'G')
        {
            someonehasWon = true;
            versusText.text = greenIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, greenIs);
        }
        if (boardSet[6] == 'R' && boardSet[7] == 'R' && boardSet[8] == 'R')
        {
            someonehasWon = true;
            versusText.text = redIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, redIs);
        }
        if (boardSet[6] == 'G' && boardSet[7] == 'G' && boardSet[8] == 'G')
        {
            someonehasWon = true;
            versusText.text = greenIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, greenIs);
        }
        if (boardSet[0] == 'R' && boardSet[3] == 'R' && boardSet[6] == 'R')
        {
            someonehasWon = true;
            versusText.text = redIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, redIs);
        }
        if (boardSet[0] == 'G' && boardSet[3] == 'G' && boardSet[6] == 'G')
        {
            someonehasWon = true;
            versusText.text = greenIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, greenIs);
        }
        if (boardSet[1] == 'R' && boardSet[4] == 'R' && boardSet[7] == 'R')
        {
            someonehasWon = true;
            versusText.text = redIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, redIs);
        }
        if (boardSet[1] == 'G' && boardSet[4] == 'G' && boardSet[7] == 'G')
        {
            someonehasWon = true;
            versusText.text = greenIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, greenIs);
        }
        if (boardSet[2] == 'R' && boardSet[5] == 'R' && boardSet[8] == 'R')
        {
            someonehasWon = true;
            versusText.text = redIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, redIs);
        }
        if (boardSet[2] == 'G' && boardSet[5] == 'G' && boardSet[8] == 'G')
        {
            someonehasWon = true;
            versusText.text = greenIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, greenIs); 
            
        }
        if (boardSet[0] == 'R' && boardSet[4] == 'R' && boardSet[8] == 'R')
        {
            someonehasWon = true;
            versusText.text = redIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, redIs);
        }
        if (boardSet[0] == 'G' && boardSet[4] == 'G' && boardSet[8] == 'G')
        {
            someonehasWon = true;
            versusText.text = greenIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, greenIs); //guest client won
        }
        if (boardSet[2] == 'R' && boardSet[4] == 'R' && boardSet[6] == 'R')
        {
            someonehasWon = true;
            versusText.text = redIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, redIs);
        }
        if (boardSet[2] == 'G' && boardSet[4] == 'G' && boardSet[6] == 'G')
        {
            
            someonehasWon = true;
            versusText.text = greenIs + " has won the game";
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("someoneWon", RpcTarget.All, greenIs);
            
        }
    }
    
    IEnumerator wait_until_lobby()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.LoadLevel("Loading");
    }

    [PunRPC]
    void someoneWon(string who)
    {
        versusText.text = who + " has won the game";
        someonehasWon = true;
    }

    private char GetLetterRepresentation(Color of)
    {
        if (of == Color.red)
        {
            return 'R';
        }
        else if (of == Color.green)
        {
            return 'G';
        }
        else
        {
            return 'F';
        }
    }
    [PunRPC]
    void RPCMakeTurnForGuests(string nm)
    {
        GameObject.Find(nm).GetComponent<SpriteRenderer>().color = Color.red;
        turnMaster = false;
        
    }
    [PunRPC]
    void RPCMakeTurnForMaster(string nm)
    {
        GameObject.Find(nm).GetComponent<SpriteRenderer>().color = Color.green;
        turnMaster = true;
        
    }
    

}
