using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{

    // public Camera MainCamera;
    private Vector2 screenBounds;

    [SerializeField] private float minX, maxX, minY, maxY;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
       
    }



    void LateUpdate()
    {
         Vector3 viewPos = transform.position;
         viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 +minX, screenBounds.x + maxX);
         viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + minY, screenBounds.y  + maxY);
         transform.position = viewPos;
    }

}
