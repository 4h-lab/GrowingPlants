Shader "Unlit/waterEffect2"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _WaveThickness("wavethickness", float) = .05
        _WaterOpacity("wateropacity", float) = .1
        _SurfaceWaveHeigth("Surface waves heigth", float) = .03
        _SurfaceWaveBprderThickness("Surface waves border thickness", float) = .01



        [HDR] _WaterPrimaryColor1("water primary Color", Color) = (0,0,1)
        [HDR] _WaterPrimaryColor2("water primary othery Color", Color) = (0,0,1)

        [HDR] _WaterSecondaryColor1("water secondary Color", Color) = (.44,1,1)



    }
        SubShader
        {
            Tags { "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
            }
            LOD 100

            Blend SrcAlpha OneMinusSrcAlpha
            zwrite off
            Cull Off


            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

                #include "UnityCG.cginc"


                float4 mod(float4 x, float4 y) {
                    return x - y * floor(x / y);
                }

                float4 mod289(float4 x) {
                    return x - floor(x / 289.0) * 289.0;
                }

                float4 permute(float4 x) {
                    return mod289(((x * 34.0) + 1.0) * x);
                }

                float4 taylorInvSqrt(float4 r) {
                    return (float4)1.79284291400159 - r * 0.85373472095314;
                }

                float2 fade(float2 t) {
                    return t * t * t * (t * (t * 6.0 - 15.0) + 10.0);
                }

                // Classic Perlin noise
                float cnoise(float2 P) {
                    float4 Pi = floor(P.xyxy) + float4(0.0, 0.0, 1.0, 1.0);
                    float4 Pf = frac(P.xyxy) - float4(0.0, 0.0, 1.0, 1.0);
                    Pi = mod289(Pi); // To avoid truncation effects in permutation
                    float4 ix = Pi.xzxz;
                    float4 iy = Pi.yyww;
                    float4 fx = Pf.xzxz;
                    float4 fy = Pf.yyww;

                    float4 i = permute(permute(ix) + iy);

                    float4 gx = frac(i / 41.0) * 2.0 - 1.0;
                    float4 gy = abs(gx) - 0.5;
                    float4 tx = floor(gx + 0.5);
                    gx = gx - tx;

                    float2 g00 = float2(gx.x, gy.x);
                    float2 g10 = float2(gx.y, gy.y);
                    float2 g01 = float2(gx.z, gy.z);
                    float2 g11 = float2(gx.w, gy.w);

                    float4 norm = taylorInvSqrt(float4(dot(g00, g00), dot(g01, g01), dot(g10, g10), dot(g11, g11)));
                    g00 *= norm.x;
                    g01 *= norm.y;
                    g10 *= norm.z;
                    g11 *= norm.w;

                    float n00 = dot(g00, float2(fx.x, fy.x));
                    float n10 = dot(g10, float2(fx.y, fy.y));
                    float n01 = dot(g01, float2(fx.z, fy.z));
                    float n11 = dot(g11, float2(fx.w, fy.w));

                    float2 fade_xy = fade(Pf.xy);
                    float2 n_x = lerp(float2(n00, n01), float2(n10, n11), fade_xy.x);
                    float n_xy = lerp(n_x.x, n_x.y, fade_xy.y);
                    return 2.3 * n_xy;
                }

                // Classic Perlin noise, periodic variant
                float pnoise(float2 P, float2 rep) {
                    float4 Pi = floor(P.xyxy) + float4(0.0, 0.0, 1.0, 1.0);
                    float4 Pf = frac(P.xyxy) - float4(0.0, 0.0, 1.0, 1.0);
                    Pi = mod(Pi, rep.xyxy); // To create noise with explicit period
                    Pi = mod289(Pi);        // To avoid truncation effects in permutation
                    float4 ix = Pi.xzxz;
                    float4 iy = Pi.yyww;
                    float4 fx = Pf.xzxz;
                    float4 fy = Pf.yyww;

                    float4 i = permute(permute(ix) + iy);

                    float4 gx = frac(i / 41.0) * 2.0 - 1.0;
                    float4 gy = abs(gx) - 0.5;
                    float4 tx = floor(gx + 0.5);
                    gx = gx - tx;

                    float2 g00 = float2(gx.x, gy.x);
                    float2 g10 = float2(gx.y, gy.y);
                    float2 g01 = float2(gx.z, gy.z);
                    float2 g11 = float2(gx.w, gy.w);

                    float4 norm = taylorInvSqrt(float4(dot(g00, g00), dot(g01, g01), dot(g10, g10), dot(g11, g11)));
                    g00 *= norm.x;
                    g01 *= norm.y;
                    g10 *= norm.z;
                    g11 *= norm.w;

                    float n00 = dot(g00, float2(fx.x, fy.x));
                    float n10 = dot(g10, float2(fx.y, fy.y));
                    float n01 = dot(g01, float2(fx.z, fy.z));
                    float n11 = dot(g11, float2(fx.w, fy.w));

                    float2 fade_xy = fade(Pf.xy);
                    float2 n_x = lerp(float2(n00, n01), float2(n10, n11), fade_xy.x);
                    float n_xy = lerp(n_x.x, n_x.y, fade_xy.y);
                    return 2.3 * n_xy;
                }





                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _WaveThickness;
                float _WaterOpacity;
                float _SurfaceWaveHeigth;
                float _SurfaceWaveBprderThickness;
                fixed4 _WaterPrimaryColor1;
                fixed4 _WaterPrimaryColor2;
                fixed4 _WaterSecondaryColor1;
                fixed4 _WaterSecondaryColor2;
                fixed4 _WaterSecondaryColor3;


                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // sample the texture
                    float4 col = tex2D(_MainTex, i.uv);
                    
                    float noise1d = (cnoise(float2(i.uv[0] * 10, _Time.y)) + 1) * .5;


                    float noise = (cnoise(i.uv * _SinTime.yy * 0.316) +
                    cnoise(i.uv * 5.17 * _CosTime.yy) +
                    cnoise(i.uv * .26 * _SinTime.yy) +
                    cnoise((float2(1, 1) - i.uv) * 4.712 * _CosTime.xx) +
                    cnoise((i.uv * .5) + float2(.5, .5) * 7.7 * _SinTime.xx) +
                    cnoise((i.uv * float2(.5, 1)) + float2(.5, 0) * 6.123 * _CosTime.xx)) *
                    0.1;



                   
                   if (noise > 0 && noise < _WaveThickness){
                            col = _WaterSecondaryColor1;
                            col.a = _WaterOpacity + 0.35;
                    }
                    else {
                        if (noise < 0 ) {
                            col = _WaterPrimaryColor2;
                            col.a = _WaterOpacity;
                        }
                        else {
                            col = _WaterPrimaryColor1;
                            col.a = _WaterOpacity;
                        }
                    }
                   noise1d = (noise1d * _SurfaceWaveHeigth) + (1- _SurfaceWaveHeigth);
                   
                   if (i.uv[1] > noise1d - _SurfaceWaveBprderThickness) {
                       col = _WaterSecondaryColor1;
                       col.a = _WaterOpacity + .35;
                   }
                   if (i.uv[1] > noise1d) {
                       col.a = 0;
                   }



                    return col;
                }
                ENDCG
            }
        }
}
