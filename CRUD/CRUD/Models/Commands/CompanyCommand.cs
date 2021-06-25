using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Models.Commands
{
    public static class CompanyCommandExtensions
    {
        public static Company ToCompany(this CreateCompanyCommand cmd)
        {
            return new Company
            {
                Distance = cmd.Distance,
                Id = cmd.Id,
                ImagePath = cmd.ImagePath,
                Name = cmd.Name,
                Note = cmd.Note
            };
        }

        public static Company ToCompany(this UpdateCompanyCommand cmd)
        {
            return new Company
            {
                Distance = cmd.Distance,
                Id = cmd.Id,
                ImagePath = cmd.ImagePath,
                Name = cmd.Name,
                Note = cmd.Note
            };
        }
    }

    public class CompanyCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Distance { get; set; }
        public int Note { get; set; }

        public string ErrorMsg 
        {
            get
            {
                var errorMsg = string.Empty;

                if (string.IsNullOrWhiteSpace(Name))
                    errorMsg += "Necessário preencher o nome\n";

                if (string.IsNullOrWhiteSpace(ImagePath))
                    errorMsg += "Imagem inválida\n";

                if (Distance < 0)
                    errorMsg += "A distância não pode ser menor que zero\n";

                if (Note < 1 || Note > 5)
                    errorMsg += "A nota deve ser entre 1 e 5\n";

                return errorMsg;
            } 
        }

        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(ErrorMsg);
        }
    }

    public class CreateCompanyCommand : CompanyCommand
    {

    }

    public class UpdateCompanyCommand : CompanyCommand
    {
       
    }
}
