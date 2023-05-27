using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//para que pueda cambiarse el color de la explosion (que tiene que ser del color del puyo)
[RequireComponent(typeof(SpriteRenderer))]
//necesitamos el componente de la animacion
[RequireComponent(typeof(Animator))]
public class PuyoExplosion : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animatorController;

    //creando un evento - para ejecutar una funcion cuando la animacion termine
    //podemos crear un evento ern cualquier punto de las animaciones
    public System.Action onExplosionFinished;

    //asignamos los objetos
    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animatorController = GetComponent<Animator>();
        //queremos que este apagado desde un inicio
        this.gameObject.SetActive(false);
    }

    public void UpdateExplosionColor(Color color) {
        _spriteRenderer.color = color;
    }

    public void ReproduceExplosion() {
        //activamos el objeto
        this.gameObject.SetActive(true);
        //pide el nombre de la animacion creada en el animator
        _animatorController.Play("Explosion");
    }

    //para que se ejecute la funcion cuando la animacion termine
    public void OnAnimationFinished() {
        this.gameObject.SetActive(false);
        //se encarga de avisarle a los demas scripts que la animacion ya termino
        //patron del observador
        onExplosionFinished?.Invoke();
    }
}
