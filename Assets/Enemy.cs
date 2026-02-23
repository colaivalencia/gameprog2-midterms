using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Color[] possibleColors = { Color.red, Color.blue, Color.green, Color.yellow };

    private Color enemyColor;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyColor = possibleColors[Random.Range(0, possibleColors.Length)];
        spriteRenderer.color = enemyColor;
    }

    void Update()
    {
        Transform player = Player.Instance.transform;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public Color GetColor()
    {
        return enemyColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}