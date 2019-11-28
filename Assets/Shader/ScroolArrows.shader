Shader "Custom/ScroolArrows" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
			Tags { "RenderType" = "Transparent"}
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard alpha:fade

			#pragma target 3.0

			sampler2D _MainTex;

			struct Input 
			{
				float2 uv_MainTex;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			void surf(Input IN, inout SurfaceOutputStandard o) 
			{
				float _sX;	float _sY;	float _Contrast;
				{//オブジェクトの対比計算
					_sX = 1 / sqrt(pow(unity_WorldToObject[0].x, 2) + pow(unity_WorldToObject[0].y, 2) + pow(unity_WorldToObject[0].z, 2));
					
				}

				// スクロール計算
				fixed2 scrolledUV = IN.uv_MainTex;
				scrolledUV.x *= _sX;
				scrolledUV.x += _Time * 10 ;
				fixed4 c = tex2D(_MainTex, -scrolledUV ) * _Color;
				o.Albedo = c.rgb;

				// 色計算
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
		FallBack "Diffuse"
}