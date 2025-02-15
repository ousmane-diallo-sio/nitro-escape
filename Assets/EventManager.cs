using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action<bool> OnNitroStateChanged;
    public event Action OnShotFired;
    public event Action OnCarMoved;
    public event Action OnCarStopped;
    public event Action OnCarCrashed;
    public event Action<int> OnVehicleChanged; 
    public event Action OnPoliceCarSpawned;
    public event Action OnPoliceCarDespawned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NitroStateChanged(bool isActive)
    {
        // Debug.Log("Nitro state changed: " + isActive);
        OnNitroStateChanged?.Invoke(isActive);
    }

    public void ShotFired()
    {
        // Debug.Log("Shot fired");
        OnShotFired?.Invoke();
    }

    public void CarMoved()
    {
        // Debug.Log("Car moved");
        OnCarMoved?.Invoke();
    }

    public void CarStopped()
    {
        // Debug.Log("Car stopped");
        OnCarStopped?.Invoke();
    }

    public void CarCrashed()
    {
        // Debug.Log("Car crashed");
        OnCarCrashed?.Invoke();
    }

    public void VehicleChanged(int carIndex) 
    {
        Debug.Log("Vehicle changed to index: " + carIndex);
        OnVehicleChanged?.Invoke(carIndex);
    }

    public void PoliceCarSpawned()
    {
        // Debug.Log("Police car spawned");
        OnPoliceCarSpawned?.Invoke();
    }

    public void PoliceCarDespawned()
    {
        // Debug.Log("Police car despawned");
        OnPoliceCarDespawned?.Invoke();
    }
}