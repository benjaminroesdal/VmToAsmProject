// See https://aka.ms/new-console-template for more information

using VmToHackASM;
Dictionary<string, string> labels = new Dictionary<string, string>();
var asmHandler = new VmToAsmValues();

var vmText = File.ReadAllText("C:\\Users\\Benjamin Roesdal\\Desktop\\nand2tetris\\projects\\07\\StackArithmetic\\StackTest\\StackTest.vm");
vmText = StringHelper.RemoveComments(vmText);
var test = FindOperations(vmText);

Console.WriteLine("Hello, World!");



string FindOperations(string vmText)
{
    var textLines = vmText.Split('\n');
    string finalString = "";
    for (int i = 0; i < textLines.Length; i++)
    {
        var trimmedLine = textLines[i].Trim();
        if (asmHandler.StackOperations.Any(e => textLines[i].Contains(e.Key)))
        {
            var operation = asmHandler.StackOperations.Where(e => textLines[i].Contains(e.Key)).FirstOrDefault();
            var pushValue = trimmedLine[trimmedLine.Length - 1].ToString();
            finalString = finalString + $"@{pushValue}\n" + operation.Value + "\n";
        }
        if (asmHandler.ArithmeticOperations.Any(e => textLines[i].Contains(e.Key)))
        {
            var operation = asmHandler.ArithmeticOperations.Where(e => trimmedLine.Contains(e.Key)).FirstOrDefault();
            finalString = finalString + operation.Value + "\n";
        }
        if (asmHandler.ComparisonOperations.Any(e => textLines[i].Contains(e.Key)))
        {
            var operation = asmHandler.ComparisonOperations.Where(e => trimmedLine.Contains(e.Key)).FirstOrDefault();
            finalString = finalString + operation.Value.Invoke();
        }
    }
    return finalString.Trim();
}