using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    private FirstPersonMovement _firstPersonMovement;
    void Awake()
    {
        // Get the character from the FirstPersonMovement in parents.
        _firstPersonMovement = GetComponentInParent<FirstPersonMovement>();
        character = _firstPersonMovement.transform;
    }

    void Update()
    {
        if (_firstPersonMovement.IsTopDownView)
            transform.localRotation = Quaternion.Euler(45, 0, 0);
        else         
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);

        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        
        if (!Input.GetMouseButton(1)) return;
        
        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        if (_firstPersonMovement.IsTopDownView)
            transform.localRotation = Quaternion.Euler(45, 0, 0);
        else         
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);

        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}

