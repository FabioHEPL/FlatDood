using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShootingSprite : MonoBehaviour
{
    public GameObject defaultSprite;
    public GameObject shootingSprite;

    public float changeDelay = 1.25f;
    public bool shooting = false;

    private Coroutine shootingCoroutine;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        shootingCoroutine = StartCoroutine(ChangeSprite(changeDelay));   
    }

    private void OnEnable()
    {
        player.Shot += Player_Shot;
    }

    private void OnDisable()
    {
        player.Shot -= Player_Shot;
    }

    private void Player_Shot()
    {
        if (!shooting)
            StopCoroutine(shootingCoroutine);

        shootingCoroutine = StartCoroutine(ChangeSprite(changeDelay));
    }

    private IEnumerator ChangeSprite(float delay)
    {
        shooting = true;
        defaultSprite.SetActive(false);
        shootingSprite.SetActive(true);
        yield return new WaitForSeconds(delay);
        defaultSprite.SetActive(true);
        shootingSprite.SetActive(false);
        shooting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
