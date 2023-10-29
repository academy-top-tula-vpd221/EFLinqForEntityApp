using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        static public void LinqFinds(ApplicationContext context)
        {

            var employees = context.Employees
                                    .Include(e => e.Company);
            foreach(var e in employees)
                Console.WriteLine($"{e.Id} {e.Name} {e.Company?.Title}");
            //Console.WriteLine($"\n{(await employees.FirstOrDefaultAsync(e => e.Name.StartsWith("W")))?.Name}");
            string comp = "Yandex";
            Console.WriteLine($"\n{employees.Single(e => e.Company.Title == comp).Name}");
        }

        static public void LinqSelect(ApplicationContext context)
        {
            var employees = context.Employees
                                    .Select(
                    e => new
                    {
                        FirstName = e.Name,
                        Age = e.Age,
                        CompanyTitle = e.Company.Title
                    }
                )
                                    .OrderByDescending(o => o.Age)
                                    .ThenByDescending(o => o.FirstName);

            var employees2 = from e in context.Employees
                             orderby e.Name
                             select new
                             {
                                 FirstName = e.Name,
                                 Age = e.Age,
                                 CompanyTitle = e.Company.Title
                             };
                              

            foreach(var e in employees2)
                Console.WriteLine($"{e.FirstName} {e.Age} {e.CompanyTitle}");

        }

        static public void LinqJoin(ApplicationContext context)
        {
            //var employees = context.Employees
            //                        .Join(context.Companies,
            //                        e => e.CompanyId,
            //                        c => c.Id,
            //                        (e, c) => new
            //                        {
            //                            FirstName = e.Name,
            //                            Age = e.Age,
            //                            CompanyTitle = c.Title
            //                        });

            var employees = context.Employees
                                    .Join(context.Companies,
                                    e => e.CompanyId,
                                    c => c.Id,
                                    (e, c) => new
                                    {
                                        FirstName = e.Name,
                                        Age = e.Age,
                                        CompanyTitle = c.Title,
                                        CountryId = c.Country.Id
                                    })
                                    .Join(context.Countries,
                                    c => c.CountryId,
                                    cnt => cnt.Id,
                                    (e, cnt) => new
                                    {
                                        FirstName = e.FirstName,
                                        Age = e.Age,
                                        CompanyTitle = e.CompanyTitle,
                                        CountryTitle = cnt.Title
                                    });



            //var employees2 = from e in context.Employees
            //                 join c in context.Companies
            //                    on e.CompanyId equals c.Id
            //                 select new
            //                 {
            //                     FirstName = e.Name,
            //                     Age = e.Age,
            //                     CompanyTitle = c.Title
            //                 };

            var employees2 = from e in context.Employees
                             join c in context.Companies
                                on e.CompanyId equals c.Id
                             join cnt in context.Countries
                                on c.Country.Id equals cnt.Id
                             select new
                             {
                                 FirstName = e.Name,
                                 Age = e.Age,
                                 CompanyTitle = c.Title,
                                 CountryTitle = cnt.Title
                             };

            foreach (var e in employees)
                Console.WriteLine($"{e.FirstName} {e.Age} {e.CompanyTitle} {e.CountryTitle}");
        }

        static public void LinqGroups(ApplicationContext context)
        {
            var employees = context.Employees
                                    .GroupBy(e => e.Company!.Title)
                                    .Select(o => new
                                    {
                                        o.Key,
                                        Count = o.Count()
                                    });

            foreach (var e in employees)
                Console.WriteLine($"{e.Key} {e.Count}");

        }

        static public void LinqSets(ApplicationContext context)
        {
            var emplsUnion = context.Employees
                                .Where(e => e.Company!.Country!.Title == "Russia")
                                .Union(context.Employees.Where(e => e.Age == 29));

            foreach(var e in emplsUnion)
                Console.WriteLine($"{e.Name}");
            Console.WriteLine();

            var empplsInter = context.Employees
                                    .Where(e => e.Company!.Country!.Title == "Russia")
                                    .Intersect(
                                        context.Employees
                                                .Where(e => e.Age <= 30));
            foreach (var e in empplsInter)
                Console.WriteLine($"{e.Name}");
            Console.WriteLine();

            var emplExcept = context.Employees
                                    .Where(e => e.Company!.Country!.Title == "Russia")
                                    .Except(
                                        context.Employees
                                                .Where(e => e.Company!.Title == "Yandex"));
            foreach (var e in emplExcept)
                Console.WriteLine($"{e.Name}");
            Console.WriteLine();
        }

        static public void LinqAgregate(ApplicationContext context)
        {
            var isGoogle = context.Employees
                                    .Any(e => e.Company!.Title == "Google");
            Console.WriteLine($"Any in Google: {isGoogle}\n");

            var isYoung = context.Employees
                                 .All(e => e.Age < 60);
            Console.WriteLine($"All empls is young: {isYoung}\n");

            var emplOthers = context.Employees
                                    .Count(e => e.Company!.Country!.Title != "Russia");
            Console.WriteLine($"Count empl from others countries: {emplOthers}\n");


            Console.WriteLine($"Min age: {context.Employees.Min(e => e.Age)}\n");
            Console.WriteLine($"Max age: {context.Employees.Max(e => e.Age)}\n");
            Console.WriteLine($"Avg age: {context.Employees.Average(e => e.Age)}\n");
            Console.WriteLine($"Sum age: {context.Employees.Sum(e => e.Age)}\n");
        }

        static public void LinqTracking(ApplicationContext context)
        {
            var empl = context.Employees
                            //.AsNoTracking()
                            .FirstOrDefault();
            if(empl is not null)
            {
                empl.Age = 26;
                context.SaveChanges();
            }

            foreach(var e in context.Employees)
                Console.WriteLine($"{e.Name} {e.Age}");
            Console.WriteLine();
        }

        static public void LinqQuerable(ApplicationContext context)
        {
            IEnumerable<Employee> emplE = context.Employees;
            var e1 = emplE.Where(e => e.Age <= 30).ToList();
            foreach(var e in e1)
                Console.WriteLine(e.Name);
            Console.WriteLine();

            IQueryable<Employee> emplQ = context.Employees;
            var e2 = emplQ.Where(e => e.Age <= 30).ToList();
            foreach (var e in e2)
                Console.WriteLine(e.Name);
            Console.WriteLine();

            var emplA = context.Employees
                                .IgnoreQueryFilters();
            var e3 = emplA.Where(e => e.Age <= 30);
            foreach (var e in e3)
                Console.WriteLine(e.Name);
            Console.WriteLine();
        }

        static public void LinqExecuteMethods(ApplicationContext context)
        {
            context.Employees.ExecuteUpdate(o => o.SetProperty(e => e.Age, e => e.Age + 1));
        }
    }
}
