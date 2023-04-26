Shader "ScreenPocket/Skybox/Gradation"
{
	Properties
	{
		_TopColor("Top", Color) = (1, 0, 0, 1)
		_MiddleColor("Middle", Color) = (0, 1, 0, 1)
		_BottomColor("Bottom", Color) = (0, 0, 1, 1)
		_Length("Length", Float) = 1
	}

		SubShader
	{
		Tags { "Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
		Cull Off ZWrite Off

		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform half3 _TopColor;
			uniform half3 _MiddleColor;
			uniform half3 _BottomColor;
			uniform half _Length;

			struct appdata_t
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4  pos            : SV_POSITION;
				float3  color          : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			v2f vert(appdata_t v)
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

				OUT.pos = UnityObjectToClipPos(v.vertex);
				OUT.color = lerp(lerp(_MiddleColor,_TopColor,saturate(v.vertex.y / _Length)),_BottomColor, saturate((-v.vertex.y) / _Length));
				return OUT;
			}

			half4 frag(v2f IN) : SV_Target
			{
				return half4(IN.color,1);
			}
			ENDCG
		}
	}
		Fallback Off
}