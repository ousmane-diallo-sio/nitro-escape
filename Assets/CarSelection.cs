using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    public SpriteRenderer carSpriteRenderer;
    public List<Sprite> carSprites;
    private int currentCarIndex = 0;

    private void Start()
    {
        UpdateCarSprite();
    }

    public void SelectCarLeft()
    {
        currentCarIndex--;
        if (currentCarIndex < 0) currentCarIndex = carSprites.Count - 1;
        UpdateCarSprite();
        EventManager.Instance.VehicleChanged(currentCarIndex);
    }

    public void SelectCarRight()
    {
        currentCarIndex++;
        if (currentCarIndex >= carSprites.Count) currentCarIndex = 0;
        UpdateCarSprite();
        EventManager.Instance.VehicleChanged(currentCarIndex);
    }

    private void UpdateCarSprite()
    {
        if (carSprites != null && carSprites.Count > 0 && carSpriteRenderer != null)
        {
            Debug.Log("Mise à jour du sprite de la voiture à l'index : " + currentCarIndex);
            carSpriteRenderer.sprite = carSprites[currentCarIndex];
        }
        else
        {
            Debug.LogError("La liste des sprites de voiture est vide ou le SpriteRenderer n'est pas assigné.");
        }
    }

    public int GetCurrentCarIndex()
    {
        return currentCarIndex;
    }
}