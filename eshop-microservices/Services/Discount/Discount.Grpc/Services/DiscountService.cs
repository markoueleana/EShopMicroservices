using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService 
        (DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(DiscountRequest request, ServerCallContext context)
        {

            var coupon = await dbContext
                 .Coupons
                 .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon == null)
                coupon = new Coupon{ ProductName = "No Discount", Amount = 0, Description = "" };

            var couponModel = coupon.Adapt<CouponModel>();
            logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", couponModel.ProductName, couponModel.Amount);
            return couponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request object."));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully created for ProductName : {ProductName}, Amount : {Amount}, Description {Description}", coupon.ProductName, coupon.Amount, coupon.Description);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            return base.UpdateDiscount(request, context);
        }
        public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        } 
    }
}
