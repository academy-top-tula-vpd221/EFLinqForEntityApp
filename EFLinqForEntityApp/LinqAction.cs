using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLinqForEntityApp
{
    public class LinqAction
    {
        static public void LinqWhere(ApplicationContext context)
        {
            var employees = context.Employees
                           .Include(e => e.Position)
                           .Include(e => e.Company)
                           .Where(e => e.Company.Title != "Yandex")
                           .ToList();
            foreach (var e in employees)
                Console.WriteLine($"Name: {e.Name},\tPosition: {e?.Position?.Title},\tCompany: {e?.Company?.Title}");
            Console.WriteLine();

            var employees2 = (from empl in context.Employees
                                                  .Include(e => e.Company)
                              where empl.Company.Title != "Yandex"
                              select empl).ToList();
            foreach (var e in employees2)
                Console.WriteLine($"Name: {e.Name},\tPosition: {e?.Position?.Title},\tCompany: {e?.Company?.Title}");
            Console.WriteLine();

            // EF.Functions

            var employees3 = context.Employees
                                   .Include(e => e.Company)
                                   .Where(e => EF.Functions.Like(e.Name, "%a%"))
                                   .ToList();
            foreach (var e in employees3)
                Console.WriteLine($"Name: {e.Name},\tCompany: {e?.Company?.Title}");
            Console.WriteLine();
        }


    }
}
