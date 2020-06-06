// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Gray"{
    Properties {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        [HDR] _UnColouredColor("Color before colouring", Color) = (.3, .59, .11)
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
                float4 _UnColouredColor;

				//float2 pp = float2(_PlayerPosX, _PlayerPosY);

                fixed4 frag(v2f IN) : COLOR{
					
                    half4 texcol = tex2D(_MainTex, IN.texcoord);
					//half maskcol = 1-tex2D(_ColorMaskTexture, IN.texcoord).r;
                    
                    half maskcol = ((1 - tex2D(_ColorMaskTexture, IN.texcoord).r) +
                        (1 - tex2D(_ColorMaskTexture, float2(IN.texcoord.x, IN.texcoord.y + 0.0213)).r) +
                        (1 - tex2D(_ColorMaskTexture, float2(IN.texcoord.x, IN.texcoord.y - 0.0213)).r) +
                        (1 - tex2D(_ColorMaskTexture, float2(IN.texcoord.x + 0.0213, IN.texcoord.y)).r) +
                        (1 - tex2D(_ColorMaskTexture, float2(IN.texcoord.x - 0.0213, IN.texcoord.y)).r)) * .2;
                    /*
                    float maskcol = 1- ((tex2D(_ColorMaskTexture, IN.texcoord).r) +
                        (tex2D(_ColorMaskTexture, float2(IN.texcoord.x, IN.texcoord.y + 0.0213)).r) +
                        (tex2D(_ColorMaskTexture, float2(IN.texcoord.x, IN.texcoord.y - 0.0213)).r) +
                        (tex2D(_ColorMaskTexture, float2(IN.texcoord.x + 0.0213, IN.texcoord.y)).r) +
                        (tex2D(_ColorMaskTexture, float2(IN.texcoord.x - 0.0213, IN.texcoord.y)).r) * .2);
                    */



                    texcol.rgb = lerp(texcol.rgb, dot(texcol.rgb, _UnColouredColor.rgb),maskcol);
					texcol = texcol * IN.color; // * (1 - maskcol.r);
                    return texcol;
                }
            ENDCG
            }
        }
            Fallback "Sprites/Default"
}
