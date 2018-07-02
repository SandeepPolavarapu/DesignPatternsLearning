using System;

namespace DesignPatternsLearning.Behavioral
{
    /*
     Source:http://www.dofactory.com/net/command-design-pattern

     Definition
        Encapsulate a request as an object, thereby letting you parameterize clients with different requests, queue or log requests, and support undoable operations.

    Participants

        The classes and objects participating in this pattern are:

        Command  (Command)
            declares an interface for executing an operation
        ConcreteCommand  (CalculatorCommand)
            defines a binding between a Receiver object and an action
            implements Execute by invoking the corresponding operation(s) on Receiver
        Client  (CommandApp)
            creates a ConcreteCommand object and sets its receiver
        Invoker  (User)
            asks the command to carry out the request
        Receiver  (Calculator)
            knows how to perform the operations associated with carrying out the request.
    */
    class CommandPattern
    {
        public static void Run()
        {
            Receiver receiver = new Receiver();
            Command cmd = new ConcreteCommand(receiver);
            Invoker invoker = new Invoker();

            invoker.SetCommand(cmd);
            invoker.ExecuteCommnand();
        }
    }

    abstract class Command
    {
        protected Receiver receiver;

        public Command(Receiver receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
    }

    class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver receiever) : base(receiever)
        {

        }

        public override void Execute()
        {
            receiver.Action();
        }
    }

    class Receiver
    {
        public void Action()
        {
            Console.WriteLine("Receiver Action");
        }
    }

    class Invoker
    {
        public Command command;

        public void SetCommand(Command command)
        {
            this.command = command;
        }

        public void ExecuteCommnand()
        {
            command.Execute();
        }
    }

}
