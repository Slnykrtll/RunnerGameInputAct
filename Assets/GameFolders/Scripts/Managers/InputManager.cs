
//using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;//?

namespace RunnerGameInputAct.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;
        public Vector2 Move {  get; private set; } //ActionMap i�inde ActionPropertisini  value-Vector2 olarak ald�k.
        public bool Run {  get; private set; } //ActionMap i�inde ActionPropertisini button olarak ald�k.
        public bool Jump { get; private set; }
        public bool Crouch {  get; private set; }

        private InputActionMap currentMap;
        private InputAction moveAction;
        private InputAction runAction;
        private InputAction jumpAction;
        private InputAction crouchAction;
        
        private void Awake()
        {
            currentMap = PlayerInput.currentActionMap;//1.InputActions haritas�na ula��yoruz.
            moveAction = currentMap.FindAction("Move");
            runAction = currentMap.FindAction("Run");
            jumpAction = currentMap.FindAction("Jump");
            crouchAction = currentMap.FindAction("Crouch");
           

            //2.Geri arama eylemlerini Giri� eylemlerimize abone edece�iz.(performed tu�a bas�l� s�re = ger�ekle�tirilen)
            moveAction.performed += OnMove;
            runAction.performed += OnRun;
           jumpAction.performed += OnJump;
            crouchAction.performed += OnCrouch;
            //_crouchAction.started += onCrouch;

            //4.Geri arama eylemine son verdi�imizde de bir bildirime ihtiya� duyar�z.
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
        {//3.Her geri �a��rma i�levinin kendi geri �a��rma parametresi vard�r.
            //Her geri �a��rma i�levinde, ge�en ba�lam parametresinden temel bilgiler al�nmal�.
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

