using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    int currentHealth;
    [SerializeField] Slider healthSlider;
    private void Awake()
    {
        if (FindObjectsOfType<HealthManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        currentHealth = 100;
    }
    private void Update()
    {
        healthSlider.value = currentHealth;
    }
    public void SetCurentPlayerHealth(int health)
    {
        currentHealth = health;
    }
    public void TouchedaTrap()
    {
        while (healthSlider.value > 0)
        {
            healthSlider.value -= Time.deltaTime;
        }
    }
}
