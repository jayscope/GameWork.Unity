using GameWork.Core.Audio.Clip;
using GameWork.Core.Audio.PlatformAdaptors;
using GameWork.Unity.Engine.GameObject.Components;
using UnityEngine;

namespace GameWork.Unity.Engine.Audio
{
	public class AudioChannel : IAudioChannel
	{
		private static UnityEngine.GameObject AudioSourceRootInstance;

		private static UnityEngine.GameObject AudioSourceRoot
		{
			get
			{
				if (AudioSourceRootInstance == null)
				{
					AudioSourceRootInstance = UnityEngine.GameObject.Find("AudioChannels");

					if (AudioSourceRootInstance == null)
					{
						AudioSourceRootInstance	= new UnityEngine.GameObject("AudioChannels", typeof(DontDestroyOnLoad));
					}
				}

				return AudioSourceRootInstance;
			}
		}

		private readonly AudioSource _audioSource;
		private IAudioChannel _master;

		public bool IsPlaying
		{
			get { return _audioSource.isPlaying; }
		}

		public float PlaybackSeconds
		{
			get { return _audioSource.time; }
		}

		public int PlaybackSamples
		{
			get { return _audioSource.timeSamples; }
		}

		public float Volume
		{
			get { return _audioSource.volume; }
			set { _audioSource.volume = value; }
		}

		public AudioChannel()
		{
			_audioSource = AudioSourceRoot.AddComponent<AudioSource>();
		}

		public void Play(AudioClipModel clip, IAudioChannel master = null)
		{
			_master = master;
			_audioSource.clip = LoadClip(clip.Name);
			_audioSource.time = 0f;
			_audioSource.Play();
		}

		public void Stop()
		{
			_audioSource.Stop();
		}

		public void Sync()
		{
			if(_master != null)
			{
				if(_master.IsPlaying && _master.PlaybackSamples < _audioSource.clip.samples)
				{
					_audioSource.timeSamples = (int)_master.PlaybackSamples;
				}
			}
		}

		protected virtual AudioClip LoadClip(string path)
		{
			return Resources.Load<AudioClip>(path);
		}
	}
}