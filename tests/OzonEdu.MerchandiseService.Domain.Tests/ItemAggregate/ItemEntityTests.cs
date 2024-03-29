﻿using OzonEdu.MerchandiseService.Domain.AggregationModels.ItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeMerchAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests.ItemAggregate
{
    public class ItemEntityTests
    {
        [Fact]
        public void CreateInstance_ValidParameters_Success()
        {
            //Arrange
            var expectedSku = 15;
            var expectedName = "Футболка";
            var expectedClothingSize = ClothingSize.M;
            var expectedItemType = ItemType.TShirt;

            //Act
            var actualItem = new Item(
                new Sku(expectedSku),
                new Name(expectedName),
                ClothingSize.M,
                ItemType.TShirt);

            //Assert
            Assert.Equal(expectedSku, actualItem.Sku.Value);
            Assert.Equal(expectedName, actualItem.Name.Value);
            Assert.Equal(expectedClothingSize, actualItem.ClothingSize);
            Assert.Equal(expectedItemType, actualItem.ItemType);
        }

        [Fact]
        public void CreateInstance_InvalidClothingSizeAndItemType_ExceptionThrown()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ClothingSizeException>(() =>
                new Item(
                    new Sku(15),
                    new Name("Сумка"),
                    ClothingSize.M,
                    ItemType.Bag)
            );
        }
        
        [Fact]
        public void CreateInstance_InvalidName_ExceptionThrown()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<EmptyNameException>(() =>
                new Item(
                    new Sku(15),
                    new Name(string.Empty),
                    ClothingSize.M,
                    ItemType.TShirt)
            );
        }
    }
}