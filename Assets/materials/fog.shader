Shader "Custom/FogCardMasked"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex ("Mask (Grayscale)", 2D) = "white" {}

        _FogColor ("Fog Color", Color) = (0.6, 0.6, 0.6, 1)
        _FogIntensity ("Fog Intensity", Range(0,1)) = 1.0
        _MaskStrength ("Mask Strength", Range(0,2)) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _MaskTex;

            float4 _FogColor;
            float _FogIntensity;
            float _MaskStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert(appdata v)
            {
                v2f o;
                
                UNITY_SETUP_INSTANCE_ID(v); 
                UNITY_INITIALIZE_OUTPUT(v2f, o); 
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); 
                
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);

                float mask = tex2D(_MaskTex, i.uv).r;
                mask *= _MaskStrength;

                float3 fogged = lerp(col.rgb, _FogColor.rgb, mask * _FogIntensity);

                // Use mask as alpha too (important for fog cards)
                float alpha = saturate(mask);

                return float4(fogged, alpha);
            }
            ENDCG
        }
    }
}