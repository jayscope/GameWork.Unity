using System;
using GameWork.Core.Audio.Clip;
using GameWork.Core.Audio.PlatformAdaptors;
using GameWork.Unity.Components;
using UnityEngine;

namespace GameWork.Unity.Audio
{
    /// <summary>
    /// This finds for or creates a GameObject called AudioChannels that the Audio Channels are added to.
    /// </summary>
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
						_audioSourceRootInstance = new GameObject("AudioChannels", typeof(DontDestroyOnLoad));
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
			get => _audioSource.volume;
		    set => _audioSource.volume = value;
		}

		public AudioChannel()
		{
			_audioSource = AudioSourceRoot.AddComponent<AudioSource>();
		}

        /// <summary>
        /// Set <see cref="AudioClipModel.Name"/> to the path of the audio clip in the resources folder.
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="master"></param>
        /// <param name="onComplete"></param>
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

        /// <summary>
        /// Override this to load audio in a different way.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
		protected virtual AudioClip LoadClip(string path)
		{
			return Resources.Load<AudioClip>(path);
		}
	}
}