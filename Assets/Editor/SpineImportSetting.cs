using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Text;
using Spine;
using Newtonsoft.Json;
using UnityEditor.Timeline.Actions;
using XFGameFramework;
/// <summary>
/// 修改spine文件 版本
/// </summary>
public class SpineImportSetting : AssetPostprocessor
{
    //任何资源（包括文件夹）导入都会被调用的方法
    void OnPreprocessAsset()
    {
        try
        {

            if (!this.assetPath.EndsWith(".json"))
                return;

            //先判断是否是 spine 文件
            string msg = File.ReadAllText(this.assetPath, Encoding.UTF8);
            var jo = JsonConvert.DeserializeObject<dynamic>(msg);
            string item = jo["skeleton"]["spine"].ToString();

            if (!string.IsNullOrEmpty(item) && item.ToString() != "3.8")
            {
                jo["skeleton"]["spine"] = "3.8";//修改版本为3.8版本
                File.WriteAllText(this.assetPath, JsonConvert.SerializeObject(jo));
                TimerManager.DelayInvoke(Refresh, 1f);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"SpineImportSetting 异常 {e.Message}");
        }
    }


    void Refresh()
    {
        AssetDatabase.Refresh();
    }

}

