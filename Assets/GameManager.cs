using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player[] players;
    private Camera menuCamera;

    [SerializeField]
    private AudioSource musicSource;

    private bool gameStarted = false;

    //    [SerializeField]
    private GameView view;

    private void OnEnable()
    {
        foreach (Player player in players)
        {
            player.Death += OnPlayerDeath;
        }
    }

    private void OnPlayerDeath()
    {
        // disable player input
        foreach (Player player in players)
        {
            PlayerInput input = player.GetComponent<PlayerInput>();
            input.enabled = false;
        }

        StartCoroutine(Restart(5f));
    }

    private void OnDisable()
    {
        foreach (Player player in players)
        {
            player.Death -= OnPlayerDeath;
        }
    }

    private void Awake()
    {
        view = GetComponent<GameView>();
        view.PlayerA = players[0];
        view.PlayerB = players[1];

        menuCamera = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DisablePlayers(players);
   
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Input.anyKeyDown)
        {
            StartGame();
            gameStarted = true;
        }
    }

    void StartGame()
    {
        EnablePlayers(players);
        view.Split();
        menuCamera.gameObject.SetActive(false);
        musicSource.Play();
    }

    private IEnumerator Restart(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }


    private void EnablePlayers(Player[] players)
    {
        foreach (Player player in players)
        {
            player.gameObject.SetActive(true);
        }
    }

    private void DisablePlayers(Player[] players)
    {
        foreach (Player player in players)
        {
            player.gameObject.SetActive(false);
        }
    }
}
