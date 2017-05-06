using OnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
  public interface IUserService
  {
    List<User> GetAllUsers();
        void CreateUser(User newUser);
      List<User> GetList(int pageNumber, int pageSize, string sort, string sortDir);
      int Count();
  }
}
