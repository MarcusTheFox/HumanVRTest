using UnityEngine;
using Valve.VR;

public class PlayerJump : MonoBehaviour
{
    public SteamVR_Action_Boolean jumpAction;

    public float jumpForce = 5.0f;

    private Rigidbody playerRigidbody;

    private bool isGrounded = true;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (jumpAction.stateDown && isGrounded)
        {
            PerformJump();
        }
    }

    private void PerformJump()
    {
        if (playerRigidbody != null)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) <= 45.0f)
            {
                isGrounded = true;
                return;
            }
        }
    }
}