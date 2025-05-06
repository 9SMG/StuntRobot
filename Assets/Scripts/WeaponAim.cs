using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    void AllRenderEnable(bool enabled)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = enabled;
        }
    }
}
