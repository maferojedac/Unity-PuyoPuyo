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
    public Color PuyoColor { get { return puyoColor; } private set { puyoColor = value; } }

    [SerializeField] [Range(0.0f, 255.0f)] private float whiteAlphaChannel;
    public float WhiteAlphaChannel { get { return whiteAlphaChannel / 255; } private set { whiteAlphaChannel = value; } }

    [Header("Puyo Sprites")]
    [SerializeField] private PuyoSprites PuyoSprites;

    public void InitializePuyoStates() {
        PuyoSprites.InitializePuyoStates();
    }

    public PuyoState GetPuyoState(bool up, bool down, bool right, bool left) {
        return PuyoSprites.GetPuyoStateSprites(up, down, right, left);
    }

    public PuyoState GetPuyoState(string puyoStateID) {
        return PuyoSprites.GetPuyoStateSprites(puyoStateID);
    }

    public PuyoState GetPuyoState(int[] puyoState) {
        return PuyoSprites.GetPuyoStateSprites(puyoState);
    }

}