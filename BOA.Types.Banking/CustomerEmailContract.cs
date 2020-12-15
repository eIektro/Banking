namespace BOA.Types.Banking
{
    public class CustomerEmailContract
    {
        public int CustomerMailId { get; set; }

        public int CustomerId { get; set; }

        public int MailType { get; set; }

        public string MailAdress { get; set; }
    }
}