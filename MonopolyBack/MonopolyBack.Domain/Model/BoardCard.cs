namespace MonopolyBack.Domain.Model;

public sealed class BoardCard
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int Position { get; set; }

    public string Name { get; set; } = string.Empty;

    public BoardCardType Type { get; set; } = BoardCardType.Property;

    public string? ColorGroup { get; set; }

    public bool IsPurchasable { get; set; } = true;

    public int Price { get; set; }

    public int MortgagePrice { get; set; }

    public int MortgageBuyoutPrice { get; set; }

    public int HousePrice { get; set; }

    public int RentWithoutHouses { get; set; }

    public int RentWithOneHouse { get; set; }

    public int RentWithTwoHouses { get; set; }

    public int RentWithThreeHouses { get; set; }

    public int RentWithFourHouses { get; set; }

    public int RentWithHotel { get; set; }
}
