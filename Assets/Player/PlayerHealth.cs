using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Slider slide;
    public float maxHP;
    public float currHP;
    public TextMeshProUGUI HPtext;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slide.value = currHP/maxHP;
        HPtext.text = currHP + "/" + maxHP;
    }

    public void TakeDamage(float damage) {
        currHP -= damage;
    }
}
