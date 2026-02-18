using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    [Header("Здоровье")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI - TextMeshPro")]
    public TMP_Text healthText;

    [Header("Эффекты")]
    public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        UpdateUI();
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

       
        SceneManager.LoadScene(3);
    }

    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHealth}/{maxHealth}";

            if (currentHealth < 30)
                healthText.color = Color.red;
            else if (currentHealth < 60)
                healthText.color = Color.yellow;
            else
                healthText.color = Color.white;
        }
    }
}