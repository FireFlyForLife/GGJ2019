Shader "Unlit/RTShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma target 4.5
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			float4 _Positions[100];
			float4 _Colors[100];
			int _Size = 0;

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

			struct Ray
			{
				float3 O;
				float3 D;
			};

			struct HitData
			{
				bool hit;
				float3 col;
				float dist;
				float3 N;
				float3 P;
			};

			float4x4 rotationfloat4x4(float3 axis, float angle)
			{
				axis = normalize(axis);
				float s = sin(angle);
				float c = cos(angle);
				float oc = 1.0 - c;

				return float4x4(oc * axis.x * axis.x + c, oc * axis.x * axis.y - axis.z * s, oc * axis.z * axis.x + axis.y * s, 0.0,
					oc * axis.x * axis.y + axis.z * s, oc * axis.y * axis.y + c, oc * axis.y * axis.z - axis.x * s, 0.0,
					oc * axis.z * axis.x - axis.y * s, oc * axis.y * axis.z + axis.x * s, oc * axis.z * axis.z + c, 0.0,
					0.0, 0.0, 0.0, 1.0);
			}

			float3 GetBackgroundColor(Ray r)
			{
				return .5 * 1. - r.D.y * float3(2, 1, 1.);
			}

			struct Vertex
			{
				float3 pos;
				float3 color;
			};

			struct Cube
			{
				float3 pos;
				float3 col;
				Vertex vtx[36];
			};

			struct Sphere
			{
				float3 pos;
				float3 col;
				float r;
			};


			Cube GenerateCube(float3 pos, float angle, float3 axis, float3 col)
			{
				Cube cube;
				cube.pos = pos;
				cube.col = float3(1.0, 0.0, 0.0);

				float4x4 mat = rotationfloat4x4(axis, angle);
				cube.vtx[0].pos = mul(mat, float4(-0.5, -0.5, 0.5, 1.)).xyz;
				cube.vtx[1].pos = mul(mat, float4(0.5, 0.5, 0.5, 1.)).xyz;
				cube.vtx[2].pos = mul(mat, float4(0.5, -0.5, 0.5, 1.)).xyz;
				cube.vtx[3].pos = mul(mat, float4(-0.5, -0.5, 0.5, 1.)).xyz;
				cube.vtx[4].pos = mul(mat, float4(-0.5, 0.5, 0.5, 1.)).xyz;
				cube.vtx[5].pos = mul(mat, float4(0.5, 0.5, 0.5, 1.)).xyz;

				cube.vtx[6].pos = mul(mat, float4(-0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[7].pos = mul(mat, float4(0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[8].pos = mul(mat, float4(0.5, 0.5, -0.5, 1.)).xyz;
				cube.vtx[9].pos = mul(mat, float4(-0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[10].pos = mul(mat,  float4(0.5, 0.5, -0.5, 1.)).xyz;
				cube.vtx[11].pos = mul(mat,  float4(-0.5, 0.5, -0.5, 1.)).xyz;

				cube.vtx[12].pos = mul(mat, float4(0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[14].pos = mul(mat, float4(0.5, 0.5, -0.5, 1.)).xyz;
				cube.vtx[13].pos = mul(mat, float4(0.5, 0.5, 0.5, 1.)).xyz;
				cube.vtx[15].pos = mul(mat, float4(0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[17].pos = mul(mat, float4(0.5, 0.5, 0.5, 1.)).xyz;
				cube.vtx[16].pos = mul(mat, float4(0.5, -0.5, 0.5, 1.)).xyz;

				cube.vtx[18].pos = mul(mat, float4(-0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[19].pos = mul(mat, float4(-0.5, 0.5, -0.5, 1.)).xyz;
				cube.vtx[20].pos = mul(mat, float4(-0.5, 0.5, 0.5, 1.)).xyz;
				cube.vtx[21].pos = mul(mat, float4(-0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[22].pos = mul(mat, float4(-0.5, 0.5, 0.5, 1.)).xyz;
				cube.vtx[23].pos = mul(mat, float4(-0.5, -0.5, 0.5, 1.)).xyz;

				cube.vtx[24].pos = mul(mat, float4(-0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[26].pos = mul(mat, float4(0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[25].pos = mul(mat, float4(0.5, -0.5, 0.5, 1.)).xyz;
				cube.vtx[27].pos = mul(mat, float4(-0.5, -0.5, -0.5, 1.)).xyz;
				cube.vtx[29].pos = mul(mat, float4(0.5, -0.5, 0.5, 1.)).xyz;
				cube.vtx[28].pos = mul(mat, float4(-0.5, -0.5, 0.5, 1.)).xyz;

				cube.vtx[30].pos = mul(mat, float4(-0.5, 0.5, -0.5, 1.)).xyz;
				cube.vtx[31].pos = mul(mat, float4(0.5, 0.5, -0.5, 1.)).xyz;
				cube.vtx[32].pos = mul(mat, float4(0.5, 0.5, 0.5, 1.)).xyz;
				cube.vtx[33].pos = mul(mat, float4(-0.5, 0.5, -0.5, 1.)).xyz;
				cube.vtx[34].pos = mul(mat, float4(0.5, 0.5, 0.5, 1.)).xyz;
				cube.vtx[35].pos = mul(mat, float4(-0.5, 0.5, 0.5, 1.)).xyz;

				cube.vtx[0].color = col;
				cube.vtx[1].color = col;
				cube.vtx[2].color = col;
				cube.vtx[3].color = col;
				cube.vtx[4].color = col;
				cube.vtx[5].color = col;

				cube.vtx[6].color = col;
				cube.vtx[7].color = col;
				cube.vtx[8].color = col;
				cube.vtx[9].color = col;
				cube.vtx[10].color =col;
				cube.vtx[11].color =col;

				cube.vtx[12].color = col;
				cube.vtx[13].color = col;
				cube.vtx[14].color = col;
				cube.vtx[15].color = col;
				cube.vtx[16].color = col;
				cube.vtx[17].color = col;

				cube.vtx[18].color = col;
				cube.vtx[19].color = col;
				cube.vtx[20].color = col;
				cube.vtx[21].color = col;
				cube.vtx[22].color = col;
				cube.vtx[23].color = col;

				cube.vtx[24].color = col;
				cube.vtx[25].color = col;
				cube.vtx[26].color = col;
				cube.vtx[27].color = col;
				cube.vtx[28].color = col;
				cube.vtx[29].color = col;

				cube.vtx[30].color = col;
				cube.vtx[31].color = col;
				cube.vtx[32].color = col;
				cube.vtx[33].color = col;
				cube.vtx[34].color = col;
				cube.vtx[35].color = col;
				return cube;
			}

			HitData Intersect(Vertex vertices[3], float3 objectCenter, Ray r, float maxDist)
			{
				float3 A = vertices[0].pos + objectCenter;
				float3 B = vertices[1].pos + objectCenter;
				float3 C = vertices[2].pos + objectCenter;
				float3 N = cross(B - A, C - A);

				HitData hd;
				hd.hit = false;

				// don't render flipped triangles
				if (dot(r.D, N) < 0.)
					return hd;

				float3 TC = (A + B + C) / 3.;
				float t = dot(TC - r.O, N) / dot(r.D, N);

				if (t >= maxDist || t < 0.)
					return hd;

				float3 P = r.O + r.D * t;
				float area = length(N)*.5;

				//t1
				float u = length(cross(A - P, B - P)) / 2. / area;
				float v = length(cross(B - P, C - P)) / 2. / area;
				float w = length(cross(C - P, A - P)) / 2. / area;

				float x = abs(u + v + w - 1.);
				if (u + v + w - 1. > 0.0001)
					return hd;

				float3 color0 = vertices[0].color;
				color0 = float3(v * color0.x, v * color0.y, v * color0.z);

				float3 color1 = vertices[1].color;
				color1 = float3(w * color1.x, w * color1.y, w * color1.z);

				float3 color2 = vertices[2].color;
				color2 = float3(u * color2.x, u * color2.y, u * color2.z);

				hd.col = color0 + color1 + color2;
				hd.hit = true;
				hd.dist = t;
				hd.N = N;
				hd.P = P;
				return hd;
			}

			HitData IntersectFloor(Ray r, float maxDist)
			{
				HitData hd;
				float3 PC = float3(0, -1, 0);
				float3 N = normalize(float3(0, -1, 0));
				hd.hit = false;

				if (dot(r.D, N) < 0.)
					return hd;

				float t = dot(PC - r.O, N) / dot(r.D, N);

				if (t < maxDist)
				{
					hd.hit = true;
					hd.dist = t;
					hd.N = N;

					float3 P = r.O + r.D * t;
					if (fmod(floor(abs(P.x)) + floor(abs(P.z)), 2.) < 1.)
					{
						hd.col = float3(1,1,1);
					}
					else hd.col = float3(.6,.6,.6);
				}
				return hd;
			}

			HitData IntersectCube(Cube c, Ray r, float maxDist)
			{
				HitData hd;
				hd.hit = false;
				hd.dist = maxDist;
				hd.N = float3(0, 0, 0);
				hd.P = float3(0, 0, 0);
				for (int i = 0; i < 36; i += 3)
				{
					Vertex vtx[3];
					vtx[0] = c.vtx[i];
					vtx[1] = c.vtx[i + 1];
					vtx[2] = c.vtx[i + 2];

					HitData d = Intersect(vtx, c.pos, r, hd.dist);
					if (d.hit)
					{
						hd.dist = d.dist;
						hd.col = d.col;
						hd.hit = d.hit;
						hd.N = d.N;
					}
				}
				return hd;
			}

			HitData IntersectSphere(Sphere s, Ray r, float maxDist)
			{
				HitData hd;
				hd.hit = false;
				hd.dist = maxDist;
				hd.N = float3(0, 0, 0);
				hd.P = float3(0, 0, 0);
				hd.col = float3(0, 0, 0);

				float3 sc = s.pos - r.O;
				float t = dot(sc, r.D);
				float3 rsc = r.O + r.D * t;

				// sphere center to ray
				float y = length(sc - rsc);

				if (y <= s.r)
				{
					float dist = sqrt(s.r * s.r - y * y);
					float t1 = t - dist;

					// prevent rounding errors
					//if (t1 < maxDist && t1 > 0.)
					{
						hd.P = r.O + r.D * t1;
						hd.N = normalize(s.pos - hd.P);
						hd.col = s.col;
						hd.hit = true;
						hd.dist = t1;
					}

				}
				return hd;
			}

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _CamRotation;
			float3 _CamPosition;

			RWStructuredBuffer<float> _OutBuffer : register(u1);
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float time = _Time * 5;
				Ray r;
				r.O = float3(0,0,0);
				r.D = normalize(float3(i.uv - float2(.5,.5), 1.0f));

				/*float4x4 matX = rotationfloat4x4(float3(0, 1., 0), -time - 9.4);
				float x = sin(-time) * 10. + _Time;
				float y = 0.0;
				float z = cos(-time) * 10. + _Time;*/
				float4x4 matX = rotationfloat4x4(float3(0, 1, 0), _CamRotation);
				r.O = _CamPosition;
				r.D = mul(matX, float4(r.D.xyz, 1.)).xyz;

				float3 col = float3(0,0,0);

				float3 L = normalize(r.D + float3(0, -.25, 0));

				float maxDist = 99999999.;

				Cube cubes[10];
				for (int i = 0; i < _Size; ++i)
				{
					if(_Colors[i].x + _Colors[i].y + _Colors[i].z > 0.)
					cubes[i] = GenerateCube(_Positions[i], time, float3(1, 1, 10), _Colors[i].xyz);
					else cubes[i] = GenerateCube(_Positions[i], 0 + i * 5, float3(1, 1, 10), _Colors[i].xyz);
				}

				float3 pos = float3(0, 0, 0);
				HitData hitData;
				hitData.hit = false;
				hitData.dist = maxDist;
				float3 closestOrigin = r.O;
				float3 closestDirection = r.D;
				int closestIdx;
				for (int i = 0; i < _Size; ++i)
				{
					HitData hd;
					hd.dist = hitData.dist;

					hd = IntersectCube(cubes[i], r, hd.dist);
					if (hd.hit)
					{
						hitData = hd;
						closestOrigin = hd.P;
						closestDirection = reflect(r.D, hd.N);
						closestIdx = i;
					}
				}
				if (hitData.hit) 
				{
					if (hitData.col.x + hitData.col.y + hitData.col.z == 0)
					{
						r.O = closestOrigin;
						r.D = closestDirection;

						HitData hd2;
						hd2.hit = false;
						hd2.dist = maxDist;
						for (int i = 0; i < _Size; ++i) {
							HitData hd3;
							hd3.dist = hd2.dist;
							hd3.hit = false;
							if (closestIdx == i) continue;
							hd3 = IntersectCube(cubes[i], r, hd3.dist);
							if (hd3.hit) {
								hd2 = hd3;
							}
						}
						if (hd2.hit) {
							_OutBuffer[0] += hd2.col.r;
							_OutBuffer[1] += hd2.col.g;
							_OutBuffer[2] += hd2.col.b;

							col = hd2.col /* dot(hd2.N, L)*/;
						}
						else {
							hd2 = IntersectFloor(r, hd2.dist);
							if (hd2.hit) col = hd2.col * dot(hd2.N, L);
							else col = GetBackgroundColor(r);
						}
					}
					else col = hitData.col * dot(hitData.N,L);
				} 
				else {
					hitData = IntersectFloor(r, hitData.dist);
					if (!hitData.hit) col = GetBackgroundColor(r);
					else col = hitData.col * dot(hitData.N, L);
				}

				//if (col.r > 0.8)
				//	_OutBuffer[0] = 1.0;
				//if (col.g > 0.8)
				//	_OutBuffer[1] = 1.0;
				//if (col.b > 0.8)
				//	_OutBuffer[2] = 1.0;
				return float4(col.rgb,1);
			}
			ENDCG
		}
	}
}
