﻿using Basket.Api.Basket.StoreBasket;

namespace Basket.Api.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBakset(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);

}