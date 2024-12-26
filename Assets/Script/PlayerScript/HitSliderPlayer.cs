using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HitSliderPlayer : MonoBehaviour
{
    public Slider healthSlider;     // Referência ao Slider na HUD
    public Vector3 scaleS;
    public float currentHealth;     // Vida atual
    public float maxHealth = 100f;  // Vida máxima
    public float lerpSpeed = 5f;    // Velocidade de interpolação
    public float invincibilityTime; // Tempo de espera entre danos

    private float targetHealth;     // Vida alvo após o ataque
    private float lastDamageTime;   // Tempo do último dano recebido
    public CapsHit _capsHit;
    public SliderPLayer _hitSliderPlayer;

    public ParticleSystem _particleSystemHit;
    SliderPLayer _sliderPLayer;

    void Start()
    {
        _sliderPLayer = GetComponent<SliderPLayer>();
        scaleS = healthSlider.transform.localScale;
        healthSlider.transform.localScale = Vector3.zero;
       // _playerMove=GetComponent<PlayerMove>();
       // Invoke("TimeStart", 1);
        ResetLife();
     //   _enemeyMove=GetComponent<EnemeyMove>();
    }
    void TimeStart()
    {
        healthSlider.transform.localScale = scaleS;
    }

    void Update()
    {
        if (_sliderPLayer._playerMove != null)
        {
            if (!_sliderPLayer._playerMove._deathOn)
            {
                // Gradualmente diminui a barra de vida até atingir o valor alvo
                currentHealth = Mathf.Lerp(currentHealth, targetHealth, Time.deltaTime * lerpSpeed);

                // Atualiza o valor do Slider
                healthSlider.value = currentHealth;

                // Para a interpolação quando o valor está próximo do alvo
                if (Mathf.Abs(currentHealth - targetHealth) < 0.01f)
                {
                    currentHealth = targetHealth;
                    if (!_sliderPLayer._playerMove._deathOn && currentHealth <= 0)
                    {
                        _sliderPLayer._playerMove._deathOn = true;//morte do inimigo
                                                    // _enemeyMove._invuneravel = true;
                        Death();
                    }
                }

            }
        }
    }

    // Método para simular dano ao levar um ataque
    public void TakeDamage(float damage)
    {
        if (_sliderPLayer._playerMove != null)
        {
            // Verifica se o jogador está invulnerável
            if (!_sliderPLayer._playerMove._deathOn && Time.time - lastDamageTime >= invincibilityTime)
            {
                // Aplica o dano e atualiza o tempo do último dano
                targetHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
                lastDamageTime = Time.time;
                // _enemeyMove.AtivarPorTempo(.1f);
                // _particleSystemHit.Play();
            }
        }
    }

    // Método para curar (opcional)
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

        // Inicializa o tempo do último dano
        lastDamageTime = -invincibilityTime; // Permite tomar dano imediatamente no início
                                             //  _enemeyMove._invuneravel = false;
                                             //  _playerMove._deathOn = false;//morte do inimigo
        if (_sliderPLayer._playerMove != null)
        {
            if (_sliderPLayer._playerMove._deathOn)
            {
                _sliderPLayer._playerMove._deathOn = false;
                // healthSlider.transform.localScale = scaleS;
            }
        }
       // _playerMove.transform.localScale = Vector3.one;
        Invoke("TimeStart", 1);
        // scaleS = healthSlider.transform.lossyScale;
        //  healthSlider.transform.localScale = scaleS;
    }

    public void Death()
    {
    //    _capsHit.capsuleCollider.enabled = false;
        StartCoroutine(DeathTime());
      //  Material meshRenderer = GetComponent<MeshRenderer>().materials[0];
      //  _hitSliderPlayer.HitMove(_playerMove.transform, meshRenderer);
      
      
    }
    IEnumerator DeathTime()
    {


        yield return new WaitForSeconds(1.3f);
     


    }
   
}
