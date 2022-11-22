using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float Velocidad = 4;
    [SerializeField] private float daño;
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
            collision.gameObject.GetComponent<Enemigo>().TomarDaño(daño);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    
}
