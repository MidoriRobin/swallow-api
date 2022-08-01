using System;
using AutoMapper;
using Swallow.Authorization;
using Swallow.Models;
using Swallow.Models.Requests;
using Swallow.Util;
// ! Why do i have to do this
using BCryptNet = BCrypt.Net.BCrypt;

namespace Swallow.Services.Postgres;
    public interface IUserService 
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Create(CreateRequest model);
        void Update(int id, UpdateRequest model);
        void Update(int id, User user);
        void Delete(int id);

        User CredCheck(string email, string password);
        string Authenticate(User userData);

        void invalidateToken(string userId, string token);

        bool IsLoggedIn(int userId);

        void RevokeToken(string token, string ipAddress);

    }
    public class UserService : IUserService
    {
        private SwallowContext _context;

        private readonly IMapper _mapper;

        private readonly IJwtAuth _jwtAuth;

        public UserService(SwallowContext context, IMapper mapper, IJwtAuth jwtAuth)
        {
            _context = context;
            _mapper = mapper;
            _jwtAuth = jwtAuth;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public void Create(CreateRequest model) 
        {
            // validate
            if(_context.Users.Any(x => x.Email == model.Email))
                throw new ApplicationException("User with the email '" + model.Email + "' already exists");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.Password = BCryptNet.HashPassword(model.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            // var newUser = _context.Users.Where(u => u.Email == user.Email).First();

            // return newUser;
        }

        public void Update(int id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.Email != user.Email && _context.Users.Any(x => x.Email == model.Email))
                throw new ApplicationException("User with the email '" + model.Email + "' already exists");

            // hash password if it was entered
            if(!string.IsNullOrEmpty(model.Password))
                user.Password = BCryptNet.HashPassword(model.Password);

            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();

        }

        public void Update(int id, User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        private User getUser(int id)
        {
            var user = _context.Users.Find(id);

            if(user == null) throw new KeyNotFoundException("User not found");

            return user;
        }
    
        public User CredCheck(string email, string password)
        {
            User? returnedUser = _context.Users.Where(x => x.Email == email).FirstOrDefault();

            if (returnedUser != null && !BCryptNet.Verify(password, returnedUser.Password))
            {
                // throw AuthenticationException("");
                returnedUser = null;
            }


            return returnedUser;
        }
    
        public string Authenticate(User userData)
        {
            string token = null;

            // token = _jwtAuth.Authentication(userData);

            return token;
        }

        public void invalidateToken(string userId, string token)
        {
            DateTime nullDate = DateTime.UnixEpoch;

            User user = getUser(int.Parse(userId));

            
            // TokenBlacklist blacklistedToken = new TokenBlacklist();
            // blacklistedToken.UserId = user.Id;
            // blacklistedToken.Token = token;
            // blacklistedToken.EntryDate = user.TokenExpiry;

            user.TokenExpiry = nullDate;
            // _context.TokenBlacklist.Add(blacklistedToken);
            _context.Users.Update(user);

            _context.SaveChanges();
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var user = getAccountByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");
            
            revokeRefreshToken(refreshToken, ipAddress);

            _context.Update(user);
            _context.SaveChanges();
        }

        private User getAccountByRefreshToken(string token)
        {
            var user = _context.Users.SingleOrDefault(x => x.RefreshTokens.Any(t => t.Token == token));
            if (user == null) throw new AppException("Invalid token");
            return user;
        }

        private void revokeRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
        }

        public bool IsLoggedIn(int userId)
        {
            // Time is stored in utc so use ToLocalTime
            
            DateTime expiry = _context.Users.Find(userId).TokenExpiry.Value;

            //TODO: Write into a function to process all date times coming from the database
            DateTime utcExpiry = DateTime.SpecifyKind(expiry, DateTimeKind.Utc);

            DateTime dateToLocal = utcExpiry.ToLocalTime();

            bool isOldDate = DateTime.Now.Subtract(dateToLocal).Hours >= 1 || DateTime.Now.Subtract(dateToLocal).Minutes >= 1;

            if (dateToLocal == DateTime.UnixEpoch || isOldDate)
            {
                return false;
            }

            return true;
        }
    }
