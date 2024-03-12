
//using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;//?

namespace RunnerGameInputAct.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;
        public Vector2 Move {  get; private set; } //ActionMap içinde ActionPropertisini  value-Vector2 olarak aldýk.
        public bool Run {  get; private set; } //ActionMap içinde ActionPropertisini button olarak aldýk.
        public bool Jump { get; private set; }
        public bool Crouch {  get; private set; }

        private InputActionMap currentMap;
        private InputAction moveAction;
        private InputAction runAction;
        private InputAction jumpAction;
        private InputAction crouchAction;
        
        private void Awake()
        {
            currentMap = PlayerInput.currentActionMap;//1.InputActions haritasýna ulaþýyoruz.
            moveAction = currentMap.FindAction("Move");
            runAction = currentMap.FindAction("Run");
            jumpAction = currentMap.FindAction("Jump");
            crouchAction = currentMap.FindAction("Crouch");
           

            //2.Geri arama eylemlerini Giriþ eylemlerimize abone edeceðiz.(performed tuþa basýlý süre = gerçekleþtirilen)
            moveAction.performed += OnMove;
            runAction.performed += OnRun;
           jumpAction.performed += OnJump;
            crouchAction.performed += OnCrouch;
            //_crouchAction.started += onCrouch;

            //4.Geri arama eylemine son verdiðimizde de bir bildirime ihtiyaç duyarýz.
            moveAction.canceled += OnMove;
            runAction.canceled += OnRun;
            jumpAction.canceled += OnJump;
            crouchAction.canceled += OnCrouch;
            //_crouchAction.performed -= onCrouch;

        }

        private void OnCrouch(InputAction.CallbackContext context)
        {
            Crouch= context.ReadValueAsButton();
        }

        private void OnMove(InputAction.CallbackContext context)
        {//3.Her geri çaðýrma iþlevinin kendi geri çaðýrma parametresi vardýr.
            //Her geri çaðýrma iþlevinde, geçen baðlam parametresinden temel bilgiler alýnmalý.
            Move = context.ReadValue<Vector2>();
        }
        private void OnRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();

        }
        private void OnJump(InputAction.CallbackContext context)
        {
            Jump = context.ReadValueAsButton();
           
        }

        private void OnEnable()
        {
            currentMap.Enable();
        }
        private void OnDisable()
        {
            currentMap.Disable();
        }

    }
}

