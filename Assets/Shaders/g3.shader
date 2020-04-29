Shader "Unlit/g3"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_ColorTint("ColorTint", Color) = (1,0,0,1)

		_PlayerPosX("PlayerPosX", Float) = 0
		_PlayerPosY("PlayerPosY", Float) = 0

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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 worldSpacePos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v){
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.worldSpacePos = mul(unity_ObjectToWorld, v.vertex); //this should calculate the vertex position in (unity)world coordinates

                return o;
            }

			uniform float _PlayerPosX;
			uniform float _PlayerPosY;
			uniform float _ColorTint;


            fixed4 frag (v2f i) : SV_Target{
				float d = distance(float2(_PlayerPosX, _PlayerPosY), (float2) i.worldSpacePos);
				d = clamp(d, 0, 5);

				if (d < 2) {
					return _ColorTint;
				}
				else {
					return float4(0, 1, 0, 1);
				}

            }
            ENDCG
        }
    }
}
