namespace Jalasoft.TeamUp.Resumes.API.Tests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class SetStream
    {
        public static Stream Setstream(string body)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
