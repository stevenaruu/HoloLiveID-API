using HoloLiveID_API.Data;
using HoloLiveID_API.Input;
using HoloLiveID_API.Model;
using HoloLiveID_API.Output;
using System.Diagnostics;

namespace HoloLiveID_API.Helper
{
    public class HoloUserHelper
    {
        private readonly HoloLiveIDContext _dbContext;
        private static bool _ensureCreated { get; set; } = false;
        public HoloUserHelper(HoloLiveIDContext dbContext)
        {
            _dbContext = dbContext;

            if (!_ensureCreated)
            {
                _dbContext.Database.EnsureCreated();
                _ensureCreated = true;
            }
        }

        public List<HoloUser> GetAllUsers()
        {
            try
            {
                var holoUsers = _dbContext.HoloUsers.ToList();
                return holoUsers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public HoloUser GetUser(string id)
        {
            try
            {
                var user = _dbContext.HoloUsers.Where(x => x.HoloId == id).FirstOrDefault();
                if (user != null) return user;

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public StatusOutput CreateUser(HoloUser data)
        {
            var output = new StatusOutput();
            try
            {
                if (data != null)
                {
                    var newHoloUser = new HoloUser
                    {
                        HoloId = data.HoloId,
                        Name = data.Name,
                        Gen = data.Gen,
                        Description = data.Description,
                        Birthdate = data.Birthdate,
                        Height = data.Height,
                        Zodiac = data.Zodiac,
                    };

                    _dbContext.HoloUsers.Add(newHoloUser);
                    _dbContext.SaveChangesAsync();

                    output.statusCode = 200;
                    output.message = "Holo user has arrived!";

                    return output;
                }

                output.statusCode = 500;
                output.message = "Error";

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public StatusOutput UpdateUser(string id, HoloUserInput data)
        {
            var output = new StatusOutput();
            try
            {
                var user = _dbContext.HoloUsers.Where(x => x.HoloId == id).FirstOrDefault();
                if (user != null)
                {
                    user.Name = data.Name ?? user.Name;
                    user.Gen = data.Gen ?? user.Gen;
                    user.Description = data.Description ?? user.Description;
                    user.Birthdate = data.Birthdate ?? user.Birthdate;
                    user.Height = data.Height ?? user.Height;
                    user.Zodiac = data.Zodiac ?? user.Zodiac;

                    _dbContext.HoloUsers.Update(user);
                    _dbContext.SaveChangesAsync();

                    output.statusCode = 200;
                    output.message = "Update success";

                    return output;
                }

                output.statusCode = 404;
                output.message = "User is not exist";

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public StatusOutput DeleteUser(string id)
        {
            var output = new StatusOutput();
            try
            {
                var user = _dbContext.HoloUsers.Where(x => x.HoloId == id).FirstOrDefault();
                if (user != null)
                {
                    _dbContext.HoloUsers.Remove(user);
                    _dbContext.SaveChangesAsync();

                    output.statusCode = 200;
                    output.message = "User deleted";

                    return output;
                }

                output.statusCode = 404;
                output.message = "User is not exist";

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
