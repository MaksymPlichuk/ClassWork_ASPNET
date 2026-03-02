using Microsoft.AspNetCore.Mvc;
using MVC_Shop.Models;
using MVC_Shop.ViewModels;

namespace MVC_Shop.Services
{
    public static class CartService
    {
        public static void AddToCart(ISession session, int id)
        {
            var items = session.Get<List<CartItemVM>>() ?? new List<CartItemVM>();
            items.Add(new CartItemVM { ProductId = id });
            session.Set(items);
        }

        public static void RemoveFromCart(ISession session, int id)
        {
            var items = session.Get<List<CartItemVM>>() ?? new List<CartItemVM>();
            var newList = items.Where(p => p.ProductId != id);
            session.Set(newList);
        }
        public static void Increment(ISession session, int id)
        {

        }
        public static void Decrement(ISession session, int id)
        {

        }
        public static bool IsInCart(ISession session, int id)
        {
            var items = session.Get<List<CartItemVM>>() ?? new List<CartItemVM>();
            var res = items.Any(p => p.ProductId == id);
            return res;
        }
        public static int ItemsCount(ISession session)
        {
            var items = session.Get<List<CartItemVM>>() ?? new List<CartItemVM>();
            return items.Count();
        }

    }
}
