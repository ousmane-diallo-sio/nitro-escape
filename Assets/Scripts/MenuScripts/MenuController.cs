using UnityEngine;

public class MenuController : MonoBehaviour
{
    private GameManager gameManager;
    public CarSelection carSelection;

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

    public void OnSelectCarLeft()
    {
        carSelection.SelectCarLeft();
    }

    public void OnSelectCarRight()
    {
        carSelection.SelectCarRight();
    }
}