using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Crosshair")]
    public GameObject crosshair;

    public void ActiveCrosshair()
    {
        crosshair.SetActive(true);
    }

    public void DeactiveCrosshair()
    {
        crosshair.SetActive(false);
    }
}
