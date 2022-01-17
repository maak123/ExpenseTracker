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
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _TransactionRepository;
        private readonly ICategoryRepository _ICategoryRepository;
        private readonly IUserRepository _IUserRepository;

        private readonly IMapper mapper;

        public TransactionService(
            ITransactionRepository TransactionRepository,
            ICategoryRepository CategoryRepository,
            IUserRepository UserRepository,
        IMapper mapper
            )
        {
            _TransactionRepository = TransactionRepository;
            _ICategoryRepository = CategoryRepository;
            _IUserRepository = UserRepository;
            this.mapper = mapper;

        }
        public async Task<TransactionResource> CreateAsync(TransactionAddUpdateResource transaction)
        {
            try
            {
                var updatedDetails = mapper.Map<TransactionAddUpdateResource, Transactions>(transaction);
                updatedDetails.Category = await _ICategoryRepository.FindAsync(c=>c.Id == transaction.CategoryId);
                updatedDetails.User = await _IUserRepository.FindAsync(c => c.Id == transaction.UserId);

                var result = await _TransactionRepository.AddAsync(updatedDetails);
                await _TransactionRepository.CompleteAsync();
                return mapper.Map<Transactions, TransactionResource>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<TransactionResource> EditAsync(TransactionAddUpdateResource Transaction)
        {
            try
            {
                var updatedDetails = mapper.Map<TransactionAddUpdateResource, Transactions>(Transaction);
                updatedDetails.Category = await _ICategoryRepository.FindAsync(c => c.Id == Transaction.CategoryId);
                updatedDetails.User = await _IUserRepository.FindAsync(c => c.Id == Transaction.UserId);

                var result = await _TransactionRepository.UpdateAsync(updatedDetails, updatedDetails.Id);
                await _TransactionRepository.CompleteAsync();

                return mapper.Map<Transactions, TransactionResource>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TransactionResource>> GetAllAsync(int userId)
        {
            try
            {
                var Transaction = await _TransactionRepository.FindByAllIncludingAsync(t=>t.User.Id== userId, t=>t.Category);
                return mapper.Map<IEnumerable<Transactions>, IEnumerable<TransactionResource>>(Transaction);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<TransactionResource> GetByIdAsync(int id)
        {
            try
            {
                var Transaction = await _TransactionRepository.FindAsync(ent => ent.Id == id);
                return mapper.Map<Transactions, TransactionResource>(Transaction);
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
                var Transaction = await _TransactionRepository.FindAsync(ent => ent.Id == id);
                _TransactionRepository.Delete(Transaction);
                await _TransactionRepository.CompleteAsync();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<IEnumerable<TransactionResource>> SearchAsync(string hint)
        {
            try
            {
                if (hint == null || hint == "")
                {
                    IEnumerable<Transactions> Transaction = await _TransactionRepository.GetAllAsync();
                    return mapper.Map<IEnumerable<Transactions>, IEnumerable<TransactionResource>>(Transaction);

                }
                else
                {
                    var Transaction = await _TransactionRepository.FindAllAsync(ent => ent.Amount.Equals(hint));
                    return mapper.Map<IEnumerable<Transactions>, IEnumerable<TransactionResource>>(Transaction);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
