using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombiHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    int currentHealth;
    public AudioSource zombieAudioSource;
    public AudioClip zombieDeadAudioClip;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public int GetHeal() => currentHealth;
    public void Hit(int damage)
    {
        currentHealth -= damage;
        if(currentHealth == 0)
        {
            zombieAudioSource.PlayOneShot(zombieDeadAudioClip, 0.1f);
            PlayerHealth.playerHealth.Score();
        }
        Debug.Log(currentHealth);
    }
}
