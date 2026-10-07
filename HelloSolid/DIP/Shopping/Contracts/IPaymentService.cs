namespace HelloSolid.DIP.Shopping.Contracts
{
    public interface IPaymentService
    {
        void ProcessPayment(decimal amount);
    }
}