using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{


    //[SerializeField]
    private Player player;

    // J'ai du changer "movementInputName" en horizontal et vertical
    // car maintenant il y avait plusieurs axes sur lesquels jouer
    [SerializeField]
    private string horizontalInputName;

    [SerializeField]
    private string verticalInputName;

    [SerializeField]
    private string rotationInputName;

    [SerializeField]
    private string shootInputName;

    // Performance optimization
    private Vector3 direction = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // calculer le raycast de tir avant de se déplacer
        if (Input.GetButtonDown(shootInputName))
        {
            player.Shoot();
        }

        this.direction.x = Input.GetAxis(horizontalInputName);
        this.direction.z = Input.GetAxis(verticalInputName);
        direction = direction.normalized;

        player.Move(new Vector3(direction.x, 0, 0));
        player.Move(new Vector3(0, 0, direction.z));


        this.rotation.y = Input.GetAxisRaw(rotationInputName);


        player.Rotate(rotation);

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    this.rotation.y = 1;
        //    player.Rotate(rotation);
        //}

    }
}
