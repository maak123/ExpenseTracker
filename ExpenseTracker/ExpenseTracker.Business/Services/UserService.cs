using AutoMapper;
using ExpenseTracker.Business.Core;
using ExpenseTracker.Business.Resources;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper mapper;

        public UserService(
            IUserRepository UserRepository,
            IMapper mapper
            )
        {
            _UserRepository = UserRepository;
            this.mapper = mapper;

        }
        public async Task<UserResource> CreateAsync(UserResource transaction)
        {
            try
            {
                var updatedDetails = mapper.Map<UserResource, User>(transaction);
                var result = await _UserRepository.AddAsync(updatedDetails);
                await _UserRepository.CompleteAsync();
                return mapper.Map<User, UserResource>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<UserResource> EditAsync(UserResource User)
        {
            try
            {
                var updatedDetails = mapper.Map<UserResource, User>(User);

                var result = await _UserRepository.UpdateAsync(updatedDetails, updatedDetails.Id);
                await _UserRepository.CompleteAsync();

                return mapper.Map<User, UserResource>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserResource>> GetAllAsync()
        {
            try
            {
                IEnumerable<User> User = await _UserRepository.GetAllAsync();
                return mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(User);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<UserResource> GetByIdAsync(int id)
        {
            try
            {
                var User = await _UserRepository.FindAsync(ent => ent.Id == id);
                return mapper.Map<User, UserResource>(User);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var User = await _UserRepository.FindAsync(ent => ent.Id == id);
                _UserRepository.Delete(User);
                await _UserRepository.CompleteAsync();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<IEnumerable<UserResource>> SearchAsync(string hint)
        {
            try
            {
                if (hint == null || hint == "")
                {
                    IEnumerable<User> User = await _UserRepository.GetAllAsync();
                    return mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(User);

                }
                else
                {
                    var User = await _UserRepository.FindAllAsync(ent => ent.FirstName.Equals(hint));
                    return mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(User);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
