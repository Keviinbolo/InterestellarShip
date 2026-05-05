using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaResultats : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI puntsAconseguits;
    [SerializeField] private TMPro.TextMeshProUGUI videsRecollides;

    // Start is called before the first frame update
    void Start()
    {
        puntsAconseguits.text = ValorsGlobals.puntsAconseguits;
        videsRecollides.text = "Vides recollides: " + ValorsGlobals.videsRecollides.ToString();
    }

    public void TornarAlInici()
    {
        ValorsGlobals.ReiniciarTot();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
