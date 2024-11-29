using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickPlayerMonitor : MonoBehaviour
{
    private string[] previousJoysticks;
    GameControl _gameControl;


    void Start()
    {
        // Inicializa com os dispositivos conectados no início
        _gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        // Obtém os joysticks conectados inicialmente
        previousJoysticks = GetConnectedJoysticks();
       // LogJoystickStatus();
    }

    void Update()
    {
        // Verifica a cada frame se houve alteração nos dispositivos conectados
        string[] currentJoysticks = GetConnectedJoysticks();

        if (!AreArraysEqual(previousJoysticks, currentJoysticks))
        {
            DetectDisconnectedJoystick(previousJoysticks, currentJoysticks);
            previousJoysticks = currentJoysticks;
        }
    }

    string[] GetConnectedJoysticks()
    {
        // Usando o novo InputSystem para pegar todos os gamepads conectados
        var gamepads = Gamepad.all;
        string[] joystickNames = new string[gamepads.Count];

        for (int i = 0; i < gamepads.Count; i++)
        {
            joystickNames[i] = gamepads[i].displayName;  // Armazena o nome do joystick
        }

        return joystickNames;
    }

    void DetectDisconnectedJoystick(string[] oldJoysticks, string[] newJoysticks)
    {
        // Verifica se algum joystick foi desconectado
        for (int i = 0; i < oldJoysticks.Length; i++)
        {
            if (i >= newJoysticks.Length || string.IsNullOrEmpty(newJoysticks[i]))
            {
                Debug.Log($"Joystick desconectado: Player {i + 1}");
                _gameControl._numberPlayer--;
                
                //Debug.Log[i];
                if (_gameControl._multiPlayerControl._selectsPersonList[i] != null) { }
                {
                    SelectPerson menuPlayer = _gameControl._multiPlayerControl._selectsPersonList[i];
                   // menuPlayer._pPlayer.GetComponent<InptPLayerControll>()._imgDesc.enabled = true;
                    //  menuPlayer.localScale = Vector3.zero;
                    // menuPlayer.SetParent(_gameControl._multiPlayerControl._PlayerGame);
                    // menuPlayer._desconectImg.SetActive(true);
                }
            }
        }

        // Verifica se algum joystick foi conectado
        for (int i = oldJoysticks.Length; i < newJoysticks.Length; i++)
        {
            if (!string.IsNullOrEmpty(newJoysticks[i]))
            {
                Debug.Log($"Joystick conectado: Player {i + 1} - {newJoysticks[i]}");
                _gameControl._numberPlayer++;
                bool CheckGameStart = _gameControl._gameStart;
                

                if (!CheckGameStart && _gameControl._multiPlayerControl._selectsPersonList[i] != null)
                {
                    // menuPlayer.localScale = new Vector3(2, 2, 2);
                    SelectPerson menuPlayer = _gameControl._multiPlayerControl._selectsPersonList[i];
                  //  menuPlayer._pPlayer.GetComponent<InptPLayerControll>()._imgDesc.enabled = false;
                   // menuPlayer._desconectImg.SetActive(false);
                    // menuPlayer.SetSiblingIndex(_gameControl._multiPlayerControl._selectsPersonList[i]._indexPlayer);
                }
                    
            }
        }
    }

    void LogJoystickStatus()
    {
        string[] joysticks = GetConnectedJoysticks();
        for (int i = 0; i < joysticks.Length; i++)
        {
            if (!string.IsNullOrEmpty(joysticks[i]))
            {
                Debug.Log($"Joystick conectado: Player {i + 1} - {joysticks[i]}");
               _gameControl._numberPlayer++;

            }
        }
    }

    bool AreArraysEqual(string[] array1, string[] array2)
    {
        if (array1.Length != array2.Length)
            return false;

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }

        return true;
    }
}

