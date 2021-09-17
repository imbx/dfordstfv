Shader "Custom/DfordShader"
{
    Properties
    {
        [MainColor] _Color ("Color", Color) = (1,1,1,1)
        [MainTexture] _BaseColorMap("BaseColorMap", 2D) = "white" {}
        [HideInInspector] _BaseColorMap_MipInfo("_BaseColorMap_MipInfo", Vector) = (0, 0, 0, 0)
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }

    HLSLINCLUDE

    #pragma target 4.5
    //#pragma enable_d3d11_debug_symbols

    //-------------------------------------------------------------------------------------
    // Variant
    //-------------------------------------------------------------------------------------

    #pragma shader_feature_local _ALPHATEST_ON
    #pragma shader_feature_local _ALPHATOMASK_ON
    #pragma shader_feature_local _DEPTHOFFSET_ON
    #pragma shader_feature_local _DOUBLESIDED_ON
    #pragma shader_feature_local _ _VERTEX_DISPLACEMENT _PIXEL_DISPLACEMENT
    #pragma shader_feature_local _VERTEX_DISPLACEMENT_LOCK_OBJECT_SCALE
    #pragma shader_feature_local _DISPLACEMENT_LOCK_TILING_SCALE
    #pragma shader_feature_local _PIXEL_DISPLACEMENT_LOCK_OBJECT_SCALE
    #pragma shader_feature_local _ _REFRACTION_PLANE _REFRACTION_SPHERE _REFRACTION_THIN

    #pragma shader_feature_local _ _EMISSIVE_MAPPING_PLANAR _EMISSIVE_MAPPING_TRIPLANAR _EMISSIVE_MAPPING_BASE
    #pragma shader_feature_local _ _MAPPING_PLANAR _MAPPING_TRIPLANAR
    #pragma shader_feature_local _NORMALMAP_TANGENT_SPACE
    #pragma shader_feature_local _ _REQUIRE_UV2 _REQUIRE_UV3

    #pragma shader_feature_local _NORMALMAP
    #pragma shader_feature_local _MASKMAP
    #pragma shader_feature_local _BENTNORMALMAP
    #pragma shader_feature_local _EMISSIVE_COLOR_MAP

    // _ENABLESPECULAROCCLUSION keyword is obsolete but keep here for compatibility. Do not used
    // _ENABLESPECULAROCCLUSION and _SPECULAR_OCCLUSION_X can't exist at the same time (the new _SPECULAR_OCCLUSION replace it)
    // When _ENABLESPECULAROCCLUSION is found we define _SPECULAR_OCCLUSION_X so new code to work
    #pragma shader_feature_local _ENABLESPECULAROCCLUSION
    #pragma shader_feature_local _ _SPECULAR_OCCLUSION_NONE _SPECULAR_OCCLUSION_FROM_BENT_NORMAL_MAP
    #ifdef _ENABLESPECULAROCCLUSION
    #define _SPECULAR_OCCLUSION_FROM_BENT_NORMAL_MAP
    #endif

    #pragma shader_feature_local _HEIGHTMAP
    #pragma shader_feature_local _TANGENTMAP
    #pragma shader_feature_local _ANISOTROPYMAP
    #pragma shader_feature_local _DETAIL_MAP
    #pragma shader_feature_local _SUBSURFACE_MASK_MAP
    #pragma shader_feature_local _THICKNESSMAP
    #pragma shader_feature_local _IRIDESCENCE_THICKNESSMAP
    #pragma shader_feature_local _SPECULARCOLORMAP
    #pragma shader_feature_local _TRANSMITTANCECOLORMAP

    #pragma shader_feature_local _DISABLE_DECALS
    #pragma shader_feature_local _DISABLE_SSR
    #pragma shader_feature_local _DISABLE_SSR_TRANSPARENT
    #pragma shader_feature_local _ENABLE_GEOMETRIC_SPECULAR_AA

    // Keyword for transparent
    #pragma shader_feature _SURFACE_TYPE_TRANSPARENT
    #pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT
    #pragma shader_feature_local _TRANSPARENT_WRITES_MOTION_VEC

    // MaterialFeature are used as shader feature to allow compiler to optimize properly
    #pragma shader_feature_local _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
    #pragma shader_feature_local _MATERIAL_FEATURE_TRANSMISSION
    #pragma shader_feature_local _MATERIAL_FEATURE_ANISOTROPY
    #pragma shader_feature_local _MATERIAL_FEATURE_CLEAR_COAT
    #pragma shader_feature_local _MATERIAL_FEATURE_IRIDESCENCE
    #pragma shader_feature_local _MATERIAL_FEATURE_SPECULAR_COLOR

    #pragma shader_feature_local _ADD_PRECOMPUTED_VELOCITY

    //-------------------------------------------------------------------------------------
    // Define
    //-------------------------------------------------------------------------------------

    // This shader support recursive rendering for raytracing
    #define HAVE_RECURSIVE_RENDERING

    // This shader support vertex modification
    #define HAVE_VERTEX_MODIFICATION

    #define SUPPORT_BLENDMODE_PRESERVE_SPECULAR_LIGHTING

    // If we use subsurface scattering, enable output split lighting (for forward pass)
    #if defined(_MATERIAL_FEATURE_SUBSURFACE_SCATTERING) && !defined(_SURFACE_TYPE_TRANSPARENT)
    #define OUTPUT_SPLIT_LIGHTING
    #endif

    #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
    #define _WRITE_TRANSPARENT_MOTION_VECTOR
    #endif
    //-------------------------------------------------------------------------------------
    // Include
    //-------------------------------------------------------------------------------------

    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"

    //-------------------------------------------------------------------------------------
    // variable declaration
    //-------------------------------------------------------------------------------------

    // #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.cs.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/LitProperties.hlsl"

    // TODO:
    // Currently, Lit.hlsl and LitData.hlsl are included for every pass. Split Lit.hlsl in two:
    // LitData.hlsl and LitShading.hlsl (merge into the existing LitData.hlsl).
    // LitData.hlsl should be responsible for preparing shading parameters.
    // LitShading.hlsl implements the light loop API.
    // LitData.hlsl is included here, LitShading.hlsl is included below for shading passes only.

    ENDHLSL

    SubShader
    {
        Tags{ "RenderPipeline"="HDRenderPipeline" "RenderType" = "Opaque" }
        Pass
        {
            // Cull [_CullMode]
            // Tags { "RenderType"="Opaque" }
            LOD 200

            HLSLPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch

            #pragma multi_compile_instancing
            #pragma instancing_options renderinglayer
            #pragma multi_compile _ DOTS_INSTANCING_ON

            

            #define SHADERPASS SHADERPASS_TRANSPARENT_DEPTH_POSTPASS

            #pragma vertex Vert
            #pragma fragment Frag

            
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
            struct Input
            {
                float2 uv_BaseColorMap;
            };


            float4 _Color;

            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            half4 Frag (Input IN)
            {
                // Albedo comes from a texture tinted by color
                float4 c = SAMPLE_TEXTURE2D (_BaseColorMap, sampler_BaseColorMap, IN.uv_BaseColorMap) * _Color;


                return half4(c.r, c.b, c.g, 1);

            }
            ENDHLSL
        }
        
    }
}
