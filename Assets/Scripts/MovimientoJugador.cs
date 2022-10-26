using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidadDeMovimiento = 2f;
    private float desplazamientoX, desplazamientoY;
    private Vector2 direccionDeMovimiento;
    private Rigidbody2D rbPersonaje;
    // Start is called before the first frame update
    void Start()
    {
        rbPersonaje = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CapturaDePulsaciones();
    }

    private void FixedUpdate()
    {
        MovimientoDelPersonaje();
    }

    void CapturaDePulsaciones()
    {
        desplazamientoX = Input.GetAxisRaw("Horizontal");
        desplazamientoY = Input.GetAxisRaw("Vertical");
        direccionDeMovimiento = new Vector2(desplazamientoX, desplazamientoY).normalized;
    }
    void MovimientoDelPersonaje()
    {
        rbPersonaje.velocity = new Vector2(direccionDeMovimiento.x * velocidadDeMovimiento, direccionDeMovimiento.y * velocidadDeMovimiento);
    }
}
