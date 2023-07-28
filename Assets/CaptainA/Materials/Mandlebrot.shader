
Shader "Mandlebrot/Mandlebrot"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Area("Area", vector) = (0, 0, 4, 4)
        _MaxIter("Iterations", range(4, 1000)) = 255
        _Angle("Angle", range(-3.1415, 3.1415)) = 0
        _Color("Color", range(0, 1)) = 0.5
        _Repeat("Repeat", float) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _Area;
            float _MaxIter;
            float _Angle;
            float _Color;
            float _Repeat;

            float2 rot(float2 p, float2 pivot, float a)
            {
                float s = sin(a); // sin of the angle
                float c = cos(a); // cos of the angle

                p -= pivot;
                p = float2(p.x * c - p.y * s, p.x * s + p.y * c);
                p += pivot;

                return p;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                //float uv = i.uv - 0.5;
                //uv = abs(uv);
                //float2 c = _Area.xy + uv*_Area.zw;
                float2 c = _Area.xy + (i.uv-0.5)*_Area.zw; // xyzw
                c = rot(c, _Area.xy, _Angle);

                float r = 20; // escape radius
                float r2 = r * r;

                float2 z;
                float2 zPrevious;
                float iter;
                for (iter = 0; iter < 255; iter++) {
                    zPrevious = rot(z, 0, _Time.y);
                    //zPrevious = z;
                    z = float2(z.x*z.x-z.y*z.y, 2*z.x*z.y) + c;
                    //if (length(z) > 2)
                    //    break;
                    if (dot(z, zPrevious) > r2)
                        break;
                }

                // Want to make the Mandelbrot inside black
                //if (iter > _MaxIter)
                //    return 0; // return black

                float dist = length(z); // distance fro origin
                float fracIter = (dist - r) / (r2 - r); // linear interpolation
                fracIter = log(dist) / log(r) - 1;
                //fracIter = log2(log(dist) / log(r)); //double exponential interpolation

                //iter -= fracIter;

                float m = sqrt(iter / _MaxIter);
                float j = 100; // change this number to see various different colors
                float4 col = sin(float4(0.3, 0.45, 0.65, 1) * m * j)* 0.5 + 0.5; // procedural colors
                col = tex2D(_MainTex, float2(m * _Repeat, _Color));
                //col = tex2D(_MainTex, float2(m * _Repeat + _Time.y, _Color)); // _Time.y can create animation w/ right texture
                
                float angle = atan2(z.x, z.y); // -pi and pi
                //if (i.uv.x > 0.5)
                col *= smoothstep(3, 0, fracIter);

                col *= 1+sin(angle * 2 + _Time.y*4) * 0.2;
                //col *= 1 + sin(angle * 2) * 0.2;
                return col;
            }
            ENDCG
        }
    }
}
