using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HitSliderEnemy : MonoBehaviour
{
    public Slider healthSlider;     // Refer�ncia ao Slider na HUD
    public Vector3 scaleS;
    public float currentHealth;     // Vida atual
    public float maxHealth = 100f;  // Vida m�xima
    public float lerpSpeed = 5f;    // Velocidade de interpola��o
    public float invincibilityTime = 2f; // Tempo de espera entre danos

    private float targetHealth;     // Vida alvo ap�s o ataque
    private float lastDamageTime;   // Tempo do �ltimo dano recebido
    public EnemeyMove _enemeyMove;
    public CapsHit _capsHit;
    public SliderPLayer _hitSliderPlayer;

    public ParticleSystem _particleSystemHit;
    public GameObject _textureEnemy;

    void Start()
    {
        scaleS = healthSlider.transform.localScale;
        healthSlider.transform.localScale = Vector3.zero;
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
                   // _enemeyMove._invuneravel = true;
                    Death();
                }
            }

        }
    }

    // M�todo para simular dano ao levar um ataque
    public void TakeDamage(float damage)
    {
        // Verifica se o jogador est� invulner�vel
        if (!_enemeyMove._deathOn && Time.time - lastDamageTime >= invincibilityTime)
        {
            // Aplica o dano e atualiza o tempo do �ltimo dano
            targetHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            lastDamageTime = Time.time;
            _enemeyMove.AtivarPorTempo(.1f);
           // _particleSystemHit.Play();
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
        _textureEnemy.SetActive(true);
        // Define valores iniciais
        currentHealth = maxHealth;
        targetHealth = maxHealth;
        healthSlider.value = currentHealth;

        // Inicializa o tempo do �ltimo dano
        lastDamageTime = -invincibilityTime; // Permite tomar dano imediatamente no in�cio
      //  _enemeyMove._invuneravel = false;
      //  _enemeyMove._deathOn = false;//morte do inimigo
        if (_enemeyMove._deathOn)
        {
            _enemeyMove._deathOn = false;
            healthSlider.transform.localScale = scaleS;
        }
        _enemeyMove.transform.localScale = Vector3.one;
        Invoke("TimeStart", 1);
        // scaleS = healthSlider.transform.lossyScale;
        //  healthSlider.transform.localScale = scaleS;
    }

    public void Death()
    {
        _capsHit.capsuleCollider.enabled = false;
        StartCoroutine(DeathTime());
        Material meshRenderer = GetComponent<MeshRenderer>().materials[0];
        _hitSliderPlayer.HitMove(_enemeyMove.transform, meshRenderer);
      
      
    }
    IEnumerator DeathTime()
    {
        healthSlider.transform.localScale = Vector3.zero;
        _particleSystemHit.Play();
        _textureEnemy.SetActive(false);
        for (int i = 0; i < _enemeyMove._pe.Length; i++)
        {
            _enemeyMove._pe[i].Stop();
        }

        yield return new WaitForSeconds(1.3f);
        _enemeyMove.gameObject.SetActive(false);


    }


    public void HitMili(SelectPerson _selectPerson) {
        _hitSliderPlayer = _selectPerson._sliderPLayers;
        TakeDamage(25);
        GetComponent<EnemeyMove>()._stopMove = true;
    }
   
}
