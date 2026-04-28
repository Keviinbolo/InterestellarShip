using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaResultats : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI puntsAconseguits;

    // Start is called before the first frame update
    void Start()
    {
        puntsAconseguits.text = ValorsGlobals.puntsAconseguits;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
