namespace Store.Core.Domain.Entities.Default
{
    public class Sample : DomainEntity<int>
    {
        public string Description { get; set; }
        public Sample()
        {
        }
    }
}
