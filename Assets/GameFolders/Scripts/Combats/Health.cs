//using Cinemachine.Utility;
//using RunnerGameInputAct.Ui;
//using System.Collections;
//using System.Collections.Generic;
using RunnerGameInputAct.Player;
using UnityEngine;
namespace RunnerGameInputAct.Combats
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 3;
        [SerializeField] static int currentHealth = 0;
        public int MaxHealth => maxHealth;
        public bool IsDead => currentHealth < 1;
        public event System.Action OnDead;
        public event System.Action<int> OnHealthChanged;

        PlayerController playerController;

        private void Awake()
        {
            currentHealth = maxHealth;
            playerController = GetComponent<PlayerController>();
        }
        public void Dead()
        {//health bilgisinin güncellenmesini saðlamalýsýn
            currentHealth = 0;

            OnDead?.Invoke();


        }

        public void TakeHit(Damage damage)
        {
            if (IsDead) return;
            currentHealth -= damage.HitDamage;

            if (IsDead)
            {
                OnHealthChanged?.Invoke(currentHealth);

                OnDead?.Invoke();

            }
            else
            {
                OnHealthChanged?.Invoke(currentHealth);
            }



        }
    }

}
