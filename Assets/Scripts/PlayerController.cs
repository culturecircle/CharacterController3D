using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;

    public Transform followCamera;

    private KeyCode pressingKey = KeyCode.None;

    [Range(1f, 4f)] public float speed = 3f;

    // Start is called before the first frame update
    void Start() {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        ControlMove();
    }

    void ControlMove() {
        if (!Input.GetKey(pressingKey)) {
            if (Input.GetKey(KeyCode.W)) {
                pressingKey = KeyCode.W;
            } else if (Input.GetKey(KeyCode.S)) {
                pressingKey = KeyCode.S;
            } else if (Input.GetKey(KeyCode.A)) {
                pressingKey = KeyCode.A;
            } else if (Input.GetKey(KeyCode.D)) {
                pressingKey = KeyCode.D;
            } else {
                pressingKey = KeyCode.None;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) pressingKey = KeyCode.W;
        if (Input.GetKeyDown(KeyCode.S)) pressingKey = KeyCode.S;
        if (Input.GetKeyDown(KeyCode.A)) pressingKey = KeyCode.A;
        if (Input.GetKeyDown(KeyCode.D)) pressingKey = KeyCode.D;
        Vector3 newRotation = followCamera.rotation.eulerAngles;
        newRotation.x = 0;
        newRotation.z = 0;
        switch (pressingKey) {
            case KeyCode.W:
                // no extra processing
                break;
            case KeyCode.S:
                newRotation.y += 180;
                break;
            case KeyCode.A:
                newRotation.y -= 90;
                break;
            case KeyCode.D:
                newRotation.y += 90;
                break;
        }
        animator.SetBool("Run", pressingKey != KeyCode.None);
        if (pressingKey != KeyCode.None) {
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), Time.deltaTime * 15f);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
    }
}