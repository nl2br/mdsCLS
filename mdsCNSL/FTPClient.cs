namespace mdsCNSL
{
    public class FTPClient
    {
        private string UserName { get; set; }
        private string Passwoord { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public void UploadFile(string LocalFileName)
        {

        }

        public void UploadFile(string LocalFileName, string RemoteFileName)
        {

        }

        public void ftptest()
        {
            var wc = new WebClient();
            wc.BaseAddress = "";
            wc.Credentials = new NetworkCredential("", "");

        }
    }
}
