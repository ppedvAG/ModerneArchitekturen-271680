using HelloSolid.DIP.Payment;
using HelloSolid.DIP.Shopping.Contracts;

namespace HelloSolid.DIP.Shopping.Core;

public record Product(string Name, decimal Price);

public class ShoppingCart
{
    // Wir sollten hier nicht gegen die konkrete Implementierung implementieren
    public PaymentService PaymentService_BadSample { get; } = new();

    private readonly IPaymentService _paymentService;

    // DIP: Die Abhängigkeit soll von außen hereingereicht werden (Dependency Injection)
    public ShoppingCart(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public void AddProduct(Product product)
    {
        Console.WriteLine($"Added {product.Name} to the shopping cart.");
    }

    public void PayOrder(decimal amount)
    {
        //PaymentService_BadSample.ProcessPayment(amount);

        _paymentService.ProcessPayment(amount);
    }
}
