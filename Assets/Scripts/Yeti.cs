using UnityEngine;
using UnityEngine.UIElements;

public class Yeti : MonoBehaviour
{
    private GameObject skieur;
    private Rigidbody2D rb;
    private Transform visuel;
    public float vitesse;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        visuel = transform.Find("Visuel");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            skieur = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            skieur = null;
        }
    }

    private void Update()
    {
        if (skieur != null)
        {
            Vector3 direction = -1 * (transform.position - skieur.transform.position);
            rb.linearVelocity = direction.normalized * vitesse;
        }
    }
}
