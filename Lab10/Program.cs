using lab02;
using System.Linq;


namespace lab10 {
    internal class Program {
        static void Main(string[] args) {
            // 1
            int n = 8;
            string[] months = {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            };
            string[] summerAndWinterMonthsCodes = {
                "Jun",
                "Jul",
                "Aug",
                "Dec",
                "Jan",
                "Feb"
            };

            var nLetterMonths = (
                from month in months
                where month.Length == n
                select month
            );

            var summerAndWinderMonths = (
                months
                    .Where(month => summerAndWinterMonthsCodes.Contains(month.Substring(0, 3)))
                    .Select(month => month)
            );

            var monthsInAlphabetOrder = (
                from month in months
                orderby month
                select month
            );

            var monthsLongerThan4ContainsU = (
                months
                    .Where(month => month.Contains('u'))
                    .Where(month => month.Length >= 4)
            );

            Console.WriteLine($"nLetterMonth ({n}): {string.Join(", ", nLetterMonths)}");
            Console.WriteLine($"summerAndWinderMonth: {string.Join(", ", summerAndWinderMonths)}");
            Console.WriteLine($"monthsInAlphabetOrder: {string.Join(", ", monthsInAlphabetOrder)}");
            Console.WriteLine($"monthsLongerThan4ContainsU: {string.Join(", ", monthsLongerThan4ContainsU)}");


            // 2
            List<DoubleStack> stacks = new List<DoubleStack>() {
                new DoubleStack(new List<double> { 1.5, 2.2, 3.9, 4.1 }),
                new DoubleStack(new List<double> { 5.0, 0, 7.8 }),
                new DoubleStack(new List<double> { 8.5, 9.2 }),
                new DoubleStack(new List<double> { 10.7 }),
                new DoubleStack(new List<double> { 11.3, 12.6 }),
                new DoubleStack(new List<double> { 13.1, -14.9 }),
                new DoubleStack(new List<double> { 15.2, 16.8 }),
                new DoubleStack(new List<double> { 17.3, 18.7, 19.4 }),
                new DoubleStack(new List<double> { 20.0, 21.5 }),
                new DoubleStack(new List<double> { 22.8, -23.1, 24.6 }),
                new DoubleStack(new List<double> { 25.2, 26.4 }),
                new DoubleStack(new List<double> { 0, 28.3 }),
                new DoubleStack(new List<double> { 29.7, 0 }),
                new DoubleStack(new List<double> { 31.2, 32.4, -33.6 }),
                new DoubleStack(new List<double> { 34.8, 35.0 })
            };


            // 3
            var stackWithMinTopElement = stacks.MinBy(stack => stack.Top);

            var stackWithMaxTopElement = stacks.MaxBy(stack => stack.Top);

            var stacksWithNegativeElements = (
                from stack in stacks
                where (
                    from el in stack.Storage
                    where el < 0
                    select el
                ).Count() > 0
                select stack
            );

            var minStack = stacks.MinBy(stack => stack.Storage.Sum());

            var stacksWithLenght1And3 = stacks.Where(stack => stack.Count == 1 || stack.Count == 3);

            var firstStackWithZero = stacks.Where(stack => stack.Storage.Contains(0)).First();

            var sortedStacksBySum = (
                from stack in stacks
                let storage = stack.Storage
                orderby storage.Sum() descending
                select stack
            );

            Console.WriteLine($"stackWithMinTopElement: {stackWithMinTopElement}");
            Console.WriteLine($"stackWithMaxTopElement: {stackWithMaxTopElement}");
            Console.WriteLine($"stacksWithNegativeElements: {string.Join(", ", stacksWithNegativeElements)}");
            Console.WriteLine($"minStack: {minStack}");
            Console.WriteLine($"stacksWithLenght1And3: {string.Join(", ", stacksWithLenght1And3)}");
            Console.WriteLine($"firstStackWithZero: {firstStackWithZero}");
            Console.WriteLine($"sortedStacksBySum:\n\t{string.Join("\n\t", sortedStacksBySum)}");


            // 4
            var specificStacks = (
                from stack in stacks
                let storage = stack.Storage
                where stack.Count >= 2
                where storage.Contains(0)
                orderby storage.Sum() ascending
                group stack by stack.Count
            );
            Console.WriteLine("specificStacks:");
            foreach (var stackGroup in specificStacks) {
                Console.WriteLine($"\tCount: {stackGroup.Key}, Stacks: {string.Join(", ", stackGroup)}");
            }

            //5
            var joinedStacks = (
                from stack1 in stacksWithNegativeElements
                join stack2 in stacksWithLenght1And3 on stack1.Count equals stack2.Count
                select stack1.Storage.Union(stack2.Storage)
            );
            Console.WriteLine("joinedStacks:");
            foreach (var stackJoin in joinedStacks) {
                Console.WriteLine($"\t{{ {string.Join(" ", stackJoin)} }}");
            }
        }
    }
}