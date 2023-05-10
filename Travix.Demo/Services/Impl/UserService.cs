using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Travix.Common.Exceptions;
using Travix.Common.ORM.Models;
using Travix.DB;

namespace Travix.Demo.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<long> CreateAsync(User User)
        {
            var userExist = await _unitOfWork.GenericRepository<User>().AnyAsync(x => x.Email == User.Email);
            if (userExist)
                throw new TravixArgumentException(technicalMessage: $"The user by email: {User.Email} exist.");
            _unitOfWork.GenericRepository<User>().Add(User);
            await _unitOfWork.SaveAsync();
            return User.Id;
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _unitOfWork.GenericRepository<User>().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new TravixNotFoundException("The user not exist.", technicalMessage: $"The user by id: {id} not found.");
            _unitOfWork.GenericRepository<User>().Remove(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _unitOfWork.GenericRepository<User>().Where(x => !x.Email.IsNullOrEmpty()).ToListAsync();
        }

        public async Task<User> GetAsync(long id)
        {
            var user = await _unitOfWork.GenericRepository<User>().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new TravixNotFoundException("The user not exist.", technicalMessage: $"The user by id: {id} not found.");
            return user;
        }

        public async Task UpdatAsync(long id, User user)
        {
            var dbUser = await _unitOfWork.GenericRepository<User>().FirstOrDefaultAsync(x => x.Id == id);
            if (dbUser == null)
                throw new TravixNotFoundException("The user not exist.", technicalMessage: $"The user by id: {id} not found.");
            dbUser.Email = user.Email;
            dbUser.Name = user.Name;
            await _unitOfWork.SaveAsync();
        }
    }
}
