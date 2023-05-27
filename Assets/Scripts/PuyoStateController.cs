using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PuyoStateController : MonoBehaviour
{
    
    [Header("Puyo Sprites Info")]
    //referencia al script de PuyoType
    [SerializeField] private PuyoType _puyoType;

    [Header("Puyo Parts Reference")]
    [SerializeField] private SpriteRenderer Base;
    [SerializeField] private SpriteRenderer White;
    //referencia al objeto que controla los ojos
    [SerializeField] private PuyoEyes puyoEyes;

    private SpriteRenderer _spriteRenderer;
    private PuyoState _puyoState; //referenciando a la estructura de datos del script PuyoSprites
    //elemento publico que solo el script puede modificar
    public string puyoTypeID { get; private set; }

    private void Awake() {
        _puyoType.InitializePuyoStates();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePuyoType(_puyoType);
    }

    private void UpdateSpritesInfo() {
        if(_spriteRenderer != null) {
            _spriteRenderer.color = _puyoType.PuyoColor;
            _spriteRenderer.sprite = _puyoState.Base;
        }

        if(White != null){
            Color tempColor = White.color;
            tempColor.a = _puyoType.WhiteAlphaChannel;
            White.color = tempColor;
            White.sprite = _puyoState.White;
        }

        puyoEyes.UpdateEyesInfo(_puyoState.RegularEyes, _puyoState.DieEyes, _puyoState.AnimationEyes);
    }

    //para cambiar el tipo de puyo
    public void UpdatePuyoType(PuyoType puyoType){
        _puyoType = puyoType;
        puyoTypeID = _puyoType.PuyoID;

        UpdateSpritesState(false, false, false, false);
    }

    public void UpdatePuyoType(PuyoType puyoType, int[] connections){
        _puyoType = puyoType;
        puyoTypeID = _puyoType.PuyoID;

        UpdateSpritesState(connections);
    }

    public void UpdateSpritesState(bool up, bool down, bool left, bool right) {
        _puyoState = _puyoType.GetPuyoState(up, down, left, right);
        UpdateSpritesInfo();
    }

    public void UpdateSpritesState(int[] connections) {
        _puyoState = _puyoType.GetPuyoState(connections);
        UpdateSpritesInfo();
    }
}
