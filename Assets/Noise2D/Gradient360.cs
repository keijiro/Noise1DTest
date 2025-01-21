using UnityEngine;
using Unity.Mathematics;
using NoiseTest;

namespace Noise2D {

public static class Gradient360
{
    public static float GetAt(float2 coord)
    {
        var i = (int2)coord;
        var uv = math.frac(coord).xyxy - math.float4(0, 0, 1, 1);
        var g1 = math.dot(Grad(i + math.int2(0, 0)), uv.xy);
        var g2 = math.dot(Grad(i + math.int2(1, 0)), uv.zy);
        var g3 = math.dot(Grad(i + math.int2(0, 1)), uv.xw);
        var g4 = math.dot(Grad(i + math.int2(1, 1)), uv.zw);
        return mathex.bilerp(g1, g2, g3, g4, mathex.smootherstep01(uv.xy));
    }

    static float2 Grad(int2 i2)
      => mathex.sincos(mathex.rand01(Hash(i2)) * math.PI * 2);

    static uint Hash(int2 i2)
      => (uint)i2.x | ((uint)i2.y << 16);
}

} // namespace Noise2D
