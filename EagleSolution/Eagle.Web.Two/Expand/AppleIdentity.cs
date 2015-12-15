namespace Eagle.Web.Two.Expand
{
    public class AppleIdentity
    {

        private System.Web.Security.FormsAuthenticationTicket ticket;
        public AppleIdentity(System.Web.Security.FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return ticket.Name;
            }
        }

        public string FriendlyName
        {
            get
            {
                return ticket.UserData;
            }
        }

    }
}