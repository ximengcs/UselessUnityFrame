using UnityEngine;

public class Framework : MonoBehaviour
{
    private void Start()
    {
        int refreshRate = Screen.currentResolution.refreshRate;
        Application.targetFrameRate = refreshRate;
        QualitySettings.vSyncCount = 1;
    }
}
