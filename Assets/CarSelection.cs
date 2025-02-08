using UnityEngine;

public class CarSelection : MonoBehaviour
{
    public SpriteRenderer carSpriteRenderer;
    private int currentCarIndex = 0;

    private void Start()
    {
        UpdateCarSprite();
    }

    public void SelectCarLeft()
    {
        currentCarIndex--;
        if (currentCarIndex < 0) currentCarIndex = GameManager.instance.carSprites.Count - 1;
        UpdateCarSprite();
        GameManager.instance.SetSelectedCarIndex(currentCarIndex);
    }

    public void SelectCarRight()
    {
        currentCarIndex++;
        if (currentCarIndex >= GameManager.instance.carSprites.Count) currentCarIndex = 0;
        UpdateCarSprite();
        GameManager.instance.SetSelectedCarIndex(currentCarIndex);
    }

    private void UpdateCarSprite()
	{
    Debug.Log("UpdateCarSprite() called. Index: " + currentCarIndex); 

    if (GameManager.instance == null || GameManager.instance.carSprites == null || GameManager.instance.carSprites.Count == 0 || carSpriteRenderer == null)
    {
        Debug.LogError("Problèmes avec GameManager, la liste de sprites ou le SpriteRenderer dans CarSelection.");
        return;
    }

    carSpriteRenderer.sprite = GameManager.instance.carSprites[currentCarIndex];
	}
}