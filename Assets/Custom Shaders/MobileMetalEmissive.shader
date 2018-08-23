Shader "Custom/MobileMetalEmissive" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
        _EmColor ("Emission Color", Color) = (1,1,1,1)
        _EmStrength ("Emission Strength", Range(0,1)) = 0.5
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MetallicGlossMap("Metal Texture", 2D) = "white" {}
		[Gamma] _Metallic ("Metallic" , Range(0,1)) = 0.5
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
		_BumpMap ("Normal", 2D) = "bump" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _MetallicGlossMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_MetallicGlossMap;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
        fixed4 _EmColor;
        half _EmStrength;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

			o.Albedo = c.rgb;
			o.Metallic = tex2D(_MetallicGlossMap, IN.uv_MetallicGlossMap) * _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
            o.Emission = _EmColor * _EmStrength;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
