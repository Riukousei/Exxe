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

    [SerializeField] private float daño;

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Enemy")
        //{
            
            impacto = true;
            collision.gameObject.GetComponent<Enemigo>().TomarDaño(daño);
            boxCollider.enabled = false;
            gameObject.SetActive(false);
            //Debug.Log(collision);
            animator.SetTrigger("explota");
            Desactivar();
        //}
        
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
