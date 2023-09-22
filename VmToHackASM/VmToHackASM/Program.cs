// See https://aka.ms/new-console-template for more information
using VmToHackASM;

IVmToAsmHandler asmHandler = new VmToAsmHandler();
const string VmFilePath = "C:\\Users\\benja\\Documents\\GitHub\\VmToAsmProject\\nand2tetris\\projects\\07\\MemoryAccess\\PointerTest\\PointerTest.vm";
const string AsmFileName = "PointerTest.asm";

var vmText = File.ReadAllText(VmFilePath);

var test = asmHandler.TranslateVmToAsm(vmText).Trim();

File.WriteAllText(Environment.CurrentDirectory + AsmFileName, test);

Console.WriteLine(test);