﻿using LoginRegisterIdentity.Models;

namespace LoginRegisterIdentity.ViewModels
{
    public class ShoppingCartVM
    {
        public int ShoppingCartId { get; set; }
        public int ProductQuantity { get; set; }
        public Product Product { get; set; }
    }
}
