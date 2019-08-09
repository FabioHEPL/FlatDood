using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    public Player PlayerA { get; set; }
    public Player PlayerB { get; set; }

    [SerializeField]
    private Camera playerCameraA;
    [SerializeField]
    private Camera playerCameraB;
    [SerializeField]
    private Camera uiCameraA;
    [SerializeField]
    private Camera uiCameraB;

    [SerializeField]
    private bool split = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    Split();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void Split()
    {
        if (split)
        {

        }
        else
        {
            // Player A
            Camera[] playerACameras = PlayerA.GetComponentsInChildren<Camera>();
            foreach (Camera camera in playerACameras)
            {
                camera.rect = new Rect(0, 0, 0.5f, 1);
            }

            // Player B
            Camera[] playerBCameras = PlayerB.GetComponentsInChildren<Camera>();
            foreach (Camera camera in playerBCameras)
            {
                camera.rect = new Rect(0.5f, 0, 0.5f, 1);
            }

            split = true;
        }
    }

    public void UnsplitTheScreen()
    {

    }

}
