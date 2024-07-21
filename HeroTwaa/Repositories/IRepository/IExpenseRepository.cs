using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(int id);
        void Update(Expense expense);
    }
}
