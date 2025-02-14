using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    private CarSelection carSelection; 
    private SpriteRenderer spriteRenderer;

    public float moveSpeed = 5f;
    public float turnSpeed = 400f;
    public float nitroMultiplier = 2f;
    public float nitroConsumptionRate = 10f;
    public float currentNitro = 0f;
    public float maxNitro = 100f;
    public float currentHealth = 60f;
    public float maxHealth = 100f;

    public Slider nitroBar;
    public Slider healthBar;

    private Rigidbody2D rb;
    private Camera mainCamera;
    public Vector3 cameraOffset;

    public GameObject nitroEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        nitroBar.maxValue = maxNitro;
        nitroBar.value = currentNitro;

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();

        // Vérification de GameManager et de la liste carSprites
        if (GameManager.instance == null || GameManager.instance.carSprites == null || GameManager.instance.carSprites.Count == 0)
        {
            Debug.LogError("CarController: GameManager instance or carSprites list is missing!");
            return; // Important : Sortir de la fonction Start() en cas d'erreur
        }

        int selectedCarIndex = GameManager.instance.selectedCarIndex;
        Debug.Log("CarController: Selected car index = " + selectedCarIndex); // Pour le debug

        // Vérification de l'index et assignation du sprite
        if (selectedCarIndex >= 0 && selectedCarIndex < GameManager.instance.carSprites.Count)
        {
            spriteRenderer.sprite = GameManager.instance.carSprites[selectedCarIndex];
        }
        else
        {
            Debug.LogError("CarController: Selected car index is out of range: " + selectedCarIndex);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        float prevMove = rb.linearVelocity.magnitude;
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;

        if (prevMove == 0 && move != 0)
        {
            EventManager.Instance.CarMoved();
        }
        else if (prevMove != 0 && move == 0)
        {
            EventManager.Instance.CarStopped();
        }

        if (Input.GetKey(KeyCode.Space) && currentNitro > 0)
        {
            currentNitro -= nitroConsumptionRate * Time.deltaTime;
            if (!nitroEffect.activeSelf)
            {
                EventManager.Instance.NitroStateChanged(true);
                move *= nitroMultiplier; // Boost de vitesse
                nitroEffect.SetActive(true);
            }
        }
        else if (nitroEffect.activeSelf)
        {
            nitroEffect.SetActive(false);
            EventManager.Instance.NitroStateChanged(false);
        }

        rb.linearVelocity = transform.up * move;
        rb.angularVelocity = -turn;

        currentNitro = Mathf.Clamp(currentNitro, 0, maxNitro);
        nitroBar.value = currentNitro;
        healthBar.value = currentHealth;
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            Vector3 desiredPosition = transform.position + cameraOffset;
            desiredPosition.z = mainCamera.transform.position.z;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, 0.125f);
        }
    }

    public void AddNitro(float amount)
    {
        currentNitro = Mathf.Clamp(currentNitro + amount, 0, maxNitro);
    }

    public void AddHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void RemoveHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth == 0)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    void Shoot()
    {
        GameObject bullet = BulletPool.Instance.GetBullet();
        if (bullet != null)
        {
            EventManager.Instance.ShotFired();
            Vector3 offset = transform.up * 1f;
            bullet.transform.position = transform.position + offset;
            bullet.transform.rotation = transform.rotation;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            EventManager.Instance.CarCrashed();
            currentHealth -= 5f;
        }
    }
}