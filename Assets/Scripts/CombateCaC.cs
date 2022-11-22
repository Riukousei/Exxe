using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float da�oGolpe;
    [SerializeField] private float CooldownAtaque;
    [SerializeField] private float cooldownTimer;
    private Animator animator;
    [SerializeField] private GameObject da�oArea;
    [SerializeField] private Transform golpearPiso;
    private void Start()
    {
        animator= GetComponent<Animator>();
    }
    private void Update()
    {
        if(CooldownAtaque > 0)
        {
            CooldownAtaque-=Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.X) && CooldownAtaque <=0)
        {
            Golpe();
            CooldownAtaque = cooldownTimer;
        }
            
        
    }

    private void Golpe()
    {
        animator.SetTrigger("Golpe");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach(Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDa�o(da�oGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

    private void GolpePiso()
    {
        Instantiate(da�oArea, golpearPiso.position, golpearPiso.rotation);
    }
}
