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
using LuaInterface;

namespace LuaInterface
{    
    public class LuaBeatEvent : IDisposable
    {
        protected LuaState m_LuaState;
        protected bool m_IsDisposed;

        LuaTable m_LuaTable = null;
        LuaFunction m_FunAdd = null;
        LuaFunction m_FunRemove = null;
        //LuaFunction _call = null;

        public LuaBeatEvent(LuaTable table)            
        {
            m_LuaTable = table;
            m_LuaState = table.GetLuaState();
            m_LuaTable.AddRef();

            m_FunAdd = m_LuaTable.GetLuaFunction("Add");
            m_FunRemove = m_LuaTable.GetLuaFunction("Remove");
            //_call = self.GetLuaFunction("__call");            
        }

        public void Dispose()
        {
            m_LuaTable.Dispose();
            m_FunAdd.Dispose();
            m_FunRemove.Dispose();
            //_call.Dispose();
            Clear();
        }

        void Clear()
        {
            //_call = null;
            m_FunAdd = null;
            m_FunRemove = null;
            m_LuaTable = null;
            m_LuaState = null;
        }

        public void Dispose(bool disposeManagedResources)
        {
            if (!m_IsDisposed)
            {
                m_IsDisposed = true;

                //if (_call != null)
                //{
                //    _call.Dispose(disposeManagedResources);
                //    _call = null;
                //}

                if (m_FunAdd != null)
                {
                    m_FunAdd.Dispose(disposeManagedResources);
                    m_FunAdd = null;
                }

                if (m_FunRemove != null)
                {
                    m_FunRemove.Dispose(disposeManagedResources);
                    m_FunRemove = null;
                }

                if (m_LuaTable != null)
                {
                    m_LuaTable.Dispose(disposeManagedResources);
                }

                Clear();
            }
        }

        public void Add(LuaFunction func, LuaTable obj)
        {
            if (func == null)
            {
                return;
            }

            m_FunAdd.BeginPCall();
            m_FunAdd.Push(m_LuaTable);
            m_FunAdd.Push(func);
            m_FunAdd.Push(obj);
            m_FunAdd.PCall();
            m_FunAdd.EndPCall();
        }

        public void Remove(LuaFunction func, LuaTable obj)
        {
            if (func == null)
            {
                return;
            }

            m_FunRemove.BeginPCall();
            m_FunRemove.Push(m_LuaTable);
            m_FunRemove.Push(func);
            m_FunRemove.Push(obj);
            m_FunRemove.PCall();
            m_FunRemove.EndPCall();
        }

        //public override int GetReference()
        //{
        //    return self.GetReference();
        //}
    }
}
