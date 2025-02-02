using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class MenuController : MonoBehaviour
{
    private GameManager gameManager; 
    
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    
    public void ChangeScene(string _sceneName)
    {
        gameManager.ChangeScene(_sceneName);
    }
    
    public void Quit()
    {
        gameManager.Quit();
    }

}


