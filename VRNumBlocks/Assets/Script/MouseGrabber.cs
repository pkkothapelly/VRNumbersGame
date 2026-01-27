using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGrabber : MonoBehaviour
{
    public Camera cam;
    public float holdDistance = 2.0f;
    public float handMoveSpeed = 25f;
    public float rayDistance = 6f;
    public float throwForce = 8f;

    private Rigidbody heldRb;

    void Start()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        // Move the "hand" in front of the camera
        Vector3 targetPos = cam.transform.position + cam.transform.forward * holdDistance;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * handMoveSpeed);

        // Left mouse: grab/release
        if (Input.GetMouseButtonDown(0))
        {
            if (heldRb == null) TryGrab();
            else Release();
        }

        // Right mouse: throw (if holding)
        if (Input.GetMouseButtonDown(1))
        {
            if (heldRb != null) Throw();
        }
    }

    void TryGrab()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hit, rayDistance)) return;

        // Only grab number blocks
        if (hit.rigidbody == null) return;
        if (hit.rigidbody.GetComponent<NumberBlock>() == null) return;

        heldRb = hit.rigidbody;

        // Stop its motion and "attach" to hand
        heldRb.velocity = Vector3.zero;
        heldRb.angularVelocity = Vector3.zero;
        heldRb.useGravity = false;

        heldRb.transform.SetParent(transform, true);
    }

    void Release()
    {
        if (heldRb == null) return;

        heldRb.transform.SetParent(null, true);
        heldRb.useGravity = true;
        heldRb = null;
    }

    void Throw()
    {
        Rigidbody rb = heldRb;
        Release();

        // Simple forward throw
        rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
