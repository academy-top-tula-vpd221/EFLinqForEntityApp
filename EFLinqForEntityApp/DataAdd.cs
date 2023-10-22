using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLinqForEntityApp
{
    public static class DataAdd
    {
        public static void Add()
        {
            using(ApplicationContext context = new ApplicationContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                City moscow = new City() { Title = "Moscow" };
                City peterburg = new City() { Title = "St. Peterburg" };
                City losangeles = new City() { Title = "Los Angeles" };
                City washington = new City() { Title = "Washington" };
                City beijing = new City() { Title = "Beijing" };
                City shanghai = new City() { Title = "Shanghai" };

                context.Cities.AddRange(moscow, peterburg, losangeles, washington, beijing, shanghai);


                Country russia = new Country() { Title = "Russia", Capital = moscow };
                Country usa = new Country() { Title = "Usa", Capital = washington };
                Country china = new Country() { Title = "China", Capital = beijing };
                context.Countries.AddRange(russia, usa, china);

                Company yandex = new Company()
                {
                    Title = "Yandex",
                    Country = russia
                };
                Company ozon = new Company()
                {
                    Title = "Ozon",
                    Country = russia
                };
                Company mail = new Company()
                {
                    Title = "Mail Group",
                    Country = russia
                };
                context.Companies.AddRange(yandex, ozon, mail);

                Company google = new Company()
                {
                    Title = "Google",
                    Country = usa
                };
                Company microsoft = new Company()
                {
                    Title = "Microsoft",
                    Country = usa
                };
                Company apple = new Company()
                {
                    Title = "Apple",
                    Country = usa
                };
                context.Companies.AddRange(google, microsoft, apple);

                Company huawei = new Company()
                {
                    Title = "Huawei",
                    Country = china
                };
                Company zte = new Company()
                {
                    Title = "ZTE",
                    Country = china
                };
                Company baidu = new Company()
                {
                    Title = "Baidu",
                    Country = china
                };
                context.Companies.AddRange(huawei, zte, baidu);


                Position admin = new Position() { Title = "Administrator" };
                Position manager = new Position() { Title = "Manager" };
                Position developer = new Position() { Title = "Developer" };
                Position tester = new Position() { Title = "Tester" };
                context.Positions.AddRange(admin, manager, developer, tester);

                Employee bob = new()
                {
                    Name = "Bob",
                    Age = 26,
                    Company = yandex,
                    Position = admin
                };
                context.Employees.Add(bob);

                Employee joe = new()
                {
                    Name = "Joe",
                    Age = 32,
                    Company = yandex,
                    Position = manager
                };
                context.Employees.Add(joe);

                Employee tom = new()
                {
                    Name = "Tom",
                    Age = 29,
                    Company = yandex,
                    Position = manager
                };
                context.Employees.Add(tom);

                Employee leo = new()
                {
                    Name = "Leo",
                    Age = 25,
                    Company = yandex,
                    Position = developer
                };
                context.Employees.Add(leo);

                Employee tim = new()
                {
                    Name = "Tim",
                    Age = 27,
                    Company = yandex,
                    Position = developer
                };
                context.Employees.Add(tim);

                Employee bill = new()
                {
                    Name = "Bill",
                    Age = 40,
                    Company = yandex,
                    Position = tester
                };
                context.Employees.Add(bill);


                Employee mike = new()
                {
                    Name = "Mike",
                    Age = 32,
                    Company = mail,
                    Position = admin
                };
                context.Employees.Add(mike);


                Employee anna = new()
                {
                    Name = "Anna",
                    Age = 29,
                    Company = ozon,
                    Position = manager
                };
                context.Employees.Add(anna);

                Employee billy = new()
                {
                    Name = "Bill",
                    Age = 56,
                    Company = microsoft,
                    Position = admin
                };
                context.Employees.Add(billy);

                Employee pit = new()
                {
                    Name = "Piter",
                    Age = 33,
                    Company = apple,
                    Position = developer
                };
                context.Employees.Add(pit);

                Employee stive = new()
                {
                    Name = "Stiven",
                    Age = 42,
                    Company = google,
                    Position = manager
                };
                context.Employees.Add(stive);

                Employee jo = new()
                {
                    Name = "Jo",
                    Age = 36,
                    Company = huawei,
                    Position = admin
                };
                context.Employees.Add(jo);

                Employee chan = new()
                {
                    Name = "Chan",
                    Age = 29,
                    Company = zte,
                    Position = tester
                };
                context.Employees.Add(chan);

                Employee van = new()
                {
                    Name = "Van",
                    Age = 28,
                    Company = baidu,
                    Position = manager
                };
                context.Employees.Add(van);

                context.SaveChanges();
            }
        }
    }
}
