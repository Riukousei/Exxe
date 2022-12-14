using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaludPersonaje : MonoBehaviour
{
    public GameObject vidaObject;
    private float vida = 0f;
    [SerializeField] private float vidaMaxima = 100f;
    private MovimientoJugador movimientoJugador;
    [SerializeField] private float tiempoPerdidaControl;
    public Animator playerAnimator;

    private void Start()
    {
        movimientoJugador = GetComponent<MovimientoJugador>();
        playerAnimator = GetComponent<Animator>();
        vida = vidaMaxima;
    }

    public void UpdateHealth(float da?o)
    {
        vida -= da?o;
        playerAnimator.SetTrigger("Golpeado");
        StartCoroutine(PerderControl());
        Debug.Log(vida);

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }
        else if(vida <= 0f)
        {
            vida = 0;
            Muerte();
        }
        vidaObject.GetComponent<Slider>().value = vida;
        vidaObject.GetComponentInChildren<TextMeshProUGUI>().text = vida.ToString();
    }
    public void UpdateHealth(float da?o, Vector2 posicion)
    {
        vida -= da?o;
        playerAnimator.SetTrigger("Golpeado");
        StartCoroutine(PerderControl());
        StartCoroutine(DesactivarColision());
        movimientoJugador.Rebote(posicion);

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }
        else if (vida <= 0f)
        {
            Muerte();
        }
        vidaObject.GetComponent<Slider>().value = vida;
        vidaObject.GetComponentInChildren<TextMeshProUGUI>().text = vida.ToString();
    }
    private void Muerte()
    {
        vida = 0f;
        Debug.Log("Game Over"); //Muestra si el personaje muri? y tiene que haber game over
        playerAnimator.SetTrigger("Muerte");

    }

    private void Destruir()
    {
        Destroy(gameObject);
    }

    private IEnumerator PerderControl()
    {
        movimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoJugador.sePuedeMover = true;
    }
    private IEnumerator DesactivarColision()
    {
        Physics2D.IgnoreLayerCollision(6,8,true);
        yield return new WaitForSeconds(tiempoPerdidaControl);
        Physics2D.IgnoreLayerCollision(6,8, false);
    }


}
    