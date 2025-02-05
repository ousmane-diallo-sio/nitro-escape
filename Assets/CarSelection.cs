using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    public Image carImage; 
    public List<Sprite> carSprites; 
    private int currentCarIndex = 0; 

    private void Start()
    {
        UpdateCarImage(); 
    }

    public void SelectCarLeft()
    {
        currentCarIndex--; 
        if (currentCarIndex < 0) currentCarIndex = carSprites.Count - 1; 
        UpdateCarImage();
    }

    public void SelectCarRight()
    {
        currentCarIndex++; 
        if (currentCarIndex >= carSprites.Count) currentCarIndex = 0; 
        UpdateCarImage();
    }

    private void UpdateCarImage()
    {
        carImage.sprite = carSprites[currentCarIndex]; 
    }
}