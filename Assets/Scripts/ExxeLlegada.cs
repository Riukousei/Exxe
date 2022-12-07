using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExxeLlegada : MonoBehaviour
{
    public BoxCollider2D BC;
    public CustomGameEvent response;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            response.Invoke(null,null);
            Debug.LogWarning("Llegaste al exxe");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
