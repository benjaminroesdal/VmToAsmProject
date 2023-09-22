
namespace VmToHackASM
{
    public interface IVmToAsmHandler
    {
        /// <summary>
        /// Takes VM string and translates it into ASM string.
        /// </summary>
        /// <param name="vmString">vm code string to translate</param>
        /// <returns>ASM string</returns>
        /// <exception cref="Exception">VM operation not supported</exception>
        string TranslateVmToAsm(string vmString);
    }
}
