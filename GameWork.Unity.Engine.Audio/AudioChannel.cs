using System;
using GameWork.Core.Audio.Clip;
using GameWork.Core.Audio.PlatformAdaptors;
using UnityEngine;
using GameWork.Unity.Engine.Components;

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
		private Action _onComplete;

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

		public void Play(AudioClipModel clip, IAudioChannel master = null, Action onComplete = null)
		{
			_master = master;
			_onComplete = onComplete;
			// TODO create resource manager and cache resources
			_audioSource.clip = LoadClip(clip.Name);
			_audioSource.time = 0f;
			_audioSource.Play();
		}

		public void Stop()
		{
			_audioSource.Stop();
		}

		public void Tick()
		{
			if (IsPlaying)
			{
				if (_master != null)
				{
					if (_master.IsPlaying && _master.PlaybackSamples < _audioSource.clip.samples)
					{
						_audioSource.timeSamples = (int) _master.PlaybackSamples;
					}
				}
			}
			else
			{
				if (_onComplete != null)
				{
					if (_audioSource.clip.samples <= _audioSource.timeSamples)
					{
						var onComplete = _onComplete;
						_onComplete = null;
						onComplete();
					}
				}
			}
		}

		protected virtual AudioClip LoadClip(string path)
		{
			return Resources.Load<AudioClip>(path);
		}
	}
}