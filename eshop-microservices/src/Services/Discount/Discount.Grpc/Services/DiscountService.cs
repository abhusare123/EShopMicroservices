using Discount.Grpc.Data;
using Discount.Grpc.Model;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService : DicountProtoService.DicountProtoServiceBase
{
    private readonly ILogger<DiscountService> _logger;
    private readonly DiscountContext _dbContext;

    public DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon == null)
        {
            coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
        }

        _logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel!;
    }

    public override async Task<CouponModel> CreateDicount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        _dbContext.Coupons.Add(coupon);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        var deleted = _dbContext.Coupons.Remove(coupon);
        await _dbContext.SaveChangesAsync();
        var response = new DeleteDiscountResponse
        {
            Success = true
        };

        return response;
    }
}
