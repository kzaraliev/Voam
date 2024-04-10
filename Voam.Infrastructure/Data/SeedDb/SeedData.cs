using Microsoft.AspNetCore.Identity;
using Voam.Infrastructure.Data.Models;

namespace Voam.Infrastructure.Data.SeedDb
{
    internal class SeedData
    {
        public ApplicationUser AdminUser { get; set; } = null!;
        public ApplicationUser CustomerUser { get; set; } = null!;

        public Product BlackSheepHoodie { get; set; } = null!;
        public Product DevotionBeanie { get; set; } = null!;
        public Product DevotionTShirt { get; set; } = null!;
        public Product MidnightGreenHoodie { get; set; } = null!;
        public Product MidnightGreenPants { get; set; } = null!;
        public Product VoamHoodie { get; set; } = null!;

        public ProductImage BlackSheepHoodieImg1 { get; set; } = null!;
        public ProductImage BlackSheepHoodieImg2 { get; set; } = null!;
        public ProductImage BlackSheepHoodieImg3 { get; set; } = null!;

        public ProductImage DevotionBeanieImg1 { get; set; } = null!;
        public ProductImage DevotionBeanieImg2 { get; set; } = null!;

        public ProductImage DevotionTShirtImg1 { get; set; } = null!;
        public ProductImage DevotionTShirtImg2 { get; set; } = null!;

        public ProductImage MidnightGreenHoodieImg1 { get; set; } = null!;
        public ProductImage MidnightGreenHoodieImg2 { get; set; } = null!;

        public ProductImage MidnightGreenPantsImg1 { get; set; } = null!;
        public ProductImage MidnightGreenPantsImg2 { get; set; } = null!;

        public ProductImage VoamHoodieImg1 { get; set; } = null!;
        public ProductImage VoamHoodieImg2 { get; set; } = null!;

        public Size BlackSheepHoodieSizeS { get; set; } = null!;
        public Size BlackSheepHoodieSizeM { get; set; } = null!;
        public Size BlackSheepHoodieSizeL { get; set; } = null!;

        public Size DevotionBeanieSizeM { get; set; } = null!;

        public Size DevotionTShirtSizeS { get; set; } = null!;
        public Size DevotionTShirtSizeM { get; set; } = null!;
        public Size DevotionTShirtSizeL { get; set; } = null!;

        public Size MidnightGreenHoodieSizeM { get; set; } = null!;
        public Size MidnightGreenHoodieSizeL { get; set; } = null!;

        public Size MidnightGreenPantsSizeM { get; set; } = null!;
        public Size MidnightGreenPantsSizeL { get; set; } = null!;

        public Size VoamHoodieSizeM { get; set; } = null!;

        public ShoppingCart ShoppingCartAdmin { get; set; } = null!;
        public ShoppingCart ShoppingCartCustomer { get; set; } = null!;

        public OrderItem OrderItemDevotionTShirt { get; set; } = null!;
        public OrderItem OrderItemDevotionBeanie1 { get; set; } = null!;
        public OrderItem OrderItemDevotionBeanie2 { get; set; } = null!;
        public OrderItem OrderItemMidnightGreenHoodie { get; set; } = null!;
        public OrderItem OrderItemMidnightGreenPants { get; set; } = null!;
        public OrderItem OrderItemBlackSheepHoodie1 { get; set; } = null!;
        public OrderItem OrderItemBlackSheepHoodie2 { get; set; } = null!;
        public OrderItem OrderItemVoamHoodie1 { get; set; } = null!;
        public OrderItem OrderItemVoamHoodie2 { get; set; } = null!;


        public Order OrderCustomerUser1 { get; set; } = null!;
        public Order OrderCustomerUser2 { get; set; } = null!;
        public Order OrderCustomerUser3 { get; set; } = null!;
        public Order OrderCustomerUser4 { get; set; } = null!;
        public Order OrderCustomerUser5 { get; set; } = null!;
        public Order OrderCustomerUser6 { get; set; } = null!;
        public Order OrderCustomerUser7 { get; set; } = null!;

        public SeedData()
        {
            SeedUsers();
            SeedShoppingCarts();
            SeedProducts();
            SeedImages();
            SeedSizes();
            SeedOrderItems();
            SeedOrders();
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            AdminUser = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                PhoneNumber = "0876933440",
                FirstName = "Martin",
                LastName = "Hristov"
            };

            AdminUser.PasswordHash =
                 hasher.HashPassword(AdminUser, "Vo@m2024");

            CustomerUser = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "kosebose@mail.com",
                NormalizedUserName = "KOSEBOSE@MAIL.COM",
                Email = "kosebose@mail.com",
                NormalizedEmail = "KOSEBOSE@MAIL.COM",
                PhoneNumber = "0876933660",
                FirstName = "Konstantin",
                LastName = "Zaraliev"
            };

