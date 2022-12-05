using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float CooldownAtaque;
    [SerializeField] private float cooldownTimer;
    private Animator animator;
    [SerializeField] private GameObject dañoArea;
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
                Debug.LogWarning(colisionador.transform.GetComponent<Enemigo>());
                if ((colisionador.transform.GetComponent<Enemigo>() != null)&&(!colisionador.isTrigger))
                {
                    colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
                }
                else if (colisionador.transform.GetComponent<Tamalero>() != null)
                {
                    colisionador.transform.GetComponent<Tamalero>().TomarDaño(dañoGolpe);
                }
                if (colisionador.transform.GetComponent<Moto>() != null)
                {
                    colisionador.transform.GetComponent<Moto>().TomarDaño(dañoGolpe);
                }

            }
            else if (colisionador.gameObject.tag == "NPC")
            {
                colisionador.gameObject.GetComponent<NPC>().TomarDaño(dañoGolpe);
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
        Instantiate(dañoArea, golpearPiso.position, golpearPiso.rotation);
    }
}
