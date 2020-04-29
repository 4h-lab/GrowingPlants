Shader "Unlit/updater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Point("Point", Vector) = (0,0,0,0)
		_Color("_Color",Color) = (1,0,0,0)
		_Ray("Ray",Float) = 0
		_SpritePos("Position", Vector) = (0,0,0,0)
		_SpriteScale("Scale", Vector) = (1,1,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

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

                

            sampler2D _MainTex;
            fixed4 _Point;
            float  _Ray;
			float4 _SpritePos;
			float4 _SpriteScale;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
				#ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

                OUT.worldSpacePos = mul(unity_ObjectToWorld, IN.vertex); //this should calculate the vertex position in (unity)world coordinates

                return OUT;
            }

            fixed4 frag (v2f IN) : COLOR
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, IN.texcoord);

				float2 __pos = (IN.texcoord * _SpriteScale) + (float2)_SpritePos;
				//float2 __pos = (IN.texcoord) + (float2)_SpritePos;

				//float d = clamp(distance(__pos, _Point.xy), 0, 5);
				//d = d / 5;
				//float d = saturate(distance(__pos, _Point.xy));
				float d = 2- clamp(distance(__pos, _Point.xy), 0, 2);
				d = d / 2;
				d = pow(d, 2);
                //float d = clamp(distance(IN.worldSpacePos,_Point.xy),1,0);
				//float d = saturate(distance(IN.texcoord.xy, _Point.xy));
				//float d = saturate(distance(IN.worldSpacePos.xy, _Point.xy));


                fixed4 draw = _Color * (d *1);
                return saturate(col+draw);
            }
            ENDCG
        }
    }
}
