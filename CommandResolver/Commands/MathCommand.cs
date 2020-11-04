using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;

namespace CommandResolver.Commands
{
    public class MathCommand : ICommand
    {
        public CommandContext Context { get; set; }
        public string MathOperation { get; private set; }

        /// <summary>
        /// Processes last one or two variables from stack
        /// </summary>
        /// <param name="operaion">Your operation name</param>
        /// <param name="context">Program Context where result will bes stored</param>
        public MathCommand(string operaion, CommandContext context)
        {
            MathOperation = operaion ?? throw new CommandExecutionException("Math operation must be defined");
            Context = context ?? throw new CommandExecutionException("Could process this if there isn't context");
        }

        /// <summary>
        /// Runs command execution
        /// </summary>
        public void Run()
        {
            MutableKeyValuePair<string, object>[] procNumbers;
            switch (MathOperation)
            {
                case "*":
                    try { procNumbers = Context.PeekLastTwoStackElement(); } catch (CommandExecutionException ex) { throw ex; }
                    try { Context.PushStackWithDel(new MutableKeyValuePair<string, object>("ans", (Convert.ToDouble(procNumbers[0].Value) * Convert.ToDouble(procNumbers[1].Value)))); } catch (ArithmeticException ex) { throw new CommandExecutionException(ex.Message); }
                    break;
                case "/":
                    try { procNumbers = Context.PeekLastTwoStackElement(); } catch (CommandExecutionException ex) { throw ex; }
                    if (Convert.ToDouble(procNumbers[1].Value)==0)
                    {
                        throw new CommandExecutionException("Divide by zero is not allowed");
                    }
                    try { Context.PushStackWithDel(new MutableKeyValuePair<string, object>("ans", Convert.ToDouble(procNumbers[0].Value) / Convert.ToDouble(procNumbers[1].Value))); } catch (ArithmeticException ex) { throw new CommandExecutionException(ex.Message); }
                    break;
                case "+":
                    try { procNumbers = Context.PeekLastTwoStackElement(); } catch (CommandExecutionException ex) { throw ex; }
                    try { Context.PushStackWithDel(new MutableKeyValuePair<string, object>("ans", (Convert.ToDouble(procNumbers[0].Value) + Convert.ToDouble(procNumbers[1].Value)))); } catch (ArithmeticException ex) { throw new CommandExecutionException(ex.Message); }
                    break;
                case "-":
                    try { procNumbers = Context.PeekLastTwoStackElement(); } catch (CommandExecutionException ex) { throw ex; }
                    try { Context.PushStackWithDel(new MutableKeyValuePair<string, object>("ans", (Convert.ToDouble(procNumbers[0].Value) - Convert.ToDouble(procNumbers[1].Value)))); } catch (ArithmeticException ex) { throw new CommandExecutionException(ex.Message); }
                    break;
                case "SQRT":
                    MutableKeyValuePair<string, object> number = Context.PeekLastStackElement();
                    try
                    {
                        if (Convert.ToDouble(number.Value)<=0)
                            throw new CommandExecutionException($"Can't find square root from {number.Value}", Context.Stack);

                        Context.PopLastStackElement();
                        Context.PushStack(new MutableKeyValuePair<string, object>("ans", Math.Sqrt(Convert.ToDouble(number.Value))));
                    }
                    catch
                    {
                        throw new CommandExecutionException("Not enough items in stack to do this", Context.Stack);
                    }
                    break;
                default:
                    throw new CommandExecutionException($"There no such operation: {MathOperation}", Context.Stack);
            }
        }
    }
}
