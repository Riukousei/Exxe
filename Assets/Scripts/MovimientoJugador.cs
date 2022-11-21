using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidadDeMovimiento = 5f;
    private float desplazamientoX, desplazamientoY;
    private Vector2 direccionDeMovimiento;
    private Rigidbody2D rbPersonaje;
    public Animator playerAnimator;
    // Es la primera en llamarse cuando el juego se ejecuta
    void Awake()
    {
        rbPersonaje = this.GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    // Es la segunda en llamarse cuando el juego se ejecuta
    void OnEnable()
    {
        
    }
    // Se llama después del primer frame
    void Start()
    {
        
    }

    // Es llamada 1 vez por frame
    void Update()
    {
        CapturaDePulsaciones();
        //playerAnimator.SetFloat("Horizontal", Mathf.Abs(desplazamientoX));
        playerAnimator.SetFloat("Magnitude", direccionDeMovimiento.sqrMagnitude);
        FlipPersonaje();

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

    void FlipPersonaje()
    {
        if (desplazamientoX > 0.01f)
            transform.localScale = Vector3.one;
        else if (desplazamientoX < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    void MovimientoDelPersonaje()
    {
        rbPersonaje.MovePosition(rbPersonaje.position + direccionDeMovimiento * velocidadDeMovimiento * Time.fixedDeltaTime);
    }
}
