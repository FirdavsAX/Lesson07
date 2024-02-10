using Bogus;
using Lesson07.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson07.TestDataCreator
{
    public class SaleProductCreator
    {
        public static SaleProduct SaleProductCreate(int[] ProdcutsId, int[] SalesID)
        {
            Faker faker = new Faker();
            SaleProduct sale = new();

            sale.Quantity = faker.Random.Int(1, 100);
            var unitPrice = faker.Random.Decimal(100_000, 1_000_000);
            sale.UnitPrice = unitPrice;
            sale.Discount = faker.Random.Decimal(0, unitPrice / 2);
            sale.ProductId = faker.Random.ArrayElement(ProdcutsId);
            sale.SaleId = faker.Random.ArrayElement(SalesID);

            return sale;
        }
    }
}
