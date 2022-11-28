using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePistolaPersonaje : MonoBehaviour
{
    [SerializeField] private float CooldownAtaque;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject proyectiles;
    [SerializeField] private float tiempoSinMoverse;
    private Animator animator;
    private MovimientoJugador MovimientoJugador;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        MovimientoJugador = GetComponent<MovimientoJugador>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && cooldownTimer > CooldownAtaque)
        {
            Ataque();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Ataque()
    {
        StartCoroutine(PerderControl());
        animator.SetTrigger("attack");
        cooldownTimer = 1.5f;
        Instantiate(proyectiles, firePoint.position, firePoint.rotation);
    }

    private IEnumerator PerderControl()
    {
        MovimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoSinMoverse);
        MovimientoJugador.sePuedeMover = true;
    }
}
