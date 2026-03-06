using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class Skieur : MonoBehaviour
{
    
    public float vitesse;

    public InputAction onDeplacementVertical;
    public InputAction onDeplacementHorizontal;

 
    public TMP_Text textePointage;

    
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

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (estMort == false && collision.gameObject.tag == "Danger")
        {
            
           
            estMort = true;
            
            Mourir();
            
            DeconnecterCamera();
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject.name);

       
        if (estMort == false && collision.gameObject.tag == "Bonhomme")
        {
            Bonhommes bonhomme = collision.GetComponent<Bonhommes>();

            bonhomme.Cacher();
            

            points += 1;
            textePointage.text = $"points : {points}";
        }


        if (estMort == false && collision.gameObject.tag == "Portail")
        {
            points += 1;
            textePointage.text = $"points : {points}";
        }

    }

    void Mourir()
    {
  
        rigid.constraints = RigidbodyConstraints2D.None;
      
        
       
        Invoke("RedemarrerScene", 3f);
    }


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