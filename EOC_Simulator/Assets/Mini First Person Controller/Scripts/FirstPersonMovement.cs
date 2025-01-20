using System;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{

    [Header("Running")]
    public float speed = 5;
    public float runSpeed = 9;
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public KeyCode runningKey = KeyCode.LeftShift;
    
    private Rigidbody rigidbody;
    private Vector3 hitPoint;
    private bool hitWalkablePosition = false;

    [HideInInspector] public bool IsWASDMovement = true;
    [HideInInspector] public bool IsTopDownView = false;
        
    [SerializeField] private Camera topDownCamera;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private LayerMask walkableLayer;
    
    [SerializeField] private float sphereSize = 0.2f;
    private bool canWalkOnHitPoint;

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }
    public void ChangeMovementType(bool isWADS)
    {
        IsWASDMovement = isWADS;
    }

    public void ChangeView(bool isTopDown)
    {
        IsTopDownView = isTopDown;
    }
    
    void FixedUpdate()
    {
        if (IsWASDMovement) WASDMovement();
        else TeleportPlayer();
    }
    void TeleportPlayer()
    {
        Camera cam = fpsCamera;
        if (IsTopDownView) cam = topDownCamera;
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        hitWalkablePosition = false;
            
        if (Physics.Raycast(ray, out RaycastHit hit, 100, walkableLayer))
        {
            var objectHit = hit.transform.gameObject;
            hitPoint = hit.point;

            var collides = Physics.OverlapSphere(hitPoint, sphereSize);
            if (collides != null && collides.Length > 0)
            {
                foreach (var collide in collides)
                {
                    Debug.Log($"Amount: {collides.Length} - {collide.name}");
                    if (!collide.CompareTag("WalkableArea"))
                    {
                        canWalkOnHitPoint = false;
                        return;
                    }
                }
            }
            
            canWalkOnHitPoint = true;
            
            if (!Input.GetMouseButtonDown(0)) return;
            
            Debug.Log($"Hit {objectHit.name}");
            if (objectHit != null)
            {
                // Debug.Log(objectHit.name);
                Debug.Log(objectHit.tag);
                hitWalkablePosition = true;
                // Make another check
                transform.position = hitPoint;
            }
        }
    }

    
    void WASDMovement()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
    }

    private void OnDrawGizmos()
    {
        if (canWalkOnHitPoint) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint, sphereSize);
        
    }
}