using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;
using XFGameFramework;

public enum AudioType
{
    BGM,
    Sound
}

public class AudioController : Controller
{

    public override void OnInit()
    {
        base.OnInit();
        //注册
        AudioPlayer.RegisterAudioController(AudioType.BGM.ToString(), true);
        AudioPlayer.RegisterAudioController(AudioType.Sound.ToString(), false);

        //设置音量
        AudioPlayer.GetAuidoController(AudioType.BGM.ToString()).Volume = 1f;
        AudioPlayer.GetAuidoController(AudioType.Sound.ToString()).Volume = 1f;
    }

    public void PlayBGM(string name)
    {
        var audioController = AudioPlayer.GetAuidoController(AudioType.BGM.ToString());
        var audioSource = audioController.AudioSource;
        if (audioSource.clip != null && audioSource.clip.name == name) return;
        //加载音频
        var clip = AssetBundleManager.LoadAsset<AudioClip>(Module.ProjectName, name);
        if (clip == null) throw new System.Exception("音频文件不存在！" + name);
        //playOneShot 表示只播放一次
        AudioPlayer.Play(AudioType.BGM.ToString(), clip, false);
    }

    public void PlaySound(string name)
    {
        //加载音频
        var clip = AssetBundleManager.LoadAsset<AudioClip>(Module.ProjectName, name);
        if (clip == null) throw new System.Exception("音频文件不存在！" + name);
        //playOneShot 表示只播放一次
        AudioPlayer.Play(AudioType.Sound.ToString(), clip, true);
    }

    public void SetMute(AudioType audioType, bool isMute)
    {
        AudioPlayer.GetAuidoController(audioType.ToString()).Mute = isMute;
    }

    public bool GetMute(AudioType audioType)
    {
        return AudioPlayer.GetAuidoController(audioType.ToString()).Mute;
    }
}
