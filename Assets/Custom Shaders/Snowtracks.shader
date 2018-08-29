Shader "Custom/Snowtracks" {
	Properties {
		_Tess ("Tesselation", Range(1,256)) = 4
		_SnowColor ("Snow Color", Color) = (1,1,1,1)
		_SnowTex("Snow (RGB)", 2D) = "white" {}
        _SnowNormal ("Snow Normal", 2D) = "bump"{}
        _MetallicTex ("Metal Texture", 2D) = "white" {}
        _GroundColor("Ground Color", Color) = (1,1,1,1)

        _GroundTex("Ground (RGB)", 2D) = "white" {}
		_Splat("SplatMap", 2D) = "black"{}


		_Displacement ("Displacement", Range(0,1.0)) = 0.3
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

         _EmColor ("Emission Color", Color) = (1,1,1,1)
        _EmStrength ("Emission Strength", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows  vertex:disp tessellate:tessDistance 

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 4.6
		#include "Tessellation.cginc"

			struct appdata {
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
		};

		float _Tess;

		float4 tessDistance(appdata v0, appdata v1, appdata v2) {
			float minDist = 10.0;
			float maxDist = 25.0;
			return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
		
		}

		sampler2D _Splat;
		float _Displacement;
        fixed4 _EmColor;
        half _EmStrength;

		void disp(inout appdata v)
		{
			float d = tex2Dlod(_Splat, float4(v.texcoord.xy,0,0)).r * _Displacement;
			v.vertex.xyz -= v.normal * d;
			v.vertex.xyz += v.normal * _Displacement;
		}


		sampler2D _GroundTex;
		fixed4 _GroundColor;
		sampler2D _SnowTex;
        sampler2D _SnowNormal;
		fixed4 _SnowColor;
        sampler2D _MetallicTex;

		struct Input {
			float2 uv_GroundTex;
			float2 uv_SnowTex;
			float2 uv_Splat;
            float2 uv_SnowNormal;
		};

		half _Glossiness;
		half _Metallic;
	

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color#
			
			half amount = tex2Dlod(_Splat, float4(IN.uv_Splat, 0, 0)).r;

			// Lerp between Snow and Ground by an amount
			fixed4 c = lerp(tex2D(_SnowTex, IN.uv_SnowTex) * _SnowColor, tex2D(_GroundTex, IN.uv_GroundTex) * _GroundColor, amount);
            fixed4 metal = tex2D(_MetallicTex, IN.uv_GroundTex);
			
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_SnowNormal, IN.uv_SnowNormal));
			// Metallic and smoothness come from slider variables
			o.Metallic = metal.r * _Metallic;
			o.Smoothness = metal.a * _Glossiness;
			o.Alpha = c.a;
                        o.Emission = _EmColor * _EmStrength;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
