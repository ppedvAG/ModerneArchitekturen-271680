using HelloSolid.DIP.Shopping.Contracts;

namespace HelloSolid.DIP.Payment;

public class PaymentService : IPaymentService
{
    public void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing payment of {amount:C2}.");
    }
}
