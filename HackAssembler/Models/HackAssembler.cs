namespace HackAssembler.Models
{
    public class HackAssembler : DataQuality
    {
        // Dictionary to store predefined symbols
        private Dictionary<string, int> symbolTable = new();

        // Dictionary to store predefined C-instructions
        private Dictionary<string, string> compTable = new();
        private Dictionary<string, string> destTable = new();
        private Dictionary<string, string> jumpTable = new();

        public HackAssembler()
        {
            // Initialize the symbol and instruction dictionaries
            InitializeSymbolTable();
            InitializeInstructionTables();
        }

        private void InitializeSymbolTable()
        {
            // Add predefined symbols to the symbol table
            symbolTable = new Dictionary<string, int>
            {
                { "SP", 0 },
                { "LCL", 1 },
                { "ARG", 2 },
                { "THIS", 3 },
                { "THAT", 4 },
                { "SCREEN", 16384 },
                { "KBD", 24576 }
            };

            // Add R0-R15 to the symbol table
            for (int i = 0; i <= 15; i++)
            {
                symbolTable["R" + i] = i;
            }
        }

        private void InitializeInstructionTables()
        {
            // Add predefined C-instructions to the instruction dictionaries
            compTable = new Dictionary<string, string>
            {
                { "0", "101010" },
                { "1", "111111" },
                { "-1", "111010" },
                { "D", "001100" },
                { "A", "110000" },
                { "M", "110000" },
                { "!D", "001101" },
                { "!A", "110001" },
                { "!M", "110001" },
                { "-D", "001111" },
                { "-A", "110011" },
                { "-M", "110011" },
                { "D+1", "011111" },
                { "A+1", "110111" },
                { "M+1", "110111" },
                { "D-1", "001110" },
                { "A-1", "110010" },
                { "M-1", "110010" },
                { "D+A", "000010" },
                { "D+M", "000010" },
                { "D-A", "010011" },
                { "D-M", "010011" },
                { "A-D", "000111" },
                { "M-D", "000111" },
                { "D&A", "000000" },
                { "D&M", "000000" },
                { "D|A", "010101" },
                { "D|M", "010101" }
            };

            destTable = new Dictionary<string, string>
            {
                { string.Empty, "000" },
                { "M", "001" },
                { "D", "010" },
                { "MD", "011" },
                { "A", "100" },
                { "AM", "101" },
                { "AD", "110" },
                { "AMD", "111" }
            };

            jumpTable = new Dictionary<string, string>
            {
                { string.Empty, "000" },
                { "JGT", "001" },
                { "JEQ", "010" },
                { "JGE", "011" },
                { "JLT", "100" },
                { "JNE", "101" },
                { "JLE", "110" },
                { "JMP", "111" }
            };
        }

        public void AssembleFile(string inputFilePath, string outputFilePath)
        {
            // Read the input assembly code from the file
            string[] lines = File.ReadAllLines(inputFilePath);

            // Assemble the code
            string binaryOutput = Assemble(lines);

            // Write the machine code to the output file
            File.WriteAllText(outputFilePath, binaryOutput);
        }

        public string Assemble(string[] lines)
        {
            // First pass: Add labels to symbol table and remove comments and whitespace
            var cleanLines = RemoveCommentsAndWhitespace(lines);
            AddLabelsToSymbolTable(cleanLines);

            // Second pass: Convert instructions to machine code
            return ConvertToMachineCode(cleanLines);
        }

        private void AddLabelsToSymbolTable(string[] lines)
        {
            int instructionAddress = 16;

            foreach (var line in lines)
            {
                if (line.StartsWith("@"))
                {
                    // Label declaration, add to symbol table with the correct memory address
                    string label = line.Split("@")[1];
                    if (!symbolTable.ContainsKey(label))
                    {
                        // Only add the label if it doesn't already exist in the symbol table
                        symbolTable[label] = instructionAddress;
                    }
                }
                else
                {
                    // Regular instruction, increment instruction address
                    instructionAddress++;
                }
            }
        }

        private string ConvertToMachineCode(string[] lines)
        {
            var machineCodeLines = new List<string>();

            foreach (var line in lines)
            {
                string machineCode;
                if (line.StartsWith("@"))
                {
                    machineCode = AssembleAInstruction(line);
                }
                else if (!string.IsNullOrEmpty(line)) // Add this check to skip empty lines
                {
                    machineCode = AssembleCInstruction(line);
                }
                else
                {
                    continue;
                }

                machineCodeLines.Add(machineCode);
            }

            return string.Join("\n", machineCodeLines);
        }

        private string AssembleAInstruction(string instruction)
        {
            int value;
            string symbol = instruction.Substring(1);
            bool isNumeric = int.TryParse(symbol, out value);

            if (!isNumeric)
            {
                // Symbol is not a numeric value, check symbol table
                if (!symbolTable.TryGetValue(symbol, out value))
                {
                    // Add new variable to symbol table starting at RAM address 16
                    value = symbolTable.Count;
                    symbolTable[symbol] = value;
                }
            }

            string binaryValue = Convert.ToString(value, 2).PadLeft(16, '0');

            return binaryValue;
        }

        private string AssembleCInstruction(string instruction)
        {
            string comp = string.Empty;
            string dest = string.Empty;
            string jump = string.Empty;

            // Parse the instruction into its components
            if (instruction.Contains("="))
            {
                var parts = instruction.Split("=");
                dest = parts[0];
                instruction = parts[1];
            }

            if (instruction.Contains(";"))
            {
                var parts = instruction.Split(";");
                comp = parts[0];
                jump = parts[1];
            }
            else
            {
                comp = instruction;
            }

            // Additional logging for debugging
            //Console.WriteLine($"Comp: {comp}, Dest: {dest}, Jump: {jump}");

            // Convert components to binary representation
            string compBinary = compTable[comp];
            string destBinary = destTable[dest];
            string jumpBinary = jumpTable[jump];

            // Additional logging for debugging
            //Console.WriteLine($"Comp: {compBinary}, Dest: {destBinary}, Jump: {jumpBinary}");

            int value;
            bool isNumeric = int.TryParse(comp, out value);

            // Additional logging for debugging
            //Console.WriteLine($"isNumeric?: {isNumeric}, value: {value}");

            if (isNumeric || AEqualsZero(comp))
            {
                // C instruction when A is 0 
                return "1110" + compBinary + destBinary + jumpBinary;
            }
            else
            {
                // C instruction when A is 1
                return "1111" + compBinary + destBinary + jumpBinary;
            }

        }

        private static bool AEqualsZero(string comp)
        {
            return comp == "D" || comp == "A" || comp == "!D" || comp == "!A" 
                || comp == "-D" || comp == "-A" || comp == "D+1" || comp == "A+1"
                || comp == "D-1" || comp == "A-1" || comp == "D+A" || comp == "D-A"
                || comp == "A-D" || comp == "D&A" || comp == "D|A";
        }
    }

}