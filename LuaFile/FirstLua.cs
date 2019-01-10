using System;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class FirstLua : MonoBehaviour {

    #region lua DoFile Call
    /*private LuaState m_Lua;

    private void Awake()
    {
        m_Lua = new LuaState();
        m_Lua.Start();

        string filePath = Application.dataPath + "\\LuaFile";
        m_Lua.AddSearchPath(filePath);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            m_Lua.DoFile("First.lua");      // 加载程序集
            m_Lua.Collect();
            m_Lua.CheckTop();
        }
    }

    private void OnApplicationQuit()
    {
        m_Lua.Dispose();
        m_Lua = null;
    }*/
    #endregion lua DoFile Call

    #region Call lua Function

    /*private string luaScript =
        @" function Fact(num)
                if 0 == num then
                    return 1
                else
                    return num * Fact(num - 1)
                end
            end 
            test = {}
            test.func = Fact 
        ";


    private LuaState m_Lua;
    private LuaFunction m_LuaFunction;

    private void Awake()
    {
        //new LuaResLoader();
        m_Lua = new LuaState();
        m_Lua.Start();
        DelegateFactory.Init();

        m_Lua.DoString(luaScript);
        m_LuaFunction = m_Lua.GetFunction("test.func");
        if(null != m_LuaFunction)
        {
            int result = m_LuaFunction.Invoke<int, int>(10);
            Debug.Log(string.Format("generate call return : {0}", result));

            result = CallFunc();
            Debug.Log(string.Format("expansion call return : {0}", result));

            Func<int, int> delegateFunc = m_LuaFunction.ToDelegate<Func<int, int>>();
            result = delegateFunc(10);
            Debug.Log(string.Format("delegate call return : {0}", result));

            result = m_Lua.Invoke<int, int>("test.func", 10, true);
            Debug.Log(string.Format("luastate call return : {0}", result));
        }

        m_Lua.CheckTop();
    }

    private void OnDestroy()
    {
        if(null != m_Lua)
        {
            m_Lua.Dispose();
            m_Lua = null;
        }

        if(null != m_LuaFunction)
        {
            m_LuaFunction.Dispose();
            m_LuaFunction = null;
        }
    }

    int CallFunc()
    {
        m_LuaFunction.BeginPCall();
        m_LuaFunction.Push(10);
        m_LuaFunction.PCall();
        int num = (int)m_LuaFunction.CheckNumber();
        m_LuaFunction.EndPCall();

        return num;
    }*/
    #endregion Call lua Function

    #region Accessing lua Variable
    /*private string luaScript =
        @" print('Objs2Spawn is :'..Objs2Spawn)
        var2read = 42
        varTable = {1,2,3,4,5}
        varTable.default = 1
        varTable.map = {}
        varTable.map.name = 'map'

        local a = {}
        a[0] = 100

        a["aaa"] = 100
        print(a.aaa)

        meta = {name = 'meta'}
        setmetatable(varTable, meta)

        function TestFunc(strs)
            print('get func by variable')
        end
        ";

    private LuaState m_LuaState;

    private void Awake()
    {
        DelegateFactory.Init();
        m_LuaState = new LuaState();
        m_LuaState.Start();
        m_LuaState["Objs2Spawn"] = 5;
        m_LuaState.DoString(luaScript);

        Debug.Log(string.Format("Read var form lua : {0}", m_LuaState["var2read"]));
        Debug.Log(string.Format("Read table from lua : {0}", m_LuaState["varTable.default"]));

        LuaFunction func = m_LuaState["TestFunc"] as LuaFunction;
        func.Call();
        func.Dispose();

        LuaTable table = m_LuaState.GetTable("varTable");
        Debug.Log(string.Format("Read varTable from lua, default : {0} name : {1}", table["default"], table["name"]));

        object[] array = table.ToArray();
        for(int i = 0; i < array.Length; ++ i)
        {
            Debug.Log(string.Format(" Table Variable : {0}", array[i]));
        }

        table.Dispose();
        m_LuaState.CheckTop();
        m_LuaState.Dispose();
        m_LuaState = null;
    }*/
    #endregion Accessing lua Variable

    #region lua Coroutine
    /*private LuaState m_LuaState;
    private LuaLooper m_LuaLooper;

    [SerializeField]
    private TextAsset m_TextAsset;

    private void Awake()
    {
        m_LuaState = new LuaState();
        m_LuaState.Start();
        LuaBinder.Bind(m_LuaState);
        DelegateFactory.Init();
        m_LuaLooper = gameObject.AddComponent<LuaLooper>();
        m_LuaLooper.luaState = m_LuaState;

        m_LuaState.DoString(m_TextAsset.text, "LuaCoroutine.lua");
        LuaFunction func = m_LuaState.GetFunction("TestCortinue");
        func.Call();
        func.Dispose();
        func = null;
    }

    private void OnApplicationQuit()
    {
        m_LuaLooper.Destroy();
        m_LuaState.Dispose();
        m_LuaState = null;
    }*/
    #endregion lua Coroutine

    #region lua Thread
    /*private string luaScript =
        @"
            function fib(n)
                local a,b = 0,1
                while n>0 do
                    a,b = b, a+b
                    n = n-1
                end
                return a;
            end

            function CoFunc(len)
                print('Coroutine started')
                local i = 0
                for i = 0, len, 1 do
                    local flag = coroutine.yield(fib(i))
                    if not flag then
                        break
                    end
                end
                print('Coroutine ended')
            end

            function Test()
                local co = coroutine.create(CoFunc)
                return co
            end
        ";

    private LuaState m_LuaState;
    private LuaThread m_LuaThread;

    private void Awake()
    {
        new LuaResLoader();
        m_LuaState = new LuaState();
        m_LuaState.Start();
        m_LuaState.LogGC = true;
        m_LuaState.DoString(luaScript);

        LuaFunction func = m_LuaState.GetFunction("Test");
        func.BeginPCall();
        func.PCall();
        m_LuaThread = func.CheckLuaThread();
        func.EndPCall();
        func.Dispose();
        func = null;

        m_LuaThread.Resume(10);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            int result;
            if(null != m_LuaThread && m_LuaThread.Resume(true, out result) == (int)LuaThreadStatus.LUA_YIELD)
            {
                Debug.Log(string.Format("___ Result : {0}", result));
            }
        }
    }

    private void OnApplicationQuit()
    {
        m_LuaThread.Dispose();
        m_LuaThread = null;

        m_LuaState.Dispose();
        m_LuaState = null;
    }*/
    #endregion

    #region lua Accessing Array
    /*private string luaString =
        @"
            function TestArray(array)
                local length = array.Length
                for i =0, length-1 do
                    print('Array : '.. array[i])
                end
                
                local ite = array:GetEnumerator()
                while ite:MoveNext() do
                    print('ite : '..ite.Current)
                end

                local t = array:ToTable()
                for i=1, #t do
                    print('table : '..tostring(t[i]))
                end

                local pos = array:BinarySearch(3)
                print('Array BinarySearch : pos'..pos..'   value : '..array[pos])

                pos = array:IndexOf(4)
                print('array Indexof bbb pos is : '..pos)

                return 1,'123',true
            end
        ";

    private LuaState m_LuaState;
    private LuaFunction m_LuaFunction;

    private void Awake()
    {
        new LuaResLoader();
        m_LuaState = new LuaState();
        m_LuaState.Start();
        m_LuaState.DoString(luaString, "FirstLua.cs");

        int[] array = { 1,2,3,4,5};
        m_LuaFunction = m_LuaState.GetFunction("TestArray");

        m_LuaFunction.BeginPCall();
        m_LuaFunction.Push(array);
        m_LuaFunction.PCall();
        double arg1 = m_LuaFunction.CheckNumber();
        string arg2 = m_LuaFunction.CheckString();
        bool arg3 = m_LuaFunction.CheckBoolean();
        Debug.Log(string.Format("Arg1 : {0}   Arg2 : {1}  Arg3 : {2}", arg1, arg2, arg3));
        m_LuaFunction.EndPCall();


        object[] objs = m_LuaFunction.LazyCall((object)array);
        Debug.Log(objs.ToString());

        m_LuaState.CheckTop();
    }

    private void OnApplicationQuit()
    {
        if(null != m_LuaState)
        {
            m_LuaState.Dispose();
            m_LuaState = null;
        }

        if(null != m_LuaFunction)
        {
            m_LuaFunction.Dispose();
            m_LuaFunction = null;
        }
    }*/
    #endregion lua Accessing Array

    #region Dictionary
    /*private  string luaScript =
        @"
            function TestDict(map)
                local ite = map:GetEnumerator()

                while ite:MoveNext() do
                    local v = ite.Current.Value
                    print('id : '..v.id..'  name : '..v.name..'  sex : '..v.sex)
                end

                local flag,account = map:TryGetValue(1, nil)
                if flag then
                    print('TryGetValue result ok : '..account.name)
                end


                local keys = map.Keys
                ite = keys:GetEnumerator()
                print('-----------print dictionary keys-----------')
                while ite:MoveNext() do
                    print(ite.Current.name)
                end
                print('------------------over---------------------')



                local values = map.Values
                ite = values:GetEnumerator()
                print('-----------print dictionary values----------')
                while ite:MoveNext() do
                    print(ite.Current.name)
                end
                print('------------------over----------------------')



                print('kick'..map[2].name)
                map:Remove(2)
                ite = map:GetEnumerator()
                while ite:MoveNext() do
                    local v = ite.Current.Value
                    print('id : '..v.id..'  name : '..v.name..'  sex : '..v.sex)
                end
            end
        ";

    private LuaState m_LuaState;
    private Dictionary<int, PlayerAccount> m_DicPlayerAccount = new Dictionary<int, PlayerAccount>();

    private void Awake()
    {
        m_DicPlayerAccount[1] = new PlayerAccount(1, "mingming", 0);
        m_DicPlayerAccount[2] = new PlayerAccount(2, "qn", 1);
        m_DicPlayerAccount[3] = new PlayerAccount(1, "xm", 0);

        new LuaResLoader();
        m_LuaState = new LuaState();
        m_LuaState.Start();
        BindMap(m_LuaState);

        m_LuaState.DoString(luaScript, "FirstLua.cs");
        LuaFunction func = m_LuaState.GetFunction("TestDict");
        func.BeginPCall();
        func.Push(m_DicPlayerAccount);
        func.PCall();
        func.EndPCall();

        func.Dispose();
        func = null;

        m_LuaState.CheckTop();
    }

    private void OnApplicationQuit()
    {
        if(null != m_LuaState)
        {
            m_LuaState.Dispose();
            m_LuaState = null;
        }
    }

    void BindMap(LuaState L)
    {

        LuaBinder.Bind(L);
        /*L.BeginModule(null);
        PlayerAccountWrap.Register(L);
        L.BeginModule("System");
        L.BeginModule("Collections");
        L.BeginModule("Generic");
        System_Collections_Generic_Dictionary_int_PlayerAccountWrap.Register(L);
        System_Collections_Generic_KeyValuePair_int_PlayerAccountWrap.Register(L);
        L.BeginModule("Dictionary");
        System_Collections_Generic_Dictionary_int_PlayerAccount_KeyCollectionWrap.Register(L);
        System_Collections_Generic_Dictionary_int_PlayerAccount_ValueCollectionWrap.Register(L);
        L.EndModule();
        L.EndModule();
        L.EndModule();
        L.EndModule();
        L.EndModule();
    }*/

    #endregion Dictionary


    #region Enum
    /*private string luaString =
        @"
            space = nil

            function TestEnum(e)
                print('Enum is : '..tostring(e))

                if space:ToInt() == 0 then
                    print('Enum ToInt is ok')
                end

                if space:Equals(0) then
                    print('Enum Compare is ok')
                end
            end

            function LightToType(light,type)
                light.type = type
            end
        ";

    private LuaState m_LuaState;
    private LuaFunction m_LuaFunction;

    private void Awake()
    {
        m_LuaState = new LuaState();
        m_LuaState.Start();
        LuaBinder.Bind(m_LuaState);
        new LuaResLoader();

        m_LuaState.DoString(luaString);
        m_LuaState["space"] = Space.World;
        m_LuaFunction = m_LuaState.GetFunction("TestEnum");

        m_LuaFunction.BeginPCall();
        m_LuaFunction.Push(Space.World);
        m_LuaFunction.PCall();
        m_LuaFunction.EndPCall();

    }

    private int count;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Light light = GetComponent<Light>();
            LuaFunction func = m_LuaState.GetFunction("LightToType");
            func.BeginPCall();
            func.Push(light);
            func.Push((LightType)(count++%4));
            func.PCall();
            func.EndPCall();
            func.Dispose();
            func = null;
        }
    }

    private void OnApplicationQuit()
    {
        if(null != m_LuaState)
        {
            m_LuaState.Dispose();
            m_LuaState = null; 
        }

        if(null != m_LuaFunction)
        {
            m_LuaFunction.Dispose();
            m_LuaFunction = null; 
        }
    }*/
    #endregion Enum

    #region Delegate
    #endregion Delegate
}


public class PlayerAccount
{
    public int id;
    public string name;
    public int sex;

    public PlayerAccount(int id, string name, int sex)
    {
        this.id = id;
        this.name = name;
        this.sex = sex;
    }
}
