using CRUD.Models.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Models
{
    public static class CompanyExtensions
    {
        public static CreateCompanyCommand ToCreateCommand(this Company company)
        {
            return new CreateCompanyCommand
            {
                Name = company.Name,
                Distance = company.Distance,
                ImagePath = Util.Mock.ImageMockHelper.GetRandomImage(),
                Note = company.Note,
                Id = Util.Mock.DatabaseMock.GenerateId()
            };
        }

        public static UpdateCompanyCommand ToUpdateCommand(this Company company)
        {
            return new UpdateCompanyCommand
            {
                Name = company.Name,
                Distance = company.Distance,
                ImagePath = company.ImagePath,
                Note = company.Note,
                Id = company.Id
            };
        }
    }

    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Distance { get; set; }
        public string ImagePath { get; set; }
        public int Note { get; set; }

        public string DistancePresentation 
        {
            get
            {
                return string.Format("{0} Km", Distance);
            }
        }

        public Company()
        {

        }

        public Company(string name)
        {
            var random = new Random();

            Name = name;
            Id = Util.Mock.DatabaseMock.GenerateId();
            ImagePath = Util.Mock.ImageMockHelper.GetRandomImage();
            Distance = random.Next(0, 20);
            Note = random.Next(1, 5);

        }
    }
}
