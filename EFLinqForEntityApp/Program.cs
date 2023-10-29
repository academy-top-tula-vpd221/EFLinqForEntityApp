using EFLinqForEntityApp;
using Microsoft.EntityFrameworkCore;

using (ApplicationContext context = new())
{
    //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

    var full = context.Employees
                        .Include(e => e.Company)
                            .ThenInclude(c => c.Country)
                                .ThenInclude(cnt => cnt.Capital)
                                .OrderBy(e => e.Age);
    foreach(var e in full)
        Console.WriteLine($"{e.Name} {e.Age} {e.Company!.Title} {e.Company.Country!.Title} {e.Company.Country.Capital!.Title}");
    Console.WriteLine();

    //LinqAction.LinqWhere(context);
    //LinqAction.LinqFinds(context);
    //LinqAction.LinqSelect(context);
    //LinqAction.LinqJoin(context);
    //LinqAction.LinqGroups(context);
    //LinqAction.LinqSets(context);
    //LinqAction.LinqAgregate(context);
    //LinqAction.LinqTracking(context);
    //LinqAction.LinqQuerable(context);
    LinqAction.LinqExecuteMethods(context);

    full = context.Employees
                        .Include(e => e.Company)
                            .ThenInclude(c => c.Country)
                                .ThenInclude(cnt => cnt.Capital)
                                .OrderBy(e => e.Age);
    foreach (var e in full)
        Console.WriteLine($"{e.Name} {e.Age} {e.Company!.Title} {e.Company.Country!.Title} {e.Company.Country.Capital!.Title}");
    Console.WriteLine();
}