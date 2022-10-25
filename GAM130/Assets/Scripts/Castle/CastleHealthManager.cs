using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthManager : MonoBehaviour
{
    public GameOverScript GameOverScreen;
    public IntVariable castleHealth;
    Slider healthBar;

    public AudioSource Damage1;
    public AudioSource Damage2;

    private void Start()
    {

        healthBar = GameObject.FindGameObjectWithTag("Health Bar").gameObject.GetComponent<Slider>();

        // This is so that the health bar updates if the designers change castle health from the editor
        healthBar.maxValue = castleHealth.RuntimeValue;
        healthBar.value = castleHealth.RuntimeValue;
    }

    public void TakeDamage(int damageTaken)
    {
        castleHealth.RuntimeValue -= damageTaken;
        healthBar.value = castleHealth.RuntimeValue;
        print($"Health: {castleHealth.RuntimeValue}");

        if (OptionsScript.muteSFX == false)
        {
            int DamageSound = Random.Range(1, 3);
            switch (DamageSound)
            {
                case 1:
                    Damage1.Play();
                    break;

                case 2:
                    Damage2.Play();
                    break;
            }
        }

        if (castleHealth.RuntimeValue <= 0)
        {
            print("GameOver!");
            Time.timeScale = 0f;
            GameOverScreen.GameOver();

        }
    }
}
