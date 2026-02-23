using UnityEngine;
using UnityEngine.Animations;

public class Skieur : MonoBehaviour
{
    void Start()
    {

    }

    // Utiliser ces fonctions pour activer et désactiver les InputActions
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }



    // Il faut appeller cette fonction dans la collision avec le yéti.
    void DeconnecterCamera()
    {
        Camera.main.GetComponent<PositionConstraint>().enabled = false;
    }
}
