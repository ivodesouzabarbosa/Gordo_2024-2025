using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float _speed;
    public float _gravity = -9.81f;    // Intensidade da gravidade
    public float _jumpHeight = 1.5f;   // Altura do pulo
    public float _rotationSpeed = 10f; // Velocidade de rotação para os lados

    private bool _isGrounded;          // Checa se está no chão
    private bool _checkJump;          // Checa se apertou botão de pular

    [SerializeField] Vector3 _inputDir;
    [SerializeField] float _speedAnim;
    [SerializeField] Transform _playerObject;

    CharacterController controller;
    Rigidbody _rb;
    public Vector3 _posIniMenu;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        controller= GetComponent<CharacterController>();
        _posIniMenu=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Anim();
    }

    void Move()
    {
        // Verifica se está no chão
        _isGrounded = controller.isGrounded;

        if (_isGrounded && _inputDir.y < 0)
        {
            _inputDir.y = -2f;
        }

        Vector3 move = transform.right * _inputDir.x + transform.forward * (_inputDir.z/6) * _speed;

        controller.Move(move * _speed * Time.deltaTime);


        if (move.magnitude > 0.1f)
        {
            // Calcula a rotação para alinhar o objeto do jogador à direção do movimento
            Quaternion targetRotation = Quaternion.LookRotation(move.normalized);

            // Suaviza a transição para a nova rotação
            _playerObject.rotation = Quaternion.Slerp(_playerObject.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }


        // Pulo
        if (_checkJump && _isGrounded)
        {
            _checkJump = false;
            _inputDir.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        // Gravidade
        _inputDir.y += _gravity * Time.deltaTime;
        controller.Move(_inputDir * Time.deltaTime);
    }


    private void HandleRotation()
    {
        // Captura a entrada horizontal para rotação
        float rotateY = _inputDir.y;

        // Calcula o ângulo de rotação baseado na entrada
        float rotation = rotateY * _rotationSpeed * Time.deltaTime;

        // Aplica a rotação ao personagem no eixo Y
        transform.Rotate(0, rotation, 0);
    }

    void Anim()//controle de animações
    {
        _speedAnim = MathF.Abs((_inputDir.x + _inputDir.z) * _speed);
    }

    public void SetMove(InputAction.CallbackContext value)
    {
        _inputDir.x = value.ReadValue<Vector2>().x;
        _inputDir.z = value.ReadValue<Vector2>().y;
    }
    public void SetJump(InputAction.CallbackContext value)
    {
        _checkJump = true;
    }
}
