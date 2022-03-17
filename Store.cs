public class Good
{
    public string Name { get; private set; }

    public Good(string name)
    {
        Name = name;
    }
}

public class Warehouse : KitGoodCount
{
    public void Delive(Good good, int count)
    {
        if (ContainsKey(good))
        {
            this[good] += count;
        }
        else
        {
            Add(good, count);
        }
    }

    public KeyValuePair<Good, int> GiveGoods(Good good, int count)
    {
        if (ContainsKey(good) && this[good] >= count)
        {
            this[good] -= count;
            return new KeyValuePair<Good, int>(good, count);
        }

        throw new Exception("Такого количества данного товара нет на складе");
    }
}

public class Shop
{
    private Warehouse _warehouse;
    private List<Cart> _carts;

    public Shop(Warehouse warehouse)
    {
        _warehouse = warehouse;
        _carts = new List<Cart>();
    }

    public Cart Cart()
    {
        _carts.Add(new Cart(this));
        return _carts[_carts.Count - 1];
    }

    public KeyValuePair<Good, int> GiveGoods(Good good, int count)
    {
        return _warehouse.GiveGoods(good, count);
    }
}

public class Cart : KitGoodCount
{
    private Shop _shop;

    public Cart(Shop shop)
    {
        _shop = shop;
    }

    public Order Order()
    {
        return new Order(this);
    }

    public new void Add(Good good, int count)
    {
        var goodCount = _shop.GiveGoods(good, count);

        if (ContainsKey(good))
        {
            this[good] += goodCount.Value;
        }
        else
        {
            base.Add(goodCount.Key, goodCount.Value);
        }
    }
}

public class Order : KitGoodCount
{
    public string Paylink { get; private set; }

    public Order(Dictionary<Good, int> goods)
    {
        Paylink = "просто какая-нибудь случайная строка";

        foreach (var good in goods)
        {
            Add(good.Key, good.Value);
        }
    }
}

public abstract class KitGoodCount : Dictionary<Good, int>
{
    public string GetFullInformation()
    {
        string fullInformation = "";

        foreach (var goodCount in this)
        {
            fullInformation += $"{goodCount.Key} - {goodCount.Value}\n";
        }

        return fullInformation;
    }
}