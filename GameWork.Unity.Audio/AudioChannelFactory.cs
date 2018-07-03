using GameWork.Core.Audio.PlatformAdaptors;

namespace GameWork.Unity.Engine.Audio
{
    public class AudioChannelFactory : IAudioChannelFactory
    {
        public IAudioChannel Create()
        {
            return new AudioChannel();
        }
    }
}