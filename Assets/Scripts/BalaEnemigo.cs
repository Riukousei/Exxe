using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] private float Velocidad = 4;
    [SerializeField] private float daño;
    private float posX, posY;
    private Vector2 direccionDeMovimiento;
    private Animator animator;
    private BoxCollider2D boxCollider;
    public Transform player_pos;
    public bool mirandoDerecha;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        //player_pos = GameObject.Find("Char").transform;
        player_pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        posX = player_pos.position.x;
        Debug.LogWarning(posX);
        posY = player_pos.position.y;
        Debug.LogWarning(posY);
        if (mirandoDerecha)
        {
            direccionDeMovimiento = new Vector2(-this.transform.position.x + posX, -this.transform.position.y - (-posY)).normalized;
        }
        else
        {
            direccionDeMovimiento = new Vector2(this.transform.position.x - posX, -this.transform.position.y - (-posY)).normalized; 
        }

        Debug.LogWarning(direccionDeMovimiento);
    }
    private void Update()
    {
        
        transform.Translate(direccionDeMovimiento*Velocidad*Time.deltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning(collision);
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<SaludPersonaje>().UpdateHealth(daño, collision.GetContact(0).normal);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
