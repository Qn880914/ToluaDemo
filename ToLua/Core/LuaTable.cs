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
using System.Collections;
using System.Collections.Generic;

namespace LuaInterface
{
    public class LuaTable : LuaBaseRef
    {        
        public LuaTable(int reference, LuaState state)
        {
            this.m_Reference = reference;
            this.m_LuaState = state;            
        }

        public object this[string key]
        {
            get
            {
                int top = m_LuaState.LuaGetTop();

                try
                {
                    m_LuaState.Push(this);
                    m_LuaState.Push(key);
                    m_LuaState.LuaGetTable(top + 1);
                    object ret = m_LuaState.ToVariant(top + 2);
                    m_LuaState.LuaSetTop(top);
                    return ret;
                }
                catch (Exception e)
                {
                    m_LuaState.LuaSetTop(top);
                    throw e;                    
                }                
            }

            set
            {
                int top = m_LuaState.LuaGetTop();

                try
                {
                    m_LuaState.Push(this);
                    m_LuaState.Push(key);
                    m_LuaState.PushVariant(value);
                    m_LuaState.LuaSetTable(top + 1);
                    m_LuaState.LuaSetTop(top);
                }
                catch (Exception e)
                {
                    m_LuaState.LuaSetTop(top);
                    throw e;
                }
            }
        }

        public object this[int key]
        {
            get
            {
                int oldTop = m_LuaState.LuaGetTop();

                try
                {
                    m_LuaState.Push(this);
                    m_LuaState.LuaRawGetI(oldTop + 1, key);
                    object obj = m_LuaState.ToVariant(oldTop + 2);
                    m_LuaState.LuaSetTop(oldTop);
                    return obj;
                }
                catch (Exception e)
                {
                    m_LuaState.LuaSetTop(oldTop);
                    throw e;
                }
            }

            set
            {
                int oldTop = m_LuaState.LuaGetTop();

                try
                {
                    m_LuaState.Push(this);
                    m_LuaState.PushVariant(value);
                    m_LuaState.LuaRawSetI(oldTop + 1, key);
                    m_LuaState.LuaSetTop(oldTop);
                }
                catch (Exception e)
                {
                    m_LuaState.LuaSetTop(oldTop);
                    throw e;
                }
            }
        }

        public int Length
        {
            get
            {
                m_LuaState.Push(this);
                int n = m_LuaState.LuaObjLen(-1);
                m_LuaState.LuaPop(1);
                return n;
            }
        }

        public T RawGetIndex<T>(int index)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                m_LuaState.LuaRawGetI(top + 1, index);
                T ret = m_LuaState.CheckValue<T>(top + 2);
                m_LuaState.LuaSetTop(top);
                return ret;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public void RawSetIndex<T>(int index, T value)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                m_LuaState.PushGeneric(value);
                m_LuaState.LuaRawSetI(top + 1, index);
                m_LuaState.LuaSetTop(top);                
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public V RawGet<K, V>(K key)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                m_LuaState.PushGeneric(key);
                m_LuaState.LuaRawGet(top + 1);
                V ret = m_LuaState.CheckValue<V>(top + 2);
                m_LuaState.LuaSetTop(top);
                return ret;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public void RawSet<K, V>(K key, V arg)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                m_LuaState.PushGeneric(key);
                m_LuaState.PushGeneric(arg);
                m_LuaState.LuaRawSet(top + 1);
                m_LuaState.LuaSetTop(top);
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public T GetTable<T>(string key)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                m_LuaState.Push(key);
                m_LuaState.LuaGetTable(top + 1);
                T ret = m_LuaState.CheckValue<T>(top + 2);
                m_LuaState.LuaSetTop(top);
                return ret;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public void SetTable<T>(string key, T arg)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                m_LuaState.Push(key);
                m_LuaState.PushGeneric(arg);
                m_LuaState.LuaSetTable(top + 1);
                m_LuaState.LuaSetTop(top);
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public LuaFunction RawGetLuaFunction(string key)
        {            
            int top = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                m_LuaState.Push(key);
                m_LuaState.LuaRawGet(top + 1);
                LuaFunction func = m_LuaState.CheckLuaFunction(top + 2);
                m_LuaState.LuaSetTop(top);
#if UNITY_EDITOR
                if (func != null)
                {
                    func.name = name + "." + key;
                }
#endif
                return func;
            }
            catch(Exception e)            
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public LuaFunction GetLuaFunction(string key)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                m_LuaState.Push(key);
                m_LuaState.LuaGetTable(top + 1);
                LuaFunction func = m_LuaState.CheckLuaFunction(top + 2);
                m_LuaState.LuaSetTop(top);
#if UNITY_EDITOR
                if (func != null)
                {
                    func.name = name + "." + key;
                }
#endif
                return func;
            }
            catch(Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        bool BeginCall(string name, int top)
        {
            m_LuaState.Push(this);
            m_LuaState.ToLuaPushTraceback();
            m_LuaState.Push(name);
            m_LuaState.LuaGetTable(top + 1);
            return m_LuaState.LuaType(top + 3) == LuaTypes.LUA_TFUNCTION;
        }

        public void Call(string name)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {                
                if (BeginCall(name, top))
                {
                    m_LuaState.Call(0, top + 2, top);                    
                }

                m_LuaState.LuaSetTop(top);                
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public void Call<T1>(string name, T1 arg1)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.Call(1, top + 2, top);
                }

                m_LuaState.LuaSetTop(top);
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public void Call<T1, T2>(string name, T1 arg1, T2 arg2)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.Call(2, top + 2, top);
                }

