using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public PlayerControl _playerControl;
    public MaoMiliControl _maoMiliControl;

    public int _indexPerson;
    public int _indexSkin;
    public bool _checkSkin;
    public bool _personSeleck;
    public bool _selectTemp;
    public float _speed;
    public float _gravity = -9.81f;    // Intensidade da gravidade
    public float _jumpHeight = 1.5f;   // Altura do pulo
    public float _rotationSpeed = 10f; // Velocidade de rota��o para os lados

    private bool _isGrounded;          // Checa se est� no ch�o
    private bool _checkJump;          // Checa se apertou bot�o de pular

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

    public MeshRenderer _meshRenderer;

    public bool _deathOn;

    public bool luva;
    public GameObject luvasBox;

    public float velocidade = 2f; // Velocidade da transição
    private bool transicaoAtiva = false;
    private float t = 0f; // Tempo normalizado da interpolação
    public bool _maoOcupada;
    public bool _pegarMili;
    public bool _stopPerson;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerControl= GetComponent<PlayerControl>();
          _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        controller = GetComponent<CharacterController>();
      //  _anim = GetComponent<Animator>();
        _posIniMenu=transform.position;
        if (_playerObject.Count > 1)
        {
            _checkSkin = true;
        }
        SelectSkin(_indexSkin);

        if (_anim != null)
        {
           // _anim.SetLayerWeight(2, 0);
           // _anim.SetLayerWeight(3, 1);
           // luva = true;
          //  pesoLayer2 = _anim.GetLayerWeight(2);
          //  pesoLayer3 = _anim.GetLayerWeight(3);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LuvaOn();
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
                _selectPerson._sliderPLayers._staminaSystem.isUsingStamina = false;
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
        // Verifica se est� no ch�o
        _isGrounded = controller.isGrounded;

        if (_isGrounded && _inputDir.y < 0)
        {
            _inputDir.y = -2f;
        }
        if (!_stopPerson) {
           
        Vector3 move = transform.right * _inputDir.x + transform.forward * (_inputDir.z/6) * _speed;

        controller.Move(move * _speed * Time.deltaTime);

        
            if (move.magnitude > 0.1f)
            {
                // Calcula a rota��o para alinhar o objeto do jogador � dire��o do movimento
                Quaternion targetRotation = Quaternion.LookRotation(move.normalized);

                // Suaviza a transi��o para a nova rota��o
                _playerObject[_indexSkin].rotation = Quaternion.Slerp(_playerObject[_indexSkin].rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
        else { 
            _inputDir=new Vector3(0,0,0);
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
        // Captura a entrada horizontal para rota��o
        float rotateY = _inputDir.y;

        // Calcula o �ngulo de rota��o baseado na entrada
        float rotation = rotateY * _rotationSpeed * Time.deltaTime;

        // Aplica a rota��o ao personagem no eixo Y
        transform.Rotate(0, rotation, 0);
    }

    void Anim()//controle de anima��es
    {
        float x = MathF.Abs(_inputDir.x);
        float y = MathF.Abs(_inputDir.z);

        _speedAnim = x+y * _speed;
        if (_anim != null)
        {
            _anim.SetFloat("move", _speedAnim);
            _anim.SetInteger("atack", _nunbAtaque);
            _anim.SetBool("checkAt", _checkAt);
            _anim.SetBool("pegarMili", _pegarMili);
            _anim.SetBool("MiliM", _maoOcupada);
        }
 
    }

    public void SetMove(InputAction.CallbackContext value)
    {
        _inputDir.x = value.ReadValue<Vector2>().x;
        _inputDir.z = value.ReadValue<Vector2>().y*2;

    }

    public void SetLuva(InputAction.CallbackContext value)
    {
        if (!_maoOcupada)
        {
            IniciarTransicao();
        }
        

    }

    public void SetJogarObj(InputAction.CallbackContext value)
    {

       // Debug.Log("Jogar objeto 0");
        if (_maoOcupada && _maoMiliControl._objMili != null)
        {
         //   Debug.Log("Jogar objeto 1");
            ObjMili obj = _maoMiliControl._objMili;
            if (obj._naMao)
            {
                Debug.Log("Jogar objeto");
             //   _maoOcupada = false;
                obj._naMao = false;
                //_maoMiliControl._objMili.isLaunched = true;
                 _nunbAtaque = 4;
                //_playerControl._boxRaycast._transformOBj = null;
            }
           
        }
    }


    public void SetAtack(InputAction.CallbackContext value)
    {
      
        if (luva && !_maoOcupada)// da soco se estiver com luva
        {
            Debug.Log("atack");
            if (!_checkAt && !_selectPerson._sliderPLayers._staminaSystem.isUsingStamina && !_selectPerson._sliderPLayers._staminaSystem.isStaminaZero)
            {
                _checkAt = true;
                _selectPerson._sliderPLayers._staminaSystem.isUsingStamina = true;
                 _nunbAtaque = UnityEngine.Random.Range(1, 4);
                //_nunbAtaque = 2;
                ActivateBoolForSeconds(.1f); // Ativar por 3 segundos
            }
        }
        else if(!luva && !_maoOcupada)// pega objeto no chão se tiver sem luva
        {
            if (_playerControl._boxRaycast._transformOBj != null)
            {
                ObjMili obj = _playerControl._boxRaycast._transformOBj.GetComponent<ObjMili>();
                if (!obj._naMao)
                {
                    Debug.Log("pegaObj");
                    _pegarMili = true;
                    _maoOcupada = true;
                    luva=false;
                    obj._naMao = true;
                    _stopPerson = true;
                  //  IniciarTransicao();
                    // _playerControl._boxRaycast.ObjMove();
                }

            }
        }
        else 
        {
           Debug.Log("bater com objeto");
            if(_nunbAtaque==0)
            _nunbAtaque = UnityEngine.Random.Range(1, 3);
        }
    }

    public void SetJogarOBJ(InputAction.CallbackContext value)
    {

        if (!_checkAt && !_selectPerson._sliderPLayers._staminaSystem.isUsingStamina && !_selectPerson._sliderPLayers._staminaSystem.isStaminaZero)
        {
          //  _maoOcupada = false;
          //  _nunbAtaque = 4;
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

   void LuvaOn()
    {
        if (transicaoAtiva)
        {
            // Incrementa t baseado no tempo
            t += Time.deltaTime * velocidade;
            t = Mathf.Clamp01(t); // Mantém t entre 0 e 1

            // Aplica interpolação suave usando SmoothStep
            float pesoLayer2, pesoLayer3, pesoLayer4;
            float suaveT = Mathf.SmoothStep(0f, 1f, t); // Suaviza a transição


            if (luva && !_maoOcupada)
            {
                pesoLayer2 = Mathf.Lerp(1f, 0f, suaveT);
                pesoLayer3 = Mathf.Lerp(0f, 1f, suaveT);
                pesoLayer4 = Mathf.Lerp(1f, 0f, suaveT);
            }
            else if(!luva && !_maoOcupada)
            {
                pesoLayer2 = Mathf.Lerp(0f, 1f, suaveT);
                pesoLayer3 = Mathf.Lerp(1f, 0f, suaveT);
                pesoLayer4 = Mathf.Lerp(1f, 0f, suaveT);
            }
            else
            {
                pesoLayer2 = Mathf.Lerp(1f, 0f, suaveT);
                pesoLayer3 = Mathf.Lerp(1f, 0f, suaveT);
                pesoLayer4 = Mathf.Lerp(0f, 1f, suaveT);
            }

            // Aplica os valores ao Animator
            _anim.SetLayerWeight(2, pesoLayer2);
            _anim.SetLayerWeight(3, pesoLayer3);
            _anim.SetLayerWeight(4, pesoLayer4);

            // Se chegou ao final da transição, desativa
            if (t >= 1f)
            {
                transicaoAtiva = false;
              //  _pegarMili = false;
              //  _stopPerson=false;
            }
        }
    }
    public void IniciarTransicao()
    {
        if (!transicaoAtiva)
        {
            t = 0f; // Reinicia o tempo de interpolação
            transicaoAtiva = true;
            luva = !luva;
            luvasBox.SetActive(!luvasBox.activeInHierarchy);
        }
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==15)
        {
           
            other.transform.parent.GetComponent<HitSliderEnemy>()._hitSliderPlayer = _selectPerson._sliderPLayers;
            other.transform.parent.GetComponent<HitSliderEnemy>().TakeDamage(25);
            other.transform.parent.GetComponent<EnemeyMove>()._stopMove = true;
        }
    }
}
