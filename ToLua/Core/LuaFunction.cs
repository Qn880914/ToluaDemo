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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaInterface
{
    public class LuaFunction : LuaBaseRef
    {
        protected struct FuncData
        {
            public int oldTop;
            public int stackPos;

            public FuncData(int top, int stack)
            {
                oldTop = top;
                stackPos = stack;
            }
        }

        protected int oldTop = -1;
        private int argCount = 0;
        private int stackPos = -1;
        private Stack<FuncData> stack = new Stack<FuncData>();

        public LuaFunction(int reference, LuaState state)
        {
            this.m_Reference = reference;
            this.m_LuaState = state;
        }

        public override void Dispose()
        {
#if UNITY_EDITOR
            if (oldTop != -1 && m_Count <= 1)
            {
                Debugger.LogError("You must call EndPCall before calling Dispose");
            }
#endif
            base.Dispose();
        }

        public T ToDelegate<T>() where T : class
        {
            return DelegateTraits<T>.Create(this) as T;
        }

        public virtual int BeginPCall()
        {
            if (m_LuaState == null)
            {
                throw new LuaException("LuaFunction has been disposed");
            }

            stack.Push(new FuncData(oldTop, stackPos));
            oldTop = m_LuaState.BeginPCall(m_Reference);
            stackPos = -1;
            argCount = 0;
            return oldTop;
        }

        public void PCall()
        {
#if UNITY_EDITOR
            if (oldTop == -1)
            {
                Debugger.LogError("You must call BeginPCall before calling PCall");
            }
#endif

            stackPos = oldTop + 1;

            try
            {
                m_LuaState.PCall(argCount, oldTop);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void EndPCall()
        {
            if (oldTop != -1)
            {
                m_LuaState.EndPCall(oldTop);
                argCount = 0;
                FuncData data = stack.Pop();
                oldTop = data.oldTop;
                stackPos = data.stackPos;
            }
        }

        public void Call()
        {
            BeginPCall();
            PCall();
            EndPCall();
        }

        public void Call<T1>(T1 arg1)
        {
            BeginPCall();
            PushGeneric(arg1);
            PCall();
            EndPCall();
        }

        public void Call<T1, T2>(T1 arg1, T2 arg2)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PCall();
            EndPCall();
        }

        public void Call<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PCall();
            EndPCall();
        }

        public void Call<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PCall();
            EndPCall();
        }

        public void Call<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PCall();
            EndPCall();
        }

        public void Call<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PushGeneric(arg6);
            PCall();
            EndPCall();
        }

        public void Call<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PushGeneric(arg6);
            PushGeneric(arg7);
            PCall();
            EndPCall();
        }

        public void Call<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PushGeneric(arg6);
            PushGeneric(arg7);
            PushGeneric(arg8);
            PCall();
            EndPCall();
        }

        public void Call<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PushGeneric(arg6);
            PushGeneric(arg7);
            PushGeneric(arg8);
            PushGeneric(arg9);
            PCall();
            EndPCall();
        }

        public R1 Invoke<R1>()
        {
            BeginPCall();
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        public R1 Invoke<T1, R1>(T1 arg1)
        {
            BeginPCall();
            PushGeneric(arg1);
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        public R1 Invoke<T1, T2, R1>(T1 arg1, T2 arg2)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        public R1 Invoke<T1, T2, T3, R1>(T1 arg1, T2 arg2, T3 arg3)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        public R1 Invoke<T1, T2, T3, T4, R1>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        public R1 Invoke<T1, T2, T3, T4, T5, R1>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        public R1 Invoke<T1, T2, T3, T4, T5, T6, R1>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PushGeneric(arg6);
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        public R1 Invoke<T1, T2, T3, T4, T5, T6, T7, R1>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PushGeneric(arg6);
            PushGeneric(arg7);
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        public R1 Invoke<T1, T2, T3, T4, T5, T6, T7, T8, R1>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PushGeneric(arg6);
            PushGeneric(arg7);
            PushGeneric(arg8);
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        public R1 Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, R1>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            BeginPCall();
            PushGeneric(arg1);
            PushGeneric(arg2);
            PushGeneric(arg3);
            PushGeneric(arg4);
            PushGeneric(arg5);
            PushGeneric(arg6);
            PushGeneric(arg7);
            PushGeneric(arg8);
            PushGeneric(arg9);            
            PCall();
            R1 ret1 = CheckValue<R1>();
            EndPCall();
            return ret1;
        }

        //慎用, 有gc alloc
        [System.Obsolete("LuaFunction.LazyCall() is obsolete.Use LuaFunction.Invoke()")]
        public object[] LazyCall(params object[] args)
        {
            BeginPCall();
            int count = args == null ? 0 : args.Length;

            if (!m_LuaState.LuaCheckStack(count + 6))
            {
                EndPCall();
                throw new LuaException("stack overflow");
            }
            
            PushArgs(args);
            PCall();
            object[] objs = m_LuaState.CheckObjects(oldTop);
            EndPCall();
            return objs;
        }

        public void CheckStack(int args)
        {
            m_LuaState.LuaCheckStack(args + 6);
        }

        public bool IsBegin()
        {
            return oldTop != -1;
        }

        public void Push(double num)
        {
            m_LuaState.Push(num);
            ++argCount;
        }

        public void Push(int n)
        {
            m_LuaState.Push(n);
            ++argCount;
        }

        public void PushLayerMask(LayerMask n)
        {
            m_LuaState.PushLayerMask(n);
            ++argCount;
        }

        public void Push(uint un)
        {
            m_LuaState.Push(un);
            ++argCount;
        }

        public void Push(long num)
        {
            m_LuaState.Push(num);
            ++argCount;
        }

        public void Push(ulong un)
        {
            m_LuaState.Push(un);
            ++argCount;
        }

        public void Push(bool b)
        {
            m_LuaState.Push(b);
            ++argCount;
        }

        public void Push(string str)
        {
            m_LuaState.Push(str);
            ++argCount;
        }

        public void Push(IntPtr ptr)
        {
            m_LuaState.Push(ptr);
            ++argCount;
        }

        public void Push(LuaBaseRef lbr)
        {
            m_LuaState.Push(lbr);
            ++argCount;
        }

        public void Push(object o)
        {
            m_LuaState.PushVariant(o);
            ++argCount;
        }

        public void Push(UnityEngine.Object o)
        {
            m_LuaState.Push(o);
            ++argCount;
        }

        public void Push(Type t)
        {
            m_LuaState.Push(t);
            ++argCount;
        }

        public void Push(Enum e)
        {
            m_LuaState.Push(e);
            ++argCount;
        }

        public void Push(Array array)
        {
            m_LuaState.Push(array);
            ++argCount;
        }

        public void Push(Vector3 v3)
        {
            m_LuaState.Push(v3);
            ++argCount;
        }

        public void Push(Vector2 v2)
        {
            m_LuaState.Push(v2);
            ++argCount;
        }

        public void Push(Vector4 v4)
        {
            m_LuaState.Push(v4);
            ++argCount;
        }

        public void Push(Quaternion quat)
        {
            m_LuaState.Push(quat);
            ++argCount;
        }

        public void Push(Color clr)
        {
            m_LuaState.Push(clr);
            ++argCount;
        }

        public void Push(Ray ray)
        {
            try
            {
                m_LuaState.Push(ray);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void Push(Bounds bounds)
        {
            try
            {
                m_LuaState.Push(bounds);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void Push(RaycastHit hit)
        {
            try
            {
                m_LuaState.Push(hit);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void Push(Touch t)
        {
            try
            {
                m_LuaState.Push(t);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void Push(LuaByteBuffer buffer)
        {
            try
            {
                m_LuaState.Push(buffer);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void PushValue<T>(T value) where T : struct
        {
            try
            {
                m_LuaState.PushValue(value);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void PushObject(object o)
        {
            try
            {
                m_LuaState.PushObject(o);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void PushSealed<T>(T o)
        {
            try
            {
                m_LuaState.PushSealed(o);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void PushGeneric<T>(T t)
        {
            try
            {
                m_LuaState.PushGeneric(t);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public void PushArgs(object[] args)
        {
            if (args == null)
            {
                return;
            }

            argCount += args.Length;
            m_LuaState.PushArgs(args);
        }

        public void PushByteBuffer(byte[] buffer, int len = -1)
        {
            try
            {
                if (len == -1)
                {
                    len = buffer.Length;
                }

                m_LuaState.PushByteBuffer(buffer, len);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public double CheckNumber()
        {
            try
            {
                return m_LuaState.LuaCheckNumber(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public bool CheckBoolean()
        {
            try
            {
                return m_LuaState.LuaCheckBoolean(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public string CheckString()
        {
            try
            {
                return m_LuaState.CheckString(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public Vector3 CheckVector3()
        {
            try
            {
                return m_LuaState.CheckVector3(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public Quaternion CheckQuaternion()
        {
            try
            {
                return m_LuaState.CheckQuaternion(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public Vector2 CheckVector2()
        {
            try
            {
                return m_LuaState.CheckVector2(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public Vector4 CheckVector4()
        {
            try
            {
                return m_LuaState.CheckVector4(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public Color CheckColor()
        {
            try
            {
                return m_LuaState.CheckColor(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public Ray CheckRay()
        {
            try
            {
                return m_LuaState.CheckRay(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public Bounds CheckBounds()
        {
            try
            {
                return m_LuaState.CheckBounds(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public LayerMask CheckLayerMask()
        {
            try
            {
                return m_LuaState.CheckLayerMask(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public long CheckLong()
        {
            try
            {
                return m_LuaState.CheckLong(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public ulong CheckULong()
        {
            try
            {
                return m_LuaState.CheckULong(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public Delegate CheckDelegate()
        {
            try
            {
                return m_LuaState.CheckDelegate(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public object CheckVariant()
        {
            return m_LuaState.ToVariant(stackPos++);
        }

        public char[] CheckCharBuffer()
        {
            try
            {
                return m_LuaState.CheckCharBuffer(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public byte[] CheckByteBuffer()
        {
            try
            {
                return m_LuaState.CheckByteBuffer(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public object CheckObject(Type t)
        {
            try
            {
                return m_LuaState.CheckObject(stackPos++, t);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public LuaFunction CheckLuaFunction()
        {
            try
            {
                return m_LuaState.CheckLuaFunction(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public LuaTable CheckLuaTable()
        {
            try
            {
                return m_LuaState.CheckLuaTable(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public LuaThread CheckLuaThread()
        {
            try
            {
                return m_LuaState.CheckLuaThread(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        public T CheckValue<T>()
        {
            try
            {
                return m_LuaState.CheckValue<T>(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }
    }    
}
