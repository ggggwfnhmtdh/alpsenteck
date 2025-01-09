using CSCS.InterpreterManager;
//using CSCS.Tests;
//using CSCSMath;
using SplitAndMerge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SOM_DEVELOP_TOOL;
namespace CSCS_TOOL
{
    public class CscsApp
    {
        public delegate void ShowMsgFun(string Msg);

        public ShowMsgFun pShow = null;
        protected InterpreterManagerModule _interpreterManager;
        protected Interpreter InterpreterInstance => _interpreterManager?.CurrentInterpreter;
        protected bool _startDebugger = true;

        protected virtual List<ICscsModule> GetModuleList()
        {
            return new List<ICscsModule>
            {
                new CscsSqlModule(),
                _interpreterManager
            };
        }

        protected virtual void SetupMono()
        {
            Environment.SetEnvironmentVariable("MONO_REGISTRY_PATH",
                "/Library/Frameworks/Mono.framework/Versions/Current/etc/mono/registry/");
        }

        public int Run(string scriptFilename, string[] FunctionList = null)
        {
            int exitCode = 0;
            string script = "";
            SetupMono();

            _interpreterManager = new InterpreterManagerModule();
            _interpreterManager.Modules = GetModuleList();

            _interpreterManager.OnInterpreterCreated += InterpreterCreated;

            var interpreterId = _interpreterManager.NewInterpreter(FunctionList);
            _interpreterManager.SetInterpreter(interpreterId);
 
            Console.WriteLine("Reading script from " + scriptFilename);
            script = Utils.GetFileContents(scriptFilename);
            if (!string.IsNullOrWhiteSpace(script))
            {
                ProcessScript(script, scriptFilename);
            }
     
           _interpreterManager.TerminateModules();
            return exitCode;
        }

      
        private void InterpreterCreated(object sender, EventArgs e)
        {
            // Subscribe to the printing events from the interpreter.
            // A printing event will be triggered after each successful statement
            // execution. On error an exception will be thrown.
            if (sender is Interpreter interpreter)
            {
                interpreter.OnOutput += Print;
            }
        }

        private void ProcessScript(string script, string filename = "")
        {
            s_PrintingCompleted = false;
            string errorMsg = null;
            Variable result = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(filename))
                {
                    result = Task.Run(() =>
                    //Interpreter.Instance.ProcessFileAsync(filename, true)).Result;
                    InterpreterInstance.ProcessFile(filename, true)).Result;
                }
                else
                {
                    result = Task.Run(() =>
                        InterpreterInstance.Process(script, filename, true)).Result;
                }
            }
            catch (Exception exc)
            {
                errorMsg = exc.InnerException != null ? exc.InnerException.Message : exc.Message;
                InterpreterInstance.InvalidateStacksAfterLevel(0);
            }

            if (!s_PrintingCompleted)
            {
                string output = InterpreterInstance.Output;
                if (!string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine(output);
                }
                else if (result != null)
                {
                    output = result.AsString(false, false);
                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        Console.WriteLine(output);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                Utils.PrintColor(InterpreterInstance, errorMsg + Environment.NewLine, ConsoleColor.Red);
                errorMsg = string.Empty;
            }
        }



        void Print(object sender, OutputAvailableEventArgs e)
        {
            if (pShow != null)
            {
                pShow(e.Output);
            }
            s_PrintingCompleted = true;
        }
        bool s_PrintingCompleted = false;
    }
}
