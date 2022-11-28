using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaludPersonaje : MonoBehaviour
{
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

    public void UpdateHealth(float daño)
    {
        vida -= daño;
        playerAnimator.SetTrigger("Golpeado");
        StartCoroutine(PerderControl());
        Debug.Log(vida);

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }
        else if(vida <= 0f)
        {
            Muerte();
        }
    }
    public void UpdateHealth(float daño, Vector2 posicion)
    {
        vida -= daño;
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
    }
    private void Muerte()
    {
        vida = 0f;
        Debug.Log("Game Over");
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
        Physics2D.IgnoreLayerCollision(6,7,true);
        yield return new WaitForSeconds(tiempoPerdidaControl);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }


}
