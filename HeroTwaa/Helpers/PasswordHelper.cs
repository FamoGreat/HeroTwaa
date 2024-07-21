﻿using Microsoft.AspNetCore.Identity;

namespace HeroTwaa.Helpers
{
    public class PasswordHelper
    {
        public static string GenerateRandomPassword(PasswordOptions? opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };
            string[] randomChars = new[]
            {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };

            Random random = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
            {
                chars.Insert(random.Next(0, chars.Count),
                     randomChars[0][random.Next(0, randomChars[0].Length)]);
            }
            if (opts.RequireLowercase)
            {
                chars.Insert(random.Next(0, chars.Count),
                    randomChars[1][random.Next(0, randomChars[1].Length)]);
            }
            if (opts.RequireDigit)
            {
                chars.Insert(random.Next(0, chars.Count),
                    randomChars[2][random.Next(0, randomChars[2].Length)]);
            }
            if (opts.RequireNonAlphanumeric)
            {
                chars.Insert(random.Next(0, chars.Count),
                    randomChars[3][random.Next(0, randomChars[3].Length)]);
            }
            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[random.Next(0, randomChars.Length)];
                chars.Insert(random.Next(0, chars.Count),
                    rcs[random.Next(0, rcs.Length)]);
            }
            return new string(chars.ToArray());
        }
    }
}
