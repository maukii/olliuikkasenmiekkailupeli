Shader "Sabreurs/SwordPosition" {
	Properties {
		// Riikka's tooltips:
		// This is a shader for changing the tint of the sword, based on it's position
		// You need to set the _ColorScale variable to 0.0 (inner position) or 1.0 (outer position)...
		// ... to change the sword's color.
		// I recommend using either grayscale or unsaturated colors for the position indicators,..
		//	...so it doesn't look like shite.
		_Color("Default Color", Color) = (1,1,1,1)
		// For tinting
		//_ColorTint("Color tint", Color) = (0.1,1,1,1)
		_ColorInner("Color For Inner Position", Color) = (1, 0.7, 0.7, 1)
		_ColorOuter("Color For Outer Position", Color) = (0.7, 1, 0.7, 1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap ("Normal Map", 2D) = "normal" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
			// _ColorScale tells how much the color is tinted. 0.5 = max tint, 1.0 = no tint
			// Change the _ColorScale from other scripts
		_ColorScale ("Position from gradient", Range(0, 1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		// For tinting
		//fixed4 _ColorTint;
		fixed4 _ColorInner;
		fixed4 _ColorOuter;
		float _ColorScale;

		// For tinting
		//void mycolor(Input IN, SurfaceOutput o, inout fixed4 color)
		//{
		//	color *= _ColorTint;
		//}

		sampler2D _MainTex;
		sampler2D _NormalMap;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			// For tinting
			//o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * (_ColorTint * _ColorScale);
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * lerp(_ColorInner, _ColorOuter, _ColorScale);
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
