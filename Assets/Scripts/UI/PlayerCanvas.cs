using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private KeyCode invenotryBtn = KeyCode.I;
    [SerializeField] private GameObject uiContainer = default;

    private void Start()
        => uiContainer.SetActive(false);

    private void Update()
    {
        if (Input.GetKeyDown(invenotryBtn))
            ToggleUiContainer();
    }

    private void ToggleUiContainer()
        => uiContainer.SetActive(!uiContainer.activeSelf);
}