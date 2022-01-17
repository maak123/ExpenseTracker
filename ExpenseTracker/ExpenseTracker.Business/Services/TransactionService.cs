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
        private readonly IMapper mapper;

        public TransactionService(
            ITransactionRepository TransactionRepository,
            IMapper mapper
            )
        {
            _TransactionRepository = TransactionRepository;
            this.mapper = mapper;

        }
        public async Task<TransactionResource> CreateAsync(TransactionResource transaction)
        {
            try
            {
                var updatedDetails = mapper.Map<TransactionResource, Transactions>(transaction);
                var result = await _TransactionRepository.AddAsync(updatedDetails);
                await _TransactionRepository.CompleteAsync();
                return mapper.Map<Transactions, TransactionResource>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<TransactionResource> EditAsync(TransactionResource Transaction)
        {
            try
            {
                var updatedDetails = mapper.Map<TransactionResource, Transactions>(Transaction);

                var result = await _TransactionRepository.UpdateAsync(updatedDetails, updatedDetails.Id);
                await _TransactionRepository.CompleteAsync();

                return mapper.Map<Transactions, TransactionResource>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TransactionResource>> GetAllAsync()
        {
            try
            {
                IEnumerable<Transactions> Transaction = await _TransactionRepository.GetAllAsync();
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