                m_LuaState.LuaSetTop(top);
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public void Call<T1, T2, T3>(string name, T1 arg1, T2 arg2, T3 arg3)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.PushGeneric(arg3);
                    m_LuaState.Call(3, top + 2, top);
                }

                m_LuaState.LuaSetTop(top);
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public void Call<T1, T2, T3, T4>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.PushGeneric(arg3);
                    m_LuaState.PushGeneric(arg4);
                    m_LuaState.Call(4, top + 2, top);
                }

                m_LuaState.LuaSetTop(top);
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public void Call<T1, T2, T3, T4, T5>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.PushGeneric(arg3);
                    m_LuaState.PushGeneric(arg4);
                    m_LuaState.PushGeneric(arg5);
                    m_LuaState.Call(5, top + 2, top);
                }

                m_LuaState.LuaSetTop(top);
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public void Call<T1, T2, T3, T4, T5, T6>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.PushGeneric(arg3);
                    m_LuaState.PushGeneric(arg4);
                    m_LuaState.PushGeneric(arg5);
                    m_LuaState.PushGeneric(arg6);
                    m_LuaState.Call(6, top + 2, top);
                }

                m_LuaState.LuaSetTop(top);
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public R1 Invoke<R1>(string name)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                R1 ret1 = default(R1);

                if (BeginCall(name, top))
                {
                    m_LuaState.Call(0, top + 2, top);
                    ret1 = m_LuaState.CheckValue<R1>(top + 3);
                }

                m_LuaState.LuaSetTop(top);
                return ret1;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public R1 Invoke<T1, R1>(string name, T1 arg1)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                R1 ret1 = default(R1);

                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.Call(1, top + 2, top);
                    ret1 = m_LuaState.CheckValue<R1>(top + 3);
                }

                m_LuaState.LuaSetTop(top);
                return ret1;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public R1 Invoke<T1, T2, R1>(string name, T1 arg1, T2 arg2)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                R1 ret1 = default(R1);

                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.Call(2, top + 2, top);
                    ret1 = m_LuaState.CheckValue<R1>(top + 3);
                }

                m_LuaState.LuaSetTop(top);
                return ret1;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public R1 Invoke<T1, T2, T3, R1>(string name, T1 arg1, T2 arg2, T3 arg3)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                R1 ret1 = default(R1);

                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.PushGeneric(arg3);
                    m_LuaState.Call(3, top + 2, top);
                    ret1 = m_LuaState.CheckValue<R1>(top + 3);
                }

                m_LuaState.LuaSetTop(top);
                return ret1;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public R1 Invoke<T1, T2, T3, T4, R1>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                R1 ret1 = default(R1);

                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.PushGeneric(arg3);
                    m_LuaState.PushGeneric(arg4);
                    m_LuaState.Call(4, top + 2, top);
                    ret1 = m_LuaState.CheckValue<R1>(top + 3);
                }

                m_LuaState.LuaSetTop(top);
                return ret1;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public R1 Invoke<T1, T2, T3, T4, T5, R1>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                R1 ret1 = default(R1);

                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.PushGeneric(arg3);
                    m_LuaState.PushGeneric(arg4);
                    m_LuaState.PushGeneric(arg5);
                    m_LuaState.Call(5, top + 2, top);
                    ret1 = m_LuaState.CheckValue<R1>(top + 3);
                }

                m_LuaState.LuaSetTop(top);
                return ret1;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public R1 Invoke<T1, T2, T3, T4, T5, T6, R1>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            int top = m_LuaState.LuaGetTop();

            try
            {
                R1 ret1 = default(R1);

                if (BeginCall(name, top))
                {
                    m_LuaState.PushGeneric(arg1);
                    m_LuaState.PushGeneric(arg2);
                    m_LuaState.PushGeneric(arg3);
                    m_LuaState.PushGeneric(arg4);
                    m_LuaState.PushGeneric(arg5);
                    m_LuaState.PushGeneric(arg6);
                    m_LuaState.Call(6, top + 2, top);
                    ret1 = m_LuaState.CheckValue<R1>(top + 3);
                }

                m_LuaState.LuaSetTop(top);
                return ret1;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(top);
                throw e;
            }
        }

        public string GetStringField(string name)
        {
            int oldTop = m_LuaState.LuaGetTop();
     
            try
            {
                m_LuaState.Push(this);
                m_LuaState.LuaGetField(oldTop + 1, name);
                string str = m_LuaState.CheckString(-1);
                m_LuaState.LuaSetTop(oldTop);
                return str;
            }
            catch(LuaException e)
            {
                m_LuaState.LuaSetTop(oldTop);
                throw e;
            }
        }

        public void AddTable(string name)
        {
            int oldTop = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                m_LuaState.Push(name);
                m_LuaState.LuaCreateTable();
                m_LuaState.LuaRawSet(oldTop + 1);
                m_LuaState.LuaSetTop(oldTop);
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(oldTop);
                throw e;
            }
        }

        public object[] ToArray()
        {
            int oldTop = m_LuaState.LuaGetTop();

            try
            {
                m_LuaState.Push(this);
                int len = m_LuaState.LuaObjLen(-1);
                List<object> list = new List<object>(len + 1);
                int index = 1;
                object obj = null;

                while(index <= len)
                {
                    m_LuaState.LuaRawGetI(-1, index++);
                    obj = m_LuaState.ToVariant(-1);
                    m_LuaState.LuaPop(1);
                    list.Add(obj);
                }

                m_LuaState.LuaSetTop(oldTop);
                return list.ToArray();
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(oldTop);
                throw e;
            }
        }

        public override string ToString()
        {
            m_LuaState.Push(this);
            IntPtr p = m_LuaState.LuaToPointer(-1);
            m_LuaState.LuaPop(1);
            return string.Format("table:0x{0}", p.ToString("X"));            
        }

        public LuaArrayTable ToArrayTable()
        {            
            return new LuaArrayTable(this);
        }

        public LuaDictTable ToDictTable()
        {
            return new LuaDictTable(this);
        }        

        public LuaDictTable<K, V> ToDictTable<K, V>()
        {
            return new LuaDictTable<K, V>(this);
        }

        public LuaTable GetMetaTable()
        {            
            int oldTop = m_LuaState.LuaGetTop();

            try
            {
                LuaTable t = null;
                m_LuaState.Push(this);

                if (m_LuaState.LuaGetMetaTable(-1) != 0)
                {
                    t = m_LuaState.CheckLuaTable(-1);
                }

                m_LuaState.LuaSetTop(oldTop);
                return t;
            }
            catch (Exception e)
            {
                m_LuaState.LuaSetTop(oldTop);
                throw e;
            }
        }
    }

    public class LuaArrayTable : IDisposable, IEnumerable<object>
    {       
        private LuaTable table = null;
        private LuaState state = null;

        public LuaArrayTable(LuaTable table)           
        {
            table.AddRef();
            this.table = table;            
            this.state = table.GetLuaState();
        }

        public void Dispose()
        {
            if (table != null)
            {
                table.Dispose();
                table = null;
            }
        }

        public int Length
        {
            get
            {
                return table.Length;
            }
        }

        public object this[int key]
        {
            get
            {
                return table[key];
            }
            set 
            {
                table[key] = value;
            }
        }

        public void ForEach(Action<object> action)
        {
            using (var iter = this.GetEnumerator())
            {
                while (iter.MoveNext())
                {
                    action(iter.Current);
                }                
            }
        }

        public IEnumerator<object> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class Enumerator : IEnumerator<object>
        {            
            LuaState state;
            int index = 1;
            object current = null;
            int top = -1;

            public Enumerator(LuaArrayTable list)
            {                
                state = list.state;
                top = state.LuaGetTop();
                state.Push(list.table);                
            }

            public object Current
            {
                get
                {
                    return current;
                }
            }

            public bool MoveNext()
            {
                state.LuaRawGetI(-1, index);
                current = state.ToVariant(-1);
                state.LuaPop(1);
                ++index;
                return current == null ? false : true;
            }

            public void Reset()
            {
                index = 1;
                current = null;
            }

            public void Dispose()
            {
                if (state != null)
                {
                    state.LuaSetTop(top);
                    state = null;
                }
            }
        }
    }

    public class LuaDictTable : IDisposable, IEnumerable<DictionaryEntry>
    {
        LuaTable table;
        LuaState state;

        public LuaDictTable(LuaTable table)            
        {
            table.AddRef();
            this.table = table;
            this.state = table.GetLuaState() ;
        }

        public void Dispose()
        {
            if (table != null)
            {
                table.Dispose();
                table = null;
            }
        }

        public object this[string key]
        {
            get
            {
                return table[key];
            }

            set
            {
                table[key] = value;
            }
        }

        public Hashtable ToHashtable()
        {
            Hashtable hash = new Hashtable();
            var iter = GetEnumerator();

            while (iter.MoveNext())
            {
                hash.Add(iter.Current.Key, iter.Current.Value);                
            }

            iter.Dispose();
            return hash;
        }

        public IEnumerator<DictionaryEntry> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class Enumerator : IEnumerator<DictionaryEntry>
        {            
            LuaState state;                        
            DictionaryEntry current = new DictionaryEntry();
            int top = -1;

            public Enumerator(LuaDictTable list)
            {                
                state = list.state;
                top = state.LuaGetTop();
                state.Push(list.table);
                state.LuaPushNil();                
            }

            public DictionaryEntry Current
            {
                get 
                {
                    return current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public bool MoveNext()
            {
                if (state.LuaNext(-2))
                {
                    current = new DictionaryEntry();
                    current.Key = state.ToVariant(-2);
                    current.Value = state.ToVariant(-1);
                    state.LuaPop(1);
                    return true;
                }
                else
                {
                    current = new DictionaryEntry();
                    return false;
                }                
            }

            public void Reset()
            {
                current = new DictionaryEntry();
            }

            public void Dispose()
            {
                if (state != null)
                {
                    state.LuaSetTop(top);
                    state = null;
                }
            }
        }
    }

    public struct LuaDictEntry<K, V>
    {
        public LuaDictEntry(K key, V value)
            : this()
        {
            Key = key;
            Value = value;
        }

        public K Key { get; set; }
        public V Value { get; set; }
    }

    public class LuaDictTable<K, V> : IDisposable, IEnumerable<LuaDictEntry<K, V>>
    {
        LuaTable table;
        LuaState state;

        public LuaDictTable(LuaTable table)
        {
            table.AddRef();
            this.table = table;
            this.state = table.GetLuaState();
        }

        public void Dispose()
        {
            if (table != null)
            {
                table.Dispose();
                table = null;
            }
        }

        public V this[K key]
        {
            get
            {
                return table.RawGet<K, V>(key);
            }

            set
            {                
                table.RawSet(key, value);
            }
        }

        public Dictionary<K, V> ToDictionary()
        {
            Dictionary<K, V> dict = new Dictionary<K, V>();
            var iter = GetEnumerator();

            while (iter.MoveNext())
            {
                dict.Add(iter.Current.Key, iter.Current.Value);
            }

            iter.Dispose();
            return dict;
        }

        public IEnumerator<LuaDictEntry<K, V>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class Enumerator : IEnumerator<LuaDictEntry<K, V>>
        {
            LuaState state;
            LuaDictEntry<K, V> current = new LuaDictEntry<K, V>();
            int top = -1;

            public Enumerator(LuaDictTable<K, V> list)
            {
                state = list.state;
                top = state.LuaGetTop();
                state.Push(list.table);
                state.LuaPushNil();
            }

            public LuaDictEntry<K, V> Current
            {
                get
                {
                    return current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public bool MoveNext()
            {
                if (state.LuaNext(-2))
                {
                    current = new LuaDictEntry<K, V>();
                    current.Key = state.CheckValue<K>(-2);
                    current.Value = state.CheckValue<V>(-1);
                    state.LuaPop(1);
                    return true;
                }
                else
                {
                    current = new LuaDictEntry<K, V>();
                    return false;
                }
            }

            public void Reset()
            {
                current = new LuaDictEntry<K, V>();
            }

            public void Dispose()
            {
                if (state != null)
                {
                    state.LuaSetTop(top);
                    state = null;
                }
            }
        }
    }
}
