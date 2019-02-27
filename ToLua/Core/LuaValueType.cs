/*
Copyright (c) 2015-2017 topameng(topameng@qq.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace LuaInterface
{
    public partial struct LuaValueType
    {
        public const int none = 0;
        public const int vector3 = 1;
        public const int quaternion = 2;
        public const int vector2 = 3;
        public const int color = 4;
        public const int vector4 = 5;
        public const int ray = 6;
        public const int bounds = 7;
        public const int touch = 8;
        public const int layerMask = 9;
        public const int raycastHit = 10;
        public const int int64 = 11;
        public const int uint64 = 12;
        public const int max = 64;

        private int m_Type;

        public LuaValueType(int value)
        {
            m_Type = value;
        }

        public static implicit operator int(LuaValueType mask)
        {
            return mask.m_Type;
        }

        public static implicit operator LuaValueType(int intVal)
        {
            return new LuaValueType(intVal);
        }

        public override string ToString()
        {
            return LuaValueTypeName.Get(m_Type);
        }
    }

    public static class LuaValueTypeName
    {
        public static string[] names = new string[LuaValueType.max];

        static LuaValueTypeName()
        {
            names[LuaValueType.none] = "None";
            names[LuaValueType.vector3] = "Vector3";
            names[LuaValueType.quaternion] = "Quaternion";
            names[LuaValueType.vector2] = "Vector2";
            names[LuaValueType.color] = "Color";
            names[LuaValueType.vector4] = "Vector4";
            names[LuaValueType.ray] = "Ray";
            names[LuaValueType.bounds] = "Bounds";
            names[LuaValueType.touch] = "Touch";
            names[LuaValueType.layerMask] = "LayerMask";
            names[LuaValueType.raycastHit] = "RaycastHit";
        }

        static public string Get(int type)
        {
            if (type >= 0 && type < LuaValueType.max)
            {
                return names[type];
            }

            return "UnKnownType:" + ConstStringTable.GetNumIntern(type);
        }
    }
}
