using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/New Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [Header("General Settings")]
        [Tooltip("The display name of the enemy.")]
        [SerializeField] private string _name;

        [Tooltip("The base health of the enemy.")]
        [SerializeField] private int _health;

        [Tooltip("Movement speed of the enemy.")]
        [SerializeField] private float _speed;

        [Header("Combat Settings")]
        [Tooltip("The damage this enemy deals per attack.")]
        [SerializeField] private int _damage;

        [Tooltip("Cooldown time between attacks.")]
        [SerializeField] private float _attackCooldown;

        [Tooltip("The maximum distance at which this enemy can attack.")]
        [SerializeField] private float _attackDistance;

        [Tooltip("Types of attacks this enemy can perform.")]
        [SerializeField] private AttackType[] _attackType; // Melee, Range

        [Header("Behavior Settings")]
        [Tooltip("Behaviors/states this enemy can transition between.")]
        [SerializeField] private BehaviorType[] _startState; // Idle, Patrol
        [SerializeField] private BehaviorType[] _onVisionState; // Chase, Flee

        [Header("Prefab")]
        [Tooltip("The prefab used to instantiate this enemy in the scene.")]
        [SerializeField] private GameObject _prefab;
        
        [Header("Vision Settings")]
        [Tooltip("Detection radius for the enemy's vision system.")]
        [SerializeField] private float _visionRadius = 3f;
        

        // Public accessors
        public string Name => _name;
        public int Health => _health;
        public float Speed => _speed;
        public int Damage => _damage;
        public float AttackCooldown => _attackCooldown;
        public float AttackDistance => _attackDistance;
        public AttackType[] AttackTypes => _attackType;
        public float VisionRadius => _visionRadius;
        public BehaviorType[] StartState => _startState;
        public BehaviorType[] OnVisionState => _onVisionState;
        public GameObject Prefab => _prefab;
    }
}
