Shader "Unlit/grey2"{
	Properties{
			_MainTex("MainTex", 2D) = "white" {}
			_ColorTint("ColorTint", Color) = (1,0,0,1)


			//_PassMap("Sprite Texture", 2D) = "black" {}

			_PlayerPosX("PlayerPosX", Float) = 0
			_PlayerPosY("PlayerPosY", Float) = 0
	}

	SubShader{
		Tags{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct appdata_t{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldSpacePos : TEXCOORD1;
			};

			fixed4 _Color;

			v2f vert(appdata_t IN){
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

				OUT.worldSpacePos = mul(unity_ObjectToWorld, IN.vertex); //this should calculate the vertex position in (unity)world coordinates

				return OUT;
			}

			sampler2D _MainTex;
			//sampler2D _PassMap;
			uniform float _PlayerPosX;
			uniform float _PlayerPosY;
			uniform float _ColorTint;

			fixed4 frag(v2f IN) : COLOR{
				/*
				if (IN.texcoord.x < .5) return float4(1,0,0,1);
				else return float4(0,1,0,1);
				*/


				float d = distance(float2(_PlayerPosX, _PlayerPosY), (float2) IN.worldSpacePos);
				//float d = distance(float2(_PlayerPosX, _PlayerPosY), (float2) IN.texcoord);
				d = clamp(d, 0, 5);

				half4 texcol = tex2D(_MainTex, IN.texcoord);
				//float maskedPixelGrayscale = tex2D(_MainTex, IN.texcoord).r; //questo dovrebbe campionare la maschera
				//d = max(d, maskedPixelGrayscale);
				texcol.rgb = lerp(texcol.rgb, _ColorTint, d);


				return texcol;
			}
		ENDCG
		} 
	}
Fallback "Sprites/Default"
}