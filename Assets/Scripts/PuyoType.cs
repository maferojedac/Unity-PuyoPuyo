using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*en este script se define el tipo de puyo, el color, el alpha, los sprites*/

//ya estamos creando un puyo
[CreateAssetMenu(fileName = "New Puyo", menuName = "Scriptable Objects/Puyo")]

public class PuyoType : ScriptableObject
{
    [Header("Puyo ID")]
    [SerializeField] private string puyoID;
    //para que puedan leer (no modificar) otros scripts el objeto privado
    public string PuyoID { get { return puyoID; } private set { puyoID = value; } }

    [Header("General")]
    [SerializeField] private Color puyoColor;
    public Color PuyoColor { get { return puyoColor; } private set { puyoColor = value; } }

    //para poner un rango en el Inspector
    [SerializeField] [Range(0.0f, 255.0f)] private float whiteAlphaChannel;
    //se debe dividir porque Unity lo maneja el valor entre 0-1
    public float WhiteAlphaChannel { get { return whiteAlphaChannel / 255; } private set { whiteAlphaChannel = value; } }

    [Header("Puyo Sprites")]
    [SerializeField] private PuyoSprites PuyoSprites;

    //para inicializar los estados de los puyos -- el diccionario
    public void InitializePuyoStates() {
        PuyoSprites.InitializePuyoStates[];
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