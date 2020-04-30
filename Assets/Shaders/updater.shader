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
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				int _DaPointsCount = 20;
				float2 _DaPoints[20];
				float2 _DaRays[20];
				
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

            fixed4 frag (v2f IN) : COLOR{
				float d = 99999999;
				fixed4 col = tex2D(_MainTex, IN.texcoord);
				for (int i = 0; i < _DaPointsCount; i++) {
					float dist = max(distance(IN.texcoord, (float2)_DaPoints[i]) - _DaRays[i], 0);
					d = min(d, dist);
					//d = min(d, distance(IN.texcoord, (float2)_DaPoints[i]));
					//d = min(d, pow(distance(IN.texcoord, (float2)_DaPoints[i]), _DaRays[i]));
					//d = min(d, pow(distance(IN.texcoord, (float2)_DaPoints[i]), 5));
				}

				//fixed4 draw = _Color * (pow((1 - d), 3));
				fixed4 draw = _Color * (1 - d);
				return saturate(col+ draw);


				/*
                // sample the texture
                fixed4 col = tex2D(_MainTex, IN.texcoord);

				float2 __pos = float2(IN.texcoord.x * _SpriteScale.x, IN.texcoord.y * _SpriteScale.y)+ (float2)_SpritePos;
				//float2 __pos = (IN.texcoord) + (float2)_SpritePos;

				//float d = clamp(distance(__pos, _Point.xy), 0, 5);
				//d = d / 5;
				//float d = saturate(distance(__pos, _Point.xy));
				float d = _Ray - clamp(distance(__pos, _Point.xy), 0, _Ray);
				d = d / _Ray;
				d = pow(d, 2);
                //float d = clamp(distance(IN.worldSpacePos,_Point.xy),1,0);
				//float d = saturate(distance(IN.texcoord.xy, _Point.xy));
				//float d = saturate(distance(IN.worldSpacePos.xy, _Point.xy));

                fixed4 draw = _Color * (d *1);
                return saturate(col+draw);
				*/
            }
            ENDCG
        }
    }
}
