using UnityEngine;

[ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour {

    public Material EffectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        // render this source, to this destination with this material
        Graphics.Blit(src, dst, EffectMaterial);
       
    }
}
