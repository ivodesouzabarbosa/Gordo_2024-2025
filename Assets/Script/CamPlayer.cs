using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CamPlayer : MonoBehaviour
{
    public string targetTag = "Player";
    private List<Transform> targets = new List<Transform>(); // Lista de
    Transform _player;
    private Vector3 _lastPosition;
    public GameControl _gameControl;

    public float transitionSpeed;

    public float rotationSpeed = 5f; // Velocidade da rota��o
    public float targetYRotation = 5f; // Rota��o alvo no eixo Y
    public Transform _cameraVirtual;

    private Coroutine rotationCoroutine; // Refer�ncia para a corrotina em execu��o



    // Start is called before the first frame update
    void Start() {
        _lastPosition = transform.position;
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        // Encontra todos os objetos com a tag especificada e os adiciona � lista
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject obj in targetObjects)
        {
            targets.Add(obj.transform);
        }
        _player = targets[0];
        RotateToY(targetYRotation);
    }

    private void Update()
    {
        _gameControl._playerCamT[0] = true;
        if (_gameControl._playerCamT[0] && _gameControl._playerCamT[1] && _gameControl._playerCamT[2] && _gameControl._playerCamT[3])
        {
            MoveCam();
        }
    }

    void MoveCam()
    {
        // Calcula a nova posi��o no eixo X com suaviza��o
        float newX = Mathf.Lerp(transform.position.x, _player.position.x, transitionSpeed * Time.deltaTime);

        // Atualiza a posi��o apenas no eixo X, mantendo Y e Z
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
        // Para qualquer rota��o em andamento
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }

        // Inicia a nova rota��o
        rotationCoroutine = StartCoroutine(RotateY(newYRotation));
    }

    private IEnumerator RotateY(float newYRotation)
    {
        // Obt�m o �ngulo atual no eixo Y
        float currentYRotation = _cameraVirtual.transform.eulerAngles.y;

        // Calcula o tempo total necess�rio para completar a rota��o
        float totalTime = Mathf.Abs(newYRotation - currentYRotation) / rotationSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            // Incrementa o tempo decorrido
            elapsedTime += Time.deltaTime;

            // Interpola o �ngulo no eixo Y
            float smoothedY = Mathf.LerpAngle(currentYRotation, newYRotation, elapsedTime / totalTime);

            // Atualiza a rota��o do objeto
            _cameraVirtual.transform.rotation = Quaternion.Euler(_cameraVirtual.transform.eulerAngles.x, smoothedY, 0f);

            yield return null; // Espera at� o pr�ximo quadro
        }

        // Garante que a rota��o final seja exatamente a desejada
        _cameraVirtual.transform.rotation = Quaternion.Euler(_cameraVirtual.transform.eulerAngles.x, newYRotation, 0f);
    }

}
