using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Models
{
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Distance { get; set; }
        public string ImagePath { get; set; }
        public float Note { get; set; }

        public Company()
        {

        }

        public Company(string name)
        {
            var random = new Random();

            Name = name;
            Id = Guid.NewGuid().ToString();
            ImagePath = Util.Mock.ImageMockHelper.GetRandomImage();
            Distance = random.Next(0, 20);
            Note = random.Next(1, 5);

        }
    }
}
