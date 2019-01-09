using UnityEngine;

public class FirstLua : MonoBehaviour {

    /*#region lua DoFile Call
    private LuaState m_Lua;

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
    }
    #endregion lua DoFile Call*/

    /*#region Call lua Function

    private string luaScript =
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
    }
    #endregion Call lua Function*/


}
