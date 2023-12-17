using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerController : MonoBehaviour
{
    #region Fields
    [Header("UI Elements")]
    public Slider healthBar;
    public TMP_Text interactionPrompt;
    public Slider blinkMeter;

    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    private float maxHealth = 100;
    [SerializeField]
    private float currentHealth = 100;
    public Camera playerCamera;

    private float xRotation = 0f;
    #endregion

    #region Unity Lifecycle Methods
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateHealthBar();
        UpdateInteractionPrompt();
        LookAround();
        MovePlayer();
        CheckInteraction();
    }
    #endregion

    #region UI Elements
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    private void UpdateInteractionPrompt()
    {
        if (interactionPrompt != null)
        {
            bool isNearInteractable = CheckForInteractables();
            interactionPrompt.gameObject.SetActive(isNearInteractable);
        }
    }

    private bool CheckForInteractables()
    {
        return false; // Placeholder
    }
    #endregion

    #region Player Movement
    private void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    #endregion

    #region Interaction
    private void CheckInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
    }
    #endregion
}
