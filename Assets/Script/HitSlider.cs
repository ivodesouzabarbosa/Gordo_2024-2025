using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HitSlider : MonoBehaviour
{
    public Slider healthSlider;     // Refer�ncia ao Slider na HUD
    public float currentHealth;     // Vida atual
    public float maxHealth = 100f;  // Vida m�xima
    public float lerpSpeed = 5f;    // Velocidade de interpola��o
    public float invincibilityTime = 2f; // Tempo de espera entre danos

    private float targetHealth;     // Vida alvo ap�s o ataque
    private float lastDamageTime;   // Tempo do �ltimo dano recebido
    public EnemeyMove _enemeyMove;
 

    void Start()
    {
        healthSlider.transform.position = Vector3.zero;
        ResetLife();
     //   _enemeyMove=GetComponent<EnemeyMove>();
    }

    void Update()
    {
        if (!_enemeyMove._deathOn)
        {
            // Gradualmente diminui a barra de vida at� atingir o valor alvo
            currentHealth = Mathf.Lerp(currentHealth, targetHealth, Time.deltaTime * lerpSpeed);

            // Atualiza o valor do Slider
            healthSlider.value = currentHealth;

            // Para a interpola��o quando o valor est� pr�ximo do alvo
            if (Mathf.Abs(currentHealth - targetHealth) < 0.01f)
            {
                currentHealth = targetHealth;
                if (!_enemeyMove._deathOn && currentHealth <= 0)
                {
                    _enemeyMove._deathOn = true;//morte do inimigo
                    _enemeyMove._invuneravel = true;
                    Death();
                }
            }

        }
    }

    // M�todo para simular dano ao levar um ataque
    public void TakeDamage(float damage)
    {
        // Verifica se o jogador est� invulner�vel
        if (!_enemeyMove._invuneravel && !_enemeyMove._deathOn && Time.time - lastDamageTime >= invincibilityTime)
        {
            // Aplica o dano e atualiza o tempo do �ltimo dano
            targetHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            lastDamageTime = Time.time;
            _enemeyMove.AtivarPorTempo(.25f);
        }
       
    }

    // M�todo para curar (opcional)
    public void Heal(float amount)
    {
        targetHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void ResetLife()
    {
        StopCoroutine(DeathTime());
        // Configura o Slider para o intervalo desejado
        healthSlider.minValue = 0;
        healthSlider.maxValue = maxHealth;

        // Define valores iniciais
        currentHealth = maxHealth;
        targetHealth = maxHealth;
        healthSlider.value = currentHealth;

        // Inicializa o tempo do �ltimo dano
        lastDamageTime = -invincibilityTime; // Permite tomar dano imediatamente no in�cio
        _enemeyMove._invuneravel = false;
        _enemeyMove._deathOn = false;//morte do inimigo
        _enemeyMove.transform.localScale = Vector3.one;
    }

    public void Death()
    {
        StartCoroutine(DeathTime());
    }
    IEnumerator DeathTime()
    {
        healthSlider.transform.position = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            _enemeyMove.transform.DOScale(2f, .3f);
            yield return new WaitForSeconds(.3f);
            _enemeyMove.transform.DOScale(.5f, .3f);
        }
        _enemeyMove.transform.DOScale(2.5f, .3f);
        yield return new WaitForSeconds(.3f);
        _enemeyMove.gameObject.SetActive(false);

    }
}
