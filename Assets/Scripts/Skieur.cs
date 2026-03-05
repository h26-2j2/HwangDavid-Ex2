using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class Skieur : MonoBehaviour
{
    //===== Variables publiques
    public float vitesse;

    public InputAction onDeplacementVertical;
    public InputAction onDeplacementHorizontal;

    //===== UI
    public TMP_Text textePointage;

    //===== Variables privées
    Rigidbody2D rigid;

    private Transform visuel;
    public float angleMax = 25f;

    float deplacementHor = 0;
    float deplacementVert = 0;

    public bool estMort = false;

    public int points = 0; 
    void Start()
    {
        points = 0;

        rigid = GetComponent<Rigidbody2D>();
        visuel = transform.Find("Visuel");
    }

    // Utiliser ces fonctions pour activer et désactiver les InputActions
    private void OnEnable()
    {
        onDeplacementHorizontal.Enable();
        onDeplacementVertical.Enable();
    }

    private void OnDisable()
    {
        onDeplacementHorizontal.Disable();
        onDeplacementVertical.Disable();
    }

    void Update()
    {
        if (estMort == false)
        {

            deplacementHor = onDeplacementHorizontal.ReadValue<float>();
            deplacementVert = onDeplacementVertical.ReadValue<float>();
        }
        else
        {
            deplacementHor = 0;
            deplacementVert = 0;
        }
        rigid.linearVelocity += new Vector2(
            deplacementHor * vitesse,
            deplacementVert * vitesse
            );

        if (Keyboard.current.aKey.isPressed)
            visuel.rotation = Quaternion.Euler(0f, 0f, -angleMax);
        else if (Keyboard.current.dKey.isPressed)
            visuel.rotation = Quaternion.Euler(0f, 0f, angleMax);
        else
            visuel.rotation = Quaternion.Euler(0f, 0f, 0f);

    }

    //Ajouter une méthode OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le tag du gameobject de la collision est "Obstacle" et que le joueur n'est pas mort
        if (estMort == false && collision.gameObject.tag == "Yeti")
        {
            //// Affecter true à estMort
            estMort = true;
            //// Déclencher la fonction Mourir
            Mourir();
            //// Déclencher la fonction DeconnecterCamera
            DeconnecterCamera();
        }

    }
    //Fin OnCollisionEnter2D

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // Si le tag du gameobject de la collision est "Etoile" et que le joueur n'est pas mort
    //    if (estMort == false && collision.gameObject.tag == "Bonhomme")
    //    {
    //        //// Accéder au script "Bonhommes"
    //        Bonhommes bonhomme = collision.GetComponent<Bonhommes>();
    //        //// Déclencher la fonction publique Cacher des Bonhommes
    //        bonhomme.Cacher();
    //        //// Ajouter des points
    //        //points += 1;
    //        //textePointage.text = $"points : {points}";
    //    }

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject.name);
        // Si le tag du gameobject de la collision est "Portail" et que le joueur n'est pas mort
        if (estMort == false && collision.gameObject.tag == "Portail")
        {
            
            points += 1;
            textePointage.text = $"points : { points }";
        }
        //// Mettre à jour le texte du UI

    }

    void Mourir()
    {
        // Enlever la contrainte de rotation
        rigid.constraints = RigidbodyConstraints2D.None;
        // Ajouter une vélocité de rotation
        rigid.angularVelocity = 100f;
        // Déclencher la fonction RedemarrerScene après 3sec
        Invoke("RedemarrerScene", 3f);
    }
    // Il faut appeller cette fonction dans la collision avec le yéti.

    void DeconnecterCamera()
    {
        Camera.main.GetComponent<PositionConstraint>().enabled = false;
    }
    void RedemarrerScene()
    {
        string nomSceneCourante = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomSceneCourante);
    }
}
