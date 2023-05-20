using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //para poder ver la estructura en el Inspector
public struct PuyoState {
    [Header ("Body Base")]
    public Sprite Base;
    public Sprite White;

    [Header ("Eyes")]
    public Sprite RegularEyes;
    public Sprite DieEyes;
    public Sprite[] AnimationEyes;

    //cualquier objeto puede leer la variable, pero yo solo puedo asignar la variable
    public int[] _puyoState {get; private set; }
    //solo el mismo objeto puede cambiar sus estados, pero puede leerse de cualquier parte
    public string PuyoStateID {get; private set; }

    //para guardar los datos y asignar un estado
    
    public void AsignPuyoState(bool up, bool down, bool right, bool left) {
        _puyoState = new int[4] {0, 0, 0, 0};

        if(up) {
            _puyoState[0] = 1;
        } if(down) {
            _puyoState[1] = 1;
        } if(right) {
            _puyoState[2] = 1;
        } if(left) {
            _puyoState[3] = 1;
        }
        //el id nos dice hacia donde esta volteando
        PuyoStateID = _puyoState[0] + "," + _puyoState[1] + "," + _puyoState[2] + "," + _puyoState[3];
    }

}

//creando un menu para crear sripts especiales
[CreateAssetMenu(fileName = "New Puyo Sprite", menuName = "Scriptable Objects/Puyo Sprites")]

//los ScriptableObject no tienen un Start ni Update porque son assets, es un objeto
//no es un comportamiendo - MonoBehaviour
public class PuyoSprites : ScriptableObject
{
    [SerializeField] private PuyoState Base;
    [SerializeField] private PuyoState ConnectedUp;
    [SerializeField] private PuyoState ConnectedDown;
    [SerializeField] private PuyoState ConnectedLeft;
    [SerializeField] private PuyoState ConnectedRight;
    [SerializeField] private PuyoState ConnectedUpDown;
    [SerializeField] private PuyoState ConnectedLeftRight;

    //para almacenar los estados -- comportamientos segun los datos
    //se pueden consultar cuando quieras
    //en lugar de hacer una funcion que busque por el id (con una funcion)
    //aunque debemos hacer funciones para consultarlo porque es privado
    private Dictionary<string, PuyoState> PuyoTypes = new Dictionary<string, PuyoState>();

    public void InitializePuyoState() {
        Base.AsignPuyoState(false, false, false, false);
        PuyoTypes.Add(Base.PuyoState, Base); //para guardar la estructura en el diccionario

        ConnectedUp.AsignPuyoState(true, false, false, false);
        PuyoTypes.Add(Base.PuyoState, Base);

        ConnectedRight.AsignPuyoState(false, false, true, false);
        PuyoTypes.Add(Base.PuyoState, Base);

        ConnectedDown.AsignPuyoState(false, true, false, false);
        ConnectedLeft.AsignPuyoState(false, false, false, true);
        ConnectedLeftRight.AsignPuyoState(false, false, true, true);
        ConnectedUpDown.AsignPuyoState(true,true, false, false);

    }

    public PuyoState GetPuyoStateSprites(string puyoStateID){
        if(PuyoTypes.ContainsKey(puyoStateID))
            return PuyoTypes[puyoStateID];
        else
            return Base;
    }

    public PuyoState GetPuyoStateSprites(int[] puyoState)
    {
        string _puyoStateID;
        if (puyoState.Length == 4)
            _puyoStateID = puyoState[0] + "," + puyoState[1] + "," + puyoState[2] + "," + puyoState[3];
        else
            _puyoStateID = "null";
        return GetPuyoStateSprites(_puyoStateID);
    }

    //para obtener los numeros de los estados
    public PuyoState GetPuyoStateSprites(bool up, bool down, bool right, bool left){

       int[] _puyoState = new int[4] {0, 0, 0, 0};

        if(up) {
            _puyoState[0] = 1;
        } if(down) {
            _puyoState[1] = 1;
        } if(right) {
            _puyoState[2] = 1;
        } if(left) {
            _puyoState[3] = 1;
        }
        
    }

    
}
