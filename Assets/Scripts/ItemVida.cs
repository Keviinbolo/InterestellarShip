using UnityEngine;

public class ItemVida : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("NauJugador"))
        {
            
            ValorsGlobals.videsRecollides++;

            

            
            Destroy(gameObject);
        }
    }
}