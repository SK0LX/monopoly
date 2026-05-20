using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Infrastructure.Persistence.Configurations;

public sealed class BoardCardConfiguration : IEntityTypeConfiguration<BoardCard>
{
    public void Configure(EntityTypeBuilder<BoardCard> builder)
    {
        builder.ToTable("board_cards");

        builder.HasKey(card => card.Id);

        builder.Property(card => card.Id)
            .HasColumnName("id");

        builder.Property(card => card.Position)
            .HasColumnName("position")
            .IsRequired();

        builder.HasIndex(card => card.Position)
            .IsUnique();

        builder.Property(card => card.Name)
            .HasColumnName("name")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(card => card.Type)
            .HasColumnName("type")
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(card => card.ColorGroup)
            .HasColumnName("color_group")
            .HasMaxLength(64);

        builder.Property(card => card.IsPurchasable)
            .HasColumnName("is_purchasable")
            .IsRequired();

        builder.Property(card => card.Price)
            .HasColumnName("price")
            .IsRequired();

        builder.Property(card => card.MortgagePrice)
            .HasColumnName("mortgage_price")
            .IsRequired();

        builder.Property(card => card.MortgageBuyoutPrice)
            .HasColumnName("mortgage_buyout_price")
            .IsRequired();

        builder.Property(card => card.HousePrice)
            .HasColumnName("house_price")
            .IsRequired();

        builder.Property(card => card.RentWithoutHouses)
            .HasColumnName("rent_without_houses")
            .IsRequired();

        builder.Property(card => card.RentWithOneHouse)
            .HasColumnName("rent_with_one_house")
            .IsRequired();

        builder.Property(card => card.RentWithTwoHouses)
            .HasColumnName("rent_with_two_houses")
            .IsRequired();

        builder.Property(card => card.RentWithThreeHouses)
            .HasColumnName("rent_with_three_houses")
            .IsRequired();

        builder.Property(card => card.RentWithFourHouses)
            .HasColumnName("rent_with_four_houses")
            .IsRequired();

        builder.Property(card => card.RentWithHotel)
            .HasColumnName("rent_with_hotel")
            .IsRequired();
    }
}
