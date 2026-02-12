using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        //AudioClipリスト
        public List<AudioClip> bgmAudioClipList = new List<AudioClip>();
        public List<AudioClip> seAudioClipList = new List<AudioClip>();

        public AudioMixer audioMixer;
        public AudioMixerGroup bgmAMG, seAMG;
        public AudioMixer effectAudioMixer;

        //AudioSource
        List<AudioSource> bgmAudioSourceList = new List<AudioSource>();
        AudioSource seAudioSource;
        AudioSource seLoopSource;

        List<IEnumerator> fadeCoroutines = new List<IEnumerator>();

        const int BGMAudioSourceNum = 2;
        const string MasterVolumeParamName = "MasterVolume";
        const string SEVolumeParamName = "SEVolume";
        const string BGMVolumeParamName = "BGMVolume";

        //一時停止中か
        public bool IsPaused { get; private set; }

        public float MasterVolume
        {
            get { return audioMixer.GetVolumeByLinear(MasterVolumeParamName); }
            set { audioMixer.SetVolumeByLinear(MasterVolumeParamName, value); }
        }

        public float SEVolume
        {
            get { return audioMixer.GetVolumeByLinear(SEVolumeParamName); }
            set { audioMixer.SetVolumeByLinear(SEVolumeParamName, value); }
        }

        public float BGMVolume
        {
            get { return audioMixer.GetVolumeByLinear(BGMVolumeParamName); }
            set { audioMixer.SetVolumeByLinear(BGMVolumeParamName, value); }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
                return;
            }
            seAudioSource = InitializeAudioSource(gameObject, false, seAMG);
            seLoopSource = InitializeAudioSource(gameObject, true, seAMG);
            bgmAudioSourceList = InitializeAudioSources(gameObject, true, bgmAMG, BGMAudioSourceNum);

            IsPaused = false;
        }

        private void Start()
        {
            if (SaveManager.Instance != null)
            {
                float vm = MasterVolume,
                      vb = BGMVolume,
                      vs = SEVolume;

                //音量セーブデータの読み込み
                SaveManager.Instance.LoadSoundVolume(ref vm, ref vb, ref vs);

                MasterVolume = vm;
                BGMVolume = vb;
                SEVolume = vs;
            }
        }

        public void ChangeSnapshot(string snapshotName, float transitionTime = 1f)
        {
            AudioMixerSnapshot snapshot = effectAudioMixer.FindSnapshot(snapshotName);

            if (snapshot == null)
            {
                Debug.Log(snapshotName + "は見つかりません");
            }
            else
            {
                snapshot.TransitionTo(transitionTime);
            }
        }

        public void Pause()
        {
            IsPaused = true;
            fadeCoroutines.ForEach(StopCoroutine);
            bgmAudioSourceList.ForEach(bas => bas.Pause());
        }

        public void Resume()
        {
            IsPaused = false;
            fadeCoroutines.ForEach(routine => StartCoroutine(routine));
            bgmAudioSourceList.ForEach(bas => bas.UnPause());
        }

        private List<AudioSource> InitializeAudioSources(GameObject parentGameObject, bool isLoop = false,
            AudioMixerGroup amg = null, int count = 2)
        {
            List<AudioSource> audioSources = new List<AudioSource>();

            for (int i = 0; i < count; i++)
            {
                var audioSource = InitializeAudioSource(parentGameObject, isLoop, amg);
                audioSources.Add(audioSource);
            }

            return audioSources;
        }

        private AudioSource InitializeAudioSource(GameObject parentGameObject, bool isLoop = false,
            AudioMixerGroup amg = null)
        {
            var audioSource = parentGameObject.AddComponent<AudioSource>();

            audioSource.loop = isLoop;
            audioSource.playOnAwake = false;

            if (amg != null)
            {
                audioSource.outputAudioMixerGroup = amg;
            }

            return audioSource;
        }

        public void PlaySe(string clipName)
        {
            var audioClip = seAudioClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip == null)
            {
                Debug.Log(clipName + " は見つかりません");
                return;
            }

            seAudioSource.Play(audioClip);
        }

        public void PlaySeOneShot(string clipName)
        {
            var audioClip = seAudioClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip == null)
            {
                Debug.Log(clipName + " は見つかりません");
                return;
            }

            seAudioSource.PlayOneShot(audioClip);
        }

        //SEループ再生用メソッド
        public void PlaySeLoop(string clipName)
        {
            var audioClip = seAudioClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip == null)
            {
                Debug.Log(clipName + " は見つかりません");
                return;
            }

            seLoopSource.Stop();
            seLoopSource.clip = audioClip;
            seLoopSource.loop = true;
            seLoopSource.Play();
        }

        public void StopSeFadeOut(string clipName, float fadeDuration = 0.5f)
        {
            StopSeFadeOut(seAudioSource, clipName, fadeDuration);
        }

        public void StopSeLoopFadeOut(string clipName, float fadeDuration = 0.5f)
        {
            StopSeFadeOut(seLoopSource, clipName, fadeDuration);
        }

        private void StopSeFadeOut(AudioSource audioSource, string clipName, float fadeDuration = 0.5f)
        {
            if (IsPaused) { return; }
            if (!audioSource.isPlaying) //seが再生されていなければ何もしない
            {
                return;
            }
            if (audioSource.clip != null && audioSource.clip.name == clipName) //seが存在し、seの文字列が合っていれば
            {
                StartCoroutine(SeFadeOutCoroutine(audioSource, fadeDuration));
                return;
            }
            StartCoroutine(SeFadeOutCoroutine(audioSource, fadeDuration));
        }

        private IEnumerator SeFadeOutCoroutine(AudioSource audioSource, float fadeDuration)
        {
            float startVolume = audioSource.volume;
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.unscaledDeltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0, timer / fadeDuration);
                yield return null;
            }
            audioSource.Stop();
            audioSource.volume = startVolume; //次回再生時のために音量を戻す
        }

        public void PlayBGMWithFadeIn(string clipName, float fadeTime = 0.5f)
        {
            if (IsPaused) { return; }

            var audioClip = bgmAudioClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip == null)
            {
                Debug.Log(clipName + " は見つかりません");
                return;
            }

            if (bgmAudioSourceList.Any(source => source.clip == audioClip))
            {
                Debug.Log(clipName + " はすでに再生されています");
                return;
            }

            StopBGMsWithFadeOut(fadeTime); // 現在再生中のBGMをフェードアウトする

            AudioSource audioSource = bgmAudioSourceList.FirstOrDefault(asb => asb.isPlaying == false);

            if (audioSource != null)
            {
                IEnumerator routine = audioSource.PlayWithFadeIn(audioClip, fadeTime);
                fadeCoroutines.Add(routine);
                StartCoroutine(routine);
            }
        }

        public void StopBGMWithFadeOut(string clipName, float fadeTime = 0.5f)
        {
            if (IsPaused) { return; }

            AudioSource audioSource = bgmAudioSourceList.FirstOrDefault(bas => bas.clip.name == clipName);

            if (audioSource == null || audioSource.isPlaying == false)
            {
                Debug.Log(clipName + " は再生されていません");
                return;
            }

            IEnumerator routine = audioSource.StopWithFadeOut(fadeTime);
            StartCoroutine(routine);
            fadeCoroutines.Add(routine);
        }

        public void StopBGMsWithFadeOut(float fadeTime = 2f)
        {
            if (IsPaused) { return; }

            fadeCoroutines.ForEach(StopCoroutine);
            fadeCoroutines.Clear();

            fadeCoroutines = bgmAudioSourceList.Where(asb => asb.isPlaying)
            .ToList()
            .ConvertAll(asb =>
            {
                IEnumerator routine = asb.StopWithFadeOut(fadeTime);
                StartCoroutine(routine);
                return routine;
            });
        }
    }
}