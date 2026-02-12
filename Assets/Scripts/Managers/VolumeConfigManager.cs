using SoundSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VolumeConfigManager : MonoBehaviour
{
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider bgmVolumeSlider;
    [SerializeField] Slider seVolumeSlider;

    private void Start()
    {
        var sound = SoundManager.Instance;
        var save = SaveManager.Instance;

        //スライダーを変更した瞬間にセーブ
        SetMasterSliderEvent(vol =>
        {
            sound.MasterVolume = vol;
            save.SaveSoundVolume(sound.MasterVolume, sound.BGMVolume, sound.SEVolume);
        });
        SetBGMSliderEvent(vol =>
        {
            sound.BGMVolume = vol;
            save.SaveSoundVolume(sound.MasterVolume, sound.BGMVolume, sound.SEVolume);
        });
        SetSESliderEvent(vol =>
        {
            sound.SEVolume = vol;
            save.SaveSoundVolume(sound.MasterVolume, sound.BGMVolume, sound.SEVolume);
        });

        // 初期スライダー位置
        SetMasterVolume(sound.MasterVolume);
        SetBGMVolume(sound.BGMVolume);
        SetSeVolume(sound.SEVolume);
    }

    //スライダーの位置をボリュームに合わせてセット
    public void SetMasterVolume(float masterVolume)
    {
        masterVolumeSlider.value = masterVolume;
    }
    public void SetBGMVolume(float bgmVolume)
    {
        bgmVolumeSlider.value = bgmVolume;
    }
    public void SetSeVolume(float seVolume)
    {
        seVolumeSlider.value = seVolume;
    }

    //スライダーに変更があったら値を反映させる
    public void SetMasterSliderEvent(UnityAction<float> sliderCallback)
    {
        SetValueChangedEvent(masterVolumeSlider, sliderCallback);
    }
    public void SetBGMSliderEvent(UnityAction<float> sliderCallback)
    {
        SetValueChangedEvent(bgmVolumeSlider, sliderCallback);
    }
    public void SetSESliderEvent(UnityAction<float> sliderCallback)
    {
        SetValueChangedEvent(seVolumeSlider, sliderCallback);
    }
    void SetValueChangedEvent(Slider slider, UnityAction<float> sliderCallback)
    {
        if (slider.onValueChanged != null)
        {
            slider.onValueChanged.RemoveAllListeners();
        }
        slider.onValueChanged.AddListener(sliderCallback);
    }
}