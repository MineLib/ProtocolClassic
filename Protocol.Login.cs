using MineLib.Core;

namespace ProtocolClassic
{
    public partial class Protocol : IProtocol
    {
        public bool Login(string login, string password)
        {
            throw new ProtocolException("Connection: Login not supported.");
        }

        public bool Logout()
        {
            throw new ProtocolException("Connection: Logout not supported.");
        }
    }
}
