using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

[Serializable, VolumeComponentMenu("Post-processing/Custom/SobelOutline")]
public sealed class SobelOutline : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter outlineWidth = new ClampedFloatParameter(0f, 0f, 32f);
    public ClampedFloatParameter outlineFade = new ClampedFloatParameter(0f, 0f, 32f);
    public ColorParameter outlineColor = new ColorParameter(Color.black);

    public Material m_Material;

    public bool IsActive() => m_Material != null;

    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

    public override void Setup()
    {
        if (Shader.Find("Shader Graph/Outline") != null)
            m_Material = new Material(Shader.Find("Shader Graph/Outline"));
    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
    {
        if (m_Material == null)
            return;

        // Packing multiple float paramters into one float4 uniform

        m_Material.SetFloat("_Outline", outlineWidth.value);
        m_Material.SetFloat("_Fade", outlineFade.value);
        m_Material.SetColor("_Color", outlineColor.value);
        

        // m_Material.SetTexture("_InputTexture", source);

        HDUtils.DrawFullScreen(cmd, m_Material, destination);
    }

    public override void Cleanup() => CoreUtils.Destroy(m_Material);
}