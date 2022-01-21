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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly ITransactionRepository _TransactionRepository;
        private readonly IMapper mapper;

        public CategoryService(
            ICategoryRepository CategoryRepository,
            ITransactionRepository TransactionRepository,
            IMapper mapper
            )
        {
            _CategoryRepository = CategoryRepository;
            _TransactionRepository = TransactionRepository;
            this.mapper = mapper;

        }
        public async Task<CategoryResource> CreateAsync(CategoryResource Category)
        {
            try
            {
                var updatedDetails = mapper.Map<CategoryResource, Category>(Category);
                var result = await _CategoryRepository.AddAsync(updatedDetails);
                await _CategoryRepository.CompleteAsync();
                return mapper.Map<Category, CategoryResource>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<CategoryResource> EditAsync(CategoryResource Category)
        {
            try
            {
                var updatedDetails = mapper.Map<CategoryResource, Category>(Category);

                var result = await _CategoryRepository.UpdateAsync(updatedDetails, updatedDetails.Id);
                await _CategoryRepository.CompleteAsync();

                return mapper.Map<Category, CategoryResource>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CategoryResource>> GetAllAsync()
        {
            try
            {
                IEnumerable<Category> Category = await _CategoryRepository.GetAllAsync();
                return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(Category);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<CategoryResource> GetByIdAsync(int id)
        {
            try
            {
                var Category = await _CategoryRepository.FindAsync(ent => ent.Id == id);
                return mapper.Map<Category, CategoryResource>(Category);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CategoryTransactionResource>> GetUserCategoryDetailsAsync(int id)
        {
            try
            {
                var Transaction = await _TransactionRepository.FindByAllIncludingAsync(t => t.User.Id == id, t => t.Category);

                var CategoryList = await _CategoryRepository.GetAllAsync();
                var CategoryTransactions = new List<CategoryTransactionResource>();
                foreach (var item in CategoryList)
                {
                    var transactionsofCategory = await _TransactionRepository.FindByAllIncludingAsync(t => t.User.Id == id && t.Category.Id == item.Id);
                    var sum = 0.0;
                    foreach (var transaction in transactionsofCategory)
                    {
                        sum += transaction.Amount;
                    }
                    CategoryTransactionResource transction = new CategoryTransactionResource
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Amount = item.Amount,
                        Icon = item.Icon,
                        TransactionsTotal = sum
                    };
                    CategoryTransactions.Add(transction);
                }
                return CategoryTransactions;
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
                var Category = await _CategoryRepository.FindAsync(ent => ent.Id == id);
                _CategoryRepository.Delete(Category);
                await _CategoryRepository.CompleteAsync();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<IEnumerable<CategoryResource>> SearchAsync(string hint)
        {
            try
            {
                if (hint == null || hint == "")
                {
                    IEnumerable<Category> Category = await _CategoryRepository.GetAllAsync();
                    return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(Category);

                }
                else
                {
                    var Category = await _CategoryRepository.FindAllAsync(ent => ent.Title.Contains(hint) || ent.Amount.Equals(hint));
                    return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(Category);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
