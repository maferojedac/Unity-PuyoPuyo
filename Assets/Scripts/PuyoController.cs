using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//queremos controlar la animacion

//para controlar el controlador del controlador del puyo
public class PuyoController : MonoBehaviour
{
    //hace referencia al controlador de estados del puyo (PuyoBase en la jerarquia)
    [SerializeField] private PuyoStateController puyoBase;
    [SerializeField] private PuyoExplosion puyoExplosion;

    private bool explosionFinished;
    private bool isDying;

    //creamos un evento para avisar a otros scripts que el puyo murio
    public System.Action puyoDied;

    private void Start() {
        explosionFinished = false;
        //recibimos lo que tenga el puyoBase y su color
        puyoExplosion.UpdateExplosionColor(puyoBase.GetPuyoColor());
        puyoExplosion.onExplosionFinished += ExplosionFinished;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.A)) { //base
            ChangePuyoState(false, false, false, false);
        }
        if(Input.GetKeyDown(KeyCode.B)) { //up
            ChangePuyoState(true, false, false, false);
        }
        if(Input.GetKeyDown(KeyCode.C)) { //left right
            ChangePuyoState(false, false, true, true);
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            KillPuyo();
        }

        //se hizo de tarea
        if(Input.GetKeyDown(KeyCode.P)) { //left
            ChangePuyoState(false, false, false, true);
        }
        if(Input.GetKeyDown(KeyCode.E)) { //up down
            ChangePuyoState(true, true, false, false);
        }
        if(Input.GetKeyDown(KeyCode.O)) { //left down
            ChangePuyoState(false, true, false, true);
        }
        if(Input.GetKeyDown(KeyCode.G)) { //down right
            ChangePuyoState(false, true, true, false);
        }
        if(Input.GetKeyDown(KeyCode.H)) { //up right
            ChangePuyoState(true, false, true, false);
        }
        if(Input.GetKeyDown(KeyCode.I)) { //left up
            ChangePuyoState(true, false, false, true);
        }
        if(Input.GetKeyDown(KeyCode.J)) { //left up down
            ChangePuyoState(true, true, false, true);
        }
        if(Input.GetKeyDown(KeyCode.K)) { //right up down
            ChangePuyoState(true, true, true, false);
        }
        if(Input.GetKeyDown(KeyCode.L)) { //down right left
            ChangePuyoState(false, true, true, true);
        }
        if(Input.GetKeyDown(KeyCode.M)) { //up right left
            ChangePuyoState(true, false, true, true);
        }
        if(Input.GetKeyDown(KeyCode.N)) { //up down right left
            ChangePuyoState(true, true, true, true);
        }
    }

    public void ChangePuyoState(bool up, bool down, bool right, bool left) {
        if (!isDying)
            puyoBase.UpdateSpritesState(up, down, right, left);
    }

    public void ChangePuyoState(int[] connections) {
        if (!isDying)
            puyoBase.UpdateSpritesState(connections);
    }

    public void KillPuyo() {
        //comienza una corrutina que coienza la otra corrutina
        StartCoroutine(StartDying());
    }

    private void ExplosionFinished() {
        explosionFinished = true;
    }

    public IEnumerator StartDying() {
        isDying = true;
        //al puyo que este pone los ojos de que ya se va a morir
        puyoBase.SetDieEyes();
        yield return new WaitForSeconds(0.2f);
        puyoBase.gameObject.SetActive(false);
        puyoExplosion.ReproduceExplosion();
        //esperamos a que la explosion termine, hatsat que sea tru
        //usamos una funcion lambda
        yield return new WaitUntil(() => explosionFinished);
        //porque ya termino la animacion
        Destroy(this.gameObject);
        puyoDied?.Invoke();
        Debug.Log("Puyo muerto");
    }
}
