using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePistolaPersonaje : MonoBehaviour
{
    [SerializeField] private float CooldownAtaque;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject proyectiles;
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
            Ataque();
        cooldownTimer += Time.deltaTime;
    }

    private void Ataque()
    {
        animator.SetTrigger("attack");
        cooldownTimer = 1.5f;
        Instantiate(proyectiles, firePoint.position, firePoint.rotation);

        //proyectiles[EncontrarProyectil()].transform.position = firePoint.position;
        //proyectiles[EncontrarProyectil()].GetComponent<Bala>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    /*private int EncontrarProyectil()
    {
        for (int i = 0; i < proyectiles.Length; i++)
        {
            if (!proyectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }*/
}
