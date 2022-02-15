using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeButton : MonoBehaviour
{
    public int myID;
    private Game mainframe;
    private void Start()
    {
        mainframe = GameObject.Find("Game").GetComponent<Game>();
    }
    private void OnMouseDown()
    {
        mainframe.makeTurn(myID.ToString());
    }
}
