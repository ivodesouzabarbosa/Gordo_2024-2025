using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitSlider : MonoBehaviour
{
    public Slider healthSlider;     // Referência ao Slider na HUD
    public float currentHealth;     // Vida atual
    public float maxHealth = 100f;  // Vida máxima
    public float lerpSpeed = 5f;    // Velocidade de interpolação
    public float invincibilityTime = 2f; // Tempo de espera entre danos

    private float targetHealth;     // Vida alvo após o ataque
    private float lastDamageTime;   // Tempo do último dano recebido

    void Start()
    {
        ResetLife();
    }

    void Update()
    {
        // Gradualmente diminui a barra de vida até atingir o valor alvo
        currentHealth = Mathf.Lerp(currentHealth, targetHealth, Time.deltaTime * lerpSpeed);

        // Atualiza o valor do Slider
        healthSlider.value = currentHealth;

        // Para a interpolação quando o valor está próximo do alvo
        if (Mathf.Abs(currentHealth - targetHealth) < 0.01f)
        {
            currentHealth = targetHealth;
        }
    }

    // Método para simular dano ao levar um ataque
    public void TakeDamage(float damage)
    {
        // Verifica se o jogador está invulnerável
        if (Time.time - lastDamageTime >= invincibilityTime)
        {
            // Aplica o dano e atualiza o tempo do último dano
            targetHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            Debug.Log("dano");
            lastDamageTime = Time.time;
        }
    }

    // Método para curar (opcional)
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

        // Inicializa o tempo do último dano
        lastDamageTime = -invincibilityTime; // Permite tomar dano imediatamente no início
    }
}
