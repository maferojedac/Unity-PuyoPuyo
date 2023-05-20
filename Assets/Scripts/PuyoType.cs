using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puyo", menuName = "Scriptable Objects/Puyo")]

public class PuyoType : ScriptableObject
{
    [Header("Puyo ID")]
    [SerializeField] private string puyoID;
    public string PuyoID { get { return puyoID; } private set { puyoID = value; } }

    [Header("General")]
    [SerializeField] private Color puyoColor;
    public float WhiteAlphaChannel { get { return whiteAlphaChannel / 255; } private set { whiteAlphaChannel = value; } }

//    [SerializeField] private Color puyoColor;
//    public float WhiteAlphaChannel { get { return whiteAlphaChannel / 255; } private set { whiteAlphaChannel = value; } }


    public void InitializePuyoStates()
    {
        PuyoSprites.InitializePuyoStates();
    }

    public PuyoState GetPuyoState(bool up, bool down, bool left, bool right)
    {
        return PuyoSprites.GetPuyoStateSprites(up, down, left, right);
    }

    public PuyoState GetPuyoState(string puyoStateID)
    {
        return PuyoSprites.GetPuyoStateSprites(puyoStateID);
    }

   public PuyoState GetPuyoState(int[] puyoState)
    {
        return PuyoSprites.GetPuyoStateSprites(puyoState);
    }

}