namespace smc
{
    using System.Collections.Generic;

    using java.io;

    using smc.builder;
    using smc.generator;
    using smc.parser;

    public class Smc
    {
        private string itsInputFile;
        private string itsOutputDir;
        private string itsFSMGeneratorName;
        private bool itsForceOverwrite;

        public static void main(string[] args)
        {
            Smc smc = new Smc();
            smc.execute(args);
        }

        private bool checkForOverwrite(IEnumerable<string> files)
        {
            var existingFiles = new List<string>();
            var overwrite = true;
            foreach(var ofName in files)
            {
                try
                {
                    var ofile = new File(ofName);
                    if (ofile.canRead())
                    {
                        existingFiles.Add(ofName);
                    }
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine(e);
                }
            }

            if (existingFiles.Count > 0)
            {
                System.Console.WriteLine("The following files will be overwritten if you choose to continue:");
                foreach(var ofName in existingFiles)
                {
                    System.Console.WriteLine($"   {ofName}");
                }
                System.Console.Write("overwrite files and continue? (Y/N): ");
                System.Console.Out.Flush();
                string b;
                try
                {
                    b = System.Console.In.ReadLine();
                    if (!(b.StartsWith("y") || b.StartsWith("Y")))
                    {
                        overwrite = false;
                        System.Console.WriteLine("please delete or rename the file and try again.");
                    }
                }
                catch (System.Exception ie)
                {
                    System.Console.WriteLine(ie);
                }
            }

            return overwrite;
        }

        public void parseCommandLine(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                string parsedValue;

                if (args[i].StartsWith("-"))
                {
                    if (args[i] == "-f")
                    {
                        itsForceOverwrite = true;
                    }
                    else if (ParseFlagWithArgument("-o", args, ref i, out parsedValue))
                    {
                        itsOutputDir = parsedValue;
                    }
                    else if (ParseFlagWithArgument("-g", args, ref i, out parsedValue))
                    {
                        itsFSMGeneratorName = parsedValue;
                    }
                    else
                    {
                        System.Console.WriteLine($"Unknown flag: {args[i]}");
                    }
                }
                else
                {
                    this.itsInputFile = args[i];
                }
            }
        }

        private static bool ParseFlagWithArgument(string flag, string[] args, ref int i, out string argValue)
        {
            var s = args[i];
            if (s.StartsWith(flag))
            {
                if (s.Length > flag.Length)
                {
                    argValue = s.Substring(2);
                    return true;
                }
                else
                {
                    i++;
                    if (i < args.Length)
                    {
                        argValue = args[i];
                        return true;
                    }
                }
            }

            argValue = string.Empty;
            return false;
        }

        private void printUsage()
        {
            System.Console.WriteLine("Usage: Smc [-g generator] [-f] [-o output_dir] file");
            System.Console.WriteLine("       -g is optional, it overrides the code generator class");
            System.Console.WriteLine("          specified in the State machine definition");
            System.Console.WriteLine("       -f is optional, it forces existing generated files to be overwritten");
            System.Console.WriteLine("       -o is optional,  the default output directory is the current directory");
            System.Console.WriteLine("     file is required, it should contain the State machine definition");
        }

        public void execute(string[] args)
        {
            itsOutputDir = "./";
            itsInputFile = string.Empty;
            itsFSMGeneratorName = string.Empty;
            itsForceOverwrite = false;
            parseCommandLine(args);
            if (itsInputFile.Length > 0)
            {
                var iFile = new File(itsInputFile);
                if (iFile.canRead())
                {
                    hasInputFile();
                }
                else
                {
                    System.Console.WriteLine("Cannot read state machine definition file: " + itsInputFile);
                    System.Console.WriteLine("Aborting due to invalid state machine file.");
                }
            }
            else
            {
                printUsage();
            }
        }

        private void hasInputFile()
        {
            if (itsOutputDir.EndsWith("/") == false && itsOutputDir.EndsWith("\\") == false)
            {
                itsOutputDir = itsOutputDir + "/";
            }

            var fsmbld = new FSMRepresentationBuilder();
            var parser = new FSMParser(fsmbld, itsInputFile);
            var status = parser.parse();
            if (status == true)
            {
                generateCode(fsmbld, parser);
            }
        }

        private void generateCode(FSMRepresentationBuilder fsmbld, FSMParser parser)
        {
            var sm = fsmbld.getStateMap();

            if (itsFSMGeneratorName.Length == 0)
            {
                itsFSMGeneratorName = parser.getFSMGeneratorName();
                if (itsFSMGeneratorName.Length == 0)
                {
                    System.Console.WriteLine("Using default C# Generator.");
                    itsFSMGeneratorName = "smc.generator.csharp.SMCSharpGenerator";
                }
            }

            try
            {
                var generator = (FSMGenerator)System.Activator.CreateInstance(System.Type.GetType(itsFSMGeneratorName));
                generator.FSMInit(sm, itsInputFile, itsOutputDir);

                bool overwrite = itsForceOverwrite;
                if (overwrite == false)
                {
                    overwrite = checkForOverwrite(generator.getGeneratedFileNames());
                }

                if (overwrite == true)
                {
                    try
                    {
                        generator.initialize();
                        generator.generate();
                    }
                    catch (System.IO.IOException e)
                    {
                        System.Console.WriteLine(e);
                    }
                }
            }
            catch (System.Exception cnf)
            {
                System.Console.WriteLine($"{itsFSMGeneratorName} is not a valid FSMGenerator");
                System.Console.WriteLine("Aborting due to invalid generator.");
                System.Console.WriteLine(cnf.StackTrace);
            }
        }

        public string getInputFilename()
        {
            return itsInputFile;
        }

        public string getOutputDir()
        {
            return itsOutputDir;
        }

        public string getFSMGeneratorName()
        {
            return itsFSMGeneratorName;
        }

        public bool getForcedOverwrite()
        {
            return itsForceOverwrite;
        }
    }
}
