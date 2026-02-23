using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jeu : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void RedemarrerScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
