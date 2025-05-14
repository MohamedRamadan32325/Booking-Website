using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;

public class User_Repo : IUser
{
    private readonly DepiContext dbContext;

    public User_Repo(DepiContext context)
    {
        dbContext = context;
    }

    public string GetUser(string id)
    {
        var user=dbContext.Users.Find(id);
        if (user != null)
        {
            return user.UserName;
        }
        else { return "UnKnown"; }
    }
    public RegisterViewModel Update(string id)
    {
        User ss = dbContext.User.FirstOrDefault(x => x.Id == id);
        RegisterViewModel registerSeekr = new RegisterViewModel
        {
            UserName = ss.UserName,
            Email = ss.Email,
            LastName = ss.LastName,
            MobilePhone = ss.PhoneNumber
        };
        return registerSeekr;
    }

    public void UpdateUser(RegisterViewModel s)
    {
        User ss = dbContext.User.FirstOrDefault(x => x.Id == s.Id);
        if (ss != null)
        {
            ss.UserName = s.UserName;
            ss.LastName = s.LastName;
            ss.Email = s.Email;
            ss.PhoneNumber = s.MobilePhone;

            dbContext.Update(ss);
            dbContext.SaveChanges();
        }
    }

    public bool DeleteUser(string id)
    {
        User usertodelete = dbContext.User.FirstOrDefault(x => x.Id == id);
        if (usertodelete != null)
        {
            dbContext.User.Remove(usertodelete);
            dbContext.SaveChanges();
            return true;
        }
        return false;
    }
}
