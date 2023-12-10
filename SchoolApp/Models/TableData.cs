using Spectre.Console;

namespace SchoolApp.Models;

public class TableData(string title, string caption, List<string> headers, List<List<string>> rows,
    string columnColor = "blue", Color? borderColor = null, TableBorder? tableBorder = null)
{
    public string Title { get; } = title;
    public string Caption { get; } = caption;
    public List<string> Headers { get; } = headers;
    public List<List<string>> Rows { get; } = rows;
    public string ColumnColor { get; } = columnColor;
    public Color BorderColor { get; } = borderColor ?? Color.SpringGreen3;
    public TableBorder TableBorder { get; } = tableBorder ?? TableBorder.MinimalHeavyHead;
}