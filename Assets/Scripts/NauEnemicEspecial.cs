using System.Collections;
using UnityEngine;

public class NauEnemicEspecial : MonoBehaviour
{
    [Header("Moviment")]
    public float velocitatHoritzontal = 2f;
    public float velocitatVertical = 3f;          // Igual que l'enemic normal
    public float tempsMovimentHoritzontal = 3f;   // Segons abans de baixar

    [Header("Atac")]
    public GameObject projectilPrefab;
    public float cadenciaTret = 0.5f;

    [Header("Eliminació (heretat de NauEnemic)")]
    public GameObject _ExplosioPrefab;            // Assigna el prefab d'explosió
    private int puntsEnemic = 200;                // Punts que dona en morir

    private bool moureVertical = false;
    private float limitEsquerra = -8f;   // Ajusta als teus límits de pantalla
    private float limitDreta = 8f;

    void Start()
    {
        // Direcció horitzontal segons on ha aparegut
        Vector2 dirHoritz = (transform.position.x > 0) ? Vector2.left : Vector2.right;
        StartCoroutine(MoureHoritzontal(dirHoritz));

        // Dispara cada 'cadenciaTret' segons
        InvokeRepeating(nameof(Disparar), 0f, cadenciaTret);
    }

    // --------------- MOVIMENT ESPECIAL ---------------
    IEnumerator MoureHoritzontal(Vector2 direccio)
    {
        float t = 0f;
        while (t < tempsMovimentHoritzontal)
        {
            transform.Translate(direccio * velocitatHoritzontal * Time.deltaTime);

            // Si toca un lateral, deixa de moure's horitzontalment
            if (transform.position.x <= limitEsquerra || transform.position.x >= limitDreta)
                break;

            t += Time.deltaTime;
            yield return null;
        }
        moureVertical = true;
    }

    void Update()
    {
        if (moureVertical)
        {
            transform.Translate(Vector2.down * velocitatVertical * Time.deltaTime);

            // Destruir si surt per baix de la pantalla
            Vector2 minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            if (transform.position.y < minPantalla.y)
            {
                Destroy(gameObject);
            }
        }
    }

    void Disparar()
    {
        Instantiate(projectilPrefab, transform.position, Quaternion.identity);
        // Assegura't que el projectil tingui la direcció (0, -1)
        // Si el teu ProjectilEnemic no la té per defecte, afegeix aquí:
        // GameObject p = Instantiate(...);
        // p.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);  // per exemple
    }

    // --------------- ELIMINACIÓ (EXPLOSIÓ + PUNTS) ---------------
    private void OnTriggerEnter2D(Collider2D objecteTocat)
    {
        if (objecteTocat.CompareTag("ProjectilJugador") || objecteTocat.CompareTag("NauJugador"))
        {
            // Crear explosió al lloc de l'enemic
            GameObject explosio = Instantiate(_ExplosioPrefab);
            explosio.transform.position = transform.position;

            // Sumar punts (la mateixa quantitat que l'enemic normal)
            GameObject.Find("TextPunts").GetComponent<TextPuntsJugador>().setPuntsJugador(puntsEnemic);

            // Destruir l'enemic
            Destroy(gameObject);
        }
    }
}