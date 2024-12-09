using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public int _indexPerson;
    public int _indexSkin;
    public bool _checkSkin;
    public bool _personSeleck;
    public bool _selectTemp;
    public float _speed;
    public float _gravity = -9.81f;    // Intensidade da gravidade
    public float _jumpHeight = 1.5f;   // Altura do pulo
    public float _rotationSpeed = 10f; // Velocidade de rotação para os lados

    private bool _isGrounded;          // Checa se está no chão
    private bool _checkJump;          // Checa se apertou botão de pular

    [SerializeField] Vector3 _inputDir;
    [SerializeField] float _speedAnim;
    public List<Transform> _playerObject;

    CharacterController controller;
    Rigidbody _rb;
    public Vector3 _posIniMenu;
    public bool _selectPersonMove;
    public bool _personMoveCam;
    public SelectPerson _selectPerson;
    GameControl _gameControl;
    int _numberTrue;
    public Animator _anim;
    public bool _checkAnim;
   
    public int _nunbAtaque;
    public bool _checkAt=false;

  
    private float timer = 0f; // Temporizador
    private bool isTiming = false; // Controle do temporizador


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _posIniMenu=transform.position;
        if (_playerObject.Count > 1)
        {
            _checkSkin = true;
        }
        SelectSkin(_indexSkin);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (_checkAnim)
        {
            Anim();
        }

        if (isTiming)
        {
            timer -= Time.deltaTime; // Reduz o tempo restante
            if (timer <= 0f)
            {
                _checkAt = false; // Desativar a booleana
                isTiming = false; // Parar o temporizador
            }
        }

    }

    public void ActivateBoolForSeconds(float duration)
    {
        _checkAt = true; // Ativar a booleana
        timer = duration; // Definir o tempo
        isTiming = true; // Iniciar o temporizador
    }
    public void AtacFalse()
    {
        _checkAt = false;
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
            _playerObject[_indexSkin].rotation = Quaternion.Slerp(_playerObject[_indexSkin].rotation, targetRotation, _rotationSpeed * Time.deltaTime);
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
        _anim.SetFloat("move", _speedAnim);
        _anim.SetInteger("atack", _nunbAtaque);
        _anim.SetBool("checkAt", _checkAt);
    }

    public void SetMove(InputAction.CallbackContext value)
    {
        _inputDir.x = value.ReadValue<Vector2>().x;
        _inputDir.z = value.ReadValue<Vector2>().y;

    }


    public void SetAtack(InputAction.CallbackContext value)
    {
        Debug.Log("ataque");
        if (!_checkAt)
        {
            _checkAt = true;
            _nunbAtaque = UnityEngine.Random.Range(1, 4);
            ActivateBoolForSeconds(1.2f); // Ativar por 3 segundos
        }
    }
    public void SelectSkin(int value)
    {
        for (int i = 0; i < _playerObject.Count; i++)
        {
            _playerObject[i].gameObject.SetActive(false);
        }
        //Debug.Log("_indexSkin " + _indexSkin);
        _playerObject[_indexSkin].gameObject.SetActive(true);


    }
    void CheckInter()
    {
        for (int i = 0; i < _gameControl._playerMove.Count; i++)
        {
            if (_gameControl._playerMove[i]._checkSkin)
            {
                _numberTrue++;
               
                Debug.Log("_numberTrue "+_numberTrue+ " "+ _checkSkin);

            }
        }
        if (_numberTrue == 0)
        {
            _checkSkin = true;
        }
        else
        {
            _checkSkin = false;
        }
        _numberTrue = 0;
    }
    public void SkinU()
    {     
        
        _indexSkin++;
        if (_indexSkin >= _playerObject.Count-1)
        {
            _indexSkin= _playerObject.Count-1;
        }
      //  CheckInter();
        SelectSkin(_indexSkin);

    }
    public void SkinD()
    {
        _indexSkin--;
        if (_indexSkin < 0)
        {
            _indexSkin=0;
        }
    //    CheckInter();
        SelectSkin(_indexSkin);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Debug.Log("hit");
        }
    }
}
