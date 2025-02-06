using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 400f;
    public float nitroMultiplier = 2f;
    public float nitroConsumptionRate = 30f;
    public float currentNitro = 0f;
    public float maxNitro = 100f;
    public float currentHealth = 60f;

    public float maxHealth = 100f;

    public Slider nitroBar;
    public Slider healthBar;

    private Rigidbody2D rb;

    private Camera mainCamera;
    public Vector3 cameraOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        nitroBar.maxValue = maxNitro;
        nitroBar.value = currentNitro;

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
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
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;

        bool isNitroActive = Input.GetKey(KeyCode.Space) && currentNitro > 0;
        if (isNitroActive)
        {
            move *= nitroMultiplier; // Speed boost
            currentNitro -= nitroConsumptionRate * Time.deltaTime;
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
        currentHealth = Mathf.Clamp(maxNitro + amount, 0, maxHealth);
    }


    void Shoot()
    {
        GameObject bullet = BulletPool.Instance.GetBullet();
        if (bullet != null)
        {
            Vector3 offset = transform.up * 1f;
            bullet.transform.position = transform.position + offset;
            bullet.transform.rotation = transform.rotation;
        }
    }

}