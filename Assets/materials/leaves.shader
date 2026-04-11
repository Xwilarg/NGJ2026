Shader "Custom/SimpleLeaf"
{
    Properties
    {
        _MainTex ("Albedo", 2D) = "white" {}
        _MaskTex ("Mask (Black = Cutout)", 2D) = "white" {}
        _Cutoff ("Cutoff", Range(0,1)) = 0.5

        _ShadowColor ("Shadow Color", Color) = (0, 0.3, 0, 1)
        _ShadowStrength ("Shadow Strength", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags 
        { 
            "RenderType"="TransparentCutout"
            "Queue"="AlphaTest"
        }

        LOD 100
        Cull Off

        // ----------- Forward Pass -----------
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma alphaTest _Cutoff

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _MaskTex;

            float _Cutoff;

            float4 _ShadowColor;
            float _ShadowStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD1;
                
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert (appdata v)
            {
                v2f o;
                
                UNITY_SETUP_INSTANCE_ID(v); 
                UNITY_INITIALIZE_OUTPUT(v2f, o); 
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); 
                
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                o.normal = UnityObjectToWorldNormal(v.normal);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float mask = tex2D(_MaskTex, i.uv).r;

                // Cutout
                clip(mask - _Cutoff);

                float3 albedo = tex2D(_MainTex, i.uv).rgb;

                float3 normal = normalize(i.normal);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                float NdotL = saturate(dot(normal, lightDir));

                float shadow = lerp(1.0, NdotL, _ShadowStrength);
                float3 shadowTint = lerp(_ShadowColor.rgb, float3(1,1,1), shadow);

                float3 color = albedo * shadowTint;

                return float4(color, 1.0);
            }
            ENDCG
        }

        // ----------- Shadow Caster Pass -----------
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode"="ShadowCaster" }

            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vertShadow
            #pragma fragment fragShadow
            #pragma multi_compile_shadowcaster

            #include "UnityCG.cginc"

            sampler2D _MaskTex;
            float _Cutoff;

            struct v2f
            {
                V2F_SHADOW_CASTER;
                float2 uv : TEXCOORD1;
            };

            v2f vertShadow(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                o.uv = v.texcoord;
                return o;
            }

            float4 fragShadow(v2f i) : SV_Target
            {
                float mask = tex2D(_MaskTex, i.uv).r;

                // Same cutout logic as main pass
                clip(mask - _Cutoff);

                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}