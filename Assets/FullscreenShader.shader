Shader "Custom/LessVibrantColorShader"
{
    Properties
    {
        _MainTex("Base Texture", 2D) = "white" { }
        _Color("Color Tint", Color) = (1, 1, 1, 1)
        _TimeSpeed("Time Speed", Float) = 1.0
        _WaveFrequency("Wave Frequency", Float) = 10.0
        _WaveAmplitude("Wave Amplitude", Float) = 0.1
        _ColorBrightness("Color Brightness", Float) = 0.5
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }

            Pass
            {
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
                float _WaveFrequency;
                float _WaveAmplitude;
                float _ColorBrightness;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    // Time for wave animation
                    float time = _Time.y * _TimeSpeed;

                // Multi-directional wave distortions
                float waveH = sin(i.uv.x * _WaveFrequency + time) * _WaveAmplitude;
                float waveV = cos(i.uv.y * _WaveFrequency + time) * _WaveAmplitude;
                float waveDiag1 = sin((i.uv.x + i.uv.y) * _WaveFrequency * 0.8 + time * 1.5) * _WaveAmplitude;
                float waveDiag2 = cos((i.uv.x - i.uv.y) * _WaveFrequency * 1.2 - time * 2.0) * _WaveAmplitude;
                float waveRadial = sin(sqrt(i.uv.x * i.uv.x + i.uv.y * i.uv.y) * _WaveFrequency * 2.0 + time) * _WaveAmplitude;

                // Combine waves for UV distortion
                float2 distortedUV = i.uv + float2(
                    waveH + waveDiag1 + waveRadial, // X-direction
                    waveV + waveDiag2 + waveRadial  // Y-direction
                );

                // Dynamic color waves
                float3 colorWaves = float3(
                    sin(distortedUV.x * 10.0 + time * 2.0), // Red channel
                    cos(distortedUV.y * 15.0 - time * 1.5), // Green channel
                    sin((distortedUV.x + distortedUV.y) * 5.0 + time * 3.0) // Blue channel
                );

                // Scale down the brightness of the color waves
                colorWaves *= _ColorBrightness;

                // Blend color waves with base texture
                float3 baseColor = tex2D(_MainTex, distortedUV).rgb;
                float3 finalColor = baseColor * 0.5 + colorWaves * 0.5; // Even blend of base and waves

                // Apply final tint
                return half4(finalColor * _Color.rgb, 1.0);
            }
            ENDCG
        }
        }
            Fallback "Unlit/Color"
}
