Shader "Custom/HallucinationShader"
{
    Properties
    {
        _MainTex("Base Texture", 2D) = "white" { }
        _Color("Color Tint", Color) = (1, 1, 1, 1)
        _TimeSpeed("Time Speed", Float) = 1.0
        _DistortionStrength("Distortion Strength", Float) = 0.1
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }

            Pass
            {
                // Fullscreen hallucination shader
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : POSITION;
                    float2 uv : TEXCOORD0;
                };

                sampler2D _MainTex;
                float4 _Color;
                float _TimeSpeed;
                float _DistortionStrength;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    // Get the current time
                    float time = _Time.y * _TimeSpeed;

                // Distort the UV coordinates to create a wavy effect
                float waveX = sin(i.uv.y * 10.0 + time) * _DistortionStrength;
                float waveY = cos(i.uv.x * 10.0 + time) * _DistortionStrength;
                float2 distortedUV = i.uv + float2(waveX, waveY);

                // Add a color shift for a psychedelic effect
                float3 colorShift = float3(
                    tex2D(_MainTex, distortedUV + float2(0.01, 0)).r,
                    tex2D(_MainTex, distortedUV + float2(0, 0.01)).g,
                    tex2D(_MainTex, distortedUV - float2(0.01, 0)).b
                );

                // Apply a color tint
                return half4(colorShift * _Color.rgb, 1.0);
            }
            ENDCG
        }
        }
            Fallback "Unlit/Color"
}
