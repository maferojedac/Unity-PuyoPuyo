using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//automaticamente te pone el componente correspondiente y no lo puedes remover
//al añadir el script al objeto
[RequireComponent(typeof(SpriteRenderer))]
public class PuyoEyes : MonoBehaviour
{
    [Header("Eyes Sprites")]
    //los sprites estaran constantemente cambiando pero se pusieron para que fuera notorio desde el inspector
    [SerializeField] private Sprite regularEyes;
    [SerializeField] private Sprite aboutToDieEyes;

    //tiempo aleatorio para que el puyo parpadee diferente cada cierto tiempo
    [Header("Animation Time")] //1/10 de segundo porque es una animacion muy rapida y simple
    [SerializeField] private float animationTime = 0.1f; //lo que tarda en cambiar de un sprite a otro  
    //al hacer un random, necesitamos un tope minimo y maximo
    [SerializeField] private float animateEyesEverySecondsMin;
    [SerializeField] private float animateEyesEverySecondsMax;

    [Header("Eyes Animation Sprites")]
    //se declara como un arreglo porque asi se manejan los ojos de los sprites
    //desde el inspector podemos agregar mas sprites diferentes y se animaran
    [SerializeField] private Sprite[] animationSprites;

    private SpriteRenderer _spriteRenderer; //para llamar al spriteRenderer al cambiar las animaciones
    private IEnumerator _animateEyes; //para poder contorlar mejor la corrutina se agrega una variable

    //en orden de ejecucion: Awake, Start, OnEnable (ocurre solo cuando un objeto se activa)
    private void Awake() {
        //unity ejecuta todos los awake de todos los scripts pero no sabemos en que orden lo hace, no siempre es el mismo
        _spriteRenderer = GetComponent<SpriteRenderer>(); //cargamos las cosas
        _animateEyes = AnimateEyes(animationTime); //para que no repetitan las animaciones
    }

    private void OnEnable() { //se ejecuta cuando se activa el objeto
        // PROBAR CUANDO SIRVA - EJECUTAR CADA LINEA EN ORDEN (SOLAMENTE 1 A LA VEZ)
        // StartCoroutine(AnimateEyes(animationTime)); //despues de darle play en unity, desactivar los ojos y parpadeara
        //despues de darle play en unity, desactivar los ojos y parpadeara cada ciertos segundos
        // StartCoroutine(AnimateEyesEveryRandomSeconds(animateEyesEverySecondsMin, animateEyesEverySecondsMax));
        UpdateSpriteInfo(regularEyes);
    }

    private void OnDisable() { //buena practica hacerlo cuando desactivas un objeto
        StopAllCoroutines();
    }

    public void OnDieState() {
        StopAllCoroutines(); //cuando va a morir el puyo no necesitamos que se sigan ejecutando las animaciones
        UpdateSpriteInfo(aboutToDieEyes);
    }

    //para actualizar el contenido de las animaciones cuando se muere el puyo
    private void UpdateSpriteInfo(Sprite sprite) {
        if(_spriteRenderer != null) {
            _spriteRenderer.sprite = sprite;
            //inicia la animacion correspondiente, reinicia la corutina
            StartCoroutine(AnimateEyesEveryRandomSeconds(animateEyesEverySecondsMin, animateEyesEverySecondsMax));
        }
    }

    //para que se pueda cambiar la informacion de los ojos en tiempo de ejecucion
    public void UpdateEyesInfo(Sprite regularEyes, Sprite dieEyes, Sprite[] animationEyes) {
        StopAllCoroutines(); //no puede actualizar si una corrutina se esta ejecutando
        this.gameObject.SetActive(false); //por seguridad agregarlo desactivamos el objeto
        //this - hace referencia al declarado arriba, no el que entra como parametro
        this.regularEyes =  regularEyes;
        aboutToDieEyes = dieEyes;
        animationSprites = animationEyes;
        this.gameObject.SetActive(true); //por seguridad agregarlo, se activa el objeto una vez que se actualizo la info
    }

    //se ejecuta despues del Update, se ejecuta en el tiempo que se le indique (0.1f)
    //a lo largo de diferentes frames/tiempos, podemos ejecutar cierto codigo
    private IEnumerator AnimateEyes(float animationTime) {
        //recorremos el arreglo de los sprites de la animacion
        for(int i = 0; i < animationSprites.Length; i++) {
            _spriteRenderer.sprite = animationSprites[i];
            yield return new WaitForSeconds(animationTime);
        }
        _spriteRenderer.sprite = regularEyes; //una vez que termine de hacerse la animacion
    }

    //hara la animacion de parpadear
    //se ejecutara constantemente en loop durante todo el tiempo del juego
    private IEnumerator AnimateEyesEveryRandomSeconds(float minSeconds, float maxSeconds) {
        while(true) { //no queremos que se detenga durante todo el juego
            float seconds = Random.Range(minSeconds, maxSeconds);
            //sin esta linea seria una muy mala implementacion con un while(true)
            yield return new WaitForSeconds(seconds); //regresa la ejecucion a unity esperando una cierta cantidad de tiempo
            //para que no haya conflictos de que se esta ejecutando la corrutina varias veces a la vez
            StartCoroutine(_animateEyes); //se manda llamar la instancia de la corrutina
            //esto es para que no se repita el mismo sprite de la animacion y se vea raro
            _animateEyes = AnimateEyes(animationTime);
        }
        
    }
    
}
