using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float Velocidad = 4;
    [SerializeField] private float da�o;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right*Velocidad*Time.deltaTime);
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (collision.transform.GetComponent<Enemigo>() != null)
            {
                collision.gameObject.GetComponent<Enemigo>().TomarDa�o(da�o);
            }
            else if (collision.transform.GetComponent<Tamalero>() != null)
            {
                collision.gameObject.GetComponent<Tamalero>().TomarDa�o(da�o);
            }
            else if (collision.transform.GetComponent<Moto>() != null)
            {
                collision.gameObject.GetComponent<Moto>().TomarDa�o(da�o);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    
}
