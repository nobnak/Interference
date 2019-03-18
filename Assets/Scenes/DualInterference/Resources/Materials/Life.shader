Shader "Unlit/Life" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_AlphaTex ("Alpha texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	}
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off ZTest LEqual
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _AlphaTex;

			float4 _Color;
            fixed _Cutoff;
			
			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			float4 frag (v2f i) : SV_Target {
				float4 c = tex2D(_MainTex, i.uv);
				float4 alpha = tex2D(_AlphaTex, i.uv);
                clip(alpha.a - _Cutoff);
				return c * _Color;
			}
			ENDCG
		}
	}
}
