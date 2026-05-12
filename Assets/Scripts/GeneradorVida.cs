using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject _VidaPrefab;
    void Start()
    {
       IniciGeneraVida();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IniciGeneraVida()
    {
        // Param1: Nom mètode a cridar.
        // Param2: temps fins a cridar-se.
        // Param3: temps entre repeticions
        InvokeRepeating("CreaVida", 3f, 1f);
    }
    private void CreaVida()
    {
            GameObject vida = Instantiate(_VidaPrefab);
    
            Vector2 minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
            Vector2 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
    
            float posicioHoritzontalComponentX = Random.Range(minPantalla.x, maxPantalla.x);
    
            vida.transform.position = new Vector2(posicioHoritzontalComponentX, maxPantalla.y);
    }
}
