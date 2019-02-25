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
using System.Runtime.CompilerServices;

namespace LuaInterface
{
    public abstract class LuaBaseRef : IDisposable
    {
        protected int m_Reference = -1;
        protected LuaState m_LuaState;
        protected ObjectTranslator m_Translator = null;

        protected volatile bool m_IsDisposed;
        protected int m_Count = 0;

        public volatile bool alive = true;
        public string name { get; set; }

        public LuaBaseRef()
        {
            alive = true;
            m_Count = 1;
        }

        ~LuaBaseRef()
        {
            alive = false;
            Dispose(false);
        }

        public virtual void Dispose()
        {
            if (--m_Count > 0)
            {
                return;
            }

            alive = false;
            Dispose(true);
        }

        public void AddRef()
        {
            ++m_Count;
        }

        public virtual void Dispose(bool disposeManagedResources)
        {
            if (!m_IsDisposed)
            {
                m_IsDisposed = true;

                if (m_Reference > 0 && m_LuaState != null)
                {
                    m_LuaState.CollectRef(m_Reference, name, !disposeManagedResources);
                }

                m_Reference = -1;
                m_LuaState = null;
                m_Count = 0;
            }
        }

        //慎用
        public void Dispose(int generation)
        {
            if (m_Count > generation)
            {
                return;
            }

            Dispose(true);
        }

        public LuaState GetLuaState()
        {
            return m_LuaState;
        }

        public void Push()
        {
            m_LuaState.Push(this);
        }

        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }

        public virtual int GetReference()
        {
            return m_Reference;
        }

        public override bool Equals(object o)
        {
            if (o == null) return m_Reference <= 0;

            LuaBaseRef lr = o as LuaBaseRef;
            if (lr == null || lr.m_Reference != m_Reference)
            {
                return false;
            }

            return m_Reference > 0;
        }

        static bool CompareRef(LuaBaseRef a, LuaBaseRef b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            object l = a;
            object r = b;

            if (l == null && r != null)
            {
                return b.m_Reference <= 0;
            }

            if (l != null && r == null)
            {
                return a.m_Reference <= 0;
            }

            if (a.m_Reference != b.m_Reference)
            {
                return false;
            }

            return a.m_Reference > 0;
        }

        public static bool operator ==(LuaBaseRef a, LuaBaseRef b)
        {
            return CompareRef(a, b);
        }

        public static bool operator !=(LuaBaseRef a, LuaBaseRef b)
        {
            return !CompareRef(a, b);
        }
    }
}