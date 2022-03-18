using System.Text;

namespace NicehashExporter.API
{
    public static class ApiClientHelpers
    {
        private static string CalcHMACSHA256Hash(string plaintext, string salt)
        {            
            var enc = Encoding.UTF8;
            byte[] baText2BeHashed = enc.GetBytes(plaintext),
            baSalt = enc.GetBytes(salt);
            System.Security.Cryptography.HMACSHA256 hasher = new System.Security.Cryptography.HMACSHA256(baSalt);
            byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
            return string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
        }

        private static string JoinSegments(List<string> segments)
        {
            var sb = new StringBuilder();
            bool first = true;
            foreach (var segment in segments)
            {
                if (!first)
                {
                    sb.Append("\x00");
                }
                else
                {
                    first = false;
                }

                if (segment != null)
                {
                    sb.Append(segment);
                }
            }
            return sb.ToString();
        }

        public static string HashBySegments(string key, string apiKey, string time, string nonce, string orgId, string method, string encodedPath, string query, string bodyStr)
        {
            List<string> segments = new();
            segments.Add(apiKey);
            segments.Add(time);
            segments.Add(nonce);
            segments.Add(null);
            segments.Add(orgId);
            segments.Add(null);
            segments.Add(method.ToUpperInvariant());
            segments.Add(encodedPath);
            segments.Add(query);

            if (bodyStr != null && bodyStr.Length > 0)
            {
                segments.Add(bodyStr);
            }
            return CalcHMACSHA256Hash(JoinSegments(segments), key);
        }
    }
}
