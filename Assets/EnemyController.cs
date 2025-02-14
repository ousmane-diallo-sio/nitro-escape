using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;
    public float currentHealth = 60f;
    public float maxHealth = 100f;
    public float fireRate = 1.5f;
    private float nextFireTime = 0f;

    public float driftFactor = 0.2f;
    public float speedVariation = 0.30f;
    public float reactionTime = 0.05f;
    public float predictionFactor = 0.5f;
    public float interceptionChance = 0.3f;

    public Transform player;

    private void Update()
    {
        if (player == null) return;

        FollowPlayer();
        if (Time.time >= nextFireTime && IsAimingAtPlayer())
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

    }

    private void FollowPlayer()
    {
        if (player == null) return;

        // Predict player's future position for interception
        Vector3 targetPosition = player.position + (Vector3)player.GetComponent<Rigidbody2D>().linearVelocity * predictionFactor;

        // Calculate direction to target position
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        // Add slight drift randomness
        Vector3 drift = new Vector3(Random.Range(-driftFactor, driftFactor), Random.Range(-driftFactor, driftFactor), 0);
        direction += drift;
        direction.Normalize();

        // Randomly decide whether this enemy tries to cut you off or chase you
        if (Random.value < interceptionChance)  // Example: 30% chance to intercept
        {
            direction += (Vector3)player.GetComponent<Rigidbody2D>().linearVelocity.normalized * 0.5f;
            direction.Normalize();
        }

        // Apply inconsistent speed to make pursuit feel more dynamic
        float adjustedSpeed = speed * Random.Range(1 - speedVariation, 1 + speedVariation);

        // Smooth rotation to prevent instant snapping
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f), reactionTime);

        // Move forward
        transform.Translate(Vector3.up * adjustedSpeed * Time.deltaTime);
    }

    private bool IsAimingAtPlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.up, directionToPlayer);

        return dotProduct > 0.98f;
    }


    public void RemoveHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth == 0)
        {
            EventManager.Instance.PoliceCarDespawned();
            Destroy(gameObject);
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(pushDirection * 2f, ForceMode2D.Impulse);
        }
    }

}