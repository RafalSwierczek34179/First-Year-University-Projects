using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    EnemyHealth enemyHealthScript;

    
    public GameObject fill;
    
    public GameObject background;

    float timer = 0f;
    float timeWhenDamaged = 0f;
    float timeUntilFadeOut = 1f;

    bool isVisible = false;
    bool stopUpdate = false;

    void Start()
    {
        healthBar.GetComponentInChildren<Slider>();
        // The canvas this scripts sits on is a child of the Enemy GameObject
        enemyHealthScript = this.gameObject.GetComponentInParent<EnemyHealth>();
        healthBar.maxValue = enemyHealthScript.health;

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer - timeWhenDamaged > timeUntilFadeOut && isVisible && stopUpdate == false)
        {
            FadeOutEnemyHealthBar();
            stopUpdate = true;
        }
    }

    public void DisplayEnemyHealthBar()
    {
        isVisible = true;
        timeWhenDamaged = timer;
        // Code to fade in HB
        fill.GetComponent<Image>().DOFade(1f, 1f);
        background.GetComponent<Image>().DOFade(1f, 1f);
        // Code to move slider across to new position
        healthBar.DOValue(enemyHealthScript.health, 0.5f, false);
    }

    void FadeOutEnemyHealthBar()
    {
        // Code to fade out HB
        fill.GetComponent<Image>().DOFade(0f, 0.5f);
        background.GetComponent<Image>().DOFade(0f, 0.5f);
    }

    // Mostly used for stopping tween animations from trying to play whilst the enemy game object is destroyed
    public void KillTween()
    {
        fill.GetComponent<Image>().DOComplete();
        background.GetComponent<Image>().DOComplete();
        healthBar.DOComplete();
        fill.GetComponent<Image>().DOKill();
        background.GetComponent<Image>().DOKill();
        healthBar.DOKill();

    }
}
