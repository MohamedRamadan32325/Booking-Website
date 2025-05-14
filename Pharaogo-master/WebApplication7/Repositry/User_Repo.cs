using AutoMapper;
using WebApplication7.Data;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;

namespace WebApplication7.Repositry
{
    public class User_Repo : IUser
    {
        private readonly DepiContext _dbContext;
        private readonly IMapper _mapper;

        public User_Repo(DepiContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public string GetUser(string id)
        {
            var user = _dbContext.Users.Find(id);
            return user != null ? user.UserName : "Unknown";
        }
        
        public RegisterViewModel Update(string id)
        {
            User user = _dbContext.User.FirstOrDefault(x => x.Id == id);
            
            if (user == null)
            {
                return null;
            }
            
            return _mapper.Map<RegisterViewModel>(user);
        }

        public void UpdateUser(RegisterViewModel model)
        {
            User user = _dbContext.User.FirstOrDefault(x => x.Id == model.Id);
            if (user != null)
            {
                _mapper.Map(model, user);

                _dbContext.Update(user);
                _dbContext.SaveChanges();
            }
        }

        public bool DeleteUser(string id)
        {
            User userToDelete = _dbContext.User.FirstOrDefault(x => x.Id == id);
            if (userToDelete != null)
            {
                _dbContext.User.Remove(userToDelete);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
