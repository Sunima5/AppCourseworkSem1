using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text.RegularExpressions;
using DearDiary.Models;

public class PdfExportService
{
    public byte[] GenerateJournalPdf(List<Journal> entries)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);

                page.Header()
                    .Text("Dear Diary – Journal Export")
                    .Bold().FontSize(20)
                    .AlignCenter();

                page.Content().Column(column =>
                {
                    foreach (var entry in entries)
                    {
                        column.Item().Padding(10).Border(1).Column(c =>
                        {
                            c.Item().Text(entry.EntryDate.ToString("dd MMM yyyy")).Bold();
                            c.Item().Text($"Title: {entry.Title}");
                            c.Item().Text($"Mood: {entry.PrimaryMood}");
                            c.Item().PaddingTop(5)
                                .Text(HtmlToPlainText(entry.Content));
                        });

                        column.Item().PaddingBottom(10);
                    }
                });

                page.Footer()
                    .AlignCenter()
                    .Text($"Generated on {DateTime.Now:dd MMM yyyy}");
            });
        });

        return document.GeneratePdf();
    }

    private string HtmlToPlainText(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
            return "";

        html = html.Replace("<li>", "• ");
        html = html.Replace("</li>", "\n");

        html = Regex.Replace(html, "<.*?>", string.Empty);

        return html.Trim();
    }
}
