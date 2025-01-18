using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        //Core Stats
        [Header("Core Stats")]
        [SerializeField]
        private int maxHealth;
        [SerializeField]
        private int currentHealth;
        [SerializeField]
        private int maxStamina;
        [SerializeField]
        private int currentStamina;
        
        [Header("Stamina Recovery Settings")]
        [SerializeField]
        private float staminaRegenerationRate;
        [SerializeField] 
        private float staminaRegenerationDelay;
        [SerializeField]
        private float staminaRegenerationTimer;
        [SerializeField]
        private int effectiveMaxStamina;
        
        [Header("Combat Stats")]
        [SerializeField]
        private int attackPower;
        [SerializeField]
        private int defense;

        [Header("Survival Stats")] 
        [SerializeField]
        private float hunger;
        [SerializeField]
        private float thirst;
        
        public void InitStats()
        {
            currentHealth = maxHealth;
            currentStamina = maxStamina;
            UpdateEffectiveMaxStamina();
            
            InvokeRepeating(nameof(UpdateHungerAndThirsty), 1, 3);
        }
        
        void Update()
        {
            RegenerateStaminaOverTime();
        }

        private void TakeDamage(int damage)
        {
            int effectiveDamage = Mathf.Max(damage - defense, 0);
            currentHealth -= effectiveDamage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            
            //Debug.Log($"Player took {effectiveDamage} damage. Current health: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            
        }

        public void UseStamina(int amount)
        {
            currentStamina -= amount;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            
           // Debug.Log($"Player used: {amount} stamina. Current stamina: {currentStamina}");
        }

        private void RegenerateStaminaOverTime()
        {
            if (currentStamina < maxStamina)
            {
                staminaRegenerationTimer += Time.deltaTime;
                if (staminaRegenerationTimer >= staminaRegenerationDelay)
                {
                    currentStamina += Mathf.FloorToInt(staminaRegenerationRate * Time.deltaTime);
                    currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
                }
            }
        }

        private void UpdateEffectiveMaxStamina()
        {
            if (thirst < 40f)
            {
                effectiveMaxStamina = Mathf.FloorToInt(maxStamina * 0.6f);
            }
            else
            {
                effectiveMaxStamina = maxStamina;
            }

            if (currentStamina > effectiveMaxStamina)
            {
                currentStamina = effectiveMaxStamina;
            }
        }

        private void UpdateHungerAndThirsty()
        {
            hunger -= Time.deltaTime * 0.1f;
            thirst -= Time.deltaTime * 0.2f;
            
            hunger = Mathf.Clamp(hunger, 0, 100);
            thirst = Mathf.Clamp(thirst, 0, 100);

            if (hunger <= 0)
            {
                TakeDamage(1);
            }
            
            UpdateEffectiveMaxStamina();
        }

        private void Die()
        {
            //Debug.Log("Player has died!");
        }
    }
}
