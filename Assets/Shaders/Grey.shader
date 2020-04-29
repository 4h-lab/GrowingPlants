// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Gray"{
    Properties {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_ColorMaskTexture("Lighting Mask (RGB)", 2D) = "white" {}
		_Pixels("Pixels", 2DArray) = "" {}
		//_ColorMask = 

		_Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
        _EffectAmount("Effect Amount", Range(0, 1)) = 1.0
        _PlayerPosX("PlayerPosX", Float) = 0
        _PlayerPosY("PlayerPosY", Float) = 0
        _Ray("Ray",Float) = 0

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

            Pass
            {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile DUMMY PIXELSNAP_ON
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    fixed4 color : COLOR;
                    half2 texcoord  : TEXCOORD0;
					float4 worldSpacePos : TEXCOORD1;
                };

                fixed4 _Color;

                v2f vert(appdata_t IN)
                {
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
				sampler2D _ColorMaskTexture;
                uniform float _EffectAmount;
				uniform float _PlayerPosX;
				uniform float _PlayerPosY;
                uniform float _Ray;
				float2 _Pixels;

				//float2 pp = float2(_PlayerPosX, _PlayerPosY);

                fixed4 frag(v2f IN) : COLOR{
					//float rnd = frac(sin(dot(IN.texcoord, float2(12.9898, 78.233))) * 43758.5453123);

					float x = 1;
					float d = distance(float2(_PlayerPosX, _PlayerPosY), (float2)IN.worldSpacePos);
					
					/*
					if (d < 1) {
						
					}
					*/

                    /*fixed stepFactor = step(_Ray, d);
                    x = lerp(0, 1, stepFactor);*/
                    
					//d = d - _Ray;
                    d = clamp(pow(d,2), 0, 5);

                    half4 texcol = tex2D(_MainTex, IN.texcoord);
					//float maskedPixelGrayscale = tex2D(_ColorMaskTexture, IN.texcoord).r; //questo dovrebbe campionare la maschera
					
                    texcol.rgb = lerp(texcol.rgb, dot(texcol.rgb, float3(0.3, 0.59, 0.11)), d/5);
                    texcol = texcol * IN.color;
                    return texcol;
                }
            ENDCG
            }
        }
            Fallback "Sprites/Default"
}
