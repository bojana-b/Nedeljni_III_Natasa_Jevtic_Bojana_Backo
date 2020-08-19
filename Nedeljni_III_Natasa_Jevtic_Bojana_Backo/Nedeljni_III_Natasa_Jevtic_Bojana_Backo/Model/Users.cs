using Nedeljni_III_Natasa_Jevtic_Bojana_Backo.HelperMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo.Model
{
    class Users
    {
        // Method that add User to database
        public bool AddUser(vwUser user)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    tblUser newUser = new tblUser();
                    newUser.NameAndSurname = user.NameAndSurname;
                    newUser.Username = user.Username;
                    newUser.Password = SecurePasswordHasher.Hash(user.Password);

                    context.tblUsers.Add(newUser);
                    context.SaveChanges();
                    user.UserId = newUser.UserId;
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        // Method that reads all Users from database
        public List<vwUser> GetAllUsers()
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    List<vwUser> list = new List<vwUser>();
                    list = (from x in context.vwUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        // Methot to check if User username exists in database
        public bool IsUser(string username)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    vwUser user = (from e in context.vwUsers where e.Username == username select e).First();

                    if (user == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        public vwUser FindUser(string username)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    vwUser user = (from e in context.vwUsers where e.Username == username select e).First();
                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public vwUser FindUser(int id)
        {
            try
            {
                using (RecipesDBEntities context = new RecipesDBEntities())
                {
                    vwUser user = (from e in context.vwUsers where e.UserId == id select e).First();
                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
