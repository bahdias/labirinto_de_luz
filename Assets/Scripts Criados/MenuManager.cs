using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Jogo");
        
    }

    public void Sair()
    {
        Application.Quit();
        
    }
}