            CustomerUser.PasswordHash =
            hasher.HashPassword(CustomerUser, "Kz123");
        }

        private void SeedProducts()
        {
            BlackSheepHoodie = new Product()
            {
                Id = 1,
                Name = "“BLACK SHEEP” HOODIE",
                Description = "Embrace your unique style with our \"Black Sheep\" hoodie. Designed for those who stand out from the crowd, this hoodie features a luxurious, soft fabric that ensures comfort and warmth in any weather.",
                Price = 80M,
            };

            DevotionBeanie = new Product()
            {
                Id = 2,
                Name = "“DEVOTION” BEANIE",
                Description = "Embrace warmth and style with our \"Devotion\" beanie. This cozy accessory is made from soft, stretchy material for ultimate comfort and a perfect fit.",
                Price = 25M,
            };

            DevotionTShirt = new Product()
            {
                Id = 3,
                Name = "“DEVOTION” T-SHIRT",
                Description = "Showcase your commitment in comfort with our \"Devotion\" T-shirt. This tee combines soft, breathable fabric with a relaxed fit, ensuring all-day ease and style. Adorned with a unique emblem that signifies passion and dedication, it's crafted for those who wear their heart on their sleeve.",
                Price = 50M,
            };

            MidnightGreenHoodie = new Product()
            {
                Id = 4,
                Name = "MIDNIGHT GREEN HOODIE",
                Description = "Dive into the depths of style with our \"Midnight Green\" hoodie. Crafted from premium, soft material, this hoodie promises not only unmatched comfort but also enduring warmth. Its rich, midnight green hue evokes a sense of mystery and sophistication, making it a versatile addition to any wardrobe.",
                Price = 100M,
            };

            MidnightGreenPants = new Product()
            {
                Id = 5,
                Name = "MIDNIGHT GREEN PANTS",
                Description = "Elevate your everyday look with our \"Midnight Green\" pants. Tailored from a premium, comfortable fabric, these pants offer both flexibility and durability. The deep, enigmatic shade of midnight green adds a layer of sophistication and versatility to your wardrobe, effortlessly blending with a variety of styles.",
                Price = 100M,
            };

            VoamHoodie = new Product()
            {
                Id = 6,
                Name = "“VOAM*” EMBROIDERY HOODIE",
                Description = "Discover the essence of craftsmanship with our \"VOAM*\" Embroidery Hoodie. This piece merges the warmth of a premium, soft fabric with the intricacy of detailed embroidery, showcasing the \"VOAM*\" logo in a display of artistic finesse.",
                Price = 80M,
            };
        }

        private void SeedImages()
        {
            BlackSheepHoodieImg1 = new ProductImage()
            {
                Id = 1,
                ImageData = ConvertImageToByteArray("blackSheep1.png"),
                ProductId = 1,
            };

            BlackSheepHoodieImg2 = new ProductImage()
            {
                Id = 2,
                ImageData = ConvertImageToByteArray("blackSheep2.png"),
                ProductId = 1,
            };

            BlackSheepHoodieImg3 = new ProductImage()
            {
                Id = 3,
                ImageData = ConvertImageToByteArray("blackSheep3.png"),
                ProductId = 1,
            };

            DevotionBeanieImg1 = new ProductImage()
            {
                Id = 4,
                ImageData = ConvertImageToByteArray("beani1.png"),
                ProductId = 2,
            };

            DevotionBeanieImg2 = new ProductImage()
            {
                Id = 5,
                ImageData = ConvertImageToByteArray("beani2.png"),
                ProductId = 2,
            };

            DevotionTShirtImg1 = new ProductImage()
            {
                Id = 6,
                ImageData = ConvertImageToByteArray("DevotionTShirt1.png"),
                ProductId = 3,
            };

            DevotionTShirtImg2 = new ProductImage()
            {
                Id = 7,
                ImageData = ConvertImageToByteArray("DevotionTShirt2.png"),
                ProductId = 3,
            };

            MidnightGreenHoodieImg1 = new ProductImage()
            {
                Id = 8,
                ImageData = ConvertImageToByteArray("MidnightGreenHoodie1.png"),
                ProductId = 4,
            };

            MidnightGreenHoodieImg2 = new ProductImage()
            {
                Id = 9,
                ImageData = ConvertImageToByteArray("MidnightGreenHoodie2.png"),
                ProductId = 4,
            };

            MidnightGreenPantsImg1 = new ProductImage()
            {
                Id = 10,
                ImageData = ConvertImageToByteArray("MidnightGreenPants1.png"),
                ProductId = 5,
            };

            MidnightGreenPantsImg2 = new ProductImage()
            {
                Id = 11,
                ImageData = ConvertImageToByteArray("MidnightGreenPants2.png"),
                ProductId = 5,
            };

            VoamHoodieImg1 = new ProductImage()
            {
                Id = 12,
                ImageData = ConvertImageToByteArray("Voam1.png"),
                ProductId = 6,
            };

            VoamHoodieImg2 = new ProductImage()
            {
                Id = 13,
                ImageData = ConvertImageToByteArray("Voam2.png"),
                ProductId = 6,
            };
        }

        private byte[] ConvertImageToByteArray(string imageName)
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            var reletivePath = Directory.GetParent(workingDirectory)?.ToString() ?? throw new Exception("An error occurred during seeding");
            var filePath = Path.Combine(reletivePath, @"Voam.Infrastructure\Data\SeedDb\Images");

            string imagePath = Path.Combine(filePath, imageName);
            byte[] imageData = File.ReadAllBytes(imagePath);
            return imageData;
        }

        private void SeedSizes()
        {
            BlackSheepHoodieSizeS = new Size()
            {
                Id = 1,
                ProductId = 1,
                Quantity = 5,
                SizeChar = 'S',
            };

            BlackSheepHoodieSizeM = new Size()
            {
                Id = 2,
                ProductId = 1,
                Quantity = 2,
                SizeChar = 'M',
            };

            BlackSheepHoodieSizeL = new Size()
            {
                Id = 3,
                ProductId = 1,
                Quantity = 6,
                SizeChar = 'L',
            };

            DevotionBeanieSizeM = new Size()
            {
                Id = 4,
                ProductId = 2,
                Quantity = 10,
                SizeChar = 'M'
            };

            DevotionTShirtSizeS = new Size()
            {
                Id = 5,
                ProductId = 3,
                Quantity = 4,
                SizeChar = 'S'
            };

            DevotionTShirtSizeM = new Size()
            {
                Id = 6,
                ProductId = 3,
                Quantity = 5,
                SizeChar = 'M'
            };

            DevotionTShirtSizeL = new Size()
            {
                Id = 7,
                ProductId = 3,
                Quantity = 4,
                SizeChar = 'L'
            };

            MidnightGreenHoodieSizeM = new Size()
            {
                Id = 8,
                ProductId = 4,
                Quantity = 2,
                SizeChar = 'M'
            };

            MidnightGreenHoodieSizeL = new Size()
            {
                Id = 9,
                ProductId = 4,
                Quantity = 5,
                SizeChar = 'L'
            };

            MidnightGreenPantsSizeM = new Size()
            {
                Id = 10,
                ProductId = 5,
                Quantity = 6,
                SizeChar = 'M'
            };

            MidnightGreenPantsSizeL = new Size()
            {
                Id = 11,
                ProductId = 5,
                Quantity = 3,
                SizeChar = 'L'
            };

            VoamHoodieSizeM = new Size()
            {
                Id = 12,
                ProductId = 6,
                Quantity = 7,
                SizeChar = 'M'
            };
        }

        private void SeedShoppingCarts()
        {
            ShoppingCartAdmin = new ShoppingCart()
            {
                Id = 1,
                CustomerId = "dea12856-c198-4129-b3f3-b893d8395082",
            };

            ShoppingCartCustomer = new ShoppingCart()
            {
                Id = 2,
                CustomerId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
            };
        }

        private void SeedOrderItems()
        {
            OrderItemDevotionTShirt = new OrderItem()
            {
                Id = 1,
                Name = "“DEVOTION” T-SHIRT",
                Quantity = 2,
                SizeChar = 'S',
                Price = 50M,
                OrderId = 1,
            };

            OrderItemDevotionBeanie1 = new OrderItem()
            {
                Id = 2,
                Name = "“DEVOTION” BEANIE",
                Quantity = 1,
                SizeChar = 'M',
                Price = 25M,
                OrderId = 1,
            };

            OrderItemDevotionBeanie2 = new OrderItem()
            {
                Id = 3,
                Name = "“DEVOTION” BEANIE",
                Quantity = 3,
                SizeChar = 'M',
                Price = 25M,
                OrderId = 2,
            };

            OrderItemMidnightGreenHoodie = new OrderItem()
            {
                Id = 4,
                Name = "MIDNIGHT GREEN HOODIE",
                Quantity = 1,
                SizeChar = 'L',
                Price = 100M,
                OrderId = 3,
            };

            OrderItemMidnightGreenPants = new OrderItem()
            {
                Id = 5,
                Name = "MIDNIGHT GREEN PANTS",
                Quantity = 1,
                SizeChar = 'L',
                Price = 100M,
                OrderId = 3,
            };

            OrderItemBlackSheepHoodie1 = new OrderItem()
            {
                Id = 6,
                Name = "“BLACK SHEEP” HOODIE",
                Quantity = 2,
                SizeChar = 'S',
                Price = 80M,
                OrderId = 4,
            };

            OrderItemBlackSheepHoodie2 = new OrderItem()
            {
                Id = 7,
                Name = "“BLACK SHEEP” HOODIE",
                Quantity = 1,
                SizeChar = 'L',
                Price = 80M,
                OrderId = 5,
            };

            OrderItemVoamHoodie1 = new OrderItem()
            {
                Id = 8,
                Name = "“VOAM*” EMBROIDERY HOODIE",
                Quantity = 1,
                SizeChar = 'L',
                Price = 80M,
                OrderId = 6,
            };

            OrderItemVoamHoodie2 = new OrderItem()
            {
                Id = 9,
                Name = "“VOAM*” EMBROIDERY HOODIE",
                Quantity = 1,
                SizeChar = 'S',
                Price = 80M,
                OrderId = 7,
            };
        }

        private void SeedOrders()
        {
            OrderCustomerUser1 = new Order()
            {
                Id = 1,
                CustomerId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                OrderDate = new DateTime(2024, 3, 8),
                FullName = "Konstantin Zaraliev",
                Email = "kosebose@mail.com",
                PhoneNumber = "0876933660",
                Econt = "булевард „Александър Стамболийски“ №215, 215 217, 1213 София",
                PaymentMethod = "Upon delivery",
                City = "София",
                TotalPrice = 125M,
            };

            OrderCustomerUser2 = new Order()
            {
                Id = 2,
                CustomerId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                OrderDate = new DateTime(2024, 3, 10),
                FullName = "Konstantin Zaraliev",
                Email = "kosebose@mail.com",
                PhoneNumber = "0876933660",
                Econt = "ж.к. Сердика, ул. „Гюешево“ №83, 1000 София",
                PaymentMethod = "Upon delivery",
                City = "София",
                TotalPrice = 75M,
            };

            OrderCustomerUser3 = new Order()
            {
                Id = 3,
                CustomerId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                OrderDate = new DateTime(2024, 3, 16),
                FullName = "Konstantin Zaraliev",
                Email = "kosebose@mail.com",
                PhoneNumber = "0876933660",
                Econt = "НПЗ Военна рампа, бул. „Илиянци“ №12, 1062 София",
                PaymentMethod = "Upon delivery",
                City = "София",
                TotalPrice = 200M,
            };

            OrderCustomerUser4 = new Order()
            {
                Id = 4,
                CustomerId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                OrderDate = new DateTime(2024, 3, 16),
                FullName = "Konstantin Zaraliev",
                Email = "kosebose@mail.com",
                PhoneNumber = "0876933660",
                Econt = "Район, ул. „Беласица“ №49, 4000 Пловдив",
                PaymentMethod = "Upon delivery",
                City = "Пловдив",
                TotalPrice = 160M,
            };

            OrderCustomerUser5 = new Order()
            {
                Id = 5,
                CustomerId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                OrderDate = new DateTime(2024, 3, 24),
                FullName = "Konstantin Zaraliev",
                Email = "kosebose@mail.com",
                PhoneNumber = "0876933660",
                Econt = "Район Южен, ул, Бл. „Кукленско шосе“ №11, 4004 Пловдив",
                PaymentMethod = "Upon delivery",
                City = "Пловдив",
                TotalPrice = 80M,
            };

            OrderCustomerUser6 = new Order()
            {
                Id = 6,
                CustomerId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                OrderDate = new DateTime(2024, 4, 2),
                FullName = "Konstantin Zaraliev",
                Email = "kosebose@mail.com",
                PhoneNumber = "0876933660",
                Econt = "Район Западен, ул. „Мизия“ №28, 5002 Велико Търново",
                PaymentMethod = "Upon delivery",
                City = "Велико Търново",
                TotalPrice = 80M,
            };

            OrderCustomerUser7 = new Order()
            {
                Id = 7,
                CustomerId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                OrderDate = new DateTime(2024, 4, 6),
                FullName = "Konstantin Zaraliev",
                Email = "kosebose@mail.com",
                PhoneNumber = "0876933660",
                Econt = "ж.к. Зона Б, ул. „Стоян Коледаров“ №44, 5000 Велико Търново",
                PaymentMethod = "Upon delivery",
                City = "Велико Търново",
                TotalPrice = 80M,
            };
        }

    }
}