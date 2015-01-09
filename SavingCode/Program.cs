using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SavingCode
{
    class Program
    {
        private static readonly List<Expression<Func<DateTime, string>>> Expressions = new List<Expression<Func<DateTime, string>>>
        {
            text => text.Date.ToString("dd MMMM yyyy"),
            futureLongDate => futureLongDate.Date.AddDays(4).ToLongDateString()
        };

        static void Main()
        {
            //GET AN EXPRESSION
            var expression = Expressions.PickRandom();

            //WRAP IT FOR PERSISTANCE
            var useclass = new ClassForPersistance(expression);

            //PERSIST IT
            Repo.Save(useclass);

            //NEW RETRIEVAL
            var fromDb = Repo.First();

            //GET THE EXPRESSION FROM THE STORED DATA
            var found = FindExpression(fromDb);

            //EXECUTE 
            Console.Write(found.Invoke(new DateTime(1970, 1, 1)));
        }

        private static Func<DateTime, string> FindExpression(ClassForPersistance fromDb)
        {
            return Expressions
                  .Where(expression1 => fromDb.Code == expression1.ToString())
                  .Select(expression1 => expression1.Compile())
                  .First();
        }
    }

    public class ClassForPersistance
    {
      

        public ClassForPersistance(Expression<Func<DateTime, string>> expression)
        {
            Code = expression.ToString();
        }
        public string Code { get; set; }
    }
}
