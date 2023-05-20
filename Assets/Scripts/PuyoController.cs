using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//para controlar el controlador del controlador del puyo
public class PuyoController : MonoBehaviour
{
    [SerializeField] private PuyoStateController puyoBase;

    void Update() {
        if(Input.GetKeyDown(KeyCode.A)) {
            ChangePuyoState(false, false, false, false);
        }
        if(Input.GetKeyDown(KeyCode.B)) { //mira hacia arriba
            ChangePuyoState(true, false, false, false);
        }
        if(Input.GetKeyDown(KeyCode.C)) { //left right
            ChangePuyoState(false, false, true, true);
        }
    }

    public void ChangePuyoState(bool up, bool down, bool left, bool right) {
        puyoBase.UpdateSpritesState(up, down, left, right);
    }
}
