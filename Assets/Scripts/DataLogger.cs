using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLogger : Singleton<DataLogger>
{

    public object DataLogged;
 



    public void TestCastData<T>() where T: ParseHTML_To_DTO
    {
        var data = DataLogged as T;
        Debug.Log(data.url);
    }
}
