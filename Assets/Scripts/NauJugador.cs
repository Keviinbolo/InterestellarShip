using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NauJugador : MonoBehaviour
{
    private float _vel;

    public GameObject _ExplosioPrefab;

   

    public int VidaMaxima = 3;
    private int _vidaActual;
    public Image[] VidaImagen;


    // Start is called before the first frame update
    void Start()
    {
        _vel = 8f;
        _vidaActual = VidaMaxima;
        actualizarInterfaz();
    }

    // Update is called once per frame
    void Update()
    {
        float direccioInputX = Input.GetAxisRaw("Horizontal");
        float direccioInputY = Input.GetAxisRaw("Vertical");
        //Debug.Log(direccioInputX + " - " + direccioInputY);

        Vector2 direccioIndicada = new Vector2(direccioInputX, direccioInputY).normalized;
        //Debug.Log(direccioIndicada + " magnitud=" + direccioIndicada.magnitude);

        MoureNau(direccioIndicada);

    }

    void MoureNau(Vector2 direccioIndicada)
    {
        // Anem a moure la nau:
        // 1) Agafem la posició actual (x, y) de la nau:
        //      transform.position ens retorna la posició actual de la nau.
        Vector2 posNau = transform.position;

        // 2) Trobem la nova posició de la nau:
        posNau = posNau + direccioIndicada * _vel * Time.deltaTime;
        //Debug.Log("Time.deltaTime=" + Time.deltaTime);

        Vector2 minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        maxPantalla.x = maxPantalla.x - 0.6f;
        minPantalla.x = minPantalla.x + 0.6f;
        maxPantalla.y = maxPantalla.y - 0.8f;
        minPantalla.y = minPantalla.y + 0.8f;

        posNau.x = Mathf.Clamp(posNau.x, minPantalla.x, maxPantalla.x);
        posNau.y = Mathf.Clamp(posNau.y, minPantalla.y, maxPantalla.y);

        // 3) Assignem la nova posició (movem l'objecte):
        transform.position = posNau;
    }

    void actualizarInterfaz()
    {
        for (int i = 0; i < VidaImagen.Length; i++)
        {
            VidaImagen[i].enabled = i < _vidaActual;
        }
        if (_vidaActual <= 0)
        {
            GameObject explosio = Instantiate(_ExplosioPrefab);
            explosio.transform.position = transform.position;
            SceneManager.LoadScene("EscenaResultats");
            
        }
    }


    void RecibirDanyo(int danyo)
    {
        _vidaActual -= danyo;
        //_vidaActual = Mathf.Clamp(_vidaActual, 0, VidaMaxima);
        actualizarInterfaz();
    }
    void ObtenerVida(int vida)
    {
        _vidaActual += vida;
       // _vidaActual = Mathf.Clamp(_vidaActual, 0, VidaMaxima);
        actualizarInterfaz();
    }
    private void OnTriggerEnter2D(Collider2D objecteTocat)
    {
        if (objecteTocat.tag == "Enemic" || objecteTocat.tag == "ProjectilEnemic")
        {

            RecibirDanyo(1);

            // Gestió de vides jugador i canvi d'escena.

            // No cal destruir la nau del jugador si es canvia l'escena.
            //Destroy(gameObject);

        } 
        if (objecteTocat.tag == "Vida")
        {
            ObtenerVida(1);
            Destroy(objecteTocat.gameObject);
        }
    }
}
