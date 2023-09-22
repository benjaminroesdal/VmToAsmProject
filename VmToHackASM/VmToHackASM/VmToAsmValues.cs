using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VmToHackASM
{
    public class VmToAsmValues
    {
        public VmToAsmValues()
        {
            AddComparisonDelegates();
        }
        public List<string> Labels = new List<string>();
        public int labelCount = 0;
        public delegate string StringDelegate();
        public Dictionary<string, string> StackOperations = new Dictionary<string, string>()
        {
            {"push constant", "D=A\r\n@SP\r\nA=M\r\nM=D\r\n@SP\r\nM=M+1"}
        };

        public Dictionary<string, string> ArithmeticOperations = new Dictionary<string, string>()
        {
            {"add", "@SP\r\nAM=M-1\r\nD=M\r\nA=A-1\r\nM=D+M" }
        };

        public Dictionary<string, StringDelegate> ComparisonOperations = new Dictionary<string, StringDelegate>()
        {
        };

        public string VmToAsmComparisonOperation()
        {
            return $"@SP\r\nAM=M-1\r\nD=M\r\nA=A-1\r\nD=M-D\r\n@TRUE_LABEL{labelCount}\r\nD;JEQ\r\n@SP\r\nA=M-1\r\nM=0\r\n@END_LABEL{labelCount}\r\n0;JMP\r\n(TRUE_LABEL{labelCount})\r\n@SP\r\nA=M-1\r\nM=-1\r\n(END_LABEL{labelCount})\n";
        }

        private void AddComparisonDelegates()
        {
            this.ComparisonOperations.Add("eq", VmToAsmComparisonOperation);
        }
    }
}
