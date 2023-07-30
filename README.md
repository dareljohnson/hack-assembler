## Project Title

### Hack Assembler using .NET 6

## Project Description

An assembler that translates Hack assembly language instructions to Hack 16-bit machine language (binary) for the Hack computer.

## Example program

An excerpt from an assembly program in a file called prog.asm.

```
// Adds 1 + ... + 100
@i
M=1 // i=1
@sum
M=0 // sum=0
(LOOP)
@i
D=M // D=i
@100
D=D-A // D=i-100
@END
D;JGT // if (i-100)>0 goto END
@i
D=M // D=i
@sum
M=D+M // sum=sum+i
@i
M=M+1 // i=i+1
@LOOP
0;JMP // goto LOOP
(END)
@END
0;JMP // infinite loop
```

## Example output

The 16-bit machine code generated from running the Hack Assembler on prog.asm assembly file.

```
0000000000010000
1110111111001000
0000000000010001
1110101010001000
0000000000010000
1111110000010000
0000000001100100
1110010011010000
0000000000010100
1110001100000001
0000000000010000
1111110000010000
0000000000010001
1111000010001000
0000000000010000
1111110111001000
0000000000011000
1110101010000111
0000000000010100
1110101010000111
```


## Assumptions

I assume you are developing this project on a Microsoft Windows PC. A built-in Windows tool like notepad.exe is used in this project to create and edit files. If you are developing on Linux, you can use vim.

```
notepad .gitignore
```

On Linux:

```
vim .gitignore
```

You can also use a modern code editor like VSCode to follow along.

## How to Use the Project

You can clone this project.

```
 git clone https://github.com/dareljohnson/hack-assembler.git
```

## Project Requirements

Requirements:

- .NET 6.0 or higher
- C# version 10 or higher programming language

## Check the version of the SDK installed

```
dotnet sdk check
```

## Build the project

Change directory to: /HackAssembler\HackAssembler directory.

```
dotnet build
```

## How to Run the Project

Run the local application:

```
dotnet run
```

## Running the executable

In the: C:\HackAssembler\HackAssembler\bin\Debug\net6.0 directory of your project.

The default input and output files are prog.asm and prog.hack. You can use another filename to suit your needs. Make sure to use the .asm and .hack file extensions.

```
HackAssembler --asm <inputfile.asm> --hack <outputfile.hack>
```


## How to Build the Project for production


```
dotnet build --configuration Release
```

Built executable in the: C:\HackAssembler\HackAssembler\bin\Release\net6.0


## Include Credits

Thanks to Noam Nisan and Shimon Schocken for their dedicated work on the Nand to Tetris Companion: Building a Modern Computer from First Principles. 

Website: https://www.nand2tetris.org/


## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)