using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Helpers
{
    public sealed class StreamConvertor
    {
        public static byte[] UseStreamDotReadMethod(Stream stream)
        {
            byte[] bytes;
            List<byte> totalStream = new();
            byte[] buffer = new byte[stream.Length];
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                totalStream.AddRange(buffer.Take(read));
            }
            bytes = totalStream.ToArray();
            return bytes;
        }

        public static byte[] UseStreamReader(Stream stream)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(stream))
            {
                stream.Position = 0;
                bytes = br.ReadBytes(Convert.ToInt32(stream.Length));
            }
            return bytes;
        }

    }
}
