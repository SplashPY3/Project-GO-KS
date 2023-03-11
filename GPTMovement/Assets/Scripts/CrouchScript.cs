using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour
{
    CharacterController charCollider;

    public Camera MainCamera;

    public GameObject playerModel;


    public PlayerMovement player;

    Vector3 playerSize;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        charCollider = gameObject.GetComponent<CharacterController>(); // getting the character controller to manipulate its height

        GameObject FPSPlayer = GameObject.Find("FPSPlayer");        // importing the PlayerMovement script to access the speed variable
        player = FPSPlayer.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left ctrl"))
        {
            player.speed -= 4f;                                     // modifying speed in the movement script when crouched


            charCollider.height = 1.9f;                             // decreased collider height

            playerSize = playerModel.transform.localScale;

            playerSize.y = 0.9f;                                    // decreasing collider size so it's smaller when crouched

            playerModel.transform.localScale = playerSize;


            pos = MainCamera.transform.position;                    // lowering camera position to fit the downsized player model
            pos.y -= 0.5f;
            MainCamera.transform.position = pos;
        }

        if (Input.GetKeyUp("left ctrl"))
        {
            player.speed += 4f;                                     // adding speed when crouch is released

            charCollider.height = 3.8f;                             // default collider height

            playerSize = playerModel.transform.localScale;

            playerSize.y = 1.8f;                                    // collider size back to normal

            playerModel.transform.localScale = playerSize;

            pos = MainCamera.transform.position;
            pos.y += 0.5f;                                          // camera position adjusted to its original position
            MainCamera.transform.position = pos;
        }
    }
}
