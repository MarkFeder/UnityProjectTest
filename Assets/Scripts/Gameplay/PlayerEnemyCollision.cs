using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement();
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        player.Bounce(2);
                    }
                    else
                    {
                        player.Bounce(7);
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
            }
            else
            {
                // Skip damage if player is immune
                if (player.IsImmune)
                {
                    return;
                }

                var playerHealth = player.health;
                if (playerHealth != null)
                {
                    playerHealth.Decrement(enemy.damage);
                    if (!playerHealth.IsAlive)
                    {
                        Schedule<PlayerDeath>();
                    }
                    else
                    {
                        // Activate immunity after taking damage (but not dying)
                        player.ActivateImmunity();
                        // Play hurt audio feedback
                        if (player.audioSource && player.ouchAudio)
                            player.audioSource.PlayOneShot(player.ouchAudio);
                    }
                }
                else
                {
                    Schedule<PlayerDeath>();
                }
            }
        }
    }
}