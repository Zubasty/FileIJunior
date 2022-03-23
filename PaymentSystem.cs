public static class Programm
{
    public static void Main()
    {
        Order order = new Order(4, 400);
        Console.WriteLine(new PaymentSystem1("pay.system1.ru/order", new MD5()).GetPayingLink(order));
        Console.WriteLine(new PaymentSystem2("order.system2.ru/pay", new MD5()).GetPayingLink(order));
        Console.WriteLine(new PaymentSystem3("system3.com/pay", new SHA1(), "key").GetPayingLink(order));
    }
}

public interface IPaymentSystem
{
    public string GetPayingLink(Order order);
}

public interface IHash
{
    public string GetHash(string data);
}

public class MD5 : IHash
{
    public string GetHash(string data)
    {
        System.Security.Cryptography.HMACMD5 md5 = new System.Security.Cryptography.HMACMD5();
        return Encoding.ASCII.GetString(md5.ComputeHash(new ASCIIEncoding().GetBytes(data)));
    }
}

public class SHA1 : IHash
{
    public string GetHash(string data)
    {
        System.Security.Cryptography.HMACSHA1 sha1 = new System.Security.Cryptography.HMACSHA1();
        return Encoding.ASCII.GetString(sha1.ComputeHash(new ASCIIEncoding().GetBytes(data)));
    }
}

public class Order
{
    public readonly int Id;
    public readonly int Amount;

    public Order(int id, int amount) => (Id, Amount) = (id, amount);
}

public abstract class PaymentSystem : IPaymentSystem
{
    private string _startLink;
    private IHash _hash;

    protected IHash Hash => _hash;

    protected string StartLink => _startLink;

    public PaymentSystem(string startLink, IHash hash)
    {
        _startLink = startLink;
        _hash = hash;
    }

    public abstract string GetPayingLink(Order order);
}

public class PaymentSystem1 : PaymentSystem
{
    public PaymentSystem1(string startLink, IHash hash) : base(startLink, hash) { }

    public override string GetPayingLink(Order order)
    {
        return $"{StartLink}?amount={order.Amount}RUB&hash={Hash.GetHash(order.Id.ToString())}";
    }
}

public class PaymentSystem2 : PaymentSystem
{
    public PaymentSystem2(string startLink, IHash hash) : base(startLink, hash) { }

    public override string GetPayingLink(Order order)
    {
        return $"{StartLink}?hash={Hash.GetHash($"{order.Id} {order.Amount}")}";
    }
}

public class PaymentSystem3 : PaymentSystem
{
    private string _key;

    public PaymentSystem3(string startLink, IHash hash, string key) : base(startLink, hash)
    {
        _key = key;
    }

    public override string GetPayingLink(Order order)
    {
        return $"{StartLink}?amount={order.Amount}&currency=RUB&hash={Hash.GetHash($"{order.Amount} {order.Id} {_key}")}";
    }
}