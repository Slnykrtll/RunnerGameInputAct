using RunnerGameInputAct.Combats;
using RunnerGameInputAct.Controllers;
using RunnerGameInputAct.Manager;
using RunnerGameInputAct.Obstacle.Spawner;
using RunnerGameInputAct.Tile.Spawner;
using RunnerGameInputAct.Ui;
using System;
using UnityEngine;

namespace RunnerGameInputAct.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Range(1, 500)] private float jumpFactor = 315f;
        [SerializeField] private float DisGround = 0.8f;
        [SerializeField] private LayerMask GroundCheck;

        private Rigidbody playerRigidbody;
        private InputManager inputManager;
        private Animator animator;
        private CapsuleCollider playerCollider;
        //private GameCanvas gameCanvas;
        //ACoinSpawner coinSpawner;
        private Health health;
        private bool hasAnimator;
        private int xVelHash;
        private int yVelHash;
        private int zVelHash;
        private int jumpHash;
        private int groundHash;
        private int fallingHash;
        private int crouchHash;
        private bool grounded = false;
        private float horizontalSpeed = 4f;
        private float maxSpeed = 7f;
        private  float speedIncreaseRate = 0.01f;
        private Vector2 currentVelocity;//hýzýmýzýn temsili olan currentVelocitylerimiz.
        [SerializeField] float currentZVelocity = 5f;
        private float crouchSpeed;

        public void Start()
        {
            hasAnimator = TryGetComponent<Animator>(out animator); //nesnede olup olmadýðýný kontrol etmek için kullanýlýr
            playerRigidbody = GetComponent<Rigidbody>();
            inputManager = GetComponent<InputManager>();
            playerCollider = GetComponent<CapsuleCollider>();
            //coinSpawner = GetComponent<ACoinSpawner>();
            health = GetComponent<Health>();
            xVelHash = Animator.StringToHash("X_Velocity");
            yVelHash = Animator.StringToHash("Y_Velocity");
            zVelHash = Animator.StringToHash("Z_Velocity");
            jumpHash = Animator.StringToHash("Jump");
            groundHash = Animator.StringToHash("Grounded");
            fallingHash = Animator.StringToHash("Falling");
            crouchHash = Animator.StringToHash("Crouch");
            //_originalMass = _playerRigidbody.mass;
            HealthControl();


        }
        private void FixedUpdate()
        {
            SampleGround();
            Move();
            HandleJump();
            HandleCrouch();

            if (currentZVelocity < maxSpeed)
            {
                currentZVelocity += speedIncreaseRate * Time.fixedDeltaTime;
            }
            if (crouchSpeed < maxSpeed)
            {
                crouchSpeed += speedIncreaseRate * Time.fixedDeltaTime;
            }
  

        }
        public void Move()
        {
            if (!hasAnimator) return;
            currentVelocity.x = horizontalSpeed * inputManager.Move.x;
            if (inputManager.Crouch) crouchSpeed = 5f;
            var xVelDifference = currentVelocity.x - playerRigidbody.velocity.x;
            var zVelDifference = currentZVelocity - playerRigidbody.velocity.z;

            playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);

            animator.SetFloat(xVelHash, currentVelocity.x);
            animator.SetFloat(zVelHash, currentZVelocity);

        }
        private void HandleCrouch()
        {
            animator.SetBool(crouchHash, inputManager.Crouch);
            CrouchCollider();
        }
        private void CrouchCollider()
        {
            bool isCrouching = animator.GetBool(crouchHash);
            if (inputManager.Crouch && isCrouching)
            {
                playerCollider.height = 0.2f;
                playerCollider.center = new Vector3(0, 0.1f, 0);


            }
            else
            {
                playerCollider.height = 1.79f;
                playerCollider.center = new Vector3(0, 0.9f, 0);
            }

        }

        private void HandleJump()
        {
            if (!hasAnimator) return;
            if (!inputManager.Jump) return;
            if (!grounded) return;
            animator.SetTrigger(jumpHash);
        }
        public void JumpAddForce()
        {
            playerRigidbody.AddForce(-playerRigidbody.velocity.y * Vector3.up, ForceMode.VelocityChange);
            playerRigidbody.AddForce(Vector3.up * jumpFactor, ForceMode.Impulse);
            animator.ResetTrigger(jumpHash);

        }
        private void SampleGround()
        {
            if (!hasAnimator) return;
            RaycastHit hitInfo;
            if (Physics.Raycast(playerRigidbody.worldCenterOfMass, Vector3.down, out hitInfo, DisGround + 0.3f, GroundCheck))
            {
                grounded = true;
                //Debug.Log("on the ground");
                SetAnimationGrounding();
                return;
            }
            grounded = false;
            animator.SetFloat(yVelHash, playerRigidbody.velocity.y);
            SetAnimationGrounding();
            return;
        }
        private void SetAnimationGrounding()
        {
            animator.SetBool(fallingHash, !grounded);
            animator.SetBool(groundHash, grounded);

        }


        public void OnTriggerEnter(Collider spawnCollider)
        {//burada olmasa daha iyi

            if (spawnCollider.gameObject.CompareTag("PivotCollider"))
            {
                TileSpawner.instance.AddNewTile();

                //ACoinSpawner.instance.SpawnCoins(spawnCollider.transform.parent.GetComponentInChildren<ACoinPosition>());

            }
            if (spawnCollider.gameObject.CompareTag("ObstaclePivot"))
            {
                ObstacleSpawner.instance.InstantiateRandomObject();
               
            }



        }
        private void OnCollisionEnter(Collision collision)
        {
            Damage damage = collision.collider.GetComponent<Damage>();
            if (damage != null)
            {
                //health.TakeHit(damage);
                damage.HitTarget(health);
                return;
            }
        }
        public void HealthControl()
        {
            GameCanvas gameCanvas = FindObjectOfType<GameCanvas>();//bundan kurtul
            if (gameCanvas != null)
            {
                health.OnDead += gameCanvas.ShowGameOverPanel;
                health.OnDead += OnCharacterDead;
                
                DisplayHealth displayHealth = gameCanvas.GetComponentInChildren<DisplayHealth>();
                health.OnHealthChanged += displayHealth.WriteHealth;
                displayHealth.WriteHealth(health.MaxHealth);
                
            }
        }
        private void OnCharacterDead()
        {
            hasAnimator = false;
            speedIncreaseRate = 0f;
            currentZVelocity = 0f;
            currentVelocity = Vector2.zero;
            horizontalSpeed = 0f;
            crouchSpeed = 0f;
            jumpFactor = 0f;
            //crouchHash = 0; 

            //playerRigidbody.constraints = RigidbodyConstraints.None;
            animator.SetFloat(xVelHash, 0);
            animator.SetFloat(zVelHash, 0);
            animator.SetFloat(yVelHash, 0);
            //animator.SetBool(crouchHash , false); //????? -_-
            
        }



    }
}

