using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*en este script se guardan los sprites de los puyos*/

[System.Serializable] //para poder ver la estructura en el Inspector
public struct PuyoState {
    [Header ("Body Base")]
    public Sprite Base;
    public Sprite White;

    [Header ("Eyes")]
    public Sprite RegularEyes;
    public Sprite DieEyes;
    public Sprite[] AnimationEyes; //como no sabemos cuantos sprites son al parpadear, usamos un array

    //cualquier objeto puede leer la variable, pero yo solo puedo asignar la variable
    public int[] _puyoState {get; private set; }
    //solo el mismo objeto puede cambiar sus estados, pero puede leerse de cualquier parte
    public string PuyoStateID {get; private set; }

    //asignando id a los sprites creados de las animaciones
    //para guardar los datos y asignar un estado
    public void AsignPuyoState(bool up, bool down, bool right, bool left) {
        _puyoState = new int[4] {0, 0, 0, 0};

        if(up)
            _puyoState[0] = 1;
        if(down)
            _puyoState[1] = 1;
        if(right)
            _puyoState[2] = 1;
        if(left)
            _puyoState[3] = 1;

        //el id de los srpites que nos dice hacia donde esta volteando el puyo
        //no necesitamos convertir a string porque lo hace automaticamente, solo los floats no
        PuyoStateID = _puyoState[0] + "," + _puyoState[1] + "," + _puyoState[2] + "," + _puyoState[3];
    }

}

//creando un menu para crear sripts especiales y aparezca en el menu de los assets
//file name -- nombre del archivo por default
//menu name -- nombre del menu donde aparecera
[CreateAssetMenu(fileName = "New Puyo Sprite", menuName = "Scriptable Objects/Puyo Sprites")]

//los ScriptableObject no tienen un Start ni Update porque son assets, no es un objeto ni comportamiento
//no es un comportamiendo - MonoBehaviour
//nos permite guardar y almacenar info de diferentes formas,se pueden tratar como si fueran prefabs-crear instancias
public class PuyoSprites : ScriptableObject
{
    //campo serializable para poder verlo en el inspector, como fue declarado asi en la estructura
    [SerializeField] private PuyoState Base;
    [SerializeField] private PuyoState ConnectedUp;
    [SerializeField] private PuyoState ConnectedDown;
    [SerializeField] private PuyoState ConnectedLeft;
    [SerializeField] private PuyoState ConnectedRight;
    [SerializeField] private PuyoState ConnectedUpDown;
    [SerializeField] private PuyoState ConnectedLeftRight;
    //hecho de tarea
    [SerializeField] private PuyoState ConnectedLeftDown;
    [SerializeField] private PuyoState ConnectedDownRight;
    [SerializeField] private PuyoState ConnectedUpRight;
    [SerializeField] private PuyoState ConnectedLeftUp;
    [SerializeField] private PuyoState ConnectedLeftBase;
    [SerializeField] private PuyoState ConnectedBaseRight;
    [SerializeField] private PuyoState ConnectedBaseDown;
    [SerializeField] private PuyoState ConnectedUpBase;
    [SerializeField] private PuyoState ConnectedBaseCenter;

    //para almacenar los estados -- comportamientos segun los datos
    //se pueden consultar cuando quieras mas facilmente
    //en lugar de hacer una funcion que busque por el id (con una funcion)
    //aunque debemos hacer funciones para consultarlo porque es privado
    private Dictionary<string, PuyoState> PuyoTypes = new Dictionary<string, PuyoState>();

    public void InitializePuyoState() {
        Base.AsignPuyoState(false, false, false, false);
        PuyoTypes.Add(Base.PuyoStateID, Base); //para guardar la estructura en el diccionario

        ConnectedUp.AsignPuyoState(true, false, false, false);
        PuyoTypes.Add(ConnectedUp.PuyoStateID, ConnectedUp);

        ConnectedRight.AsignPuyoState(false, false, true, false);
        PuyoTypes.Add(ConnectedRight.PuyoStateID, ConnectedRight);

        ConnectedDown.AsignPuyoState(false, true, false, false);
        PuyoTypes.Add(ConnectedDown.PuyoStateID, ConnectedDown);

        ConnectedLeft.AsignPuyoState(false, false, false, true);
        PuyoTypes.Add(ConnectedLeft.PuyoStateID, ConnectedLeft);

        ConnectedLeftRight.AsignPuyoState(false, false, true, true);
        PuyoTypes.Add(ConnectedLeftRight.PuyoStateID, ConnectedLeftRight);

        ConnectedUpDown.AsignPuyoState(true,true, false, false);
        PuyoTypes.Add(ConnectedUpDown.PuyoStateID, ConnectedUpDown);
        //hecho de tarea
        ConnectedLeftDown.AsignPuyoState(false, true, false, true);
        PuyoTypes.Add(ConnectedLeftDown.PuyoStateID, ConnectedLeftDown);

        ConnectedDownRight.AsignPuyoState(false, true, true, false);
        PuyoTypes.Add(ConnectedDownRight.PuyoStateID, ConnectedDownRight);

        ConnectedUpRight.AsignPuyoState(true, false, true, false);
        PuyoTypes.Add(ConnectedUpRight.PuyoStateID, ConnectedUpRight);

        ConnectedLeftUp.AsignPuyoState(false, false, true, true);
        PuyoTypes.Add(ConnectedLeftUp.PuyoStateID, ConnectedLeftUp);

        ConnectedLeftBase.AsignPuyoState(false, false, false, true);
        PuyoTypes.Add(ConnectedLeftBase.PuyoStateID, ConnectedLeftBase);

        ConnectedBaseRight.AsignPuyoState(false, false, true, false);
        PuyoTypes.Add(ConnectedBaseRight.PuyoStateID, ConnectedBaseRight);

        ConnectedBaseDown.AsignPuyoState(false, true, false, false);
        PuyoTypes.Add(ConnectedBaseDown.PuyoStateID, ConnectedBaseDown);

        ConnectedUpBase.AsignPuyoState(true, false, false, false);
        PuyoTypes.Add(ConnectedUpBase.PuyoStateID, ConnectedUpBase);

        ConnectedBaseCenter.AsignPuyoState(false, false, false, false);
        PuyoTypes.Add(ConnectedBaseCenter.PuyoStateID, ConnectedBaseCenter);
    }

    //para consultar el diccionario porque es privado
    public PuyoState GetPuyoStateSprites(string puyoStateID) {
        //mandamos llamar al diccionario y le pasamos el id
        if(PuyoTypes.ContainsKey(puyoStateID))
            return PuyoTypes[puyoStateID];
        else
            return Base;
    }

    //para obtener los numeros de los estados -- sobrecarga de funciones
    public PuyoState GetPuyoStateSprites(int[] puyoState) {
        string _puyoStateID;
        //para que no se rompa si no hay 4 estados, chequeo por seguridad
        if (puyoState.Length == 4)
            _puyoStateID = puyoState[0] + "," + puyoState[1] + "," + puyoState[2] + "," + puyoState[3];
        else
            _puyoStateID = "null";
        return GetPuyoStateSprites(_puyoStateID);
    }

    //nos devuelve la estructura del set de sprites que se tiene en ese momento
    public PuyoState GetPuyoStateSprites(bool up, bool down, bool right, bool left) {
       int[] _puyoState = new int[4] {0, 0, 0, 0};

        if(up)
            _puyoState[0] = 1;
        if(down)
            _puyoState[1] = 1;
        if(right)
            _puyoState[2] = 1;
        if(left)
            _puyoState[3] = 1;
        
        return GetPuyoStateSprites(_puyoState);
    }
    
}
