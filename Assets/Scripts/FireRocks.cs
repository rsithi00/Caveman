using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireRocks : MonoBehaviour
{
    public GameObject rock;     // Drag-and-drop prefab here
    public Vector3 mousePos;
    public Vector3 worldPosition;

    public void OnFireTopLeft(InputAction.CallbackContext context)
    {
        if (context.started)
            CreateRock(new Vector3(-10, 5, 0), new Vector3(5, 0, 0));
    }

    public void OnFireTop(InputAction.CallbackContext context)
    {
        if (context.started)
            CreateRock(new Vector3(0, 5, 0), new Vector3(0, 0, 0));
    }

    public void OnFireTopRight(InputAction.CallbackContext context)
    {
        if (context.started)
            CreateRock(new Vector3(10, 5, 0), new Vector3(-8, 0, 0));
    }

    public void OnFireRight(InputAction.CallbackContext context)
    {
        if (context.started)
            CreateRock(new Vector3(10, 1, 0), new Vector3(-15, 0, 0));
    }

    public void OnFireLeft(InputAction.CallbackContext context)
    {
        if (context.started)
            CreateRock(new Vector3(-10, 0, 0), new Vector3(5, 15, 0));
    }

    public void OnFireAll(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CreateRock(new Vector3(-10, 5, 0), new Vector3(5, 0, 0));
            CreateRock(new Vector3(0, 5, 0), new Vector3(0, 0, 0));
            CreateRock(new Vector3(10, 5, 0), new Vector3(-8, 0, 0));
            CreateRock(new Vector3(10, 1, 0), new Vector3(-15, 0, 0));
            CreateRock(new Vector3(-10, 0, 0), new Vector3(5, 15, 0));
        }
    }

    // public void OnFireMouse(InputAction.CallbackContext context)
    // {

    //     CreateRock(new Vector3(0, 5, 0), mousePos);

    // }

    private void CreateRock(Vector3 location, Vector3 velocity)
    {
        GameObject spawnedRock = Instantiate(rock, location, Quaternion.identity);        // Parameters are the object, position, and rotation.
        Rigidbody2D body = spawnedRock.GetComponent<Rigidbody2D>();
        body.velocity = velocity;
        Destroy(spawnedRock, 5f);                                                 // Give it a push.
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        if(Input.GetMouseButtonDown(0))
        {
            CreateRock(new Vector3(0,6,0), worldPosition);
        }
    }

}
