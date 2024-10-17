using System;
using System.Text;

namespace BattleScene.InterfaceAdapter.Service
{
    public class Message
    {
        private readonly string _message;
        private readonly StringBuilder _sbMessage;

        public Message(
            string message)
        {
            _message = message;
            _sbMessage = new StringBuilder(message);
        }

        public Message Replace(string old, Action<StringBuilder> messageReplacer)
        {
            if (!_message.Contains(old)) return this;

            messageReplacer(_sbMessage);
            return this;
        }

        public string GetMessage()
        {
            return _sbMessage.ToString();
        }
    }
}