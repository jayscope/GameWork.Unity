using System;
using GameWork.Core.Audio.Clip;
using GameWork.Core.Audio.PlatformAdaptors;
using UnityEngine;
using GameWork.Unity.Engine.Components;

namespace GameWork.Unity.Engine.Audio
{
	public class AudioChannel : IAudioChannel
	{
		private static GameObject _audioSourceRootInstance;

		private static GameObject AudioSourceRoot
		{
			get
			{
				if (_audioSourceRootInstance == null)
				{
					_audioSourceRootInstance = GameObject.Find("AudioChannels");

					if (_audioSourceRootInstance == null)
					{
						_audioSourceRootInstance	= new GameObject("AudioChannels", typeof(DontDestroyOnLoad));
					}
				}

				return _audioSourceRootInstance;
			}
		}

		private readonly AudioSource _audioSource;
		private IAudioChannel _master;
		private Action _onComplete;

		public bool IsPlaying => _audioSource.isPlaying;

		public float PlaybackSeconds => _audioSource.timeSamples / (float)_audioSource.clip.frequency;

		public int PlaybackSamples => _audioSource.timeSamples;

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