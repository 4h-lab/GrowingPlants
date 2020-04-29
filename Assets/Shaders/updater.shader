Shader "Unlit/updater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Point("Point", Vector) = (0,0,0,0)
        _Color("_Color",Color) = (1,0,0,0)
        _Ray("Ray",Float) = 0
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
                float d = pow(saturate(distance(IN.texcoord.xy,_Point.xy)),2);
                
                fixed4 draw = _Color * (d *1);
                return saturate(col+draw);
            }
            ENDCG
        }
    }
}
