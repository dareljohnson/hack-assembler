## Project Title

### Hack Assembler using .NET 6

## Project Description

An assembler that translates hack assembly language to hack 16-bit machine language (binary) for the Hack computer.

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
- C# 8 or higher programming language

## Build the project

```
dotnet build
```

## How to Run the Project

Run the local application:

```
HackAssembler --asm <inputfile.asm> --hack <outputfile.hack>
```

## How to Build the Project for production


```
dotnet build --configuration Release
```

## Include Credits

TBA

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)