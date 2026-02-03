using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A bullet fired by the player that damages enemies on contact.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        [Tooltip("Speed of the bullet")]
        public float speed = 10f;

        [Tooltip("Time in seconds before the bullet is destroyed")]
        public float lifetime = 3f;

        /// <summary>
        /// Amount of damage this bullet deals to enemies.
        /// Set by PlayerController when bullet is spawned.
        /// </summary>
        [HideInInspector]
        public int damage = 1;

        Vector2 direction;
        Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
        }

        void Start()
        {
            // Destroy bullet after lifetime expires
            Destroy(gameObject, lifetime);
        }

        /// <summary>
        /// Initialize the bullet with a direction.
        /// </summary>
        /// <param name="dir">Direction to travel (1 for right, -1 for left)</param>
        public void Initialize(float dir)
        {
            direction = new Vector2(dir, 0);
            rb.velocity = direction * speed;

            // Flip sprite if going left
            if (dir < 0)
            {
                var sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                    sr.flipX = true;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // Check if we hit an enemy
            var enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement(damage);
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                }

                // Destroy bullet on enemy hit
                Destroy(gameObject);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // Destroy bullet when hitting walls/ground (non-enemy collisions)
            var enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy == null)
            {
                Destroy(gameObject);
            }
        }
    }
}
