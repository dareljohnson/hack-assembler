namespace HackAssembler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "prog.asm";
            string outputFilePath = "prog.hack";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--asm" && i + 1 < args.Length)
                {
                    inputFilePath = args[i + 1];
                }
                else if (args[i] == "--hack" && i + 1 < args.Length)
                {
                    outputFilePath = args[i + 1];
                }
            }

            if (inputFilePath == null || outputFilePath == null)
            {
                Console.WriteLine("Usage: HackAssembler --asm <inputfile.asm> --hack <outputfile.hack>");
                return;
            }

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Error: Input file not found.");
                return;
            }

            Models.HackAssembler assembler = new Models.HackAssembler();
            assembler.AssembleFile(inputFilePath, outputFilePath);

            Console.WriteLine("Assembly code successfully assembled and written to: " + outputFilePath + " binary output.");
        }
    }
}