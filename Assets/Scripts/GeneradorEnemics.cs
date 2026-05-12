using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemics : MonoBehaviour
{
    /*
     * Crear múltiples objectes d'un:
     * 
     * 1) Convertim l'objecte a copiar en Prefab.
     * 2) Creem un objecte buit a l'escena.
     * 3) Creem un script i l'assignem a l'objecte buit.
     * 4) En l'objecte buit:
     *      - Creem un atribut de tipus GameObject i públic.
     *      - Des de Unity, arrosseguem el Prefab sobre el camp públic anterior (el 
     *              de tipus GameObject, que apareixerà a l'editor de Unity).
     *      - Creem un mètode i hi fem el Instantiate (en l'exemple, el mètode "CreaEnemic").
     *      - En el Start(), cridem el InvokeRepeating().
     */

    public GameObject _NauEnemicPrefab;

    // --- Enemic Especial (probabilitat) ---
    [Header("Enemic Especial")]
    public GameObject _NauEnemicEspecialPrefab;   // Prefab de la nau enemiga especial
    [Range(0f, 1f)] public float probabilitatEspecial = 0.2f; // 0.2 = 20% de probabilitat
    // ------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        IniciGeneraEnemics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciGeneraEnemics()
    {
        // Generem un enemic cada 1 segon, començant als 2 segons (enemics normals o especials)
        InvokeRepeating("CreaEnemic", 2f, 1f);
    }

    public void AturaGenerarEnemics()
    {
        CancelInvoke("CreaEnemic");
    }

    private void CreaEnemic()
    {
        // Decidim quin prefab utilitzar: normal o especial (si n'hi ha)
        GameObject prefabAEscollir = _NauEnemicPrefab;

        // Si tenim el prefab especial i la probabilitat es compleix, canviem al prefab especial
        if (_NauEnemicEspecialPrefab != null && Random.value < probabilitatEspecial)
        {
            prefabAEscollir = _NauEnemicEspecialPrefab;
        }

        GameObject nauEnemic = Instantiate(prefabAEscollir);

        // Anem a situar en una posició aleatòria (però a dalt) l'enemic creat.
        Vector2 minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
        Vector2 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));

        // Trobem posició x aleatoria entre el marge esquerra i dret de la pantalla.
        float posicioHoritzontalComponentX = Random.Range(minPantalla.x, maxPantalla.x);

        nauEnemic.transform.position = new Vector2(posicioHoritzontalComponentX, maxPantalla.y);
    }
}