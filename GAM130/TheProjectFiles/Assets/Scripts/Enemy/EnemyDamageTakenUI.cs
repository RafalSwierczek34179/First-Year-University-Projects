using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyDamageTakenUI : MonoBehaviour
{ 
    [SerializeField] private Vector3 textOffset = new Vector3(0f, 1f, 1.25f);
    private Text displayText;

    private void Awake()
    {
        displayText = this.GetComponentInChildren<Text>();
        //damageDisplaySequence.Join(this.transform.DOLocalMoveZ(this.transform.position.z + 2, 1.5f));
    }

    public void DisplayDamage(Transform enemyTransform, int damageTaken)
    {
        this.GetComponentInChildren<Text>().text = damageTaken.ToString();
        this.transform.SetPositionAndRotation(enemyTransform.position + textOffset, Quaternion.Euler(90f, 0f, 0f));

        // DOTween stuff
        Sequence damageDisplaySequence = DOTween.Sequence();
        var fadeTween = damageDisplaySequence.Append(displayText.DOFade(1f, 0.5f)).Append(displayText.DOFade(0f, 1f));
        var posTween = this.transform.DOLocalMoveZ(this.transform.position.z + 1f, 1.5f);
        damageDisplaySequence.OnComplete(() => { fadeTween.Kill(); posTween.Kill(); Object.Destroy(this.gameObject, 0.5f); });
    }
}
