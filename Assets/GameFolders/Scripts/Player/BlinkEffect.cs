using RunnerGameInputAct.Combats;
using RunnerGameInputAct.Ui;
using System.Collections;
using UnityEngine;
namespace RunnerGameInputAct.Player
{
    public class BlinkEffect : MonoBehaviour
    {
        Health health;
        SkinnedMeshRenderer skinnedMeshRenderer;
        GameCanvas canvas;
        CapsuleCollider playerCollider;
        Rigidbody rb;
        bool isBlink = false;
        Color origColor;
        float flashSpeed = 0.15f;
        private static IEnumerator blinkCoroutine;

        private void Awake()
        {
            canvas = GetComponent<GameCanvas>();
            health = GetComponent<Health>();
            playerCollider = GetComponent<CapsuleCollider>();
            skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            rb = GetComponent<Rigidbody>();
            origColor = skinnedMeshRenderer.material.color;
        }
        private void Start()
        {
            health.OnHealthChanged += DamageFlash;

        }
        private void DamageFlash(int currentHealth)
        {

            blinkCoroutine = Flash();
            if (!isBlink)
            {
                StartCoroutine(blinkCoroutine);
            }
            else
            {
                health.Dead();
            }

        }

        public IEnumerator Flash()
        {
            isBlink = true;
            playerCollider.isTrigger = true;
            rb.useGravity = false;
            for (int i = 0; i < 12; i++)
            {
                skinnedMeshRenderer.material.color = Color.red;
                yield return new WaitForSeconds(flashSpeed);
                skinnedMeshRenderer.material.color = origColor;
                yield return new WaitForSeconds(flashSpeed);
                rb.useGravity = true;
                playerCollider.isTrigger = false;
            }
            isBlink = false;

        }



    }

}
