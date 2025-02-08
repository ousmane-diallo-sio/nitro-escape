using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance { private set; get; }
    public int selectedCarIndex { private set; get; }
    public List<Sprite> carSprites; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(this.gameObject);
            return; 
        }
    }

    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetSelectedCarIndex(int index)
    {
        selectedCarIndex = index;
        Debug.Log("GameManager: Voiture sélectionnée index " + index);
    }
}