using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CamPlayer : MonoBehaviour
{
    public string targetTag = "Player";
    private Vector3 _lastPosition;
    public GameControl _gameControl;

    public float transitionSpeed;

    public float rotationSpeed = 5f; // Velocidade da rotação
    public float targetYRotation = 5f; // Rotação alvo no eixo Y
    public Transform _cameraVirtual;

    private Coroutine rotationCoroutine; // Referência para a corrotina em execução

    public List<PlayerMove> _playerMoves;



    // Start is called before the first frame update
    void Start() {
        _lastPosition = transform.position;
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        // Encontra todos os objetos com a tag especificada e os adiciona à lista
       

        RotateToY(targetYRotation);
    }

    private void Update()
    {
        _playerMoves[0]._personMoveCam = true;
        if (_gameControl._checkCamOn  && _playerMoves[1]._personMoveCam && _playerMoves[2]._personMoveCam && _playerMoves[3]._personMoveCam)
        {
            MoveCam();
        }
    }

    void MoveCam()
    {
        // Calcula a nova posição no eixo X com suavização
        float newX = Mathf.Lerp(transform.position.x, _gameControl._player.position.x, transitionSpeed * Time.deltaTime);

        // Atualiza a posição apenas no eixo X, mantendo Y e Z
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        

        if (!_gameControl._isDir && transform.position.x < _lastPosition.x)
        {
            transform.position = _lastPosition;
            //transform.position = Vector3.Lerp(transform.position, _lastPosition, transitionSpeed * Time.deltaTime);

        }
        if (_gameControl._isDir && transform.position.x > _lastPosition.x)
        {
            transform.position = _lastPosition;
            //transform.position = Vector3.Lerp(transform.position, _lastPosition, transitionSpeed * Time.deltaTime);

        }
        _lastPosition = transform.position;
        if (_gameControl._isDirIvert)
        {
            _gameControl._isDirIvert = false;
            targetYRotation *=-1;
            RotateToY(targetYRotation);
        }
    }


    public void RotateToY(float newYRotation)
    {
        // Para qualquer rotação em andamento
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }

        // Inicia a nova rotação
        rotationCoroutine = StartCoroutine(RotateY(newYRotation));
    }

    private IEnumerator RotateY(float newYRotation)
    {
        // Obtém o ângulo atual no eixo Y
        float currentYRotation = _cameraVirtual.transform.eulerAngles.y;

        // Calcula o tempo total necessário para completar a rotação
        float totalTime = Mathf.Abs(newYRotation - currentYRotation) / rotationSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            // Incrementa o tempo decorrido
            elapsedTime += Time.deltaTime;

            // Interpola o ângulo no eixo Y
            float smoothedY = Mathf.LerpAngle(currentYRotation, newYRotation, elapsedTime / totalTime);

            // Atualiza a rotação do objeto
            _cameraVirtual.transform.rotation = Quaternion.Euler(_cameraVirtual.transform.eulerAngles.x, smoothedY, 0f);

            yield return null; // Espera até o próximo quadro
        }

        // Garante que a rotação final seja exatamente a desejada
        _cameraVirtual.transform.rotation = Quaternion.Euler(_cameraVirtual.transform.eulerAngles.x, newYRotation, 0f);
    }

}
