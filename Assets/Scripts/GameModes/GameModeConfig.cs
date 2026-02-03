using UnityEngine;

namespace Platformer.GameModes
{
    /// <summary>
    /// ScriptableObject that defines a game mode configuration.
    /// Designers can create new game modes via Assets > Create > Platformer > Game Mode Config.
    /// </summary>
    [CreateAssetMenu(fileName = "NewGameMode", menuName = "Platformer/Game Mode Config", order = 1)]
    public class GameModeConfig : ScriptableObject
    {
        [Header("Player Settings")]
        [Tooltip("Maximum health points for the player")]
        public int playerMaxHealth = 3;

        [Tooltip("Duration of immunity after taking damage (in seconds)")]
        public float playerImmunityDuration = 1f;

        [Tooltip("Player movement speed")]
        public float playerSpeed = 7f;

        [Tooltip("Player jump velocity")]
        public float playerJumpSpeed = 7f;

        [Header("Spawn Settings")]
        [Tooltip("Override the default spawn point position. If null, uses scene's spawn point.")]
        public Vector3 spawnPositionOverride;

        [Tooltip("Enable to use the spawn position override")]
        public bool useSpawnPositionOverride = false;

        [Header("Weapon Settings")]
        [Tooltip("Damage dealt by player bullets")]
        public int bulletDamage = 1;

        [Header("Game Modifiers")]
        [Tooltip("Global jump height modifier")]
        public float jumpModifier = 1.5f;

        [Tooltip("Jump deceleration when releasing jump button")]
        public float jumpDeceleration = 0.5f;
    }
}
