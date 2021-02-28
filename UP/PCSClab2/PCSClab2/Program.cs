using System;

namespace PCSC
{
    class Program
    {
        public enum ReadType { Bytes, ASCII }

        static void Main(string[] args)
        {
            var context = new SCardContext();
            context.Establish(SCardScope.System);

            // Get readers
            Console.WriteLine("Currently connected readers: ");
            var readerNames = context.GetReaders();
            foreach (var readerName in readerNames)
                Console.WriteLine("\t" + readerName);

            if (readerNames.Length == 0)
                Console.WriteLine("No readers");
            else
            {
                var reader = new SCardReader(context);
                reader.Connect(readerNames[0], SCardShareMode.Shared, SCardProtocol.T0 | SCardProtocol.T1);
                IntPtr protocol;
                switch (reader.ActiveProtocol)
                {
                    case SCardProtocol.T0:
                        protocol = SCardPCI.T0;
                        break;
                    case SCardProtocol.T1:
                        protocol = SCardPCI.T1;
                        break;
                    default:
                        throw new PCSCException(SCardError.ProtocolMismatch, "No protocol: " + reader.ActiveProtocol.ToString());
                }

                // SELECT TELECOM
                SendCommand(reader, protocol, new byte[] { 0xA0, 0xA4, 0x00, 0x00, 0x02, 0x7F, 0x10, }, "SELECT TELECOM", ReadType.Bytes);

                // GET RESPONSE
                SendCommand(reader, protocol, new byte[] { 0xA0, 0xC0, 0x00, 0x00, 0x16 }, "GET RESPONSE", ReadType.Bytes);

                // SELECT ADN
                SendCommand(reader, protocol, new byte[] { 0xA0, 0xA4, 0x00, 0x00, 0x02, 0x6F, 0x3A }, "SELECT ADN", ReadType.Bytes);

                // GET RESPONSE
                SendCommand(reader, protocol, new byte[] { 0xA0, 0xC0, 0x00, 0x00, 0x0F }, "GET RESPONSE", ReadType.Bytes);

                // READ RECORD
                for (byte i = 0; i < 45; ++i)
                    SendCommand(reader, protocol, new byte[] { 0xA0, 0xB2, i, 0x04, 0x2E }, "READ RECORD", ReadType.ASCII);
                context.Release();
            }
            Console.Read();
        }

        // Send APDU command
        public static void SendCommand(SCardReader reader, IntPtr protocol, byte[] comand, string codeTag, ReadType readType)
        {
            byte[] recivedBytes = new byte[256];
            var response = reader.Transmit(protocol, comand, ref recivedBytes);
            if (response == SCardError.Success)
            {
                Console.Write(codeTag + ": SW2: ");
                switch (readType)
                {
                    case ReadType.Bytes:
                        foreach (byte code in recivedBytes)
                            Console.Write("{0:X2} ", code);
                        break;
                    case ReadType.ASCII:
                        bool numberFlag = false;
                        int numLength = -1;
                        foreach (byte code in recivedBytes)
                        {
                            if (code == 255)
                                numberFlag = true;
                            else if (numberFlag && numLength == -1)
                                numLength = code;
                            else if (numLength != -1)
                                Console.Write("{0}{1}", (code & 0xF0) >> 4, code & 0xF);
                            else
                                Console.Write("{0:X2}", (char)code);
                        }
                        break;
                }
                Console.WriteLine();
            }
            else
                Console.WriteLine("Send command error");
        }
    }
}
