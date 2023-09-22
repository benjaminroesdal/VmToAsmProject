
namespace VmToHackASM
{
    public static class VmAsmValues
    {
        /// <summary>
        /// Stack operations. {0} will be replaced with format value provided  when called with string.Format.
        /// </summary>
        public static Dictionary<string, string> StackOperations = new Dictionary<string, string>()
        {
            {"push constant", "@{0}\r\nD=A\r\n@SP\r\nA=M\r\nM=D\r\n@SP\r\nM=M+1\n"},
            {"pop local", "@LCL\r\nD=M\r\n@{0}\r\nD=D+A\r\n@R13\r\nM=D\r\n@SP\r\nAM=M-1\r\nD=M\r\n@R13\r\nA=M\r\nM=D\n"},
            {"pop argument", "@ARG\r\nD=M\r\n@{0}\r\nD=D+A\r\n@R13\r\nM=D\r\n@SP\r\nAM=M-1\r\nD=M\r\n@R13\r\nA=M\r\nM=D\n"},
            {"pop this", "@THIS\r\nD=M\r\n@{0}\r\nD=D+A\r\n@R13\r\nM=D\r\n@SP\r\nAM=M-1\r\nD=M\r\n@R13\r\nA=M\r\nM=D\n"},
            {"pop that", "@THAT\r\nD=M\r\n@{0}\r\nD=D+A\r\n@R13\r\nM=D\r\n@SP\r\nAM=M-1\r\nD=M\r\n@R13\r\nA=M\r\nM=D\n"},

            // Base address of temp segment is 5, so argument needs to be x + 5.
            {"pop temp", "@{0}\r\nD=A\r\n@R13\r\nM=D\r\n@SP\r\nAM=M-1\r\nD=M\r\n@R13\r\nA=M\r\nM=D\n"},
            {"push temp", "@{0}\r\nD=M\r\n@SP\r\nA=M\r\nM=D\r\n@SP\r\nM=M+1\n"},

            {"push local", "@LCL\r\nD=M\r\n@{0}\r\nA=D+A\r\nD=M\r\n@SP\r\nA=M\r\nM=D\r\n@SP\r\nM=M+1\n"},
            {"push that", "@THAT\r\nD=M\r\n@{0}\r\nA=D+A\r\nD=M\r\n@SP\r\nA=M\r\nM=D\r\n@SP\r\nM=M+1\n"},
            {"push argument", "@ARG\r\nD=M\r\n@{0}\r\nA=D+A\r\nD=M\r\n@SP\r\nA=M\r\nM=D\r\n@SP\r\nM=M+1\n"},
            {"push this", "@THIS\r\nD=M\r\n@{0}\r\nA=D+A\r\nD=M\r\n@SP\r\nA=M\r\nM=D\r\n@SP\r\nM=M+1\n"},

            // Base address of pointer segment is 3, so argument needs to be x + 3  
            {"pop pointer", "@SP\r\nAM=M-1\r\nD=M\r\n@{0}\r\nM=D\n"},
            {"push pointer", "@{0}\r\nD=M\r\n@SP\r\nA=M\r\nM=D\r\n@SP\r\nM=M+1\n"},

            {"pop static", "@SP\r\nAM=M-1\r\nD=M\r\n@{0}\r\nM=D\n"},
            {"push static", "@{0}\r\nD=M\r\n@SP\r\nA=M\r\nM=D\r\n@SP\r\nM=M+1\n"},
        };

        public static Dictionary<string, string> ArithmeticOperations = new Dictionary<string, string>()
        {
            {"add", "@SP\r\nAM=M-1\r\nD=M\r\nA=A-1\r\nM=M+D\n" },
            {"sub", "@SP\r\nAM=M-1\r\nD=M\r\nA=A-1\r\nM=M-D\n" },
            {"neg", "D=0\r\n@SP\r\nA=M-1\r\nM=D-M\n" },
        };

        public static Dictionary<string, string> BitwiseOperations = new Dictionary<string, string>()
        {
            {"and", "@SP\r\nAM=M-1\r\nD=M\r\nA=A-1\r\nM=M&D\n" },
            {"or", "@SP\r\nAM=M-1\r\nD=M\r\nA=A-1\r\nM=M|D\n" },
            {"not", "@SP\r\nA=M-1\r\nM=!M" },
        };

        public static Dictionary<string, string> ComparisonOperations = new Dictionary<string, string>()
        {
            {"eq", "JNE"},
            {"lt", "JGE"},
            {"gt", "JLE"},
        };
    }
}
