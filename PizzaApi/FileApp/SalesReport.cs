using System.Text;

public class SalesReport
{
    public static void GenerateSalesReport(string directoryPath)
    {
        var files = Directory.GetFiles(directoryPath, "*.txt");
        decimal totalSales = 0;
        var report = new StringBuilder();

        report.AppendLine("Sales Summary");
        report.AppendLine("----------------------------");

        var details = new List<string>();

        foreach (var file in files)
        {
            decimal fileTotal = 0;

            foreach (var line in File.ReadAllLines(file))
            {
                if (decimal.TryParse(line, out decimal value))
                {
                    fileTotal += value;
                }
            }

            totalSales += fileTotal;
            details.Add($"{Path.GetFileName(file)}: {fileTotal:C}");
        }

        report.AppendLine($"Total Sales: {totalSales:C}");
        report.AppendLine("\nDetails:");

        foreach (var detail in details)
        {
            report.AppendLine(detail);
        }

        File.WriteAllText(Path.Combine(directoryPath, "SalesSummary.txt"), report.ToString());
    }
}