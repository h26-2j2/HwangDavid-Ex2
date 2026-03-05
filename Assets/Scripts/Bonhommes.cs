using UnityEngine;

public class Bonhommes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Cacher()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

}