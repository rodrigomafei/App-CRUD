using CRUD.Models;
using CRUD.Models.Commands;
using CRUD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRUD.Services
{
    public class CompanyService : ICompanyService
    {
        public Company Create(CreateCompanyCommand cmd)
        {
            try
            {
                var company = cmd.ToCompany();

                Util.Mock.DatabaseMock.Companies.Add(company);

                return company;

            }

            catch(Exception e)
            {
                return null;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                Util.Mock.DatabaseMock.Companies.Remove(Util.Mock.DatabaseMock.Companies.FirstOrDefault(x => x.Id.Equals(id)));

                return true;
            }

            catch(Exception e)
            {
                return false;
            }
        }

        public IList<Company> GetAll()
        {
            try
            {
                return Util.Mock.DatabaseMock.Companies;
            }

            catch(Exception e)
            {
                return null;
            }
        }

        public Company Update(UpdateCompanyCommand cmd)
        {
            try
            {
                var company = Util.Mock.DatabaseMock.Companies.FirstOrDefault(x => x.Id.Equals(cmd.Id));

                company = cmd.ToCompany();

                return company;
            }

            catch(Exception e)
            {
                return null;
            }
        }
    }
}
