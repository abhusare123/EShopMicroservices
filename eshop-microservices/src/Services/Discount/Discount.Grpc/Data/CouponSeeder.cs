using Discount.Grpc.Model;

namespace Discount.Grpc.Data;

public class CouponSeeder(DiscountContext dbContext)
{
    public void Seed()
    {
        var coupons = new List<Coupon>
        {
            new Coupon { ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
            new Coupon { ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 },
            new Coupon { ProductName = "Huawei P30", Description = "Huawei Discount", Amount = 50 }
        };

        foreach (var coupon in coupons)
        {
            if (!dbContext.Coupons.Any(x => x.ProductName == coupon.ProductName))
            {
                dbContext.Coupons.Add(coupon);
            }
        }

        dbContext.SaveChanges();
    }

}
