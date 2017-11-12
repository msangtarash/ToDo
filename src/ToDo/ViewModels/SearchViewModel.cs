using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.DataAccess;

namespace ToDo.ViewModels
{
    public class SearchViewModel : BindableBase, IDestructible
    {
        private readonly ToDoDbContext _dbContext;

        public SearchViewModel(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Destroy()
        {
            _dbContext.Dispose();
        }
    }
}
