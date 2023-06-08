using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//necesitamos el componente porque lo vamos a ir modificando
[RequireComponent(typeof(SpriteRenderer))]
public class PuyoStateController : MonoBehaviour
{
    //necesitamos todas las refrencias (instancias) para poder ir actualizando/cambiando
    //la info (los sprites) dependiendo del estado del puyo

    [Header("Puyo Sprites Info")]
    //referencia al script de PuyoType
    //para extraer toda a info del objeto: id, color, alpha channel, sprites que van cambiando
    [SerializeField] private PuyoType _puyoType;

    //referencia de las partes del puyo para poder cambiarlas
    [Header("Puyo Parts Reference")]
    [SerializeField] private SpriteRenderer Base;
    [SerializeField] private SpriteRenderer White;
    //referencia al objeto que controla los ojos
    [SerializeField] private PuyoEyes puyoEyes;

    private SpriteRenderer _spriteRenderer;
    private PuyoState _puyoState; //referenciando a la estructura de datos del script PuyoSprites
    //elemento publico que solo este script puede modificar
    public string puyoTypeID { get; private set; }

    private void Awake() {
        //es recomendable que todas las inicializaciones esten en el Awake a menos que
        //se tenga un objeto/elemento en otro script dependa de la inicializacion de otra cosa en otro script

        //para definir manualmente el orden especifico de ejecucion de cada script se puede configurar en
        //Project Settings -> Script Execution Order
        //solo es usado cuando se estan haciendo llamadas a servicios externos (como un servidor o BD)
        
        //mandando llamar la funcion desde el objeto (scrptable object) que
        //inicializa el diccionario donde se guardan cada uno de los tipos de ojos (estados)
        _puyoType.InitializePuyoStates(); 
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePuyoType(_puyoType);
    }

    //para actualizar la info de los sprites - depende de lo que le mande el estado
    private void UpdateSpritesInfo() {
        if(_spriteRenderer != null) { //para que no de error en caso de que no este asignado
            _spriteRenderer.color = _puyoType.PuyoColor;
            _spriteRenderer.sprite = _puyoState.Base;
        }

        if(White != null) {
            Color tempColor = White.color; //crea un color temporal
            tempColor.a = _puyoType.WhiteAlphaChannel;
            White.color = tempColor;
            White.sprite = _puyoState.White;
        }

        puyoEyes.UpdateEyesInfo(_puyoState.RegularEyes, _puyoState.DieEyes, _puyoState.AnimationEyes);
    }

    //para cambiar el tipo de puyo
    public void UpdatePuyoType(PuyoType puyoType) {
        _puyoType = puyoType;
        puyoTypeID = _puyoType.PuyoID;

        UpdateSpritesState(false, false, false, false);
    }

    public void UpdatePuyoType(PuyoType puyoType, int[] connections) {
        _puyoType = puyoType;
        puyoTypeID = _puyoType.PuyoID;

        UpdateSpritesState(connections);
    }

    public void UpdateSpritesState(bool up, bool down, bool right, bool left) {
        _puyoState = _puyoType.GetPuyoState(up, down, right, left);
        UpdateSpritesInfo();
    }

    public void UpdateSpritesState(int[] connections) {
        _puyoState = _puyoType.GetPuyoState(connections);
        UpdateSpritesInfo();
    }

    public void SetDieEyes() {
        puyoEyes.OnDieState();
    }

    //nos devuelve el color del puyo
    public Color GetPuyoColor() {
        return _puyoType.PuyoColor;
    }
}
