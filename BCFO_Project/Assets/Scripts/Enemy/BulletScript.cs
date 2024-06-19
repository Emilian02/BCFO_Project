using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float force;
    private GameObject player;
    private Rigidbody2D rb;
    public LayerMask players;
    float timer = 0;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 8);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler( 0, 0, rot);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().health -= 1;
        }

        Destroy(this.gameObject);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
