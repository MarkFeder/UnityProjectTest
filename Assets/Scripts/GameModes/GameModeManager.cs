using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.GameModes
{
    /// <summary>
    /// Manages game mode configuration. Attach to the GameController object and
    /// assign a GameModeConfig asset to configure the game.
    /// </summary>
    public class GameModeManager : MonoBehaviour
    {
        [Tooltip("The game mode configuration to use. Create new configs via Assets > Create > Platformer > Game Mode Config")]
        public GameModeConfig gameModeConfig;

        PlatformerModel model;

        void Awake()
        {
            model = Simulation.GetModel<PlatformerModel>();
        }

        void Start()
        {
            ApplyGameModeConfig();
        }

        /// <summary>
        /// Applies the game mode configuration to all relevant game systems.
        /// </summary>
        public void ApplyGameModeConfig()
        {
            if (gameModeConfig == null)
            {
                Debug.LogWarning("GameModeManager: No game mode config assigned, using default values.");
                return;
            }

            // Apply player settings
            if (model.player != null)
            {
                var player = model.player;

                // Health settings
                if (player.health != null)
                {
                    player.health.maxHP = gameModeConfig.playerMaxHealth;
                }

                // Movement settings
                player.maxSpeed = gameModeConfig.playerSpeed;
                player.jumpTakeOffSpeed = gameModeConfig.playerJumpSpeed;
            }

            // Apply spawn position override
            if (gameModeConfig.useSpawnPositionOverride && model.spawnPoint != null)
            {
                model.spawnPoint.position = gameModeConfig.spawnPositionOverride;
            }

            // Apply global modifiers
            model.jumpModifier = gameModeConfig.jumpModifier;
            model.jumpDeceleration = gameModeConfig.jumpDeceleration;

            Debug.Log($"GameModeManager: Applied game mode '{gameModeConfig.name}'");
        }
    }
}
