using System.Text;
using System.Text.RegularExpressions;

namespace VmToHackASM
{
    /// <summary>
    /// This class is responsible for translating VM code to ASM code, and all the formatting that this requires.
    /// </summary>
    public class VmToAsmHandler : IVmToAsmHandler
    {
        private int labelCount = 0;

        /// <summary>
        /// Takes VM string and translates it into ASM string.
        /// </summary>
        /// <param name="vmString">vm code string to translate</param>
        /// <returns>ASM string</returns>
        /// <exception cref="Exception">VM operation not supported</exception>
        public string TranslateVmToAsm(string vmString)
        {
            vmString = StringHelper.RemoveComments(vmString).Trim();
            var textLines = vmString.Split('\n');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < textLines.Length; i++)
            {
                var trimmedLine = textLines[i].Trim();
                if (IsVmLineStackOperation(trimmedLine))
                {
                    var formattedOperation = ConstructStackOperationAsm(trimmedLine);
                    sb.Append(formattedOperation);
                    continue;
                }
                if (IsVmLineArithmeticOperation(trimmedLine))
                {
                    var operation = VmAsmValues.ArithmeticOperations.Where(e => trimmedLine.Contains(e.Key)).FirstOrDefault();
                    sb.Append(operation.Value);
                    continue;
                }
                if (IsVmLineComparisonOperation(trimmedLine))
                {
                    // Does lookup on VM operation in ComparisonOperations collection to find corresponding ASM comparison identifier
                    var operation = VmAsmValues.ComparisonOperations.Where(e => trimmedLine.Contains(e.Key)).FirstOrDefault();
                    // Uses found identifier to create ASM string based on VM comparison operation.
                    sb.Append(VmToAsmComparisonOperation(operation.Value));
                    continue;
                }
                if (IsVmLineBitwiseOperation(trimmedLine))
                {
                    var operation = VmAsmValues.BitwiseOperations.Where(e => trimmedLine.Contains(e.Key)).FirstOrDefault();
                    sb.Append(operation.Value);
                    continue;
                }
                else
                {
                    throw new Exception($"{textLines[i]} is not a supported VM operation to translate.");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Constructs an ASM comparison string based on a comparisonSegment, and current amount of labels encountered.
        /// </summary>
        /// <param name="comparisonSegment">ASM comparison identifier, e.g 'JNE', 'JGE' etc</param>
        /// <returns>ASM comparison string.</returns>
        private string VmToAsmComparisonOperation(string comparisonSegment)
        {
            labelCount++;
            return $"@SP\r\nAM=M-1\r\nD=M\r\nA=A-1\r\nD=M-D\r\n@FALSE{labelCount}\r\nD;{comparisonSegment}\r\n@SP\r\nA=M-1\r\nM=-1\r\n@CONTINUE{labelCount}\r\n0;JMP\r\n(FALSE{labelCount})\r\n@SP\r\nA=M-1\r\nM=0\r\n(CONTINUE{labelCount})\n";
        }

        /// <summary>
        /// Constructs an Asm string based on a VM stack operation string line
        /// </summary>
        /// <param name="stackSegment">stack operation string line</param>
        /// <returns>Asm string</returns>
        /// <exception cref="Exception">VM address value in stack operation cannot be parsed to an integer.</exception>
        private string ConstructStackOperationAsm(string stackSegment)
        {
            var operation = VmAsmValues.StackOperations.Where(e => stackSegment.Contains(e.Key)).FirstOrDefault();
            var intParse = int.TryParse(Regex.Match(stackSegment, @"\d+").Value, out var result);
            if (!intParse)
                throw new Exception($"{stackSegment} address value cannot be parsed to an int.");
            var formattedOperation = string.Format(operation.Value, ComputeArgumentValue(operation.Key, result));
            return formattedOperation;
        }

        /// <summary>
        /// Computes the address value based the type of operation
        /// Temp operations has base address of 5, so the value will be - 5 + i = x
        /// Pointer operations has base address of 3 so the value will be - 3 + i = x
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private int ComputeArgumentValue(string operation, int value)
        {
            if (IsTempOperation(operation))
                return value + 5;
            if (IsPointerOperation(operation))
                return value + 3;
            return value;
        }

        /// <summary>
        /// Checks if operation is a temp operation.
        /// </summary>
        /// <param name="operation">operation to evaluate</param>
        /// <returns>True if operation is temp, false if not</returns>
        private bool IsTempOperation(string operation)
        {
            return operation.Contains("push temp") || operation.Contains("pop temp");
        }

        /// <summary>
        /// Checks if operation is a pointer operation.
        /// </summary>
        /// <param name="operation">operation to evaluate</param>
        /// <returns>True if operation is pointer, false if not</returns>
        private bool IsPointerOperation(string operation)
        {
            return operation.Contains("pop pointer") || operation.Contains("push pointer");
        }

        /// <summary>
        /// Checks if current string line is a stack operation.
        /// </summary>
        /// <param name="line">string Line to check</param>
        /// <returns>True if string line is stack operation, false if not</returns>
        private bool IsVmLineStackOperation(string line)
        {
            return VmAsmValues.StackOperations.Any(e => line.Contains(e.Key));
        }

        /// <summary>
        /// Checks if current string line is a Comparison operation.
        /// </summary>
        /// <param name="line">string Line to check</param>
        /// <returns>True if string line is Comparison operation, false if not</returns>
        private bool IsVmLineComparisonOperation(string line)
        {
            return VmAsmValues.ComparisonOperations.Any(e => line.Contains(e.Key));
        }

        /// <summary>
        /// Checks if current string line is a Arithmetic operation.
        /// </summary>
        /// <param name="line">string Line to check</param>
        /// <returns>True if string line is Arithmetic, false if not</returns>
        private bool IsVmLineArithmeticOperation(string line)
        {
            return VmAsmValues.ArithmeticOperations.Any(e => line.Contains(e.Key));
        }

        /// <summary>
        /// Checks if current string line is a BitWise operation.
        /// </summary>
        /// <param name="line">string Line to check</param>
        /// <returns>True if string line is bitwise, false if not</returns>
        private bool IsVmLineBitwiseOperation(string line)
        {
            return VmAsmValues.BitwiseOperations.Any(e => line.Contains(e.Key));
        }
    }
}
