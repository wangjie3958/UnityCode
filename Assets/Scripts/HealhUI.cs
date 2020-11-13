using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterStats))]
public class HealhUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    Transform ui;
    Image healthSlider;
    Transform cam;
    CharacterStats stats;
    float visibleTime = 5;
    float lastVisibleTime;
    private void Start()
    {
        cam = Camera.main.transform;
        foreach (Canvas canvas in FindObjectsOfType<Canvas>()) 
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, canvas.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                    ui.gameObject.SetActive(false);
                break;
            }
        }
        stats = GetComponent<CharacterStats>();
        stats.OnHealthChanged += OnHealthChanged;

    }
    private void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if ((Time.time - lastVisibleTime) >= visibleTime)
            {
                ui.gameObject.SetActive(false);
            }
        }
    }
    public void OnHealthChanged(int maxHealth,int currentHealth)
    {
        if (ui != null)
        {
            lastVisibleTime = Time.time;
            ui.gameObject.SetActive(true);
            healthSlider.fillAmount = (float)currentHealth / maxHealth;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }
}
