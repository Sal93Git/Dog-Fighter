Shader "Custom/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _Scale ("Noise scale", Range(0.01, 0.1)) = 0.03
        _Amplitude ("Amplitude", Range(0.01, 0.1)) = 0.015
        _Speed ("Speed", Range(0.01, 0.3)) = 0.15


    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 300

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTex;

        float _Scale;
        float _Amplitude;
        float _Speed;

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
        };

        void vert(inout appdata_full v)
        {
            // Fixed syntax errors:
            float wave1 = sin((v.vertex.x + _Time.y * _Speed) * 4.0) * _Amplitude;
            float wave2 = cos((v.vertex.z + _Time.y * _Speed * 0.8) * 2.0) * (_Amplitude * 0.5);
            float2 noiseUV = v.texcoord.xy * _Scale + float2(_Time.y * _Speed, 0.0);
            float noiseValue = tex2Dlod(_NoiseTex, float4(noiseUV, 0, 0)).x * _Amplitude;
            v.vertex.y += wave1 + wave2 + noiseValue;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }    
        ENDCG
    }
    FallBack "Diffuse"
}
