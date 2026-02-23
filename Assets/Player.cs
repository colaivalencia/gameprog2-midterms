using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gun;
    public float fireRate = .5f;

    public float detectionRange = 5f;


    public Color[] availableColors = { Color.red, Color.blue, Color.green, Color.yellow };
    private int currentColorIndex = 0;

    private float fireCooldown = 0f;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer gunRenderer;

    public static Player Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunRenderer = gun.GetComponent<SpriteRenderer>();
        
        spriteRenderer.color = availableColors[currentColorIndex];
        gunRenderer.color = availableColors[currentColorIndex];
    }


    void Update()
    {
        HandleClick();
        TrackEnemy();

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, transform.rotation);
        Bullet b = bullet.GetComponent<Bullet>();
        b.SetColor(availableColors[currentColorIndex]);
    }

    void HandleClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.gameObject == gameObject)
            {
                currentColorIndex = (currentColorIndex + 1) % availableColors.Length;
                spriteRenderer.color = availableColors[currentColorIndex];
                gunRenderer.color = availableColors[currentColorIndex];
            }
        }
    }

    void TrackEnemy()
    {
        Enemy nearest = FindTarget();
        if (nearest != null)
        {
            Vector2 direction = (nearest.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    Enemy FindTarget()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Enemy nearest = null;
        float closestDistance = detectionRange;

        foreach (Enemy e in enemies)
        {
            float dist = Vector2.Distance(transform.position, e.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                nearest = e;
            }
        }

        return nearest;
    }

}