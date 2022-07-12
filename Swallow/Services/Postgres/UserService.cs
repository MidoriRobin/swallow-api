using System;
using AutoMapper;
using Swallow.Models;
using Swallow.Models.Requests;
// ! Why do i have to do this
using BCryptNet = BCrypt.Net.BCrypt;

namespace Swallow.Services.Postgres;
    public interface IUserService 
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Create(CreateRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }
    public class UserService : IUserService
    {
        private SwallowContext _context;

        private readonly IMapper _mapper;

        public UserService(SwallowContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            Console.WriteLine("Printing model------------------");

            Console.WriteLine(model.ToString());

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
            User returnedUser = _context.Users.Where(x => x.Email == email).First();

            if (!BCryptNet.Verify(password, returnedUser.Password))
            {
                // throw AuthenticationException("");
                returnedUser = null;
            }


            return returnedUser;
        }
    
    }
