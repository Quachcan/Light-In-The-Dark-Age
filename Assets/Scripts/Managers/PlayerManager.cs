using Player;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
    
    
        [Header("References Modules")]
        private PlayerMovement _playerMovement;
        private PlayerCombat _playerCombat;
        private PlayerStats _playerStats;
        private PlayerData _playerData;
        private Inventory _inventory;

        private bool _isAlive;

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            Instance = null;
        }

        private void Awake()
        {
            InitializeModules();
        }
        
        private void Update()
        {
            if (_isAlive)
            {
            
            }
        }

        private void InitializeModules()
        {
            if (_playerMovement == null)
                _playerMovement = GetComponent<PlayerMovement>();
            if (_playerCombat == null)    
                _playerCombat = GetComponent<PlayerCombat>();
            if (_playerStats == null)
                _playerStats = GetComponent<PlayerStats>();
            
            _playerStats.InitStats();
            _playerCombat.Initialize();
            //_playerMovement.Initialize();
        }

        public void OnPlayerDamaged(int damage)
        {
        
        }

        public void OnPlayerDeath()
        {
            _isAlive = false;
            _playerMovement.enabled = false;
            _playerCombat.enabled = false;
        }

        public void OnPlayerRespawn(Vector3 respawnPosition)
        {
            _isAlive = true;
            _playerMovement.enabled = true;
            _playerCombat.enabled = true;
            transform.position = respawnPosition;
        }

        public void AddItemToInventory(string item)
        {
            //_inventory.AddItem(item);
        }
    }
}
