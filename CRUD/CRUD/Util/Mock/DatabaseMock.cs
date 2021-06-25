using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Util.Mock
{
    public static class DatabaseMock
    {
        public static List<Company> Companies;

        public static void InitializeMockDataBase()
        {
            Companies = new List<Company>
            {
                new Company("Pizzaria do Mario"),
                new Company("Pizzaria do Luigi"),
                new Company("Pizzaria do João"),
                new Company("Pizzaria e Hamburgueria do Zé"),
                new Company("Pizzaria e Hamburgueria Mario & Luigi"),
                new Company("Pizzaria do Roberto"),
                new Company("Pizzaria da Maria"),
                new Company("Pizzaria da Ana"),
                new Company("Top Pizza")
            };
        }

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
