using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject hitMarker;

    private bool hitMakerVisible = false;

    [SerializeField]
    private float hitMakerVisiblityTime = 0.2f;

    [SerializeField]
    private Text healthLegend;

    [SerializeField]
    private Image damagedOverlay;

    [SerializeField]
    private bool damageOverlayVisible = false;

    [SerializeField]
    private float damageOverlayVisiblityTime = 0.25f;

    [SerializeField]
    private Text endScreen;

    private void OnEnable()
    {
        player.Hit += OnPlayerHit;
        player.Damaged += OnPlayerDamaged;
        player.Kill += OnPlayerKill;
        player.Death += OnPlayerDeath;
    }

    private void OnPlayerDamaged(int amount)
    {
        healthLegend.text = String.Format("{0}", player.Health);

        if (damageOverlayVisible)
            StopCoroutine(ShowDamageOverlay(damageOverlayVisiblityTime));

        StartCoroutine(ShowDamageOverlay(damageOverlayVisiblityTime));
    }

    private void OnDisable()
    {
        player.Hit -= OnPlayerHit;
        player.Damaged -= OnPlayerDamaged;
        player.Kill -= OnPlayerKill;
        player.Death -= OnPlayerDeath;
    }

    private void OnPlayerHit()
    {
        // show red mark
        if (hitMakerVisible)
            StopCoroutine(ShowHitMarker(hitMakerVisiblityTime));

        StartCoroutine(ShowHitMarker(hitMakerVisiblityTime));
    }

    private void OnPlayerDeath()
    {
        endScreen.transform.parent.gameObject.SetActive(true);
        endScreen.text = "You lose";
    }

    private void OnPlayerKill()
    {
        endScreen.transform.parent.gameObject.SetActive(true);
        endScreen.text = "You win";

    }


    private IEnumerator ShowHitMarker(float timeSpan)
    {
        hitMarker.SetActive(true);
        hitMakerVisible = true;
        yield return new WaitForSeconds(timeSpan);
        hitMarker.SetActive(false);
        hitMakerVisible = false;
    }

    private IEnumerator ShowDamageOverlay(float timeSpan)
    {
        damagedOverlay.gameObject.SetActive(true);
        damageOverlayVisible = true;
        yield return new WaitForSeconds(timeSpan);
        damagedOverlay.gameObject.SetActive(false);
        damageOverlayVisible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthLegend.text = String.Format("{0}", player.Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
