using UnityEngine;

public class SwitchRenderOnOff : MonoBehaviour
{
    void AllRenderEnable(bool enabled)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = enabled;
        }
    }
}
