namespace BOA.Types.Banking
{
    public class CustomerPhoneContract
    {
        public int? CustomerPhoneId { get; set; }

        public int? CustomerId { get; set; }

        public int PhoneType { get; set; }

        public string PhoneNumber { get; set; }
    }
}