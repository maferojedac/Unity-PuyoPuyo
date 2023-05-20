using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//automaticamente te pone el componente correspondiente y no lo puedes remover
//al añadir el script al objeto
[RequireComponent(typeof(SpriteRenderer))]
public class PuyoEyes : MonoBehaviour
{
    [Header("Eyes Sprites")]
    [SerializeField] private Sprite regularEyes;
    [SerializeField] private Sprite aboutToDieEyes;

    //tiempo aleatorio para que el puyo parpadee diferente cada cierto tiempo
    [Header("Animation Time")]
    [SerializeField] private float animationTime = 0.1f; //lo que tarda en cambiar de un sprite a otro  
    [SerializeField] private float animateEyesEverySecondsMin;
    [SerializeField] private float animateEyesEverySecondsMax;

    [Header("Eyes Animation Sprites")]
    //se declara como un arreglo porque asi se manejan los ojos de los sprites
    [SerializeField] private Sprite[] animationSprites;

    private SpriteRenderer _spriteRenderer; //para llamar al spriteRenderer al cambiar las animaciones
    private IEnumerator _animateEyes; //para llamar al spriteRenderer al cambiar las animaciones

    //en orden de ejecucion: Awake, Start, OnEnable (ocurre cuando un objeto es activado)
    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>(); //cargamos las cosas
        _animateEyes = AnimateEyes(animationEyes); //para que no repetita las animaciones
    }

    private void OnEnable() {
        // PROBAR CUANDO SIRVA
        // StartCoroutine(AnimateEyes(animationTime));
        // StartCoroutine(AnimateEyesEveryRandomSeconds(animateEyesEverySecondsMin, animateEyesEverySecondsMax));
        // UpdateSpriteInfo(regularEyes);
    }

    private void OnDisable() { //buena practica hacerlo
        StopAllCoroutines(); //cuando desactivas el objeto
    }

    public void OnDieState() {
        StopAllCoroutines(); //cuando va a morir el puyo
        UpdateSpriteInfo(aboutToDieEyes);
    }

    private void UpdateSpriteInfo(Sprite sprite) {
        if(_spriteRenderer != null) {
            _spriteRenderer.sprite = sprite;
            //inicia la animacion correspondiente, reinicia la corutina
            StartCoroutine(AnimateEyesEveryRandomSeconds(animateEyesEverySecondsMin, animateEyesEverySecondsMax));
        }
    }

    public void UpdateEyesInfo(Sprite regularEyes, Sprite dieEyes, Sprite[] animationEyes) {
        StopAllCoroutines();
        this.gameObject.setActive(false); //por seguridad agregarlo
        //this hace referencia al declarado arriba
        this.regularEyes =  regularEyes;
        aboutToDieEyes = dieEyes;
        animationSprites = animationEyes;
        this.gameObject.setActive(true); //por seguridad agregarlo
    }

    //se ejecuta despues del Update
    //a lo largo de diferentes frames, podemos ejecutar cierto codigo
    private IEnumerator AnimateEyes(float animationTime) {
        //recorremos los sprites de la animacion
        for(int i = 0; i < animationSprites.length; i++) {
            _spriteRenderer.sprite = animationSprites[i];
            yield return new WaitForSeconds(animationTime);
        }
        _spriteRenderer.sprite = RegularEyes;
    }

    //se ejecutara constantemente en loop durante todo el tiempo del juego
    private IEnumerator AnimateEyesEveryRandomSeconds(float minSeconds, float maxSeconds) {
        while(true) {
            float seconds = Random.Range(minSeconds, maxSeconds);
            //sin esta linea seria una mala implementacion con un while(true)
            yield return new WaitForSeconds(seconds); //regresa la ejecucion a unity esperando una cierta cantidad de tiempo
            StartCoroutine(_animateEyes);
            _animateEyes = AnimateEyes(animationTime);
        }
        
    }
    
}
