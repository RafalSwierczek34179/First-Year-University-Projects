using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int health = 10;
    [SerializeField] private Canvas damageCanvas;
    [SerializeField] int MoneyOnKill;

    public void TakeDamage(int damageAmount, GameObject tower)
    {
        health -= damageAmount;
        GameObject.Instantiate(damageCanvas).GetComponent<EnemyDamageTakenUI>().DisplayDamage(this.transform, damageAmount);
        this.GetComponentInChildren<EnemyHealthBar>().DisplayEnemyHealthBar();
        if (health <= 0)
        {
            tower.GetComponent<TowerAttackScript>().DeleteEnemyFromList(this.gameObject);
            //Stops the tween animating the enemy HB when the object gets destroyed
            this.GetComponentInChildren<EnemyHealthBar>().KillTween();
            FindObjectOfType<MoneyManager>().GainMoney(MoneyOnKill);
            GameObject.Destroy(this.gameObject);
        }
    }

}