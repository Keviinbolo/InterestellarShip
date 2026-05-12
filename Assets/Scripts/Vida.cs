using UnityEngine;

public class Vida1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float _vel = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 novaPos = transform.position;

        Vector2 direccio = new Vector2(0f, -1f);

        novaPos = novaPos + direccio * _vel * Time.deltaTime;

        transform.position = novaPos;

        Vector2 minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < minPantalla.y)
        {
            //Debug.Log("Ha sortit fora.");
            // GameObject és l'objecte actual que té aquest script (com si fos un "this").
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D objecteTocat)
    {
        if ( objecteTocat.tag == "NauJugador")
        {
            Destroy(gameObject);
            

        }
    }

}
