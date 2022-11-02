using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float Velocidad = 4;
    private float direccion;
    private bool impacto;
    private float TiempoDeVida;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (impacto) return;
        float VelocidadMovimiento = Velocidad * Time.deltaTime * direccion;
        transform.Translate(VelocidadMovimiento, 0, 0);

        TiempoDeVida += Time.deltaTime;
        if (TiempoDeVida > 2)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        impacto = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explota");
    }
    public void SetDirection (float _direction)
    {
        TiempoDeVida = 0;
        direccion = _direction;
        gameObject.SetActive(true);
        impacto = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }
    private void Desactivar()
    {
        gameObject.SetActive(false);
    }
}
