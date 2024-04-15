using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class JSManager : MonoBehaviour
{
    private static JSManager instance;

    public static JSManager Instance
    {
        get { return instance; }
    }

    [DllImport("__Internal")]
    private static extern void LoginTelegram();
    
    // [DllImport("__Internal")]
    // private static extern void PrintFloatArray(float[] array, int size);
    //
    // [DllImport("__Internal")]
    // private static extern int AddNumbers(int x, int y);
    //
    // [DllImport("__Internal")]
    // private static extern string StringReturnValueFunction();
    //
    // [DllImport("__Internal")]
    // private static extern void BindWebGLTexture(int texture);
    private void Awake()
    {
        instance = this;
    }

    public void TestLogin()
    {
        Debug.Log("Test login");
        LoginTelegram();
        // var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        // BindWebGLTexture(texture.GetNativeTexturePtr());
    }

    public void OnLoginErrorCallBack(string data)
    {
        Debug.Log($"Login Error data {data}");
    }
    
    public void OnLoginSuccessCallBack(string data)
    {
        Debug.Log($"Login Success data {data}");

    }
}