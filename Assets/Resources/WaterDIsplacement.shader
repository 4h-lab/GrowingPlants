Shader "Hidden/WaterDIsplacement"{


    Properties{
        _MaxY ("MaxY", Float) = 0
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vertdefault
            #pragma fragment frag

            #include "UnityCG.cginc"
                            

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 worldSpacePos : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldSpacePos : TEXCOORD1;
            };

            v2f vert (appdata v){
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldSpacePos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target{
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                col.rgb = lerp(col.rgb, float3(1, 0, 1), 0.3);



                return col;
            }
            ENDHLSL
        }
    }
}
