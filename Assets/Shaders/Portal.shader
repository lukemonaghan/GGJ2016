Shader "Custom/Portal" {
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		[NoScaleOffset][Normal] _Normal("Normal (RGB)", 2D) = "white" {}

		[NoScaleOffset][Normal] _PortalNormal1 ("Normal1 (RGB)", 2D) = "white" {}
		[NoScaleOffset][Normal] _PortalNormal2 ("Normal2 (RGB)", 2D) = "white" {}

		_NormalScale("Bump Roughness", Range(0,2)) = 0.0
		_Speed("Speed", Range(0,1)) = 0.1

		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Normal;

		sampler2D _PortalNormal1;
		sampler2D _PortalNormal2;

		float _NormalScale;
		float _Speed;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Offset
			float2 uvOff = float2(_Time.g, _Time.g) * _Speed;

			// Normal
			float4 norm0  = tex2D(_Normal, IN.uv_MainTex);
			float4 norm1 = tex2D(_PortalNormal1, IN.uv_MainTex + uvOff);
			float4 norm2 = tex2D(_PortalNormal2, IN.uv_MainTex + uvOff * -0.5f);
			float4 nCol = (norm0 + norm1 + norm2) * 0.33f;
			float3 norm = UnpackScaleNormal(nCol, _NormalScale);

			// Color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			// Feed out
			o.Albedo = c.rgb;
			o.Alpha = c.a;

			o.Normal = norm;

			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
