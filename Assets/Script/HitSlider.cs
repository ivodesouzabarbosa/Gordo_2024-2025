using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitSlider : MonoBehaviour
{
    public Slider healthSlider;     // Refer�ncia ao Slider na HUD
    public float currentHealth;     // Vida atual
    public float maxHealth = 100f;  // Vida m�xima
    public float lerpSpeed = 5f;    // Velocidade de interpola��o
    public float invincibilityTime = 2f; // Tempo de espera entre danos

    private float targetHealth;     // Vida alvo ap�s o ataque
    private float lastDamageTime;   // Tempo do �ltimo dano recebido

    void Start()
    {
        ResetLife();
    }

    void Update()
    {
        // Gradualmente diminui a barra de vida at� atingir o valor alvo
        currentHealth = Mathf.Lerp(currentHealth, targetHealth, Time.deltaTime * lerpSpeed);

        // Atualiza o valor do Slider
        healthSlider.value = currentHealth;

        // Para a interpola��o quando o valor est� pr�ximo do alvo
        if (Mathf.Abs(currentHealth - targetHealth) < 0.01f)
        {
            currentHealth = targetHealth;
        }
    }

    // M�todo para simular dano ao levar um ataque
    public void TakeDamage(float damage)
    {
        // Verifica se o jogador est� invulner�vel
        if (Time.time - lastDamageTime >= invincibilityTime)
        {
            // Aplica o dano e atualiza o tempo do �ltimo dano
            targetHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            Debug.Log("dano");
            lastDamageTime = Time.time;
        }
    }

    // M�todo para curar (opcional)
    public void Heal(float amount)
    {
        targetHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void ResetLife()
    {
        // Configura o Slider para o intervalo desejado
        healthSlider.minValue = 0;
        healthSlider.maxValue = maxHealth;

        // Define valores iniciais
        currentHealth = maxHealth;
        targetHealth = maxHealth;
        healthSlider.value = currentHealth;

        // Inicializa o tempo do �ltimo dano
        lastDamageTime = -invincibilityTime; // Permite tomar dano imediatamente no in�cio
    }
}
