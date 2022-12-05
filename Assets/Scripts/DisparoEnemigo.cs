using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    [SerializeField] private float CooldownAtaque;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject proyectiles;
    private Animator animatorEnemigo;
    private Enemigo Enemigo;
    private Tamalero Tamalero;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        animatorEnemigo = GetComponent<Animator>();
        Enemigo = GetComponent<Enemigo>();
    }

    private void Update()
    {
        if ((Enemigo.puedeAtacar == true))
        {
            if (cooldownTimer > CooldownAtaque)
            {
                Ataque();
            }
            cooldownTimer += Time.deltaTime;
        }
        
    }

    private void Ataque()
    {
        animatorEnemigo.SetTrigger("Ataque");
        cooldownTimer = 1.5f;
        var bala=Instantiate(proyectiles, firePoint.position, firePoint.rotation);
        if (this.gameObject.GetComponent<Enemigo>() != null)
        {
            bala.GetComponent<BalaEnemigo>().mirandoDerecha = this.gameObject.GetComponent<Enemigo>().mirandoDerecha;
        }
        else if (this.gameObject.GetComponent<Tamalero>() != null)
        {
            bala.GetComponent<BalaEnemigo>().mirandoDerecha = this.gameObject.GetComponent<Tamalero>().mirandoDerecha;
        }
        
    }
}
