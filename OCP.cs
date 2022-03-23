namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PaymentSystem> systems = new List<PaymentSystem>()
            {
                new QIWI("QIWI"),
                new WebMoney("WebMoney"),
                new Card("Card")
            };
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();

            var systemId = orderForm.ShowForm();

            foreach (var system in systems)
            {
                if (system.Id == systemId)
                {
                    system.ShowPaymentProcess();
                    paymentHandler.ShowPaymentResult(systemId);
                    break;
                }
            }
        }
    }

    public abstract class PaymentSystem
    {
        public string Id { get; private set; }

        public PaymentSystem(string id)
        {
            Id = id;
        }

        public void ShowPaymentProcess()
        {
            Console.WriteLine($"������� �� �������� {Id}...");
        }
    }

    public class QIWI : PaymentSystem
    {
        public QIWI(string id) : base(id) { }
    }

    public class WebMoney : PaymentSystem
    {
        public WebMoney(string id) : base(id) { }
    }

    public class Card : PaymentSystem
    {
        public Card(string id) : base(id) { }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("�� ���������: QIWI, WebMoney, Card");

            //��������� ��� ����������
            Console.WriteLine("����� �������� �� ������ ��������� ������?");
            return Console.ReadLine();
        }
    }

    public class PaymentHandler
    {
        public void ShowPaymentResult(string systemId)
        {
            Console.WriteLine($"�� �������� � ������� {systemId}");

            Console.WriteLine($"�������� ������� ����� {systemId}...");

            Console.WriteLine("������ ������ �������!");
        }
    }
}