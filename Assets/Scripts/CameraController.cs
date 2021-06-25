using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    [Range(1f, 10f)] public float rotateSpeed;

    public Transform target;

    private Vector3 lastTargetPosition;
    
    private Vector2 lastMousePosition;
    
    // Start is called before the first frame update
    void Start() {
        //Cursor.visible = false;
        lastMousePosition = Input.mousePosition;
        lastTargetPosition = target.position;
    }

    // Update is called once per frame
    void LateUpdate() {
        RotateCamera();
        FollowPlayer();
    }
    
    private void RotateCamera() {
        // processing data
        Vector2 mousePosition = Input.mousePosition;
        Vector2 deltaPosition = mousePosition - lastMousePosition;
        float deltaX = deltaPosition.x;
        transform.RotateAround(target.position, Vector3.up, deltaX * Time.deltaTime * rotateSpeed);
        lastMousePosition = mousePosition;
        
        // processing mouse position boundaries
        int bufferWidth = 20;
        if (mousePosition.x > Screen.width - bufferWidth) {
            int newX = bufferWidth;
            int newY = Screen.height - (int) mousePosition.y;
            SetCursorPos(newX, newY);
            lastMousePosition = new Vector2(newX, newY);
        }

        if (mousePosition.x < bufferWidth) {
            int newX = Screen.width - bufferWidth;
            int newY = Screen.height - (int) mousePosition.y;
            SetCursorPos(newX, newY);
            lastMousePosition = new Vector2(newX, newY);
        }

    }

    private void FollowPlayer() {
        Vector3 targetPosition = target.position;
        Vector3 deltaPos = targetPosition - lastTargetPosition;
        deltaPos.y = 0;
        transform.Translate(deltaPos, Space.World);
        lastTargetPosition = targetPosition;
    }
}