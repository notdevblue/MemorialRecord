// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("A/8ogNfCVTWtH656mTeI+sHlj6GMgMtvALH/bFm8FQZsA5eDzJE9imXXVHdlWFNcf9Md06JYVFRUUFVWw6PEdMNVuKYWKIVYE/FCUgKAkG7HpTslHam3vKA5hYGeMIGV6qDf4kt/4QKYn7SAT7Ov/M4G0wBUfTQKs5aY0rB2RqFggbmnDSw+ua9qVJve9i7Y+qFb5ToCmltqDPycqxVIatdUWlVl11RfV9dUVFWRkykUpdhG/hXBUEJ0r7j4m/w+TJ05YlJRpKi7qmHtPeCgxGNm7RHvTnNr9Ebr3J8929XDoVWj50q43laaGHYccQiJ8iq6TcpqBgbB0uaPKY7J8dkG7DB2/6kJGloaFfJ4f7kDldz/XhNbCL7TY9OReH/VqFdWVFVU");
        private static int[] order = new int[] { 10,4,10,8,8,11,9,10,8,12,13,11,13,13,14 };
        private static int key = 85;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
