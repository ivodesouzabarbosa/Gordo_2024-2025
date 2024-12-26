using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public Slider staminaSlider; // Refer�ncia ao Slider
    public float maxStamina = 100f; // Valor m�ximo de stamina
    public float currentStamina; // Valor atual de stamina
    public float staminaUsageRate = 10f; // Taxa de uso da stamina por segundo
    public float staminaRecoveryRate = 5f; // Taxa de recupera��o de stamina por segundo
    public bool isUsingStamina = false; // Indica se o jogador est� consumindo stamina
    public bool isStaminaZero; // Indica se o jogador est� consumindo stamina

    public float delayInSeconds = 1f; // Tempo de atraso
    private float timer = 0f;
    private bool functionCalled = false;

    private void Start()
    {
        currentStamina = maxStamina; // Inicializa a stamina no valor m�ximo
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
    }

    private void Update()
    {
        if (isUsingStamina && currentStamina > 0)
        {
            UseStamina(); // Consome stamina
            if (currentStamina <= 0)
            {
                isStaminaZero=true;
                functionCalled = true;
            }

        }
        else if (!functionCalled && !isUsingStamina && currentStamina < maxStamina)
        {
            RecoverStamina(); // Recupera stamina
            if(currentStamina == maxStamina)
            {
                isStaminaZero = false;
            }
        }

        staminaSlider.value = currentStamina; // Atualiza o Slider
        TimeZeroSt();
    }

    public void UseStamina()
    {
        currentStamina -= staminaUsageRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // Garante que n�o passe de 0
    }

    public void RecoverStamina()
    {
        currentStamina += staminaRecoveryRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // Garante que n�o passe do m�ximo
    }

    void TimeZeroSt()
    {
        if (functionCalled)
        {
            timer += Time.deltaTime;
            currentStamina = 0;
            if (timer >= delayInSeconds)
            {
                timer = 0;
                functionCalled = false; 
            }
        }
    }
}
