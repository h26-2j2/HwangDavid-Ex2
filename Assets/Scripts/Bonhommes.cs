using UnityEngine;

public class Bonhommes : MonoBehaviour
{
    
    public void Cacher()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

}