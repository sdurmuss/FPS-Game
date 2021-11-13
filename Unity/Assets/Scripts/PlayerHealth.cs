using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth playerHealth;
    public GameObject panelObject;
    int maxHealth = 100;
    int currentHealth, score;
    public Text playerHealtText, scoreText;
    void Start()
    {
        panelObject.SetActive(true);
        Time.timeScale = 0;
        playerHealth = this;
        currentHealth = maxHealth;
        int score = 0;
    }
    public void DeductHealth(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Playerın canı: " + currentHealth);
        playerHealtText.text = currentHealth.ToString();
        if (currentHealth <= 30)
        {
            playerHealtText.color = Color.red;
        }
        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }
    public int GetHeal() => currentHealth;
    private void KillPlayer()
    {
        SceneManager.LoadScene("Demo7");
    }
    public void AddHealth(int value)
    {
        currentHealth += value;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        playerHealtText.text = currentHealth.ToString();
        if (currentHealth > 30)
        {
            playerHealtText.color = Color.white;
        }
    }

    public void Score()
    {
        score += 5;
        scoreText.text = score.ToString();
    }

    public void StartGame()
    {
        panelObject.SetActive(false);
        Time.timeScale = 1;
    }
    /*private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if(collision.tag == "Heal")
        {
            if(currentHealth != 100)
            {
                AddHealth(20);
                Destroy(collision.gameObject);
                Debug.Log(currentHealth);
            }
        }
    }*/
}
