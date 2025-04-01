using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : EntityHealth
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider slide;
    public TextMeshProUGUI HPtext;

    // Update is called once per frame
    void Update()
    {
        slide.value = currHP/maxHP;
        HPtext.text = currHP + "/" + maxHP;
    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);

        print("hello");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}

