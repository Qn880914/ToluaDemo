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

namespace LuaInterface
{
    public class LuaObjectPool
    {        
        class PoolNode
        {
            public int index { get; set; }
            public object obj { get; set; }

            public PoolNode(int index, object obj)
            {
                this.index = index;
                this.obj = obj;
            }
        }

        private List<PoolNode> m_NodeList;
        //同lua_ref策略，0作为一个回收链表头，不使用这个位置
        private PoolNode m_NodeHead = null;   
        private int m_Count = 0;
        private int m_CollectStep = 2;
        private int m_CollectedIndex = -1;

        public LuaObjectPool()
        {
            m_NodeList = new List<PoolNode>(1024);
            m_NodeHead = new PoolNode(0, null);
            m_NodeList.Add(m_NodeHead);
            m_NodeList.Add(new PoolNode(1, null));
            m_Count = m_NodeList.Count;
        }

        public object this[int i]
        {
            get 
            {
                if (i > 0 && i < m_Count)
                {
                    return m_NodeList[i].obj;
                }

                return null;
            }
        }

        public void Clear()
        {
            m_NodeList.Clear();
            m_NodeHead = null;
            m_Count = 0;
        }

        public int Add(object obj)
        {
            int pos = -1;

            if (m_NodeHead.index != 0)
            {
                pos = m_NodeHead.index;
                m_NodeList[pos].obj = obj;
                m_NodeHead.index = m_NodeList[pos].index;
            }
            else
            {
                pos = m_NodeList.Count;
                m_NodeList.Add(new PoolNode(pos, obj));
                m_Count = pos + 1;
            }

            return pos;
        }

        public object TryGetValue(int index)
        {
            if (index > 0 && index < m_Count)
            {
                return m_NodeList[index].obj;                
            }
            
            return null;
        }

        public object Remove(int pos)
        {
            if (pos > 0 && pos < m_Count)
            {
                object o = m_NodeList[pos].obj;
                m_NodeList[pos].obj = null;
                m_NodeList[pos].index = m_NodeHead.index;
                m_NodeHead.index = pos;

                return o;
            }

            return null;
        }

        public object Destroy(int pos)
        {
            if (pos > 0 && pos < m_Count)
            {
                object o = m_NodeList[pos].obj;
                m_NodeList[pos].obj = null;
                return o;
            }

            return null;
        }

        public void StepCollect(Action<object, int> collectListener)
        {
            ++m_CollectedIndex;
            for (int i = 0; i < m_CollectStep; ++i)
            {
                m_CollectedIndex += i;
                if (m_CollectedIndex >= m_Count)
                {
                    m_CollectedIndex = -1;
                    return;
                }

                var node = m_NodeList[m_CollectedIndex];
                object o = node.obj;
                if (o != null && o.Equals(null))
                {
                    node.obj = null;
                    if (collectListener != null)
                    {
                        collectListener(o, m_CollectedIndex);
                    }
                }
            }
        }

        public object Replace(int pos, object o)
        {
            if (pos > 0 && pos < m_Count)
            {
                object obj = m_NodeList[pos].obj;
                m_NodeList[pos].obj = o;
                return obj;
            }

            return null;
        }
    }
}