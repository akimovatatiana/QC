using System.Collections.Generic;

namespace TV
{
    public class TVSet
    {
        private bool _isTurnedOn = false;
        private int _currentChannel;
        private int _previousChannel;
        private Dictionary<int, string> _channels = new Dictionary<int, string>();

        public bool IsTurnedOn()
        {
            return _isTurnedOn;
        }

        public bool TurnOn()
        {
            _isTurnedOn = true;
            return true;
        }

        public bool TurnOff()
        {
            _isTurnedOn = false;
            return false;
        }

        public int GetChannel()
        {
            if (_isTurnedOn)
            {
                return _currentChannel;
            }
            return 0;
        }

        public bool SelectChannel(int channel)
        {
            if ((channel >= 1 && channel <= 99) && IsTurnedOn())
            {
                _previousChannel = _currentChannel;
                _currentChannel = channel;
                return true;
            }
            return false;
        }

        public bool SelectPreviousChannel()
        {
            if (_isTurnedOn)
            {
                int temp;
                temp = _previousChannel;
                _previousChannel = _currentChannel;
                _currentChannel = temp;
                return true;
            }
            return false;
        }

        public bool SetChannelName(int channel, string channelName)
        {
            string newChannelName = channelName;
            if (_isTurnedOn && newChannelName != "" && (channel >= 1 && channel <= 99))
            {
                _channels.Add(channel, channelName);
                return true;
            }
            return false;
        }
    }
}
