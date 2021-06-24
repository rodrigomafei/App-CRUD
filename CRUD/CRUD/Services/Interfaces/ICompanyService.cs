using CRUD.Models;
using CRUD.Models.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Services.Interfaces
{
    public interface ICompanyService
    {
        IList<Company> GetAll();

        Company Create(CreateCompanyCommand cmd);

        Company Update(UpdateCompanyCommand cmd);

        bool Delete(string id);
    }
}
