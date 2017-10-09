using FitnessNotebook.Models;
using System.Data.Entity;

namespace FitnessNotebook.DAO
{
    public class FitNotebookDB_Context : DbContext
    {
        public FitNotebookDB_Context() : base("DefaultConnection")
        {

        }

        public DbSet<Exercises> Exercises { get; set; }


    }
}