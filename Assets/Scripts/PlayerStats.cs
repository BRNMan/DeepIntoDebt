using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;

    [SerializeField]
    public float maxHealth = 100.0f;

    [SerializeField]
    public double netWorth = 300.0;

    public TextMeshProUGUI debtCounterText;
    public RectTransform healthBar;

    // Start is called before the first frame update
    void Start()
    {
        ChangeNetWorthText();
        StartCoroutine(RegenHealth());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("interact")) {
            AddNetWorth(-123.45);
            DamagePlayer(3.0f);
        }

    }

    private IEnumerator RegenHealth() {
        while(true) {
            health = Mathf.Clamp(health + 1.0f, 0.0f, maxHealth);
            UpdateHealthBar();
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void DamagePlayer(float damage) {
        //Particle effect for attack.
        health -= damage;
        UpdateHealthBar();
        //Game Over?
    }

    public void UpdateHealthBar() {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(this.health/this.maxHealth, 0.0f,1.0f),1.0f,1.0f);
    }

    public void AddNetWorth(double value) {
        this.netWorth += value;
        ChangeNetWorthText();
    }

    private void ChangeNetWorthText() {
        if(this.netWorth < 0.0f) {
            debtCounterText.color = new Color(255.0f,0.0f,0.0f);
        }
        debtCounterText.SetText(this.netWorth.ToString("0.##"));
    }
}
